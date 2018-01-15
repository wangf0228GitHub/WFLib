using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib.NMEA//美国国家海洋电子协会（National Marine Electronics Association ）
{
    public class NMEA0183
    {
        public char[] RxList;
        string ID;
        bool bVerify;
        public NMEA0183(int len, string id)
        {
            RxList = new char[len];
            ID = id;
            bVerify=false;
        }
        public NMEA0183(int len,string id ,bool bv)
        {
            RxList = new char[len];
            ID = id;
            bVerify = bv;
        }
        byte RxCount = 0;
        public string RxProc(char rx)
        {
            RxList[RxCount++] = rx;
            if (RxCount >= RxList.Length)
            {
                RxCount = 0;
                return string.Empty;
            }            
            if (RxCount == 1)
            {
                if (rx != '$')
                    RxCount = 0;
            }
            else if (RxCount == ID.Length)
            {
                RxList[6] = '\0';
                string str = new string(RxList,0,ID.Length);
                if (str != ID)
                    RxCount = 0;
            }
            else if (rx == '*')
            {
                if (!bVerify)
                {
                    int len = RxCount;
                    RxCount = 0;
                    return new string(RxList, 0, len);
                }                    
            }
            else if (rx==0x0a)
            {
                int len = RxCount;
                RxCount = 0;
                if (RxList[len - 2] == 0x0d)
                {                    
                    if (bVerify)//校验
                    {
                        byte cs = 0;
                        for (int i = 1; i < len - 5; i++)
                        {
                            cs ^= (byte)RxList[i];
                        }
                        byte cs1 = BytesOP.ASCII2Byte((byte)RxList[len - 4], (byte)RxList[len - 3]);
                        if(cs==cs1)
                            return new string(RxList, 0,len);
                    }                        
                }
            }
            return string.Empty;
        }
    }
}
