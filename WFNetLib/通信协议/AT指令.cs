using System;
using System.Collections.Generic;

using System.Text;
using System.IO.Ports;
using System.Diagnostics;

namespace WFNetLib
{
    public class AT指令
    {
        SerialPort Com;
        public List<string> RxList;
        public AT指令(SerialPort _Com)
        {
            Com = _Com;
            Com.ReadTimeout = 10000;
            Com.RtsEnable = true;
            RxList = new List<string>();
        }       
        public bool ATSend(string at, string strEnd,int RetryTiems)
        {
            Debug.WriteLine(Com.PortName + at);
            while (RetryTiems != 0)
            {
                RetryTiems--;
                try
                {
                    RxList.Clear();
                    string str;
                    byte[] tx = Encoding.UTF8.GetBytes(at + "\r");
                    Com.Write(tx, 0, tx.Length);
                    Com.Encoding = Encoding.UTF8;
                    while (true)
                    {
                        str = Com.ReadTo("\r\n");
                        Debug.WriteLine(Com.PortName + str);
                        if (str.IndexOf("ERROR") != -1)
                        {
                            RxList.Add(str);
                            return false;
                        }
                        if (str == strEnd)
                        {
                            return true;                            
                        }
                        else
                        {
                            if (str != "")
                                RxList.Add(str);
                        }
                    }                    
                }
                catch
                {
                	
                }
            }
            return false;
        }
        public bool ATSend(string at)
        {
            return ATSend(at, "OK");
        }
        public bool ATSend(string at, string strEnd)
        {
            return ATSend(at, strEnd, 1);
        }
        public bool ATSend(string at, int RetryTiems)
        {
            return ATSend(at, "OK", RetryTiems);
        }
    }
}
