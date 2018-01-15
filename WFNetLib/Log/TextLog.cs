using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Data;

namespace WFNetLib.Log
{
    public class TextLog
    {
        public static int MaxUsage = -1;//最大使用空间，单位为M
        public static void AddTextLog(string strLog,string file,bool newSection)
        {            
            FileInfo f = new FileInfo(file);
            // 如果文件所在的文件夹不存在则创建文件夹
            if (!Directory.Exists(f.DirectoryName))
                Directory.CreateDirectory(f.DirectoryName);
            FileStream fs;
            try
            {
                fs = new FileStream(file, FileMode.Open);
            }
            catch (FileNotFoundException)
            {
                if (MaxUsage != -1)//有使用空间的限制
                {
                    while(true)
                    {
                        long[] dir = FileOP.getDirInfos(f.DirectoryName);
                        int usage = (int)(dir[0] / 1024 / 1024);
                        if (usage > MaxUsage)
                        {
                            DataTable dtfile = FileOP.getDirectoryInfos(f.DirectoryName, FileOPMethod.File);
                            DataRow[] drs = dtfile.Select("", "createTime DESC");
                            string delFile = f.DirectoryName + "\\" + drs[0]["name"].ToString();
                            File.Delete(delFile);
                        }
                        else
                            break;
                    }
                }
                fs = new FileStream(file, FileMode.Create);
            }
            catch
            {
                return;
            }
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            DateTime dt = DateTime.Now;
            fs.Seek(0, SeekOrigin.End);
            if(newSection)
                sw.WriteLine("---------------------" + dt.ToString() + "-------------------");
            sw.WriteLine(strLog);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();/**/
        }
        public static void AddTextLog(string strLog)
        {
            DateTime dt = DateTime.Now;
            AddTextLog(strLog, System.Windows.Forms.Application.StartupPath + "\\TextLog\\" + String.Format("{0:D4}", dt.Year) + "-" + String.Format("{0:D2}", dt.Month) + "-" + String.Format("{0:D2}", dt.Day) + "运行信息.txt",false);
        }
        public static void AddTextLog(string strLog, bool newSection)
        {
            DateTime dt = DateTime.Now;
            AddTextLog(strLog, System.Windows.Forms.Application.StartupPath + "\\TextLog\\" + String.Format("{0:D4}", dt.Year) + "-" + String.Format("{0:D2}", dt.Month) + "-" + String.Format("{0:D2}", dt.Day) + "运行信息.txt", newSection);
        }
    }
}
