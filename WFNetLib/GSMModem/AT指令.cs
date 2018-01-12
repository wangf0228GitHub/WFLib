using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

namespace WFNetLib.GSMModem
{
    public class ATCommand
    {
        public ATCommand(SerialPort _com)
        {
            Com = _com;
            Com.DataReceived += COM_DataReceived;
        }
        void COM_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            char[] MyChar = { '\r', '\n' };
            string atres = Com.ReadExisting();
            OnATCommandLog(atres, (int)ATCommandLogType.RxEx);
            if (atres.IndexOf("RING") != -1)
            {
                //Thread.Sleep(2000);
                //Com.Write("ATA\r");
                //Thread.Sleep(50);
                Com.Write("ATH\r");
            }
        } 
        SerialPort Com;
        private string atResult;
        //bool bCLIP = true;//是否有来电显示
        public event intstrEventHandler ATCommandLogEvent;//事件声明
        public string ATResult//at指令的返回值
        {
            get
            {
                string atres = atResult;
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
        public bool SendAT(string at, string strEnd, string strErr, bool bCanError, int Retry)
        {
            Com.DataReceived -= COM_DataReceived;
            if (Com.BytesToRead != 0)
            {
                Debug.WriteLine(Com.PortName+"放弃接收数据:" + Com.ReadExisting());
                OnATCommandLog(Com.ReadExisting(),(int)ATCommandLogType.Discard);
                Com.DiscardInBuffer();
            }
            while (Retry != 0)
            {
                if (Retry != -1)
                    Retry--;
                try
                {
                    string str;
                    Com.Write(at + "\r");
                    Debug.WriteLine(Com.PortName + "发送:" + at);
                    ATResult = "";
                    OnATCommandLog(at, (int)ATCommandLogType.Tx);
                    while (true)
                    {
                        str = Com.ReadTo("\r\n");                        
                        if (str.IndexOf(strErr) != -1)
                        {
                            OnATCommandLog(at, (int)ATCommandLogType.Error);
                            ATResult += "\r\n";
                            ATResult += str;
                            Debug.WriteLine(Com.PortName + "接收到:" + ATResult);
                            if (!bCanError)
                            {
                                int ComTimeOut = Com.ReadTimeout;
                                Com.ReadTimeout = 1000;
                                while (true)
                                {
                                    try
                                    {
                                        Com.ReadTo("\r\n");
                                    }
                                    catch
                                    {
                                        Com.ReadTimeout = ComTimeOut;
                                        break;
                                    };
                                }
                                break;
                            }                            
                            return false;
                        }
                        if (str.IndexOf(strEnd)!=-1)
                        {
                            OnATCommandLog(ATResult, (int)ATCommandLogType.Rx);
                            Debug.WriteLine(Com.PortName + "接收到:" + ATResult+"\r\n"+str);
                            return true;
                        }
                        else
                        {
                            if (str != "")
                            {
                                ATResult += "\r\n";
                                ATResult += str;
                            }
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    throw ex;
                    //Debug.WriteLine(Com.PortName + ":" + ex.Message);
                    //return false;
                }
                catch// (Exception ex)
                {
                    //throw ex;
                    //OnATCommandLog("错误:" + str);
                    
                }
                finally
                {
                    //事件重新绑定 正常监视串口数据
                    Com.DataReceived += COM_DataReceived;
                }
            }
            return false;
        }
        public bool SendAT(string at, string strEnd, bool bCanError)
        {
            return SendAT(at, strEnd, "ERROR",bCanError,3);
        }
        public bool SendAT(string at, string strEnd)
        {
            return SendAT(at, strEnd, false);
        }
        public bool SendAT(string at, string strErr,int Retry)
        {
            return SendAT(at, "OK", strErr,false,Retry);
        }
        public bool SendAT(string at, string strEnd,string strErr, int Retry)
        {
            return SendAT(at, strEnd, strErr, false, Retry);
        }
        public bool SendAT(string at, bool bCanError)
        {
            return SendAT(at, "OK", bCanError);
        }
        public bool SendAT(string at, int Retry)
        {
            return SendAT(at, "OK", "ERROR", false, Retry);
        }
        public bool SendAT(string at)
        {
            return SendAT(at, "OK");
        }
        void OnATCommandLog(string strLog,int type)
        {
            if (ATCommandLogEvent != null)
            {
                ATCommandLogEvent(this, new intstrEventArgs(type,strLog));
            }
        }
    }
    public enum ATCommandLogType
    {
        Rx=1,
        Tx=2,
        Error=3,
        Discard=4,
        TCPTimeOut=5,
        RxEx //在接收中断中接收的数据，一般为来电
    }
}
