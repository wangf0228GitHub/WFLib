using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFNetLib.Strings.CryptoService;
using System.IO;

namespace WFNetLib
{
    public class SoftDataProtect
    {
        public static string filePath = System.Windows.Forms.Application.StartupPath + "\\Runinfo.mkt";
        public static string sKey;
        public static string sIV;
        public static string sKey_Len;
        public static string sIV_Len;
        public static int lenlen = 12;
        public static int blockSize = 100;
        public static void SetSoftData(string str,int index)
        {
            DESCrypto des = new DESCrypto();
            string strDes = "", strLen;
            FileStream fs;
            fs = new FileStream(filePath, FileMode.Open);            
            strDes = des.EncryptString(str, sKey, sIV);
            BinaryWriter sw = new BinaryWriter(fs);
            //开始写入            
            fs.Seek(index*blockSize*2, SeekOrigin.Begin);
            strLen = des.EncryptString(strDes.Length.ToString("d4"), sKey_Len, sIV_Len);
            strLen += GenerateCheckCode(blockSize - lenlen);
            strDes += GenerateCheckCode(blockSize - strDes.Length);
            sw.Write(Encoding.UTF8.GetBytes(strLen + strDes));
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();/**/
        }
        public static string GetSoftData(int index)
        {
            DESCrypto des = new DESCrypto();
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryReader sw = new BinaryReader(fs);
                //开始写入            
                fs.Seek(index * blockSize*2, SeekOrigin.Begin);
                byte[] rd = sw.ReadBytes(blockSize*2);
                sw.Close();
                fs.Close();
                
                string strLen = Encoding.UTF8.GetString(rd, 0, lenlen);
                strLen = des.DecryptString(strLen, sKey_Len, sIV_Len);
                int len = int.Parse(strLen);
                string strDes = Encoding.UTF8.GetString(rd, blockSize, len);
                strDes = des.DecryptString(strDes, sKey, sIV);
                return strDes;
            }
            catch
            {
                throw;
            }
        }
        public static void InitDataFile(int ParamConut)
        {
            FileStream fs;
            try
            {
                fs = new FileStream(filePath, FileMode.OpenOrCreate);
            }
            catch (FileNotFoundException)
            {
                fs = new FileStream(filePath, FileMode.Create);
            }
            Int64 len = (Int64)(blockSize*2) * (Int64)ParamConut;
            fs.SetLength(len); //设置文件大小            
            fs.Close();
            File.SetAttributes(filePath, FileAttributes.System | FileAttributes.Hidden);
            try
            {
                sKey = ReadSystemHardDeviceParam.GetHardDisk_SerialNumber().Trim().Substring(0,8);
            }
            catch
            {
                sKey = "wfvf_key";
            }
            try
            {
                sIV = ReadSystemHardDeviceParam.GetMasterBoard_SerialNumber().Trim().Substring(0, 8);
            }
            catch
            {
                sIV = "wfvf__iv";
            }
            try
            {
                sKey_Len = ReadSystemHardDeviceParam.GetCPU_SerialNumber().Trim().Substring(0, 8);
            }
            catch
            {
                sKey_Len = "wfvf__kl";
            }
            try
            {
                sIV_Len = ReadSystemHardDeviceParam.GetMacAddrs().Trim().Substring(0, 8);
            }
            catch
            {
                sIV_Len = "wfvf_ivl";
            }
        }
        private static int rep = 0;
        /// 
        /// 生成随机字母字符串(数字字母混和)
        /// 
        /// 待生成的位数
        /// 生成的字母字符串
        private static string GenerateCheckCode(int codeCount)
        {
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}
