using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib.PacketProc
{
    public class easyStringPacket
    {
        public static string strHeader = "@";
        public static string strEnder = "$";
        public static byte[] MakePacket(string str)
        {            
            string ret = strHeader + str + strEnder;            
            return Encoding.ASCII.GetBytes(ret);
        }        
        public static object SaveDataProcessCallbackProc(byte[] tempbuffer, ref byte[] buffer, ref int dataOffset, int length)
        {          
            //将新读取的数据拷贝到缓冲区
            Array.Copy(tempbuffer, 0, buffer, dataOffset, length);
            //修改"数据实际长度"
            dataOffset += length;
            string rx = Encoding.ASCII.GetString(buffer);
            int endIndex = rx.IndexOf(strEnder);
            if (endIndex == -1)
                return null;           
            int startIndex = rx.IndexOf(strHeader);
            //获得了完整的数据包
            string packet = rx.Substring(startIndex+strHeader.Length,endIndex-startIndex-strEnder.Length);
            Array.Copy(buffer, endIndex+1, buffer, 0, dataOffset - (endIndex-startIndex+1));
            dataOffset -= (endIndex - startIndex+1);//缓冲区实际数据长度减去一个完整封包长度
            return packet;
        }        
    }
}
