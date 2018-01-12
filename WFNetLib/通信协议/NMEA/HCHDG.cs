using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.NMEA
{
    //$HCHDG, x.x,x.x,a,x.x,a*hh<cr><lf>
    public class HCHDG
    {
        static string ID = "$HCHDG";
        public static int MaxLength = 33;
        public static bool bVerify = false;
        public string Heading;//航向
        public string Deviation;//偏向角
        public string Variation;//磁偏角
        public string DeviationEW;//偏向角正负
        public string VariationEW;//磁偏角正负
        public string AllRx;     
        NMEA0183 nmea0183 ;
        public bool RxProc(char rx)
        {
            string strRx=nmea0183.RxProc(rx);
            if (string.IsNullOrEmpty(strRx))
                return false;
            string[] Params = strRx.Split(',');
            if(Params[0] == ID)
            {
                if (Params.Length == 6)
                {
                    Heading = Params[1];
                    Deviation = Params[2];
                    DeviationEW = Params[3];
                    Variation = Params[4];
                    VariationEW = Params[5].Substring(0, Params[5].IndexOf('*'));                
                    AllRx = strRx;
                    return true;
                }
            }            
            return false;
        }
        public HCHDG()
        {
            nmea0183 = new NMEA0183(MaxLength, ID);
        }
        public HCHDG(bool bv)
        {
            bVerify = bv;
            nmea0183 = new NMEA0183(MaxLength, ID, bVerify);
        }
    }
}
