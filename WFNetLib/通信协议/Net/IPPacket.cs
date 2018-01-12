using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.Net
{
    public class IPPacket
    {
        public byte Version=0x04;
        public byte HeaderLength=0x05;//32位的数量，即4个字节的数量，即5代表20字节
        public byte TypeOfService=0x00;
        public ushort TotalLength;//总长度指首部和数据之和的长度，单位为字节
        public ushort Identification=0;//标识
        public byte Flags=0;//占3位
        public ushort FragmentOffset=0;//片偏移
        public byte TimeToLive=0x0ff;//生存时间，通常为0xff
        private byte protocol;
        public byte Protocol//协议
        {
            get { return protocol; }           
            set
            {
                protocol = value;
                switch (protocol)
                {
                    case 0x06://TCP
                        oProtocolContent = new TCPPacket();
                        break;
                }
            }
        }
        public ushort HeaderVerify=0;//首部校验和
        public byte[] SourceAddress;//源地址
        public byte[] DastinationAddress;//目标地址        
        public List<byte> Options;
        public Object oProtocolContent;
        public IPPacket()
        {
            SourceAddress=new byte[4];
            DastinationAddress = new byte[4];
        }
        public IPPacket(byte[] sa,byte[] da)
        {
            SourceAddress = new byte[4];
            DastinationAddress = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                SourceAddress[i] = sa[i];
                DastinationAddress[i] = da[i];
            }
        }

        public ushort SetTotalLength()
        {
            switch (Protocol)
            {
                case 0x06://TCP
                    TCPPacket tcp = (TCPPacket)oProtocolContent;
                    return (ushort)(HeaderLength * 4 + tcp.DataOffset * 4 + tcp.Datas.Count);
            }
            return 0;
        }
        public List<byte> ToArray()
        {
            byte x;
            TotalLength = SetTotalLength();
            List<byte> ret = new List<byte>();
            ret.Add(BytesOP.MakeByte(Version, HeaderLength));
            ret.Add(TypeOfService);
            ret.Add(BytesOP.GetHighByte(TotalLength));
            ret.Add(BytesOP.GetLowByte(TotalLength));
            ret.Add(BytesOP.GetHighByte(Identification));
            ret.Add(BytesOP.GetLowByte(Identification));
            x = (byte)(Flags << 5);
            x += (byte)(FragmentOffset >> 8);
            ret.Add(x);
            ret.Add(BytesOP.GetLowByte(FragmentOffset));
            ret.Add(TimeToLive);
            ret.Add(Protocol);
            ret.Add(0x00);//校验和
            ret.Add(0x00);
            ret.Add(SourceAddress[0]);
            ret.Add(SourceAddress[1]);
            ret.Add(SourceAddress[2]);
            ret.Add(SourceAddress[3]);
            ret.Add(DastinationAddress[0]);
            ret.Add(DastinationAddress[1]);
            ret.Add(DastinationAddress[2]);
            ret.Add(DastinationAddress[3]);
            if (HeaderLength > 0x05)
            {
                for (int i = 0; i < ((HeaderLength - 0x05) * 4); i++)
                {
                    ret.Add(Options[i]);
                }
            }
            HeaderVerify = Verify.GetVerify_IP(ret.ToArray());
            ret[10] = BytesOP.GetHighByte(HeaderVerify);
            ret[11] = BytesOP.GetLowByte(HeaderVerify);
            switch (Protocol)
            {
                case 0x06://TCP
                    FakeTcpHeader fh = new FakeTcpHeader(SourceAddress, DastinationAddress);
                    fh.count = (ushort)(TotalLength - HeaderLength * 4);                    
                    TCPPacket tcp = (TCPPacket)oProtocolContent;
                    List<Byte> TCPList = tcp.ToArray(fh); 
                    foreach (byte b in TCPList)
                    {
                        ret.Add(b);
                    }
                    break;
            }
            Identification++;
            return ret;
        }
        public static IPPacket MakeIPPacket(List<byte> RxList)
        {
            return MakeIPPacket(RxList, 0);
        }
        public static IPPacket MakeIPPacket(List<byte> RxList, int start)
        {
            IPPacket ip = new IPPacket();
            ip.Version = BytesOP.GetHighNibble(RxList[start]);
            ip.HeaderLength = BytesOP.GetLowNibble(RxList[start]);
            ip.TypeOfService = RxList[start+1];
            ip.TotalLength = BytesOP.MakeShort(RxList[start + 2], RxList[start+3]);
            ip.Identification = BytesOP.MakeShort(RxList[start + 4], RxList[start+5]);
            ip.Flags = (byte)(RxList[start+6] >> 5);
            ip.FragmentOffset = (ushort)(RxList[start + 6] & 0x1f + RxList[start+7]);
            ip.TimeToLive = RxList[start+8];
            ip.Protocol = RxList[start+9];
            ip.HeaderVerify = BytesOP.MakeShort(RxList[start + 10], RxList[start+11]);
            for (int i = 0; i < 4; i++)
            {
                ip.SourceAddress[i] = RxList[12 + start + i];
                ip.DastinationAddress[i] = RxList[16 + start + i];
            }
            int DataStart = 20;
            if (ip.HeaderLength > 5)
            {
                ip.Options = new List<byte>();
                for (int i = 0; i < ((ip.HeaderLength - 5) * 4); i++)
                    ip.Options.Add(RxList[22 + i]);
                DataStart += ((ip.HeaderLength - 5) * 4);
            }
            switch (ip.Protocol)
            {
                case 0x06://tcp
                    ip.oProtocolContent = new TCPPacket();
                    TCPPacket tcp = (TCPPacket)ip.oProtocolContent;
                    tcp.SourcePort = BytesOP.MakeShort(RxList[start + DataStart], RxList[start + DataStart + 1]);
                    tcp.DastinationPort = BytesOP.MakeShort(RxList[start + DataStart + 2], RxList[start + DataStart + 3]);
                    tcp.InitialSeqNumber=BytesOP.MakeInt(RxList[start + DataStart + 4],
                                                         RxList[start + DataStart + 5],
                                                         RxList[start + DataStart +6],
                                                         RxList[start + DataStart + 7]);
                    tcp.AckSeqNumber = BytesOP.MakeInt( RxList[start + DataStart + 8],
                                                        RxList[start + DataStart + 9],
                                                        RxList[start + DataStart + 10],
                                                        RxList[start + DataStart + 11]);
                    tcp.DataOffset = BytesOP.GetHighNibble(RxList[start + DataStart + 12]);
                    tcp.TCPFlags = (byte)(RxList[start + DataStart + 13] & 0x3f);
                    tcp.Window = BytesOP.MakeShort(RxList[start + DataStart + 14], RxList[start + DataStart + 15]);
                    tcp.CheckSum = BytesOP.MakeShort(RxList[start + DataStart + 16], RxList[start + DataStart + 17]);
                    tcp.UrgentPoint = BytesOP.MakeShort(RxList[start + DataStart + 18], RxList[start + DataStart + 19]);
                    if (tcp.DataOffset > 5)
                    {
                        tcp.TCPOptions = new List<byte>();
                        for (int i = 0; i < ((tcp.DataOffset - 5) * 4); i++)
                        {
                            tcp.TCPOptions.Add(RxList[start + DataStart + 20 + i]);
                        }
                    }
                    int count = ip.TotalLength - ip.HeaderLength * 4 - tcp.DataOffset * 4;
                    tcp.Datas = new List<byte>();
                    for (int i = 0; i < count; i++)
                    {
                        tcp.Datas.Add(RxList[start + DataStart + tcp.DataOffset * 4 + i]);
                    }
                    break;
            }
            return ip;
        }
    }
}
