using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib
{
#region 只有一个字符串参数
    public class strEventArgs : EventArgs
    {
        public string strArg="";
        public strEventArgs(string atcomm)
        {
            this.strArg = atcomm;
        }
        public strEventArgs()
        {

        }
    }
    public delegate void strEventHandler(object sender, strEventArgs e);
#endregion
#region 只有一个整数参数
    public class intEventArgs : EventArgs
    {
        public int nArg=0;
        public intEventArgs(int atcomm)
        {
            this.nArg = atcomm;
        }
        public intEventArgs()
        {

        }
    }
    public delegate void intEventHandler(object sender, intEventArgs e);
#endregion

#region 一个字符串参数,一个整数参数
    public class intstrEventArgs : EventArgs
    {
        public int nArg=0;
        public string strArg="";
        public intstrEventArgs(int atcomm,string str)
        {
            this.nArg = atcomm;
            this.strArg = str;
        }
        public intstrEventArgs(int atcomm)
        {
            this.nArg = atcomm;
            strArg = "";
        }
        public intstrEventArgs()
        {

        }
    }
    public delegate void intstrEventHandler(object sender, intstrEventArgs e);
#endregion
}
