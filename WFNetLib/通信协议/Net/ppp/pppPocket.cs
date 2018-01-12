using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace WFNetLib.Net
{
    public class pppPocket
    {
        public ushort Protocol;//协议
        public byte Code;//代码号
        public byte ID;//标识码
        //        public byte[] Data;
        public List<byte> Datas;
        public Hashtable Commands;
        public pppPocket()
        {
            Datas = new List<byte>();
            Commands = new Hashtable();
            Datas.Clear();
            Commands.Clear();
            ID = 0x01;
        }
        public void AddCommand(byte comm, params byte[] list)
        {
            Commands.Add(comm, list);
            if (list == null)
            {
                Datas.Add(comm);
                Datas.Add(0x02);
            }
            else
            {
                Datas.Add(comm);
                Datas.Add((byte)(list.Length + 0x02));
                Datas.AddRange(list);
            }
        }
        public void RemoveCommand(byte comm)
        {
            Commands.Remove(comm);
            for (int i = 0; i < Datas.Count; )
            {
                if (Datas[i] == comm)
                {
                    Datas.RemoveRange(i, Datas[i + 1]);
                    break;
                }
                else
                    i = i + Datas[i + 1];
            }
        }
        public void ClearCommand()
        {
            Commands.Clear();
            Datas.Clear();
        }
    }
}
