using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using WFNetLib.Strings.CryptoService;
using System.Security.Cryptography;

namespace WFNetLib
{
    public class SoftVerify_byRegedit
    {
        public static SoftVerifyData GetVerifyData(string soft)
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey software = key.OpenSubKey("software\\" + soft);
            DESCrypto des = new DESCrypto();
            string desstr="";
            if (software == null)//不存在
            {
                throw (new CryptographicException("系统关键数据丢失"));
//                 software = key.CreateSubKey("software\\" + soft);//首次运行	                
//                 desstr = des.EncryptString(DateTime.Now.ToString("yyyy-MM-dd"), "wfsoftvf", "verifyfd");
//                 software.SetValue("FirstTime", desstr);
//                 desstr = des.EncryptString(DateTime.Now.ToString("yyyy-MM-dd"), "wfsoftvf", "verifycd");
//                 software.SetValue("CurTime", desstr);
//                 desstr = des.EncryptString("0", "wfsoftvf", "verifyud");
//                 software.SetValue("UsedDays", desstr);
//                 desstr = des.EncryptString("0", "wfsoftvf", "verifyut");
//                 software.SetValue("UsedTimes", desstr);
            }
            SoftVerifyData ret = new SoftVerifyData();
            desstr = software.GetValue("FirstTime").ToString();
            ret.FirstTime = DateTime.Parse(des.DecryptString(desstr, "wfsoftvf", "verifyfd"));
            desstr = software.GetValue("CurTime").ToString();
            ret.CurTime = DateTime.Parse(des.DecryptString(desstr, "wfsoftvf", "verifycd"));
            desstr = software.GetValue("UsedDays").ToString();
            ret.UsedDays = Int32.Parse(des.DecryptString(desstr, "wfsoftvf", "verifyud"));
            desstr = software.GetValue("UsedTimes").ToString();
            ret.UsedTimes = Int32.Parse(des.DecryptString(desstr, "wfsoftvf", "verifyut"));
            return ret;
        }
        public static SoftVerifyData CheckVerifyData(string soft)
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey software = key.OpenSubKey("software\\" + soft);
            DESCrypto des = new DESCrypto();
            string desstr = "";
            if (software == null)//不存在
            {
                return null;
            }
            SoftVerifyData ret = new SoftVerifyData();
            desstr = software.GetValue("FirstTime").ToString();
            ret.FirstTime = DateTime.Parse(des.DecryptString(desstr, "wfsoftvf", "verifyfd"));
            desstr = software.GetValue("CurTime").ToString();
            ret.CurTime = DateTime.Parse(des.DecryptString(desstr, "wfsoftvf", "verifycd"));
            desstr = software.GetValue("UsedDays").ToString();
            ret.UsedDays = Int32.Parse(des.DecryptString(desstr, "wfsoftvf", "verifyud"));
            desstr = software.GetValue("UsedTimes").ToString();
            ret.UsedTimes = Int32.Parse(des.DecryptString(desstr, "wfsoftvf", "verifyut"));
            return ret;
        }
        public static int SetVerifyData(string soft,SoftVerifyData svd)
        {
            RegistryKey key = Registry.LocalMachine;
            RegistryKey software = key.OpenSubKey("software\\" + soft);
            DESCrypto des = new DESCrypto();
            string desstr = "";
            if (software == null)//不存在
            {
                return 2;//缺少键值
            }
            software = key.CreateSubKey("software\\" + soft);	                
//             desstr = des.EncryptString(svd.FirstTime.ToString("yyyy-MM-dd"), "wfsoftvf", "verifyfd");
//             software.SetValue("FirstTime", desstr);
            desstr = des.EncryptString(svd.CurTime.ToString("yyyy-MM-dd"), "wfsoftvf", "verifycd");
            software.SetValue("CurTime", desstr);
            desstr = des.EncryptString(svd.UsedDays.ToString(), "wfsoftvf", "verifyud");
            software.SetValue("UsedDays", desstr);
            desstr = des.EncryptString(svd.UsedTimes.ToString(), "wfsoftvf", "verifyut");
            software.SetValue("UsedTimes", desstr);
            return 1;
        }
    }
    public class SoftVerifyData
    {
        public DateTime FirstTime;
        public DateTime CurTime;
        public int UsedDays;
        public int UsedTimes;
    }
}
