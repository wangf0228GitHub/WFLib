using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace WFNetLib
{
    public class iniFileOP
    {
        // section：要读取的段落名
        // key: 要读取的键
        // defVal: 读取异常的情况下的缺省值
        // retVal: key所对应的值，如果该key不存在则返回空值
        // size: 值允许的大小
        // filePath: INI文件的完整路径和文件名
 
//         section：要读取的段落名
//         key: 要读取的键
//         defVal: 读取异常的情况下的缺省值
//         retVal: 此参数类型不是string，而是Byte[]用于返回byte类型的section组或键值组。
//         size: 值允许的大小
//         filePath: INI文件的完整路径和文件名
        
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNames", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

        [DllImport("KERNEL32.DLL ", EntryPoint = "GetPrivateProfileSection", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);
        //section: 要写入的段落名
        //key: 要写入的键，如果该key存在则覆盖写入
        //val: key所对应的值
        //filePath: INI文件的完整路径和文件名
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Ansi)]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        public static string iniFilePath;

        /// <summary>
        /// 向INI写入数据。
        /// </summary>
        /// <PARAM name="Section">节点名。</PARAM>
        /// <PARAM name="Key">键名。</PARAM>
        /// <PARAM name="Value">值名。</PARAM>
        public static void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, iniFilePath);
        }
        public static void Write(string Section, string Key, string Value, string path)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        /// <summary>
        /// 读取INI数据。
        /// </summary>
        /// <PARAM name="Section">节点名。</PARAM>
        /// <PARAM name="Key">键名。</PARAM>
        /// <PARAM name="Path">值名。</PARAM>
        /// <returns>相应的值。</returns>
        public static string Read(string Section, string Key)
        {
            return Read(Section, Key, iniFilePath);
        }
        public static string Read(string Section, string Key, string path)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, path);
            return temp.ToString();
        }

        /// <summary>
        /// 读取一个ini里面所有的节
        /// </summary>
        /// <param name="sections"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetAllSectionNames(out string[] sections)
        {
            return GetAllSectionNames(out sections, iniFilePath);
        }
        public static int GetAllSectionNames(out string[] sections, string path)
        {
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, path);
            if (bytesReturned == 0)
            {
                sections = null;
                return -1;
            }
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            //use of Substring below removes terminating null for split
            sections = local.Substring(0, local.Length - 1).Split('\0');
            return 0;
        }

        /// <summary>
        /// 得到某个节点下面所有的key和value组合
        /// </summary>
        /// <param name="section"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int GetAllKeyValues(string section, out string[] keys, out string[] values)
        {
            return GetAllKeyValues(section, out keys, out values, iniFilePath);
        }
        public static int GetAllKeyValues(string section, out string[] keys, out string[] values, string path)
        {
            byte[] b = new byte[65535];

            GetPrivateProfileSection(section, b, b.Length, path);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            ArrayList result = new ArrayList();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            keys = new string[result.Count];
            values = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].ToString().Split(new char[] { '=' });
                if (item.Length == 2)
                {
                    keys[i] = item[0].Trim();
                    values[i] = item[1].Trim();
                }
                else if (item.Length == 1)
                {
                    keys[i] = item[0].Trim();
                    values[i] = "";
                }
                else if (item.Length == 0)
                {
                    keys[i] = "";
                    values[i] = "";
                }
            }

            return 0;
        }
    }
}
