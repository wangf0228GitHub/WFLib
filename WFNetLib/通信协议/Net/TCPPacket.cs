using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WFNetLib.Net
{
    public class TCPPacket
    {
        public ushort SourcePort;//源端口
        public ushort DastinationPort;//目标端口
        public uint InitialSeqNumber;//当前序列号
        public uint NextSeqNumber;//下一个序列号
        public uint AckSeqNumber=0;//应答序列号
        public byte DataOffset=5;//数据偏移，即4个字节的数量，即5代表20字节
        public byte TCPFlags;//标志
        public ushort Window=0x1fa0;//窗口字段用来控制对方发送的数据量，单位为字节。TCP连接的一端根据设置的缓存空间大小确定自己的接收窗口大小，然后通知对方以确定对方的发送窗口的上限
        public ushort CheckSum=0;//校验和
        public ushort UrgentPoint=0;//紧急指针指出在本报文段中的紧急数据的最后一个字节的序号
        public List<byte> TCPOptions;//选项
        public List<byte> Datas;
        public TCPPacket()
        {
            Datas = new List<byte>();
            Random x = new Random();
            InitialSeqNumber = (uint)x.Next(1000, 5000);
        }
        public void AddDatas(List<byte> data)
        {
            foreach (byte b in data)
            {
                Datas.Add(b);
                NextSeqNumber++;
            }
        }
        public List<byte> ToArray(FakeTcpHeader fh)
        {
            List<Byte> FakeTCP = new List<Byte>();
            //构建TCP的伪首部
            FakeTCP.Add(fh.SourceAddress[0]);
            FakeTCP.Add(fh.SourceAddress[1]);
            FakeTCP.Add(fh.SourceAddress[2]);
            FakeTCP.Add(fh.SourceAddress[3]);
            FakeTCP.Add(fh.DastinationAddress[0]);
            FakeTCP.Add(fh.DastinationAddress[1]);
            FakeTCP.Add(fh.DastinationAddress[2]);
            FakeTCP.Add(fh.DastinationAddress[3]);
            FakeTCP.Add(0x00);
            FakeTCP.Add(0x06);
            FakeTCP.Add(BytesOP.GetHighByte(fh.count));
            FakeTCP.Add(BytesOP.GetLowByte(fh.count));
            //伪首部完成
            FakeTCP.Add(BytesOP.GetHighByte(SourcePort));
            FakeTCP.Add(BytesOP.GetLowByte(SourcePort));
            FakeTCP.Add(BytesOP.GetHighByte(DastinationPort));
            FakeTCP.Add(BytesOP.GetLowByte(DastinationPort));
            Union_UInt32 u32 = new Union_UInt32();
            u32.ofs_32 = InitialSeqNumber;
            FakeTCP.Add(u32.ofs_h.ofs_h);
            FakeTCP.Add(u32.ofs_h.ofs_l);
            FakeTCP.Add(u32.ofs_l.ofs_h);
            FakeTCP.Add(u32.ofs_l.ofs_l);
            u32.ofs_32 = AckSeqNumber;
            FakeTCP.Add(u32.ofs_h.ofs_h);
            FakeTCP.Add(u32.ofs_h.ofs_l);
            FakeTCP.Add(u32.ofs_l.ofs_h);
            FakeTCP.Add(u32.ofs_l.ofs_l);
            FakeTCP.Add((byte)(DataOffset << 4));
            FakeTCP.Add(TCPFlags);
            FakeTCP.Add(BytesOP.GetHighByte(Window));
            FakeTCP.Add(BytesOP.GetLowByte(Window));
            int sumaddr = FakeTCP.Count;
            FakeTCP.Add(0x00);
            FakeTCP.Add(0x00);
            FakeTCP.Add(BytesOP.GetHighByte(UrgentPoint));
            FakeTCP.Add(BytesOP.GetLowByte(UrgentPoint));
            if (DataOffset > 0x05)
            {
                for (int i = 0; i < ((DataOffset - 0x05) * 4); i++)
                {
                    FakeTCP.Add(TCPOptions[i]);
                }
            }
            foreach (byte b in Datas)
                FakeTCP.Add(b);
            CheckSum = Verify.GetVerify_IP(FakeTCP.ToArray());
            FakeTCP[sumaddr] = BytesOP.GetHighByte(CheckSum);
            FakeTCP[sumaddr + 1] = BytesOP.GetLowByte(CheckSum);
            FakeTCP.RemoveRange(0, 12);
            if (Datas.Count == 0)
            {
                if (TCPFlags == 0x02)
                    InitialSeqNumber++;
            }
            else
                InitialSeqNumber += (uint)Datas.Count;
            return FakeTCP;
        }
    }
    public class FakeTcpHeader
    {
        public byte[] SourceAddress;
        public byte[] DastinationAddress;
        public ushort count;
        public FakeTcpHeader(byte[] s, byte[] d)
        {
            SourceAddress=new byte[4];
            DastinationAddress=new byte[4];
            for (int i = 0; i < 4; i++)
            {
                SourceAddress[i] = s[i];
                DastinationAddress[i] = d[i];
            }
        }
    }
    public enum TCPState
    {
        SYN,
        TransData,
        FIN,
        WaitFIN,
        WaitFIN1
    }
    public enum TCPDataType
    {
        HTTP
    }    
}
