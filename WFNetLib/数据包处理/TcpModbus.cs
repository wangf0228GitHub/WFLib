using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WFNetLib.PacketProc
{
    /// <summary>
    /// 包头
    /// </summary>
    public class TcpModbusPacketHead
    {
        /// <summary>
        /// 包头大小
        /// </summary>
        public const Int32 HEAD_SIZE = 6;

        

        public UInt16 MBAP_Index;
        public static UInt16 ProtocolID = 0;
        public UInt16 Len = 0;
        //public byte SubAddr = 0;
        public UInt16 NextMBAPIndex()
        {
            return ++MBAP_Index;
        }
    }
    public class TcpModbusPacket
    {
        public enum FunctionCode : byte
        {
            /// <summary>
            /// Read Multiple Registers
            /// </summary>
            Read = 3,

            /// <summary>
            /// Write Multiple Registers
            /// </summary>
            Write = 16
        }
        public static Int32 BUFFER_SIZE = 1024;
        public TcpModbusPacketHead Header;
        public byte SubAddr = 0;
        public FunctionCode functionCode;
        public byte[] Data;
//         public static MemoryStream MakePacket(NetPacket.NetPacketType type, object o)
//         {
//             NetPacket packet = new NetPacket();
//             packet._packetHead = new NetPacketHead();
//             packet.Data = o; ;
//             packet._packetHead.PType = type;
//             MemoryStream mStream = new MemoryStream();
//             mStream.Write(BitConverter.GetBytes(NetPacketHead.Version), 0, sizeof(Int32));//版本
//             mStream.Write(BitConverter.GetBytes((Int32)packet.PacketHead.PType), 0, sizeof(Int32));//类型
//             byte[] buffer = null;
//             buffer = (byte[])packet.Data;
//             packet.PacketHead.Len = buffer.Length;
//             mStream.Write(BitConverter.GetBytes(packet.PacketHead.Len), 0, sizeof(Int32));//长度
//             if (buffer != null)
//                 mStream.Write(buffer, 0, buffer.Length);
//             return mStream;
//         }

        public static object SaveDataProcessCallbackProc(byte[] tempbuffer, ref byte[] buffer, ref int dataOffset, int length)
        {
            //判断读取的字节数+缓冲区已有字节数是否超过缓冲区总大小
            if (length + dataOffset > buffer.Length)
            {
                if (dataOffset >= TcpModbusPacketHead.HEAD_SIZE)//如果缓冲区数据满足一个包头数据大小,则可以计算出本次接收的包需要的缓冲区大小,从而实现一次调整大小
                {
                    int PacketLen = BytesOP.MakeShort(buffer[4],buffer[5]);
                    Array.Resize<Byte>(ref buffer, TcpModbusPacketHead.HEAD_SIZE + PacketLen);
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
            if (dataOffset > TcpModbusPacketHead.HEAD_SIZE)//已经有头数据了
            {
                int PacketLen = BytesOP.MakeShort(buffer[4], buffer[5]);
                if (dataOffset < (TcpModbusPacketHead.HEAD_SIZE + PacketLen))
                    return null;
            }            
            //获得了完整的数据包
            TcpModbusPacket packet = GetPacket(buffer);
            Array.Copy(buffer, TcpModbusPacketHead.HEAD_SIZE + packet.Header.Len, buffer, 0, dataOffset - (TcpModbusPacketHead.HEAD_SIZE + packet.Header.Len));
            dataOffset -= (TcpModbusPacketHead.HEAD_SIZE + packet.Header.Len);//缓冲区实际数据长度减去一个完整封包长度
            return packet;
        }
        public static TcpModbusPacket GetPacket(MemoryStream m)
        {
            return GetPacket(m.GetBuffer());
        }
        public static TcpModbusPacket GetPacket(byte[] buffer)
        {
            if (buffer.Length < TcpModbusPacketHead.HEAD_SIZE)
                throw (new Exception("数据包长度过短，无法转换"));
            UInt16 PacketLen = BytesOP.MakeShort(buffer[4], buffer[5]);
            if (buffer.Length < (TcpModbusPacketHead.HEAD_SIZE + PacketLen))
                throw (new Exception("数据包长度过短，无法转换"));
            UInt16 ProtocolID = BytesOP.MakeShort(buffer[2],buffer[3]);//协议号
            if (ProtocolID != TcpModbusPacketHead.ProtocolID)
                throw (new Exception("数据协议标识不符!"));
            TcpModbusPacket packet = new TcpModbusPacket();
            TcpModbusPacketHead packetHead = new TcpModbusPacketHead();
            packetHead.MBAP_Index = BytesOP.MakeShort(buffer[0], buffer[1]);
            packetHead.Len = PacketLen;            
            packet.Header = packetHead;
            packet.SubAddr = buffer[6];
            packet.functionCode = (FunctionCode)buffer[7];
            packet.Data = new Byte[packetHead.Len-2];
            Array.Copy(buffer, TcpModbusPacketHead.HEAD_SIZE + 2, packet.Data, 0, packetHead.Len - 2);            
            return packet;
        }
        public static byte[] MakePacket(UInt16 mbapIndex,byte subAddr,FunctionCode fc,byte[] bs)
        {
            UInt16 len = (UInt16)(TcpModbusPacketHead.HEAD_SIZE + bs.Length + 2);
            byte[] ret = new byte[len];
            ret[0] = BytesOP.GetHighByte(mbapIndex);
            ret[1] = BytesOP.GetLowByte(mbapIndex);
            ret[2] = BytesOP.GetHighByte(TcpModbusPacketHead.ProtocolID);
            ret[3] = BytesOP.GetLowByte(TcpModbusPacketHead.ProtocolID);
            len-=TcpModbusPacketHead.HEAD_SIZE;
            ret[4] = BytesOP.GetHighByte(len);
            ret[5] = BytesOP.GetLowByte(len);
            ret[6] = subAddr;
            ret[7] = (byte)fc;
            Array.Copy(bs, 0, ret, 8, bs.Length);
            return ret;
        }
    }
}
