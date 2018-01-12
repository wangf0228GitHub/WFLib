using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WFNetLib.Strings.CryptoService;
using WFNetLib.StringFunc;

namespace WFNetLib
{
    public class SoftVerify_byMachineCode
    {
        public class VerifyMachineCode_DES
        {
            public string ID;
            public string Encode;
        }
        public class VerifyMachineCode_BYTE
        {
            public byte[] ID;
            public byte[] Encode;
            public VerifyMachineCode_BYTE()
            {
                ID = new byte[8];
                Encode = new byte[8];
            }
        }
        public static int lcyNum = 5;
        public static int lcyInitNum = 1;
        public static VerifyMachineCode_DES GetEncode_DES()
        {
            VerifyMachineCode_DES ret = new VerifyMachineCode_DES();
            string str1 = ReadSystemHardDeviceParam.GetHardDisk_SerialNumber().Trim();
            string str2 = ReadSystemHardDeviceParam.GetCPU_SerialNumber().Trim();
            string str3 = ReadSystemHardDeviceParam.GetMacAddrs().Trim();
            string str4 = ReadSystemHardDeviceParam.GetMasterBoard_SerialNumber().Trim();
            byte[] b1 = Encoding.ASCII.GetBytes(str1);
            byte[] b2 = Encoding.ASCII.GetBytes(str2);
            byte[] b3 = Encoding.ASCII.GetBytes(str3);
            byte[] b4 = Encoding.ASCII.GetBytes(str4);
            byte[] ID = new byte[8];
            for (int i = 0; i < 8; i++)
            {
                try
                {
                    ID[i] = (byte)(b1[i] ^ b2[i] ^ b3[i] ^ b4[i]);
                }
                catch// (System.Exception ex)
                {
                    ID[i] = (byte)'w';
                } 
            }
            lcyHashCal lcy=new lcyHashCal();
            byte[] Encode = lcy.HashCal(ID, lcyNum);
            DESCrypto des = new DESCrypto();
            ret.ID = des.EncryptString(StringsFunction.byteToHexStr(ID,""), "wfMaCovf", "Machincd");
            ret.Encode = des.EncryptString(StringsFunction.byteToHexStr(Encode, ""), "wfMaCovf", "Machincd");
            return ret;
        }
        public static bool CheckEncode_DES(VerifyMachineCode_DES vc)
        {
            lcyHashCal lcy = new lcyHashCal();
            DESCrypto des = new DESCrypto();
            byte[] ID = StringsFunction.strToHexByte(des.DecryptString(vc.ID, "wfMaCovf", "Machincd"),"");
            byte[] x = lcy.HashCal(ID, lcyNum);
            byte[] Encode = StringsFunction.strToHexByte(des.DecryptString(vc.Encode, "wfMaCovf", "Machincd"), "");
            for (int i = 0; i < 8; i++)
            {
                if (x[i] != Encode[i])
                    return false;
            }
            return true;
        }
        public static VerifyMachineCode_BYTE GetEncode_BYTE()
        {
            VerifyMachineCode_BYTE ret = new VerifyMachineCode_BYTE();
            string str1 = ReadSystemHardDeviceParam.GetHardDisk_SerialNumber().Trim();
            string str2 = ReadSystemHardDeviceParam.GetCPU_SerialNumber().Trim();
            string str3 = ReadSystemHardDeviceParam.GetMacAddrs().Trim();
            string str4 = ReadSystemHardDeviceParam.GetMasterBoard_SerialNumber().Trim();
            byte[] b1 = Encoding.ASCII.GetBytes(str1);
            byte[] b2 = Encoding.ASCII.GetBytes(str2);
            byte[] b3 = Encoding.ASCII.GetBytes(str3);
            byte[] b4 = Encoding.ASCII.GetBytes(str4);
            for (int i = 0; i < 8; i++)
            {
                try
                {
                    ret.ID[i] = (byte)(b1[i] ^ b2[i] ^ b3[i] ^ b4[i]);
                }
                catch// (System.Exception ex)
                {
                    ret.ID[i] = (byte)'w';
                } 
            }
            lcyHashCal lcy = new lcyHashCal();
            ret.Encode = lcy.HashCal(ret.ID, lcyNum);            
            return ret;
        }
        private static byte[] MachineCode=null;
        public static byte[] GetMachineCode()
        {
            if(MachineCode==null)
            {
                byte[] ID = new byte[8];
                string str1 = ReadSystemHardDeviceParam.GetHardDisk_SerialNumber().Trim();
                string str2 = ReadSystemHardDeviceParam.GetCPU_SerialNumber().Trim();
                string str3 = ReadSystemHardDeviceParam.GetMacAddrs().Trim();
                string str4 = ReadSystemHardDeviceParam.GetMasterBoard_SerialNumber().Trim();
                byte[] b1 = Encoding.ASCII.GetBytes(str1);
                byte[] b2 = Encoding.ASCII.GetBytes(str2);
                byte[] b3 = Encoding.ASCII.GetBytes(str3);
                byte[] b4 = Encoding.ASCII.GetBytes(str4);

                for (int i = 0; i < 8; i++)
                {
                    try
                    {
                        ID[i] = (byte)(b1[i] ^ b2[i] ^ b3[i] ^ b4[i]);
                    }
                    catch// (System.Exception ex)
                    {
                        ID[i] = (byte)'w';
                    }
                }
                lcyHashCal lcy = new lcyHashCal();
                MachineCode=lcy.HashCal(ID, lcyInitNum);
            }
            return MachineCode;
        }
        public static bool CheckEncode_BYTE(byte[] ver)
        {
            lcyHashCal lcy = new lcyHashCal();
            byte[] id = GetMachineCode();
            byte[] x = lcy.HashCal(id, 5);
            for (int i = 0; i < 8; i++)
            {
                if (x[i] != ver[i])
                    return false;
            }
            return true;
        }
        public static bool CheckEncode_BYTE(VerifyMachineCode_BYTE vc)
        {
            lcyHashCal lcy = new lcyHashCal();            
            byte[] x = lcy.HashCal(vc.ID, lcyNum);
            for (int i = 0; i < 8; i++)
            {
                if (x[i] != vc.Encode[i])
                    return false;
            }
            return true;
        }
    }
}
