using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;

namespace WFNetLib.PacketProc
{
    public class RTUModbus_Master
    {
        public int RetryTimes = 3;
        public int RxTimeOut = 1000;
        public byte TargetAddr = 1;
        public int RetryInterval = 500;
        public SerialPort Com;
        public RTUModbus_Master(SerialPort _com)
        {
            Com = _com;
        }
        public bool ProcCommand16(ushort RegAddr, byte[] pBuff, int offset,int Count)
        {
            int retry = RetryTimes;
	        while(retry!=0)
	        {
                byte[] txList = new byte[9 + Count];
                txList[0] = TargetAddr;
                txList[1] = 0x10;
                txList[2] = BytesOP.GetHighByte(RegAddr);
                txList[3] = BytesOP.GetLowByte(RegAddr);
                txList[4] = 0;
                txList[5] = (byte)(Count / 2);
                txList[6] = (byte)Count;
		        for(int i=0;i<Count;i++)
		        {
			        txList[7+i]=pBuff[i];
		        }
                ushort crc = Verify.GetVerify_CRC16(txList, 7 + Count);
                txList[7 + Count] = BytesOP.GetHighByte(crc);
                txList[7 + Count + 1] = BytesOP.GetLowByte(crc);
                Com.Write(txList, 0, txList.Length);
                Com.DiscardInBuffer();
                Com.ReadTimeout = RxTimeOut;
                byte[] rxList=new byte[8];
                int rxCount = 0;
                byte rx=0;
                while (true)
                {
                    try
                    {
                        rx = (byte)Com.ReadByte();
                    }
                    catch
                    {
                        break;
                    }
                    rxList[rxCount++] = rx;
                    if (rxCount == 8)
                    {
                        //校验
                        ushort rxcrc = Verify.GetVerify_CRC16(rxList, 6);
                        ushort rxcrc1 = BytesOP.MakeShort(rxList[6], rxList[7]);
                        if (rxcrc == rxcrc1)
                        {
                            return true;
                        }
                        else
                            break;
                    }
                    else if (rxCount > 8)
                    {
                        break;
                    }                     
                    else if (rxCount == 2)//命令号
                    {
                        if (rx != 0x10)
                        {
                            break;
                        }                        
                    }
                    else if (rxCount == 1)//从机地址
                    {
                        if (rx != TargetAddr)
                        {
                            break;
                        }
                    }	
                }
                Thread.Sleep(RetryInterval);
		        retry--;
	        }
            return false;
        }
        public bool ProcCommand05(ushort RegAddr, ushort data)
        {
            int retry = RetryTimes;
            while (retry != 0)
            {
                byte[] txList = new byte[8];
                txList[0] = TargetAddr;
                txList[1] = 0x05;
                txList[2] = BytesOP.GetHighByte(RegAddr);
                txList[3] = BytesOP.GetLowByte(RegAddr);
                txList[4] = BytesOP.GetHighByte(data);
                txList[5] = BytesOP.GetLowByte(data);                
                ushort crc = Verify.GetVerify_CRC16(txList, 6);
                txList[6] = BytesOP.GetHighByte(crc);
                txList[7] = BytesOP.GetLowByte(crc);
                Com.Write(txList, 0, txList.Length);
                Com.DiscardInBuffer();
                Com.ReadTimeout = RxTimeOut;
                byte[] rxList = new byte[8];
                int rxCount = 0;
                byte rx = 0;
                while (true)
                {
                    try
                    {
                        rx = (byte)Com.ReadByte();
                    }
                    catch
                    {
                        break;
                    }
                    rxList[rxCount++] = rx;
                    if (rxCount == 8)
                    {
                        //校验
                        ushort rxcrc = Verify.GetVerify_CRC16(rxList, 6);
                        ushort rxcrc1 = BytesOP.MakeShort(rxList[6], rxList[7]);
                        if (rxcrc == rxcrc1)
                        {
                            return true;
                        }
                        else
                            break;
                    }
                    else if (rxCount > 8)
                    {
                        break;
                    }
                    else if (rxCount == 2)//命令号
                    {
                        if (rx != 0x05)
                        {
                            break;
                        }
                    }
                    else if (rxCount == 1)//从机地址
                    {
                        if (rx != TargetAddr)
                        {
                            break;
                        }
                    }
                }
                Thread.Sleep(RetryInterval);
                retry--;
            }
            return false;
        }
        public bool ProcCommand03(ushort RegAddr,int Count,out byte[] rxList)
        {
            int retry = RetryTimes;
            while (retry != 0)
            {
                byte[] txList = new byte[8];
                txList[0] = TargetAddr;
                txList[1] = 0x03;
                txList[2] = BytesOP.GetHighByte(RegAddr);
                txList[3] = BytesOP.GetLowByte(RegAddr);
                txList[4] = 0;
                txList[5] = (byte)Count;                
                ushort crc = Verify.GetVerify_CRC16(txList, 6);
                txList[6] = BytesOP.GetHighByte(crc);
                txList[7] = BytesOP.GetLowByte(crc);
                Com.Write(txList, 0, txList.Length);
                Com.DiscardInBuffer();
                Com.ReadTimeout = RxTimeOut;
                rxList = new byte[5+2*Count];
                int rxCount = 0;
                byte rx = 0;
                while (true)
                {
                    try
                    {
                        rx = (byte)Com.ReadByte();
                    }
                    catch
                    {
                        break;
                    }
                    rxList[rxCount++] = rx;
                    if (rxCount == (5 + 2 * Count))
                    {
                        //校验
                        ushort rxcrc = Verify.GetVerify_CRC16(rxList, 3 + 2 * Count);
                        ushort rxcrc1 = BytesOP.MakeShort(rxList[3 + 2 * Count], rxList[4 + 2 * Count]);
                        if (rxcrc == rxcrc1)
                        {
                            return true;
                        }
                        else
                            break;
                    }
                    else if (rxCount > (5 + 2 * Count))
                    {
                        break;
                    }
                    else if (rxCount == 3)//字节数
                    {
                        if (rx != 2*Count)
                        {
                            break;
                        }
                    }
                    else if (rxCount == 2)//命令号
                    {
                        if (rx != 0x03)
                        {
                            break;
                        }
                    }
                    else if (rxCount == 1)//从机地址
                    {
                        if (rx != TargetAddr)
                        {
                            break;
                        }
                    }
                }
                Thread.Sleep(RetryInterval);
                retry--;
            }
            rxList = null;
            return false;
        }
        public bool ProcCommand04(ushort RegAddr, int Count, out byte[] rxList)
        {
            int retry = RetryTimes;
            while (retry != 0)
            {
                byte[] txList = new byte[8];
                txList[0] = TargetAddr;
                txList[1] = 0x04;
                txList[2] = BytesOP.GetHighByte(RegAddr);
                txList[3] = BytesOP.GetLowByte(RegAddr);
                txList[4] = 0;
                txList[5] = (byte)Count;
                ushort crc = Verify.GetVerify_CRC16(txList, 6);
                txList[6] = BytesOP.GetHighByte(crc);
                txList[7] = BytesOP.GetLowByte(crc);
                Com.Write(txList, 0, txList.Length);
                Com.DiscardInBuffer();
                Com.ReadTimeout = RxTimeOut;
                rxList = new byte[5 + 2 * Count];
                int rxCount = 0;
                byte rx = 0;
                while (true)
                {
                    try
                    {
                        rx = (byte)Com.ReadByte();
                    }
                    catch
                    {
                        break;
                    }
                    rxList[rxCount++] = rx;
                    if (rxCount == (5 + 2 * Count))
                    {
                        //校验
                        ushort rxcrc = Verify.GetVerify_CRC16(rxList, 3 + 2 * Count);
                        ushort rxcrc1 = BytesOP.MakeShort(rxList[3 + 2 * Count], rxList[4 + 2 * Count]);
                        if (rxcrc == rxcrc1)
                        {
                            return true;
                        }
                        else
                            break;
                    }
                    else if (rxCount > (5 + 2 * Count))
                    {
                        break;
                    }
                    else if (rxCount == 3)//字节数
                    {
                        if (rx != 2 * Count)
                        {
                            break;
                        }
                    }
                    else if (rxCount == 2)//命令号
                    {
                        if (rx != 0x04)
                        {
                            break;
                        }
                    }
                    else if (rxCount == 1)//从机地址
                    {
                        if (rx != TargetAddr)
                        {
                            break;
                        }
                    }
                }
                Thread.Sleep(RetryInterval);
                retry--;
            }
            rxList = null;
            return false;
        }
    }
}
