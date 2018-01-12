using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;

namespace WFNetLib.Net
{
    public class TCPDataRecombine
    {
        public TCPDataType datatype = TCPDataType.HTTP;
        private uint initSeqNum;
        public uint InitSeqNum
        {
            get { return initSeqNum; }
            set
            {
                initSeqNum = value;
                NextSeqNum = initSeqNum;
            }
        }
        uint NextSeqNum;
        public TCPDataRecombine()
        {
            Unkown = new Hashtable();
            TCPDatas = new List<byte>();
            oTCPDatas = new Http();
        }
        public TCPDataRecombine(TCPDataType type)
        {
            datatype = type;
            Unkown = new Hashtable();
            TCPDatas = new List<byte>();
            switch (datatype)
            {
                case TCPDataType.HTTP:
                    oTCPDatas = new Http();
                    break;
                default:
                    throw new Exception("未知TCP数据类型，无法分析");
            }
        }
        Hashtable Unkown;
        public object oTCPDatas;
        public List<byte> TCPDatas;
        public int bFinish(uint SeqNum, List<byte> Datas)
        {
            return bFinish(SeqNum, Datas, 100);
        }
        public int bFinish(uint SeqNum, List<byte> Datas,Single FinishPer )
        {
            Debug.WriteLine("--------------------------------------------");
            Debug.WriteLine("需求编号:" + NextSeqNum.ToString());
            Debug.WriteLine("当前编号:" + SeqNum.ToString());
            Debug.WriteLine("此帧数据量:" + Datas.Count.ToString());
            if (SeqNum > NextSeqNum)//有数据提前到
            {
                //return (int)HttpRxState.LengthOut;
                Unkown.Add(SeqNum, Datas);
            }
            else if (SeqNum < NextSeqNum)
            {
                //return (int)HttpRxState.LengthOut;
                if (Datas.Count > (NextSeqNum - SeqNum))
                {
                    for (int i = 0; i < (Datas.Count - (NextSeqNum - SeqNum)); i++)
                    {
                        switch (datatype)
                        {
                            case TCPDataType.HTTP:
                                Http http=(Http)oTCPDatas;
                                http.HttpDatas.Add(Datas[i + (int)(NextSeqNum - SeqNum)]);
                                break;
                            default:
                                throw new Exception("未知TCP数据类型，无法分析");
                        }                        
                        NextSeqNum++;
                    }
                }
            }
            else//相等
            {
                foreach (byte b in Datas)
                {
                    switch (datatype)
                    {
                        case TCPDataType.HTTP:
                            Http http=(Http)oTCPDatas;
                            http.HttpDatas.Add(b);
                            break;
                        default:
                            throw new Exception("未知TCP数据类型，无法分析");
                    }
                }
                NextSeqNum += (uint)Datas.Count;
            }
            if (Unkown.Count != 0)
            {
                List<uint> needRemove=new List<uint>();
                foreach (DictionaryEntry un in Unkown)
                {
                    List<byte> data = (List<byte>)un.Value;
                    uint seq = (uint)un.Key;
                    if (seq < NextSeqNum)
                    {
                        if (data.Count > (seq - NextSeqNum))
                        {
                            for (int i = 0; i < (Datas.Count - (NextSeqNum - seq)); i++)
                            {
                                switch (datatype)
                                {
                                    case TCPDataType.HTTP:
                                        Http http=(Http)oTCPDatas;
                                        http.HttpDatas.Add(data[i + (int)(NextSeqNum - seq)]);
                                        break;
                                    default:
                                        throw new Exception("未知TCP数据类型，无法分析");
                                }
                                NextSeqNum++;
                            }
                        }
                        needRemove.Add(seq);
                    }
                    else if (seq == NextSeqNum)
                    {
                        foreach (byte b in data)
                        {
                            switch (datatype)
                            {
                                case TCPDataType.HTTP:
                                    Http http=(Http)oTCPDatas;
                                    http.HttpDatas.Add(b);
                                    break;
                                default:
                                    throw new Exception("未知TCP数据类型，无法分析");
                            }                            
                        }
                        NextSeqNum += (uint)data.Count;
                        needRemove.Add(seq);
                    }
                }
                foreach (uint r in needRemove)
                {
                    Unkown.Remove(r);
                }
            } 
            Debug.WriteLine("计算后的编号:" + NextSeqNum.ToString());
            switch (datatype)
            {
                case TCPDataType.HTTP:
                    Http http=(Http)oTCPDatas;
                    return http.bFinish(FinishPer);
                default:
                    return (int)HttpRxState.UnKnow;
            }
        }
    }
}
