using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.NMEA
{
    //$HCHDG, x.x,x.x,a,x.x,a*hh<cr><lf>
    public class HCHDT
    {
        public static int MaxLength = 17;
        public static bool bVerify = false;
        public string Heading;//航向       
        public string True;//真假
        public string AllRx;
        static string ID = "$HCHDT";
        NMEA0183 nmea0183;
        public bool RxProc(char rx)
        {
            string strRx = nmea0183.RxProc(rx);
            if (string.IsNullOrEmpty(strRx))
                return false;
            string[] Params = strRx.Split(',');
            if (Params[0] == ID)
            {
                if (Params.Length == 3)
                {
                    Heading = Params[1];                    
                    True = Params[2].Substring(0, Params[2].IndexOf('*'));
                    AllRx = strRx;
                    return true;
                }
            }
            return false;
        }
        public HCHDT()
        {
            nmea0183 = new NMEA0183(MaxLength, ID);
        }
        public HCHDT(bool bv)
        {
            bVerify = bv;
            nmea0183 = new NMEA0183(MaxLength, ID, bVerify);
        }
    }
}
