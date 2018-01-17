using System;
using System.Collections.Generic;

using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Security.Cryptography;
using WFNetLib.Net;
using WFNetLib.Forms;

namespace WFNetLib.GSMModem
{
    public partial class GsmModem
    {
        SerialPort Com;
        //public delegate List<byte> GetSendListFunc(int status);
        private pppPocket pppLCP;
        private pppPocket pppPAP;
        private pppPocket pppIPCP;
        public event strEventHandler AddLog;
        //public bool autoDelMsg = false;//设置是否在阅读短信后自动删除 SIM 卡内短信存档        
        private string atResult;
        public event strEventHandler SpecialATReceived;//接到电话事件
        public event TCPDataEvent TCPDataProc;
        public bool bDataTransfer = false;
        public bool b7E;
        public List<byte> AutoRxList;
        private bool bLCP = false;
        public byte[] ourIP;
        public byte[] dIP;//目标IP
        public ushort ourPort;
        public byte[] DNS1;
        public byte[] DNS2;
        public ushort dPort;
        public uint TCPSeqNum;
        public ushort IPID;
        public byte[] theirIP;
        public string ATResult//at指令的返回值
        {
            get
            {
                string atres=atResult;
                char[] MyChar = { '\r', '\n' };
                atres = atres.TrimStart(MyChar);
                atres = atres.TrimEnd(MyChar);
                return atres;
            }
            set
            {
                atResult = value;
            }
        }
        public GsmModem(SerialPort _Com)
        {        
            AutoRxList = new List<byte>();
            Com = _Com;
            Com.ReadTimeout = 10000;
            Com.RtsEnable = true;
            Com.ReceivedBytesThreshold = 1;
            //Com.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(COM_DataReceived);
            pppLCP = new pppPocket();
            pppLCP.Protocol = 0xc021;
            pppLCP.Code = 0x01;
            pppLCP.AddCommand(0x02, 0x00, 0x00, 0x00, 0x00);
            Random x = new Random();
            Union_UInt32 x32 = new Union_UInt32();
            x32.ofs_32 = (uint)x.Next();
            pppLCP.AddCommand(0x05, x32.ofs_h.ofs_h, x32.ofs_h.ofs_l, x32.ofs_l.ofs_h, x32.ofs_l.ofs_l);
            //pppLCP.AddCommand(0x07);
            //pppLCP.AddCommand(0x08);

            pppPAP = new pppPocket();
            pppPAP.Protocol = 0xc023;
            pppPAP.Code = 0x01;
            byte[] Login = new byte[10] { 0x04, 0x6E, 0x6F, 0x6E, 0x65, 0x04, 0x6E, 0x6F, 0x6E, 0x65 };
            pppPAP.Datas.AddRange(Login);

            pppIPCP = new pppPocket();
            pppIPCP.Protocol = 0x8021;
            pppIPCP.Code = 0x01;
            pppIPCP.AddCommand(0x03, 0x00, 0x00, 0x00, 0x00);
            dIP = new byte[4];
            ourPort = (ushort)x.Next(4097, 4106);
            TCPSeqNum = (uint)x.Next(1000, 5000);
            IPID = 0;//(ushort)x.Next(100, 500);
            pppIPCP.AddCommand(0x81, 0x00, 0x00, 0x00, 0x00);
            pppIPCP.AddCommand(0x83, 0x00, 0x00, 0x00, 0x00);
            DNS1 = new byte[4];
            DNS2 = new byte[4];
            theirIP = new byte[4];
        }
        public void RemoveRecivedProc()
        {
            Com.DataReceived -= COM_DataReceived; //注销事件关联，为发送做准备
        }
        void OnSpecialATReceived(strEventArgs e)
        {
            if (SpecialATReceived != null)
            {
                string atres = e.strArg;
                char[] MyChar = { '\r', '\n' };
                atres = atres.TrimStart(MyChar);
                atres = atres.TrimEnd(MyChar);
                e.strArg = atres;
                SpecialATReceived(this,e);
            }
        }
        void OnAddLog(string str)
        {
            if (AddLog != null)
                AddLog(this, new strEventArgs(str));
        }
        public bool SendMsg(string phone, string msg)
        {
            return SendMsg("", phone, msg);
        }
        public bool SendMsg(string msgCenter,string phone, string msg)
        {
            PDUEncoding pe = new PDUEncoding();
            pe.ServiceCenterAddress = msgCenter;                    //短信中心号码 服务中心地址

            string tmp = string.Empty;
            foreach (CodedMessage cm in pe.PDUEncoder(phone, msg))
            {
                try
                {
                    //注销事件关联，为发送做准备
                    //Com.DataReceived -= sp_DataReceived;

                    Com.Write("AT+CMGS=" + cm.Length.ToString() + "\r");
                    Com.ReadTo(">");
                    Com.DiscardInBuffer();

                    //事件重新绑定 正常监视串口数据
                    //Com.DataReceived += sp_DataReceived;
//                     if (!SendAT(cm.PduCode + (char)(26)))//26 Ctrl+Z ascii码
//                     {
//                         return false;
//                     }
                }
                catch
                {
                    return false;
                } 
            }
            return true;
        }
        public static string MMCode(string phone, string asp_token, string time, string goodsid, string payMode, string pwd)
        {            
            MD5 m = MD5.Create();
            string a = WFNetLib.Strings.CryptoService.MD5Encrypt.GetMD5("0" + phone);
            string b = WFNetLib.Strings.CryptoService.MD5Encrypt.GetMD5(asp_token + time);
            string c = WFNetLib.Strings.CryptoService.MD5Encrypt.GetMD5(b + a + goodsid + payMode + pwd);
            return c;
        }       
    }
    public enum APN
    {
        CMWAP,
        CMNET
    }
}
