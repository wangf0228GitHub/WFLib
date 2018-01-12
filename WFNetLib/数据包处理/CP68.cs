using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace WFNetLib.PacketProc
{
    /// <summary>
    /// 包头
    /// </summary>
    public class CP68PacketHead
    {
        public static Int32 HEAD_SIZE = 11;

        public byte FrameHeader;
		public byte DeviceType;//仪表类型
		public byte[] DeviceAddr;//表地址
		public byte ControlCode;//控制码
		public byte DataLen;
        public byte[] Data;
        public CP68PacketHead()
        {
            Data = new byte[HEAD_SIZE];
            DeviceAddr=new byte[7];
        }
    }
    public class CP68Packet
    {
        public static byte DeviceType=0x20;
        public static byte[] DeviceAddr;//表地址
        public static bool bMaster=true;
        public CP68PacketHead Header;
        public byte[] Data;
        public int RxCount;
        public byte[] RxList;
        public byte NeedCommand;
        public byte[] NeedAddr;
        public static Int32 BUFFER_SIZE = 1024;
        public CP68Packet(byte ControlCode)
        {
            if(CP68Packet.bMaster)
                NeedCommand = (byte)(ControlCode | 0x80);            
            Header = new CP68PacketHead();
            RxCount = 0;
            //NeedAddr = new byte[7];
        }
        public CP68Packet()
        {
            NeedCommand = 0xff;
            Header = new CP68PacketHead();
            RxCount = 0;
            //NeedAddr = new byte[7];
        }
        public static byte[] MakeCP68Packet(byte com, byte b)
        {
            return MakeCP68Packet(com, CP68Packet.DeviceAddr, new byte[] { b });
        }
        public static byte[] MakeCP68Packet(byte com, byte[] data)
        {
            return MakeCP68Packet(com, CP68Packet.DeviceAddr, data);
        }
        public static byte[] MakeCP68Packet(byte com, byte[] addr, byte b)
        {
            return MakeCP68Packet(com, addr, new byte[] { b });
        }
        public static byte[] MakeCP68Packet(byte com, byte[] addr, byte[] data)
        {
            byte[] txbuffer;
            for (int i = 0; i < 7;i++ )
            {
                
            }
            if (data != null)
            {
                txbuffer = new byte[CP68PacketHead.HEAD_SIZE + data.Length + 2];
                txbuffer[0] = 0x68;
                txbuffer[1] = CP68Packet.DeviceType;
                for (int i = 0; i < 7;i++ )
                    txbuffer[2+i] = addr[i];
                txbuffer[9] = com;
                txbuffer[10] = (byte)data.Length;
                for (int i = 0; i < data.Length; i++)
                {
                    txbuffer[i + CP68PacketHead.HEAD_SIZE] = data[i];
                }
                txbuffer[CP68PacketHead.HEAD_SIZE + data.Length] = Verify.GetVerify_byteSum(txbuffer, CP68PacketHead.HEAD_SIZE + data.Length);
                txbuffer[CP68PacketHead.HEAD_SIZE + data.Length + 1] = 0x16;
            }
            else
            {
                txbuffer = new byte[CP68PacketHead.HEAD_SIZE + 2];
                txbuffer[0] = 0x68;
                txbuffer[1] = CP68Packet.DeviceType;
                for (int i = 0; i < 7; i++)
                    txbuffer[2 + i] = addr[i];
                txbuffer[9] = com;
                txbuffer[10] = 0;
                txbuffer[CP68PacketHead.HEAD_SIZE] = Verify.GetVerify_byteSum(txbuffer, CP68PacketHead.HEAD_SIZE);
                txbuffer[CP68PacketHead.HEAD_SIZE + 1] = 0x16;
            }
            return txbuffer;
        }
        public bool DataPacketed(byte rx)
        {
            //判断读取的字节数+缓冲区已有字节数是否超过缓冲区总大小
            if (RxCount < CP68PacketHead.HEAD_SIZE)
            {
                Header.Data[RxCount++] = rx;
                if (RxCount == 1)
                {
                    if (Header.Data[0] != 0x68)
                    {
                        RxCount = 0;
                    }
                }
                else if (RxCount == 2)
                {
                    if (Header.Data[1] != CP68Packet.DeviceType)
                    {
                        RxCount = 0;
                    }
                }
                else if (RxCount == 3)
                {
                    Header.DeviceAddr[0] = Header.Data[2];
                }
                else if (RxCount == 4)
                {
                    Header.DeviceAddr[1] = Header.Data[3];
                }
                else if (RxCount == 5)
                {
                    Header.DeviceAddr[2] = Header.Data[4];
                }
                else if (RxCount == 6)
                {
                    Header.DeviceAddr[3] = Header.Data[5];
                }
                else if (RxCount == 7)
                {
                    Header.DeviceAddr[4] = Header.Data[6];
                }
                else if (RxCount == 8)
                {
                    Header.DeviceAddr[5] = Header.Data[7];
                }
                else if (RxCount == 9)
                {
                    Header.DeviceAddr[6] = Header.Data[8];
                }
                else if (RxCount == 10)
                {
                    Header.ControlCode = Header.Data[9];
                    if (NeedCommand != 0xff)
                    {
                        if (Header.ControlCode != NeedCommand)
                        {
                            RxCount = 0;
                        }
                    }
                }
                else if (RxCount == 11)
                {
                    Header.DataLen = Header.Data[10];
                    Data = new byte[Header.DataLen + 2];
                }
            }
            else
            {
                Data[RxCount - CP68PacketHead.HEAD_SIZE] = rx;
                RxCount++;
                if (RxCount == (CP68PacketHead.HEAD_SIZE + Header.DataLen + 2))
                {
                    if (Data[Data.Length - 1] == 0x16)
                    {
                        if(NeedAddr!=null)
                        {
                            bool bValid = true;
                            /************************************************************************/
                            /* 判断地址                                                             */
                            /************************************************************************/
                            for (int i = 0; i < 7; i++)
                            {
                                if (Header.DeviceAddr[i] != NeedAddr[i])
                                {
                                    bValid = false;
                                }
                            }
                            /************************************************************************/
                            /* AAAAAAAAAAA为通用地址                                                */
                            /************************************************************************/
                            if (!bValid)
                            {                                
                                for (int i = 0; i < 7; i++)
                                {
                                    if (Header.DeviceAddr[i] != 0xaa)
                                    {
                                        RxCount = 0;
                                        return false;
                                    }
                                }
                            }
                        }
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
                        RxCount = 0;
                        return false;
                    }
                }
            }
            return false;
        }
        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, uint b)
        {
            ushort b1, b2;
            b1 = BytesOP.GetHighShort(b);
            b2 = BytesOP.GetLowShort(b);
            return CP68ComProc(ref serialPort, com, CP68Packet.DeviceAddr, new byte[4] { BytesOP.GetHighByte(b1), BytesOP.GetLowByte(b1), BytesOP.GetHighByte(b2), BytesOP.GetLowByte(b2) }, 5);
        }
        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, ushort b)
        {
            return CP68ComProc(ref serialPort, com, CP68Packet.DeviceAddr, new byte[2] { BytesOP.GetHighByte(b), BytesOP.GetLowByte(b) }, 5);
        }
        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, byte b)
        {
            return CP68ComProc(ref serialPort, com, CP68Packet.DeviceAddr, new byte[1] { b }, 5);
        }
        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, byte[] data)
        {
            return CP68ComProc(ref serialPort, com, CP68Packet.DeviceAddr, data, 5);
        }



        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, byte[] addr, uint b)
        {
            ushort b1, b2;
            b1 = BytesOP.GetHighShort(b);
            b2 = BytesOP.GetLowShort(b);
            return CP68ComProc(ref serialPort, com, addr, new byte[4] { BytesOP.GetHighByte(b1), BytesOP.GetLowByte(b1), BytesOP.GetHighByte(b2), BytesOP.GetLowByte(b2) }, 5);
        }
        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, byte[] addr, ushort b)
        {
            return CP68ComProc(ref serialPort, com, addr, new byte[2] { BytesOP.GetHighByte(b), BytesOP.GetLowByte(b) }, 5);
        }
        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, byte[] addr, byte b)
        {
            return CP68ComProc(ref serialPort, com, addr, new byte[1] { b }, 5);
        }
        public static CP68Packet CP68ComProc(ref SerialPort serialPort, byte com, byte[] addr, byte[] data, int retry)
        {
            CP68Packet ret = new CP68Packet(com);
            byte[] tx;
            tx = CP68Packet.MakeCP68Packet(com, addr, data);            
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
