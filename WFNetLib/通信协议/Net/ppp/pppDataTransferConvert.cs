using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.Net
{
    public class pppDataTransferConvert
    {
        public static pppPocket MakepppPacket(List<byte> RxList)
        {
            return MakepppPacket(RxList, 0);
        }
        public static pppPocket MakepppPacket(List<byte> RxList,int start)
        {
            pppPocket ppp = new pppPocket();
            ppp.Protocol = BytesOP.MakeShort(RxList[start], RxList[start+1]);
            ppp.Code = RxList[start+2];
            ppp.ID = RxList[start+3];
            ushort l = BytesOP.MakeShort(RxList[start + 4], RxList[start+5]);
            l = (ushort)(l - 4);
            try
            {
                for (int i = 0; i < l; )
                {
                    byte[] bs = new byte[RxList[i + start+1 + 6] - 2];
                    for (int j = 0; j < bs.Length; j++)
                    {
                        bs[j] = RxList[i + 2 + j + 6 + start];
                    }
                    ppp.AddCommand(RxList[i + 6 + start], bs);
                    i = i + RxList[i + 1 + start + 6];
                }
            }
            catch { }
            return ppp;
        }
        public static List<byte> PPPDataDecode(List<byte> RxList)
        {
            List<byte> temp = new List<byte>();
            for (int i = 0; i < RxList.Count; i++)
            {
                if (RxList[i] == 0x7d)//转义字符
                {
                    if (RxList[i + 1] == 0x5e)//0x7e
                    {
                        temp.Add(0x7e);
                    }
                    else if (RxList[i + 1] == 0x5d)//0x7d
                    {
                        temp.Add(0x7d);
                    }
                    else//-0x20
                    {
                        temp.Add((byte)(RxList[i + 1] - 0x20));
                    }
                    i++;
                }
                else
                    temp.Add(RxList[i]);
            }
            return temp;
        }
        public static List<byte> PPPDataEncode(List<byte> RxList, bool bLCP)
        {
            ushort crc = Verify.GetVerify_CRC16_CCITT(RxList.ToArray(), RxList.Count);
            RxList.Add(BytesOP.GetLowByte(crc));
            RxList.Add(BytesOP.GetHighByte(crc));
            List<byte> temp = new List<byte>();
            temp.Add(0x7e);
            byte x;
            foreach (byte b in RxList)
            {
                x = IsNeedTrans(b, bLCP);
                if (x != 0x00)
                {
                    temp.Add(0x7d);
                    temp.Add(x);
                }
                else
                    temp.Add(b);
            }
            temp.Add(0x7e);
            return temp;
        }
        public static List<byte> PPPDataEncode(pppPocket ppp, bool bLCP)
        {
            List<byte> temp = new List<byte>();
            temp.Add(0xff);
            temp.Add(0x03);
            temp.Add(BytesOP.GetHighByte(ppp.Protocol));
            temp.Add(BytesOP.GetLowByte(ppp.Protocol));
            temp.Add(ppp.Code);
            temp.Add(ppp.ID);
            ushort len = (ushort)(ppp.Datas.Count + 4);
            temp.Add(BytesOP.GetHighByte(len));
            temp.Add(BytesOP.GetLowByte(len));
            foreach (byte b in ppp.Datas)
            {
                temp.Add(b);
            }
            ushort crc = Verify.GetVerify_CRC16_CCITT(temp.ToArray(), temp.Count);
            temp.Add(BytesOP.GetLowByte(crc));
            temp.Add(BytesOP.GetHighByte(crc));
            List<byte> ret = new List<byte>();
            //ret.Add(0x7e);
            byte x;
            foreach (byte b in temp)
            {
                x = IsNeedTrans(b, bLCP);
                if (x != 0x00)
                {
                    ret.Add(0x7d);
                    ret.Add(x);
                }
                else
                    ret.Add(b);
            }
            ret.Add(0x7e);
            return ret;
        }
        public static List<byte> PPPDataList(pppPocket ppp)
        {
            List<byte> temp = new List<byte>();
            temp.Add(0xff);
            temp.Add(0x03);
            temp.Add(BytesOP.GetHighByte(ppp.Protocol));
            temp.Add(BytesOP.GetLowByte(ppp.Protocol));
            temp.Add(ppp.Code);
            temp.Add(ppp.ID);
            ushort len = (ushort)(ppp.Datas.Count + 4);
            temp.Add(BytesOP.GetHighByte(len));
            temp.Add(BytesOP.GetLowByte(len));
            foreach (byte b in ppp.Datas)
            {
                temp.Add(b);
            }
//             ushort crc = Verify.GetVerify_CRC16_CCITT(temp.ToArray(), temp.Count);
//             temp.Add(BytesOP.GetLowByte(crc));
//             temp.Add(BytesOP.GetHighByte(crc));
            return temp;
        }
        static byte IsNeedTrans(byte b, bool bLCP)
        {
            if (b == 0x7d)
                return 0x5d;
            if (b == 0x7e)
                return 0x5e;
            if (b < 0x20 && !bLCP)
                return (byte)(b + 0x20);
            return 0x00;
        }
        public static List<byte> IPDataEncode(IPPacket ip)
        {
            List<byte> ret = ip.ToArray();            
            ret.Insert(0, 0x21);
            ret.Insert(0, 0x00);
            ret.Insert(0, 0x03);
            ret.Insert(0, 0xff);
            return PPPDataEncode(ret, true);
        }
    }
}
