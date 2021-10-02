using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace WFNetLib
{
    ///
    ///错误事件委托
    ///
    public delegate void UDPErrorEvent(object sender, UDPErrorEventArgs e);
    ///
    /// 接收数据委托
    ///
    public delegate void UDPReceiveEvent(object sender, UDPReceiveEventArgs e);
    public class UDPService
    {
        /// <summary>
        /// 用于UDP发送的网络服务类
        /// </summary>
        UdpClient udpcRecv = null;

        IPEndPoint localIpep = null;

        /// <summary>
        /// 开关：在监听UDP报文阶段为true，否则为false
        /// </summary>
        bool IsUdpcRecvStart = false;
        /// <summary>
        /// 线程：不断监听UDP报文
        /// </summary>
        Thread thrRecv;

        public void StartReceive(IPAddress ip,int port)
        {
            if (!IsUdpcRecvStart) // 未监听的情况，开始监听
            {
                localIpep = new IPEndPoint(ip,port); // 本机IP和监听端口号
                udpcRecv = new UdpClient(localIpep);
                thrRecv = new Thread(ReceiveMessage);
                thrRecv.Start();
                IsUdpcRecvStart = true;
            }
        }

        public void StopReceive()
        {
            if (IsUdpcRecvStart)
            {
                thrRecv.Abort(); // 必须先关闭这个线程，否则会异常
                udpcRecv.Close();
                IsUdpcRecvStart = false;                
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="obj"></param>
        private void ReceiveMessage(object obj)
        {
            while (IsUdpcRecvStart)
            {
                try
                {
                    
                    byte[] bytRecv = udpcRecv.Receive(ref localIpep);
                    OnReceiveServerEvent(new UDPReceiveEventArgs(bytRecv, localIpep));
                    //                     string message = Encoding.UTF8.GetString(bytRecv, 0, bytRecv.Length);
                    //                     Console.WriteLine(string.Format("{0}[{1}]", localIpep, message));

                }
                catch (Exception ex)
                {
                    OnErrorServerEvent(new UDPErrorEventArgs(ex, localIpep));
                    //break;
                }
            }
        }
        public void UDPSendMessage(IPAddress ip, int port,byte[] txList)
        {
            try
            {
                IPEndPoint rIP = new IPEndPoint(ip, port);
                UdpClient udpcSend = new UdpClient();
                udpcSend.Send(txList, txList.Length, rIP);
            }
            catch(Exception ex)
            {
                string str = StringFunc.StringsFunction.byteToHexStr(txList, " ");
                OnErrorServerEvent(new UDPErrorEventArgs(ex, localIpep,"tx:"+str));
            }

        }
        ///
        /// 发生错误事件
        ///
        public event UDPErrorEvent UDPErrorEventProc;
        ///
        /// 引发错误事件
        ///
        /// 数据
        protected virtual void OnErrorServerEvent(UDPErrorEventArgs e)
        {
            if (UDPErrorEventProc != null)
            {
                UDPErrorEventProc(this, e);
            }
        }        
        ///
        /// 接收到数据事件
        ///
        public event UDPReceiveEvent UDPReceiveEventProc;
        ///
        /// 引发接收事件
        ///
        /// 数据
        protected virtual void OnReceiveServerEvent(UDPReceiveEventArgs e)
        {
            if (UDPReceiveEventProc != null)
            {
                UDPReceiveEventProc(this, e);
            }
        }
        
    }
    ///
    /// 接收事件
    ///
    public class UDPReceiveEventArgs : EventArgs
    {
        private readonly byte[] rxList;
        private readonly IPEndPoint localIpep;
        ///
        /// 构造
        ///
        /// 数据
        /// 工作套接字
        /// 提供服务的TCP/IP对象
        public UDPReceiveEventArgs(byte[] _rxList, IPEndPoint _localIpep)
        {
            rxList = _rxList;
            localIpep = _localIpep;
        }
        ///
        /// 数据
        ///
        public byte[] RxList
        {
            get { return rxList; }
        }
        ///
        /// 工作套接字
        ///
        public IPEndPoint LocalIP
        {
            get { return localIpep; }
        }
    }
    ///
    /// 错误事件委托
    ///
    public class UDPErrorEventArgs : EventArgs
    {
        private readonly Exception error;
        private readonly IPEndPoint localIpep;
        private readonly string message;
        ///
        /// 构造
        ///
        /// 数据
        /// 问题套接字
        /// 
        public UDPErrorEventArgs(Exception _error, IPEndPoint _localIpep, string _message)
        {
            error = _error;
            localIpep = _localIpep;
            message = _message;
        }
        public UDPErrorEventArgs(Exception _error)
        {
            error = _error;
        }
        public UDPErrorEventArgs(Exception _error, IPEndPoint _localIpep)
        {
            error = _error;
            localIpep = _localIpep;
        }
        ///
        /// 数据
        ///
        public Exception Error
        {
            get { return error; }
        }

        public IPEndPoint LocalIP
        {
            get { return localIpep; }
        }
        public string Message
        {
            get { return message; }
        }
    }
}
