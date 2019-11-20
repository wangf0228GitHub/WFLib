using System;
using System.Collections.Generic;

using System.Text;
using WFNetLib;
using System.IO.Ports;
using System.Diagnostics;

namespace WFNetLib.PacketProc
{
    /// <summary>
    /// 包头
    /// </summary>
    public class CP1616PacketHead
    {
        /// <summary>
        /// 包头大小
        /// </summary>        
        public static Int32 Addr_SIZE = 2;
        public static Int32 DataLen_SIZE = 2;
        public static Int32 CommandLen_SIZE = 1;
        public static Int32 HEAD_SIZE = 7;
        public static bool bIsDebugOut;
        public static bool bCheckVerify=true;
        public UInt16 RxAddr;
        public byte Command;
        public UInt16 Len = 0;
        public byte[] Data;
        public static void CalcHeaderLen()
        {
            HEAD_SIZE = Addr_SIZE + DataLen_SIZE + CommandLen_SIZE + 2;
        }
        public CP1616PacketHead()
        {
            Data=new byte[HEAD_SIZE];
        }
    }
    public class CP1616Packet
    {
        public CP1616PacketHead Header;
        public byte[] Data;
        public int RxCount;
        public byte[] RxList;
        public byte NeedCommand;
        public UInt16 NeedAddr;
        public static Int32 BUFFER_SIZE = 1024;
        public CP1616Packet(byte com,UInt16 addr)
        {
            NeedCommand=com;
            NeedAddr=addr;
            Header=new CP1616PacketHead();
            RxCount = 0;
        }
        public CP1616Packet()
        {
            NeedCommand=0xff;
            NeedAddr=0xffff;
            Header=new CP1616PacketHead();
            RxCount = 0;
        }
        public static byte[] MakeCP1616Packet(byte com, UInt16 addr, byte b)
        {
            return MakeCP1616Packet(com, addr, new byte[] { b });
        }
        public static byte[] MakeCP1616Packet(byte com, UInt16 addr, byte[] data)
        {
            int nIndex=0;
            byte[] txbuffer;
            if(data!=null)
            {
                txbuffer = new byte[CP1616PacketHead.HEAD_SIZE + data.Length + 2];
                txbuffer[nIndex++] = 0x16;
                txbuffer[nIndex++] = 0x16;
                if(CP1616PacketHead.Addr_SIZE==2)
                {
                    txbuffer[nIndex++] = BytesOP.GetHighByte(addr);
                    txbuffer[nIndex++] = BytesOP.GetLowByte(addr);
                }
                else if (CP1616PacketHead.Addr_SIZE == 1)
                {
                    txbuffer[nIndex++] = BytesOP.GetLowByte(addr);
                }
                txbuffer[nIndex++] = com;
                if(CP1616PacketHead.DataLen_SIZE==2)
                {
                    txbuffer[nIndex++] = BytesOP.GetHighByte((UInt16)data.Length);
                    txbuffer[nIndex++] = BytesOP.GetLowByte((UInt16)data.Length);
                }
                else
                {
                    txbuffer[nIndex++] = BytesOP.GetLowByte((UInt16)data.Length);
                }
                for (int i = 0; i < data.Length; i++)
                {
                    txbuffer[i + CP1616PacketHead.HEAD_SIZE] = data[i];
                }
                txbuffer[CP1616PacketHead.HEAD_SIZE + data.Length] = Verify.GetVerify_byteSum(txbuffer, CP1616PacketHead.HEAD_SIZE + data.Length);
                txbuffer[CP1616PacketHead.HEAD_SIZE + data.Length + 1] = 0x0d;
            }
            else
            {
                txbuffer = new byte[CP1616PacketHead.HEAD_SIZE+ 2];
                txbuffer[nIndex++] = 0x16;
                txbuffer[nIndex++] = 0x16;
                if (CP1616PacketHead.Addr_SIZE == 2)
                {
                    txbuffer[nIndex++] = BytesOP.GetHighByte(addr);
                    txbuffer[nIndex++] = BytesOP.GetLowByte(addr);
                }
                else if (CP1616PacketHead.Addr_SIZE == 1)
                {
                    txbuffer[nIndex++] = BytesOP.GetLowByte(addr);
                }
                txbuffer[nIndex++] = com;
                if (CP1616PacketHead.DataLen_SIZE == 2)
                {
                    txbuffer[nIndex++] = 0;
                    txbuffer[nIndex++] = 0;
                }
                else
                {
                    txbuffer[nIndex++] = 0;
                }
                txbuffer[CP1616PacketHead.HEAD_SIZE] = Verify.GetVerify_byteSum(txbuffer, CP1616PacketHead.HEAD_SIZE);
                txbuffer[CP1616PacketHead.HEAD_SIZE + 1] = 0x0d;
            }            
            return txbuffer;
        }
        public bool DataPacketed(byte rx)
        {
            //判断读取的字节数+缓冲区已有字节数是否超过缓冲区总大小
            if(RxCount<CP1616PacketHead.HEAD_SIZE)
            {
                Header.Data[RxCount++]=rx;
                if (RxCount == CP1616PacketHead.HEAD_SIZE)//判断帧长度
                {
                    if (CP1616PacketHead.DataLen_SIZE == 2)
                    {
                        Header.Len = BytesOP.MakeShort(Header.Data[CP1616PacketHead.HEAD_SIZE - 2], Header.Data[CP1616PacketHead.HEAD_SIZE - 1]);                        
                    }
                    else
                    {
                        Header.Len = Header.Data[CP1616PacketHead.HEAD_SIZE - 1];
                    }
                    Data = new byte[Header.Len + 2];
                }
                else if(RxCount==1)
                {
                    if(Header.Data[0]!=0x16)
                    {
                        RxCount=0;
                        if (CP1616PacketHead.bIsDebugOut)
                        {
                            Debug.WriteLine(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Header.Data, " "));
                        }
                    }
                }
                else if(RxCount==2)
                {
                    if(Header.Data[1]!=0x16)
                    {
                        RxCount=0;
                        if (CP1616PacketHead.bIsDebugOut)
                        {
                            Debug.WriteLine(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Header.Data, " "));
                        }
                    }
                }
                else if (RxCount == (2 + CP1616PacketHead.Addr_SIZE) && CP1616PacketHead.Addr_SIZE!=0)//判断地址
                {
                    if (CP1616PacketHead.Addr_SIZE == 1)
                    {
                        Header.RxAddr = Header.Data[2];
                        if (NeedAddr != 0xff)
                        {
                            if ((Header.RxAddr != 0xff) && (Header.RxAddr != NeedAddr))
                            {
                                RxCount = 0;
                                if (CP1616PacketHead.bIsDebugOut)
                                {
                                    Debug.WriteLine(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Header.Data, " "));
                                }
                            }
                        }
                    }
                    else if (CP1616PacketHead.Addr_SIZE == 2)
                    {
                        Header.RxAddr = BytesOP.MakeShort(Header.Data[2], Header.Data[3]);
                        if (NeedAddr != 0xffff)
                        {
                            if ((Header.RxAddr != 0xffff) && (Header.RxAddr != NeedAddr))
                            {
                                RxCount = 0;
                                if (CP1616PacketHead.bIsDebugOut)
                                {
                                    Debug.WriteLine(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Header.Data, " "));
                                }
                            }
                        }
                    }
                }
                else if (RxCount == (2 + CP1616PacketHead.Addr_SIZE+CP1616PacketHead.CommandLen_SIZE))//命令字判断
                {
                    Header.Command = Header.Data[2 + CP1616PacketHead.Addr_SIZE];
                    if(NeedCommand!=0xff)
                    {
                        if(Header.Command!=NeedCommand)
                        {
                            if (CP1616PacketHead.bIsDebugOut)
                            {
                                Debug.WriteLine(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Header.Data, " "));                                
                            }
                            RxCount=0;
                        }
                    }
                }
            }
            else
            {
                Data[RxCount-CP1616PacketHead.HEAD_SIZE]=rx;
                RxCount++;
                if(RxCount==(CP1616PacketHead.HEAD_SIZE+Header.Len+2))
                {
                    if (Data[Data.Length - 1] == 0x0d)
                    {
                        if (CP1616PacketHead.bCheckVerify)
                        {
                            byte s1 = Verify.GetVerify_byteSum(Header.Data);
                            byte s2 = Verify.GetVerify_byteSum(Data, Data.Length - 2);
                            s1 = (byte)(s1 + s2);
                            if (s1 == Data[Data.Length - 2])
                                return true;
                            else
                            {
                                RxCount = 0;
                                if(CP1616PacketHead.bIsDebugOut)
                                {
                                    Debug.Write(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Header.Data, " "));
                                    Debug.WriteLine(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Data, " "));
                                }
                                return false;
                            }
                        }
                        else
                            return true;
                    }
                    else
                    {
                        RxCount = 0;
                        if (CP1616PacketHead.bIsDebugOut)
                        {
                            Debug.Write(DateTime.Now.ToString("HH:mm:ss:ffff")+":");
                            Debug.Write(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Header.Data, " "));
                            Debug.WriteLine(WFNetLib.StringFunc.StringsFunction.byteToHexStr(Data, " "));
                        }
                        return false;
                    }
                }
            }
            return false;
        }        
        public static CP1616Packet CP1616ComProc(ref SerialPort serialPort, byte com, ushort addr, uint b)
        {
            ushort b1, b2;
            b1 = BytesOP.GetHighShort(b);
            b2 = BytesOP.GetLowShort(b);
            return CP1616ComProc(ref serialPort, com, addr, new byte[4] { BytesOP.GetHighByte(b1), BytesOP.GetLowByte(b1), BytesOP.GetHighByte(b2), BytesOP.GetLowByte(b2) },5);
        }
        public static CP1616Packet CP1616ComProc(ref SerialPort serialPort, byte com, ushort addr, ushort b)
        {
            return CP1616ComProc(ref serialPort, com, addr, new byte[2] { BytesOP.GetHighByte(b), BytesOP.GetLowByte(b) },5);
        }
        public static CP1616Packet CP1616ComProc(ref SerialPort serialPort, byte com, ushort addr, byte b)
        {
            return CP1616ComProc(ref serialPort, com, addr, new byte[1] { b },5);
        }
        public static CP1616Packet CP1616ComProc(ref SerialPort serialPort, byte com,ushort addr, byte[] data,int retry)
        {
            CP1616Packet ret = new CP1616Packet(com,addr);
            byte[] tx;
            tx = CP1616Packet.MakeCP1616Packet(com, addr, data);         
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



        public static void CP1616ComSend(ref SerialPort serialPort, byte com, ushort addr, uint b)
        {
            ushort b1, b2;
            b1 = BytesOP.GetHighShort(b);
            b2 = BytesOP.GetLowShort(b);
            CP1616ComSend(ref serialPort, com, addr, new byte[4] { BytesOP.GetHighByte(b1), BytesOP.GetLowByte(b1), BytesOP.GetHighByte(b2), BytesOP.GetLowByte(b2) });
        }
        public static void CP1616ComSend(ref SerialPort serialPort, byte com, ushort addr, ushort b)
        {
            CP1616ComSend(ref serialPort, com, addr, new byte[2] { BytesOP.GetHighByte(b), BytesOP.GetLowByte(b) });
        }
        public static void CP1616ComSend(ref SerialPort serialPort, byte com, ushort addr, byte b)
        {
            CP1616ComSend(ref serialPort, com, addr, new byte[1] { b });
        }
        public static void CP1616ComSend(ref SerialPort serialPort, byte com, ushort addr, byte[] data)
        {
            byte[] tx;
            tx = CP1616Packet.MakeCP1616Packet(com, addr, data);
            serialPort.Write(tx, 0, tx.Length);            
        }
    }
}
