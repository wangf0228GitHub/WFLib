using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Net.Sockets;
using WFNetLib.PacketProc;

namespace WFNetLib.TCP
{
    ///
    /// 接收事件
    ///
    public class ReceiveServerEventArgs : EventArgs
    {
        private readonly object np;
        private readonly ClientContext client;
        ///
        /// 构造
        ///
        /// 数据
        /// 工作套接字
        /// 提供服务的TCP/IP对象
        public ReceiveServerEventArgs(object _np, ClientContext client)
        {
            np = _np;
            this.client = client;
        }
        public ReceiveServerEventArgs(object _np)
        {
            np = _np;
            this.client = null;
        }
        ///
        /// 数据
        ///
        public object netPacket
        {
            get { return np; }
        }
        ///
        /// 工作套接字
        ///
        public ClientContext Client
        {
            get { return client; }
        }
    }
    ///
    /// 接收数据委托
    ///
    public delegate void TCPReceiveEvent(object sender, ReceiveServerEventArgs e);

    /// <summary>
    /// 错误类型
    /// </summary>
    public enum TCPErrorType
    {
        /// <summary>
        /// 无法连接
        /// </summary>
        CannotConnect = 0,
        /// <summary>
        /// 连接中断
        /// </summary>
        ConnectBreak = 1,
        /// <summary>
        /// 网络尚未连接
        /// </summary>
        NoConnect = 2,
        /// <summary>
        /// 发送中断
        /// </summary>
        SendBreak=3,
        /// <summary>
        /// 未知
        /// </summary>
        Unkown
    }

    ///
    /// 错误事件委托
    ///
    public class ErrorServerEventArgs : EventArgs
    {
        private readonly ClientContext client;
        private readonly Exception error;
        private readonly TCPErrorType errorType;
        private readonly string message;
        ///
        /// 构造
        ///
        /// 数据
        /// 问题套接字
        /// 
        public ErrorServerEventArgs(Exception Error, ClientContext client,TCPErrorType _errorType,string _message)
        {
            this.client = client;
            error = Error;
            errorType=_errorType;
            message = _message;
        }
        public ErrorServerEventArgs(Exception Error, ClientContext client,TCPErrorType _errorType)
        {
            this.client = client;
            error = Error;
            errorType=_errorType;
        }
        public ErrorServerEventArgs(Exception Error,TCPErrorType _errorType)
        {
            this.client = null;
            error = Error;
            errorType=_errorType;
        }
        ///
        /// 数据
        ///
        public Exception Error
        {
            get { return error; }
        }
        ///
        /// 套接字
        ///
        public ClientContext Client
        {
            get { return client; }
        }
        public TCPErrorType ErrorType
        {
            get{return errorType;}
        }
        public string Message
        {
            get { return message; }
        }
    }
    ///
    ///错误事件委托
    ///
    public delegate void TCPErrorEvent(object sender, ErrorServerEventArgs e);
    ///
    /// 接入事件委托
    ///
    public class AcceptServerEventArgs : EventArgs
    {
        private readonly ClientContext client;
        public bool isCancel = false;
        ///
        /// 构造
        ///
        /// 套接字
        public AcceptServerEventArgs(ClientContext client)
        {
            this.client = client;            
        }
        public AcceptServerEventArgs()
        {
            this.client = null;
        }
        ///
        /// 套接字
        ///
        public ClientContext Client
        {
            get { return client; }
        }        
    }
    ///
    ///接入事件委托
    ///
    public delegate void TCPAcceptEvent(object sender, AcceptServerEventArgs e);
    
    ///
    /// 接入事件委托
    ///
    public class DisconnectEventArgs : EventArgs
    {
        private readonly ClientContext client;
        ///
        /// 构造
        ///
        /// 套接字
        public DisconnectEventArgs(ClientContext client)
        {
            this.client = client;
        }
        public DisconnectEventArgs()
        {
            this.client = null;
        }
        ///
        /// 套接字
        ///
        public ClientContext Client
        {
            get { return client; }
        }
    }
    ///
    ///接入事件委托
    ///
    public delegate void TCPDisconnectEvent(object sender, DisconnectEventArgs e);

    ///
    /// 发送前事件委托
    ///
    public class BeforeSendPacketEventArgs : EventArgs
    {
        private readonly ClientContext client;
        public byte[] txBytes;
        public bool isCancel=false;
        ///
        /// 构造
        ///
        /// 套接字
        public BeforeSendPacketEventArgs(ClientContext client, byte[] txBytes)
        {
            this.client = client;
            this.txBytes = txBytes;
        }
        public BeforeSendPacketEventArgs(byte[] txBytes)
        {
            this.client = null;
            this.txBytes = txBytes;
        }
        ///
        /// 套接字
        ///
        public ClientContext Client
        {
            get { return client; }
        }
    }
    ///
    ///发送前事件委托
    ///
    public delegate void TCPBeforeSendPacketEvent(object sender, BeforeSendPacketEventArgs e);

    ///
    /// 发送完成事件委托
    ///
    public class SendCompleteEventArgs : EventArgs
    {
        private readonly ClientContext client;
        private readonly byte[] _txBytes;
        ///
        /// 构造
        ///
        /// 套接字
        public SendCompleteEventArgs(ClientContext client)
        {
            this.client = client;
            this._txBytes = null;
        }
        ///
        /// 构造
        ///
        /// 套接字
        public SendCompleteEventArgs(ClientContext client,byte[] txBytes)
        {
            this.client = client;
            this._txBytes = txBytes;
        }
        ///
        /// 套接字
        ///
        public ClientContext Client
        {
            get { return client; }
        }
        public byte[] txBytes
        {
            get { return _txBytes; }
        }      
    }
    ///
    ///发送完成事件委托
    ///
    public delegate void TCPSendCompleteEvent(object sender, SendCompleteEventArgs e);

    /////
    ///// 发送超时事件委托
    /////
    //public class SendOutEventArgs : EventArgs
    //{
    //    private readonly ClientContext client;       
    //    ///
    //    /// 构造
    //    ///
    //    /// 套接字
    //    public SendOutEventArgs(ClientContext client)
    //    {
    //        this.client = client;
    //    }
    //    ///
    //    /// 构造
    //    ///
    //    /// 套接字
    //    public SendOutEventArgs()
    //    {
            
    //    }       
    //    ///
    //    /// 套接字
    //    ///
    //    public ClientContext Client
    //    {
    //        get { return client; }
    //    }        
    //}
    /////
    /////发送超时事件委托
    /////
    //public delegate void TCPSendOutEvent(object sender, SendOutEventArgs e);
}
