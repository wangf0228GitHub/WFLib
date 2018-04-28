using System;
using System.Collections.Generic;

using System.Text;
using WFNetLib;
using System.IO.Ports;

namespace WFNetLib.PacketProc
{
    /// <summary>
    /// 包头
    /// </summary>
    public class CP1616_NoAddr_PacketHead
    {
        /// <summary>
        /// 包头大小
        /// </summary>
        public static Int32 DataLen_SIZE = 2;
        public static Int32 CommandLen_SIZE = 1;
        public static Int32 HEAD_SIZE = 5;
        public byte Command;
        public UInt16 Len = 0;
        public byte[] Data;
        public CP1616_NoAddr_PacketHead()
        {
            Data=new byte[HEAD_SIZE];
        }
    }
    public class CP1616_NoAddr_Packet
    {
        public CP1616_NoAddr_PacketHead Header;
        public byte[] Data;
        public int RxCount;
        public byte[] RxList;
		public static bool bVerify=true;
        public byte NeedCommand;
        public static Int32 BUFFER_SIZE = 1024;
        public CP1616_NoAddr_Packet(byte com)
        {
            NeedCommand=com;
            Header=new CP1616_NoAddr_PacketHead();
            RxCount = 0;
        }
        public CP1616_NoAddr_Packet()
        {
            NeedCommand=0xff;
            Header = new CP1616_NoAddr_PacketHead();
            RxCount = 0;
        }
        public static byte[] MakeCP1616_NoAddr_Packet(byte com)
        {
            return MakeCP1616_NoAddr_Packet(com, null);
        }
        public static byte[] MakeCP1616_NoAddr_Packet(byte com, byte b)
        {
            return MakeCP1616_NoAddr_Packet(com, new byte[] { b });
        }
        public static byte[] MakeCP1616_NoAddr_Packet(byte com, byte[] data)
        {
            byte[] txbuffer;
            if(data!=null)
            {
                txbuffer = new byte[CP1616_NoAddr_PacketHead.HEAD_SIZE + data.Length + 2];
                txbuffer[0] = 0x16;
                txbuffer[1] = 0x16;
                txbuffer[2] = com;
                txbuffer[3] = BytesOP.GetHighByte((UInt16)data.Length);
                txbuffer[4] = BytesOP.GetLowByte((UInt16)data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    txbuffer[i + CP1616_NoAddr_PacketHead.HEAD_SIZE] = data[i];
                }
                txbuffer[CP1616_NoAddr_PacketHead.HEAD_SIZE + data.Length] = Verify.GetVerify_byteSum(txbuffer, CP1616_NoAddr_PacketHead.HEAD_SIZE + data.Length);
                txbuffer[CP1616_NoAddr_PacketHead.HEAD_SIZE + data.Length + 1] = 0x0d;
            }
            else
            {
                txbuffer = new byte[CP1616_NoAddr_PacketHead.HEAD_SIZE+ 2];
                txbuffer[0] = 0x16;
                txbuffer[1] = 0x16;
                txbuffer[2] = com;
                txbuffer[3] = 0x00;
                txbuffer[4] = 0x00;
                txbuffer[CP1616_NoAddr_PacketHead.HEAD_SIZE] = Verify.GetVerify_byteSum(txbuffer, CP1616_NoAddr_PacketHead.HEAD_SIZE);
                txbuffer[CP1616_NoAddr_PacketHead.HEAD_SIZE + 1] = 0x0d;
            }            
            return txbuffer;
        }
        public bool DataPacketed(byte rx)
        {
            //判断读取的字节数+缓冲区已有字节数是否超过缓冲区总大小
            if(RxCount<CP1616_NoAddr_PacketHead.HEAD_SIZE)
            {
                Header.Data[RxCount++]=rx;
                if(RxCount==1)
                {
                    if(Header.Data[0]!=0x16)
                    {
                        RxCount=0;
                    }
                }
                else if(RxCount==2)
                {
                    if(Header.Data[1]!=0x16)
                    {
                        RxCount=0;
                    }
                }                
                else if(RxCount==3)
                {
                    Header.Command=Header.Data[2];
                    if(NeedCommand!=0xff)
                    {
                        if(Header.Command!=NeedCommand)
                        {
                            RxCount=0;
                        }
                    }
                }
                else if(RxCount==5)
                {
                    Header.Len=BytesOP.MakeShort(Header.Data[3],Header.Data[4]);
                    Data=new byte[Header.Len+2];
                }
            }
            else
            {
                Data[RxCount-CP1616_NoAddr_PacketHead.HEAD_SIZE]=rx;
                RxCount++;
                if(RxCount==(CP1616_NoAddr_PacketHead.HEAD_SIZE+Header.Len+2))
                {
                    if (Data[Data.Length - 1] == 0x0d)
                    {
						if (CP1616_NoAddr_Packet.bVerify)
						{
							byte s1 = Verify.GetVerify_byteSum(Header.Data);
							byte s2 = Verify.GetVerify_byteSum(Data, Data.Length - 2);
							s1 = (byte)(s1 + s2);
							if (s1 == Data[Data.Length - 2])
								return true;
							else
							{
								RxCount = 0;
								return false;
							}
						}
						else
						{
							return true;
						}
                    }
                    else
                    {
                        RxCount = 0;
                        return false;
                    }
                }
            }
            return false;
        }        
        public static object SaveDataProcessCallbackProc(byte[] tempbuffer, ref byte[] buffer, ref int dataOffset, int length)
        {
            //判断读取的字节数+缓冲区已有字节数是否超过缓冲区总大小
            if (length + dataOffset > buffer.Length)
            {
//                 if (dataOffset >= CP1616_NoAddr_PacketHead.HEAD_SIZE)//如果缓冲区数据满足一个包头数据大小,则可以计算出本次接收的包需要的缓冲区大小,从而实现一次调整大小
//                 {
//                     int PacketLen = BytesOP.MakeShort(buffer[5], buffer[6]);
//                     Array.Resize<Byte>(ref buffer, CP1616_NoAddr_PacketHead.HEAD_SIZE + PacketLen+2);
//                 }
//                 else //不满足一个完整的网络封包的大小
//                 {
                Array.Resize<Byte>(ref buffer, length + dataOffset);//buffer.Length + BUFFER_SIZE * 2);
//                 }
            }
            //将新读取的数据拷贝到缓冲区
            Array.Copy(tempbuffer, 0, buffer, dataOffset, length);
            //修改"数据实际长度"
            dataOffset += length;
            if (dataOffset > CP1616_NoAddr_PacketHead.HEAD_SIZE)//已经有头数据了
            {
                int PacketLen = BytesOP.MakeShort(buffer[3], buffer[4]);
                if (dataOffset < (CP1616_NoAddr_PacketHead.HEAD_SIZE + PacketLen+2))
                    return null;
            }
            //获得了完整的数据包
            CP1616_NoAddr_Packet packet=new CP1616_NoAddr_Packet();
            int index = 0;
            for (index = 0; index < dataOffset; index++)
            {
                if (packet.DataPacketed(buffer[index]))
                {
                    Array.Copy(buffer, CP1616_NoAddr_PacketHead.HEAD_SIZE + packet.Header.Len + 2, buffer, 0, dataOffset - (CP1616_NoAddr_PacketHead.HEAD_SIZE + packet.Header.Len + 2));
                    dataOffset -= (CP1616_NoAddr_PacketHead.HEAD_SIZE + packet.Header.Len + 2);//缓冲区实际数据长度减去一个完整封包长度
                    return packet;
                }
            }
            dataOffset = 0;
            return null;
//             TcpModbusPacket packet = GetPacket(buffer);
//             Array.Copy(buffer, CP1616_NoAddr_PacketHead.HEAD_SIZE + packet.Header.Len+2, buffer, 0, dataOffset - (CP1616_NoAddr_PacketHead.HEAD_SIZE + packet.Header.Len+2));
//             dataOffset -= (CP1616_NoAddr_PacketHead.HEAD_SIZE + packet.Header.Len+2);//缓冲区实际数据长度减去一个完整封包长度
//             return packet;
        }
        public static CP1616_NoAddr_Packet CP1616_NoAddr_ComProc(ref SerialPort serialPort, byte com, uint b)
        {
            ushort b1, b2;
            b1 = BytesOP.GetHighShort(b);
            b2 = BytesOP.GetLowShort(b);
            return CP1616_NoAddr_ComProc(ref serialPort, com, new byte[4] { BytesOP.GetHighByte(b1), BytesOP.GetLowByte(b1), BytesOP.GetHighByte(b2), BytesOP.GetLowByte(b2) },5);
        }
        public static CP1616_NoAddr_Packet CP1616_NoAddr_ComProc(ref SerialPort serialPort, byte com,  ushort b)
        {
            return CP1616_NoAddr_ComProc(ref serialPort, com, new byte[2] { BytesOP.GetHighByte(b), BytesOP.GetLowByte(b) },5);
        }
        public static CP1616_NoAddr_Packet CP1616_NoAddr_ComProc(ref SerialPort serialPort, byte com, byte b)
        {
            return CP1616_NoAddr_ComProc(ref serialPort, com, new byte[1] { b },5);
        }
        public static CP1616_NoAddr_Packet CP1616_NoAddr_ComProc(ref SerialPort serialPort, byte com, byte[] data,int retry)
        {
            CP1616_NoAddr_Packet ret = new CP1616_NoAddr_Packet(com);
            byte[] tx;
            tx = CP1616_NoAddr_Packet.MakeCP1616_NoAddr_Packet(com, data);         
            while (retry != 0)
            {
                serialPort.Write(tx, 0, tx.Length);
                while (true)
                {
                    try
                    {
                        if (ret.DataPacketed((byte)serialPort.ReadByte()))
                        {
                            return ret;
                        }
                    }
                    catch// (Exception ex)
                    {
                        //Debug.WriteLine("血糖通信失败" + ex.Message);
                        break;
                    }
                }
                retry--;
            }
            return null;
        }
    }
}
