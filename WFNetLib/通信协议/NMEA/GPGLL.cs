using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;

namespace WFNetLib.NMEA
{
    //$GPGLL,<1>,S,<3>,E,<5>,<6>*<7>:地理定位信息
    public class GPGLL
    {
        static string ID = "$GPGLL";
        public int MaxLength=60;
        public bool bVerify = false;
        public bool bNMEA0183300 = false;
        public string Latitude; //<1>：纬度ddmm.mmmm，度分格式（前导位数不足则补0）
        public string NorS;      //<2>：纬度N（北纬,1）或S（南纬,0）
        public string Longitude;//<3>：经度dddmm.mmmm，度分格式（前导位数不足则补0）
        public string EorW;      //<4>：经度E（东经,1）或W（西经,0）
        public string UTCTime;//<5>：UTC时间，hhmmss.sss格式
        public string State;//<6>：状态，A=定位，V=未定位
        public string Mode;//模式指示（仅NMEA0183 3.00版本输出，A=自主定位，D=差分，E=估算，N=数据无效）
        public string AllRx;
        //public byte Verify;//<7>：校验值       
        NMEA0183 nmea0183 ;
        public bool RxProc(char rx)
        {
            string strRx=nmea0183.RxProc(rx);
            if (string.IsNullOrEmpty(strRx))
                return false;
            string[] Params = strRx.Split(',');
            if(Params[0] == ID)
            {
                if(bNMEA0183300)
                {
                    if (Params.Length == 8)
                    {
                        Latitude = Params[1];
                        NorS = Params[2];
                        Longitude = Params[3];
                        EorW = Params[4];
                        UTCTime = Params[5];
                        State = Params[6];
                        Mode = Params[7].Substring(0,Params[7].IndexOf('*'));
                        AllRx = strRx;
                        return true;
                    }
                    else if (Params.Length == 9)
                    {
                        Latitude = Params[1];
                        NorS = Params[2];
                        Longitude = Params[3];
                        EorW = Params[4];
                        UTCTime = Params[5];
                        State = Params[6];
                        Mode = Params[7];
                        AllRx = strRx;
                        return true;
                    }
                }
                else if (Params.Length == 7)
                {
                    Latitude = Params[1];
                    NorS = Params[2];
                    Longitude = Params[3];
                    EorW = Params[4];
                    UTCTime = Params[5];                
    //                 DateTime utc = DateTime.ParseExact(Params[5], "HHmmss.fff", null);
    //                 DateTime d = DateTime.SpecifyKind(utc, DateTimeKind.Utc);
    //                 DateTime d1 = d.ToLocalTime();
                    State = Params[6].Substring(0, Params[6].IndexOf('*'));
                    AllRx = strRx;
                    return true;
                }
            }            
            return false;
        }
        public GPGLL()
        {
            nmea0183 = new NMEA0183(MaxLength, ID);
        }
        public GPGLL(bool bv)
        {
            bVerify = bv;
            nmea0183 = new NMEA0183(MaxLength, ID, bVerify);
        }
    }
}
