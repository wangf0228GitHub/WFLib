using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace WFNetLib.PacketProc
{
    /// <summary>
    /// 包头
    /// </summary>
    public class NetPacketHead
    {
        /// <summary>
        /// 包头大小
        /// </summary>
        public const Int32 HEAD_SIZE = sizeof(Int32) * 3;

        public static Int32 Version = 1;


        private NetPacket.NetPacketType _pType = NetPacket.NetPacketType.STRING;
        /// <summary>
        /// 包类型[决定如何解包]
        /// </summary>
        public NetPacket.NetPacketType PType
        {
            get { return _pType; }
            set { _pType = value; }
        }

        private Int32 _len = 0;
        /// <summary>
        /// 包体长度[决定后面data数据的长度]
        /// </summary>
        public Int32 Len
        {
            get { return _len; }
            set { _len = value; }
        }


    }

    /// <summary>
    /// 网络数据包【Udp建议大小不超过548个字节,Internet标准MTU为576,减去IP数据报头(20字节)和UDP数据包头(8字节)】
    /// </summary>
    public class NetPacket
    {

        /// <summary>
        /// 包类型
        /// </summary>
        public enum NetPacketType
        {
            /// <summary>
            /// 简单字符串传输
            /// </summary>
            STRING = 0,
            /// <summary>
            /// 二进制[文件传输]
            /// </summary>
            BINARY = 1,
            /// <summary>
            /// 复杂对象传输[使用序列化和反序列化]
            /// </summary>
            COMPLEX = 2
        }
        public static Encoding encoding = Encoding.Default;
        /// <summary>
        /// 缓冲区大小1024字节
        /// </summary>
        public static Int32 BUFFER_SIZE = 1024;
        private NetPacketHead _packetHead;
        /// <summary>
        /// 包头
        /// </summary>
        public NetPacketHead PacketHead
        {
            get { return _packetHead; }
            set { _packetHead = value; }
        }

        private object _data = null;
        /// <summary>
        /// 包体，根据封包类型决定包体数据类型
        /// STRING:包体为String
        /// BINARY:包体为Byte[]
        /// COMPLEX:包体为可序列化的对象
        /// </summary>
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }        
        public static byte[] MakePacket(NetPacketType type, byte[] bs)
        {            
            byte[] ret=new byte[NetPacketHead.HEAD_SIZE+bs.Length];
            Array.Copy(BitConverter.GetBytes(NetPacketHead.Version), 0, ret, 0, sizeof(Int32));
            Array.Copy(BitConverter.GetBytes((Int32)type), 0, ret, sizeof(Int32), sizeof(Int32));
            int Len = bs.Length;
            Array.Copy(BitConverter.GetBytes(Len), 0, ret, sizeof(Int32)*2, sizeof(Int32));
            Array.Copy(bs, 0, ret, NetPacketHead.HEAD_SIZE, bs.Length);
            
            return ret;
        }
        public static byte[] MakeStringPacket(string str)
        {
            return MakePacket(NetPacketType.STRING,encoding.GetBytes(str));
        }
        public static byte[] MakeFilePacket(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw (new Exception("NetPacket打包的文件不存在!"));
            }
            FileStream fs = null;
            try
            {
                Byte[] filename = encoding.GetBytes(new FileInfo(filePath).Name);
                fs = new FileStream(filePath, FileMode.Open);
                Byte[] content = new Byte[sizeof(Int32) + filename.Length + fs.Length];//文件数据包包体大小=文件名长度(4字节)+文件名+文件内容
                Array.Copy(BitConverter.GetBytes(filename.Length), 0, content, 0, sizeof(Int32));//把"文件名长度(4字节)"放入数组
                Array.Copy(filename, 0, content, sizeof(Int32), filename.Length);//把"文件名字节数组"放入数组                
                fs.Read(content, sizeof(Int32) + filename.Length, (Int32)fs.Length);//把"文件内容"放入数组
                return MakePacket(NetPacketType.BINARY,content);                
            }
            finally
            {
                fs.Close();
            }
        }
        public static byte[] MakeObjectPacket(string type, object graph)
        {
            if (graph == null)
            {
                throw (new Exception("NetPacket打包的对象不存在!"));
            }
            if (!graph.GetType().IsSerializable)
            {
                throw (new Exception("NetPacket打包的对象不可序列化!"));
            }
            Byte[] typename = encoding.GetBytes(type);
            MemoryStream m = new MemoryStream();
            BinarySerializeHelper.Serialize(m, graph);
            m.Position = 0;
            Byte[] content = new byte[m.Length + sizeof(Int32) + typename.Length];
            Array.Copy(BitConverter.GetBytes(typename.Length), 0, content, 0, sizeof(Int32));//把"文件名长度(4字节)"放入数组
            Array.Copy(typename, 0, content, sizeof(Int32), typename.Length);//把"文件名字节数组"放入数组
            m.Read(content, sizeof(Int32) + typename.Length, (Int32)m.Length);

            return MakePacket(NetPacketType.COMPLEX, content);      
        }
        public static object SaveDataProcessCallbackProc(byte[] tempbuffer, ref byte[] buffer, ref int dataOffset, int length)
        {            
            //判断读取的字节数+缓冲区已有字节数是否超过缓冲区总大小
            if (length + dataOffset > buffer.Length)
            {
                if (dataOffset >= NetPacketHead.HEAD_SIZE)//如果缓冲区数据满足一个包头数据大小,则可以计算出本次接收的包需要的缓冲区大小,从而实现一次调整大小
                {
                    int PacketLen = BitConverter.ToInt32(buffer, 8);
                    Array.Resize<Byte>(ref buffer, NetPacketHead.HEAD_SIZE + PacketLen);
                }
                else //不满足一个完整的网络封包的大小
                {
                    Array.Resize<Byte>(ref buffer, buffer.Length + BUFFER_SIZE * 2);
                }
            }
            //将新读取的数据拷贝到缓冲区
            Array.Copy(tempbuffer, 0, buffer, dataOffset, length);
            //修改"数据实际长度"
            dataOffset += length;
            if (dataOffset < (NetPacketHead.HEAD_SIZE + BitConverter.ToInt32(buffer, 8)))
                return null;
            //获得了完整的数据包
            NetPacket packet=GetPacket(buffer);
            Array.Copy(buffer, NetPacketHead.HEAD_SIZE + packet._packetHead.Len, buffer, 0, dataOffset - (NetPacketHead.HEAD_SIZE + packet._packetHead.Len));
            dataOffset -= (NetPacketHead.HEAD_SIZE + packet._packetHead.Len);//缓冲区实际数据长度减去一个完整封包长度
            return packet;    
        }
        public static NetPacket GetPacket(MemoryStream m)
        {
            return GetPacket(m.GetBuffer());
        }
        public static NetPacket GetPacket(byte[] buffer)
        {
            if (buffer.Length < NetPacketHead.HEAD_SIZE)
                throw (new Exception("数据包长度过短，无法转换"));
            if (buffer.Length < (NetPacketHead.HEAD_SIZE + BitConverter.ToInt32(buffer, 8)))
                throw (new Exception("数据包长度过短，无法转换"));
            Int32 version = BitConverter.ToInt32(buffer, 0);//协议号
            if (version != NetPacketHead.Version)
                throw (new Exception("数据协议版本号不符!"));
            NetPacket packet = new NetPacket();
            NetPacketHead packetHead = new NetPacketHead();
            packetHead.PType = (NetPacketType)BitConverter.ToInt32(buffer, sizeof(Int32));//封包类型
            packetHead.Len = BitConverter.ToInt32(buffer, sizeof(Int32) + sizeof(Int32));
            packet._packetHead = packetHead;

            Byte[] packetBuffer = new Byte[packetHead.Len];
            Array.Copy(buffer, NetPacketHead.HEAD_SIZE, packetBuffer, 0, packetHead.Len);
            switch (packetHead.PType)
            {
                case NetPacketType.STRING:
                    packet.Data = encoding.GetString(packetBuffer);
                    break;
                case NetPacketType.BINARY:
                    NetFile f = new NetFile();
                    int filenamelen = BitConverter.ToInt32(packetBuffer, 0);//文件名长度
                    f.FileName = encoding.GetString(packetBuffer, sizeof(Int32), filenamelen);
                    f.Content = new Byte[packetBuffer.Length - sizeof(Int32) - filenamelen];
                    Array.Copy(packetBuffer, sizeof(Int32) + filenamelen, f.Content, 0, f.Content.Length);
                    packet.Data = f;
                    break;
                case NetPacketType.COMPLEX:
                    NetObject no = new NetObject();
                    int typenamelen = BitConverter.ToInt32(packetBuffer, 0);//文件名长度
                    no.TypeName = NetPacket.encoding.GetString(packetBuffer, sizeof(Int32), typenamelen);

                    byte[] Content = new Byte[packetBuffer.Length - sizeof(Int32) - typenamelen];
                    Array.Copy(packetBuffer, sizeof(Int32) + typenamelen, Content, 0, Content.Length);

                    MemoryStream mStream = new MemoryStream();
                    mStream.Write(Content, 0, Content.Length);
                    mStream.Position = 0;
                    no.gragh = BinarySerializeHelper.DeSerialize(mStream);
                    mStream.Close();
                    packet.Data = no;
                    break;
            }
            return packet;
        }
    }

    /// <summary>
    /// 传输的文件
    /// </summary>
    public class NetFile
    {
        private string _fileName = string.Empty;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        private Byte[] _content;

        public Byte[] Content
        {
            get
            {
                if (_content == null)
                    _content = new Byte[] { };
                return _content;
            }
            set { _content = value; }
        }
    }
    /// <summary>
    /// 传输的对象
    /// </summary>
    public class NetObject
    {
        private string _type = string.Empty;

        public string TypeName
        {
            get { return _type; }
            set { _type = value; }
        }
        public object gragh;
    }

}
