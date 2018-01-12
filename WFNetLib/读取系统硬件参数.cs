using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace WFNetLib
{
    public class ReadSystemHardDeviceParam
    {
        //硬盘序列号
        public static string GetHardDisk_SerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_PhysicalMedia"); //Win32_DiskDrive不包含SerialNumber属性。 
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = null;
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }
            return strID;
        }

        //硬盘序列号
        public static string GetHardDisk_HDid()
        {
            String HDid="";
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid += (string)mo.Properties["Model"].Value;
                HDid += ';';
            }
            HDid = HDid.Substring(0, HDid.Length - 1);
            return HDid;
        }

        //CPU序列号
        public static string GetCPU_SerialNumber()
        {
            string cpuInfo = "";//cpu序列号
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo += mo.Properties["ProcessorId"].Value.ToString();
                cpuInfo += ";";
            }
            cpuInfo = cpuInfo.Substring(0, cpuInfo.Length - 1);
            return cpuInfo;
        }


        //获取网卡硬件地址
        public static string GetMacAddrs()
        {
            string strmac="";
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    strmac += mo["MacAddress"].ToString();
                    strmac += ";";
                }
                //Response.Write("MAC address\t{0}"+mo["MacAddress"].ToString());
                mo.Dispose();
            }
            strmac = strmac.Substring(0, strmac.Length - 1);
            return strmac;
        }

        //主板
        public static string GetMasterBoard_SerialNumber()
        {
            string strbNumber = string.Empty;
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_baseboard");
            foreach (ManagementObject mo in mos.Get())
            {
                strbNumber = mo["SerialNumber"].ToString();
                break;
            }
            return strbNumber;
        }
    }
}
