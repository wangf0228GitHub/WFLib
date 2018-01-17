using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.InteropServices;
using WFNetLib.PacketProc;

namespace WFNetLib.TCP
{
    /// <summary>
    /// 保存数据的回调函数
    /// </summary>
    /// <param name="s1"></param>
    /// <param name="s2"></param>
    /// <returns></returns>
    public delegate object SaveDataProcessCallbackDelegate(byte[] tempbuffer,ref byte[] buffer,ref int dataOffset, int length);
    
    public class TCPAsyncServer 
    {
        public static int bufferSize = 2048;
        public string TCPServerName = "";
        public int TCPServerPort = 8001;
        private Socket listener = null;
        private bool bRun=false;
        //private ManualResetEvent AcceptDone = new ManualResetEvent(false);
        //private ManualResetEvent sendDone = new ManualResetEvent(false);
//         private Mutex ClientListMutex = new Mutex();
//         private Hashtable ClientList = new Hashtable();
//         public IAsyncResult AcceptIAsyncResult;
//         public Exception LastException;

        //private Socket listenSocket;

        public int m_numConnections=160; //最大支持连接个数
        public int m_receiveBufferSize = 1024 * 4; //每个连接接收缓存大小
        private Semaphore m_maxNumberAcceptedClients; //限制访问接收连接的线程数，用来控制最大并发数

        private int m_socketTimeOutMS=6000; //Socket最大超时时间，单位为MS
        public int SocketTimeOutMS { get { return m_socketTimeOutMS; } set { m_socketTimeOutMS = value; } }

        private ClientContextPool m_ClientContextPool;
        private ClientContextList m_ClientContextList;
        public ClientContextList ClientContextList { get { return m_ClientContextList; } }

//         private LogOutputSocketProtocolMgr m_logOutputSocketProtocolMgr;
//         public LogOutputSocketProtocolMgr LogOutputSocketProtocolMgr { get { return m_logOutputSocketProtocolMgr; } }
// 
//         private UploadSocketProtocolMgr m_uploadSocketProtocolMgr;
//         public UploadSocketProtocolMgr UploadSocketProtocolMgr { get { return m_uploadSocketProtocolMgr; } }
// 
//         private DownloadSocketProtocolMgr m_downloadSocketProtocolMgr;
//         public DownloadSocketProtocolMgr DownloadSocketProtocolMgr { get { return m_downloadSocketProtocolMgr; } }

        private DaemonThread m_daemonThread;
        //回调函数
        public SaveDataProcessCallbackDelegate SaveDataProcessCallback=null;
        ///
        /// 停止服务器
        ///
        public void Stop()
        {
            bRun = false;
            m_daemonThread.Close();
            if (listener == null)
                return;
            listener.Close();

            ClientContext[] ClientList = null;
            m_ClientContextList.CopyList(ref ClientList);
            foreach (ClientContext client in ClientList)
            {
                CloseClientSocket(client);
            }
        }
        ///
        /// 开始监听访问
        ///
        public void Start()
        {
            bRun = true;
            m_ClientContextPool = new ClientContextPool(m_numConnections);
            m_ClientContextList = new ClientContextList();
            m_maxNumberAcceptedClients = new Semaphore(m_numConnections, m_numConnections);

            ClientContext client;
            for (int i = 0; i < m_numConnections; i++) //按照连接数建立读写对象
            {
                client = new ClientContext();
                client.ReceiveEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                client.SendEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(IO_Completed);
                m_ClientContextPool.Push(client);
            }
            try
            {
//                 if (SaveDataProcessCallback == null)
//                     throw (new Exception("没有定义数据的处理回调!"));
                if (listener != null)
                {
                    //listener.Dispose();
                    listener = null;
                }
                listener = new Socket(AddressFamily.InterNetwork,
                 SocketType.Stream, ProtocolType.Tcp);
//                 if (TCPServerName.Trim() == "")
//                 {
//                     //ServerName = IPAddress.Any;
//                     IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
//                     foreach (IPAddress ip in ipHostEntry.AddressList)
//                     {
//                         if (ip.IsIPv6LinkLocal)
//                             continue;
//                         if (ipHostEntry.AddressList.Length != 0)
//                         {
//                             TCPServerName = ip.ToString();
//                             break;
//                         }
//                     }
//                 }
                IPEndPoint localEndPoint;
                if (TCPServerName.Trim() == "")
                {
                    localEndPoint = new IPEndPoint(IPAddress.Any, TCPServerPort);
                }
                else
                    localEndPoint = new IPEndPoint(IPAddress.Parse(TCPServerName), TCPServerPort);
                listener.Bind(localEndPoint);
                listener.Listen(m_numConnections);
                StartAccept(null);
                m_daemonThread = new DaemonThread(this);
                //AcceptIAsyncResult=listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, null,TCPErrorType.Unkown));
                //LastException = e;
            }
        }
        public void StartAccept(SocketAsyncEventArgs acceptEventArgs)
        {
            if (!bRun)
                return;
            if (acceptEventArgs == null)
            {
                acceptEventArgs = new SocketAsyncEventArgs();
                acceptEventArgs.Completed += new EventHandler<SocketAsyncEventArgs>(AcceptEventArg_Completed);
            }
            else
            {
                acceptEventArgs.AcceptSocket = null; //释放上次绑定的Socket，等待下一个Socket连接
            }

            m_maxNumberAcceptedClients.WaitOne(); //获取信号量
            bool willRaiseEvent = listener.AcceptAsync(acceptEventArgs);
            if (!willRaiseEvent)
            {
                ProcessAccept(acceptEventArgs);
            }
        }

        void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs acceptEventArgs)
        {
            if (!bRun)
                return;
            while(true)
            {
                try
                {
                    ProcessAccept(acceptEventArgs);
                    break;
                }
                catch (Exception ex)
                {
                    //Exception ex = new Exception("acceptecompleted1:" + e.Message);                    
                    OnErrorServerEvent(new ErrorServerEventArgs(ex, null, TCPErrorType.Unkown, "acceptecompleted1:"));
                    
                    //                 Program.Logger.ErrorFormat("Accept client {0} error, message: {1}", acceptEventArgs.AcceptSocket, E.Message);
                    //                 Program.Logger.Error(E.StackTrace);
                }
                try
                {
                    StartAccept(null); //把当前异步事件释放，等待下次连接

                }
                catch (Exception e)
                {
                    //Exception ex = new Exception("acceptecompleted2:" + e.Message,e);
                    OnErrorServerEvent(new ErrorServerEventArgs(e, null, TCPErrorType.Unkown, "acceptecompleted2:"));
                }
            }
        }

        private void ProcessAccept(SocketAsyncEventArgs acceptEventArgs)
        {
//             Program.Logger.InfoFormat("Client connection accepted. Local Address: {0}, Remote Address: {1}",
//                 acceptEventArgs.AcceptSocket.LocalEndPoint, acceptEventArgs.AcceptSocket.RemoteEndPoint);
            ClientContext client = m_ClientContextPool.Pop(); 
            try
            {
                client.ClientSocket = acceptEventArgs.AcceptSocket;
                
            }
            catch (System.Exception ex)
            {
                CloseClientSocket(client);
                OnErrorServerEvent(new ErrorServerEventArgs(ex, client, TCPErrorType.Unkown, "acceptinit:"));
                StartAccept(acceptEventArgs); //把当前异步事件释放，等待下次连接
                return;
            }
            lock(client)
            {
                AcceptServerEventArgs ae = new AcceptServerEventArgs(client);                
                OnAcceptServerEvent(ae);
                if (ae.isCancel)
                {
                    CloseClientSocket(client);
                }
                else
                {
                    try
                    {
                        m_ClientContextList.Add(client); //添加到正在连接列表
                        bool willRaiseEvent = client.ClientSocket.ReceiveAsync(client.ReceiveEventArgs); //投递接收请求
                        if (!willRaiseEvent)
                        {
                            ProcessReceive(client.ReceiveEventArgs);
                        }
                    }
                    catch (Exception e)
                    {
                        CloseClientSocket(client);
                        //Exception ex = new Exception("accept:" + e.Message, e);
                        OnErrorServerEvent(new ErrorServerEventArgs(e, client, TCPErrorType.Unkown, "accept:"));
                        //                 Program.Logger.ErrorFormat("Accept client {0} error, message: {1}", userToken.ConnectSocket, E.Message);
                        //                 Program.Logger.Error(E.StackTrace);
                    }
                }
            }            
            StartAccept(acceptEventArgs); //把当前异步事件释放，等待下次连接
        }
        void IO_Completed(object sender, SocketAsyncEventArgs asyncEventArgs)
        {
            ClientContext client = asyncEventArgs.UserToken as ClientContext;
            client.ActiveDateTime = DateTime.Now;
            try
            {
                lock (client)
                {
                    if (asyncEventArgs.LastOperation == SocketAsyncOperation.Receive)
                        ProcessReceive(asyncEventArgs);
                    else if (asyncEventArgs.LastOperation == SocketAsyncOperation.Send)
                        ProcessSend(asyncEventArgs);
                    else
                        throw new ArgumentException("The last operation completed on the socket was not a receive or send");
                }
            }
            catch (Exception e)
            {
                //Exception ex = new Exception("IO_completed:" + e.Message, e);
                OnErrorServerEvent(new ErrorServerEventArgs(e, null, TCPErrorType.Unkown, "IO_completed:"));
//                 Program.Logger.ErrorFormat("IO_Completed {0} error, message: {1}", userToken.ConnectSocket, E.Message);
//                 Program.Logger.Error(E.StackTrace);
            }
        }
        private void ProcessReceive(SocketAsyncEventArgs receiveEventArgs)
        {
            ClientContext client = receiveEventArgs.UserToken as ClientContext;
            if (client.ClientSocket == null)
                return;
            if (client.SaveDataProcessCallback == null && SaveDataProcessCallback==null)
                throw (new Exception("没有定义数据的处理回调!"));
            
            client.ActiveDateTime = DateTime.Now;
            client.RxDateTime = DateTime.Now;
            if (client.ReceiveEventArgs.BytesTransferred > 0 && client.ReceiveEventArgs.SocketError == SocketError.Success)
            {
//                 int offset = client.ReceiveEventArgs.Offset;
//                 int count = client.ReceiveEventArgs.BytesTransferred;
//                 while(true)
//                 {
                object o = null;
                if (client.SaveDataProcessCallback != null)
                    o = client.SaveDataProcessCallback(client.ReceiveEventArgs.Buffer, ref client.netDataBuffer, ref client.netDataOffset, client.ReceiveEventArgs.BytesTransferred);
                else
                    o = SaveDataProcessCallback(client.ReceiveEventArgs.Buffer, ref client.netDataBuffer, ref client.netDataOffset, client.ReceiveEventArgs.BytesTransferred);
                if (o != null)
                {
                    OnReceiveServerEvent(new ReceiveServerEventArgs(o, client));
                }
//                     else
//                         break;
//                 }                    
                bool willRaiseEvent = client.ClientSocket.ReceiveAsync(client.ReceiveEventArgs); //投递接收请求
                if (!willRaiseEvent)
                    ProcessReceive(client.ReceiveEventArgs);
            }
            else
            {
                CloseClientSocket(client);
            }
        }
        private bool ProcessSend(SocketAsyncEventArgs sendEventArgs)
        {
            ClientContext client = sendEventArgs.UserToken as ClientContext;
            client.ActiveDateTime = DateTime.Now;
            if (sendEventArgs.SocketError == SocketError.Success)
            {
                lock (client)
                {
                    OnSendCompleteServerEvent(new SendCompleteEventArgs(client, client.txBytes));
                }
                return true;
            }
                //return client.AsyncSocketInvokeElement.SendCompleted(); //调用子类回调函数
            else
            {
                CloseClientSocket(client);
                return false;
            }
        }

//         public bool SendAsyncEvent(Socket connectSocket, SocketAsyncEventArgs sendEventArgs, byte[] buffer, int offset, int count)
//         {
//             if (connectSocket == null)
//                 return false;
//             OnBeforeSendPacketServerEvent(new BeforeSendPacketEventArgs(client, txBytes));
//             sendEventArgs.SetBuffer(buffer, offset, count);
//             bool willRaiseEvent = connectSocket.SendAsync(sendEventArgs);
//             if (!willRaiseEvent)
//             {
//                 return ProcessSend(sendEventArgs);
//             }
//             else
//                 return true;
//         }
        public void CloseClientSocket(ClientContext client)
        {
            if (client.ClientSocket == null)
                return;
//             string socketInfo = string.Format("Local Address: {0} Remote Address: {1}", client.ConnectSocket.LocalEndPoint,
//                 client.ConnectSocket.RemoteEndPoint);
//             Program.Logger.InfoFormat("Client connection disconnected. {0}", socketInfo);
            try
            {
                OnDisconnectServerEvent(new DisconnectEventArgs(client));
                m_maxNumberAcceptedClients.Release();
                m_ClientContextPool.Push(client);
                m_ClientContextList.Remove(client);
                if(client.ClientSocket.Connected)
                {
                    client.ClientSocket.Shutdown(SocketShutdown.Both);                    
                    client.ClientSocket.Close();
                }
                client.ClientSocket = null; //释放引用，并清理缓存，包括释放协议对象等资源
            }
            catch (Exception e)
            {
                //Exception ex = new Exception("close:" + e.Message, e);
                OnErrorServerEvent(new ErrorServerEventArgs(e, client,TCPErrorType.Unkown, "close:"));  
                //Program.Logger.ErrorFormat("CloseClientSocket Disconnect client {0} error, message: {1}", socketInfo, E.Message);
            }            
        }
        ///
        /// 发送一个流数据
        ///
        public bool Send(ClientContext client, byte[] txBytes)
        {
            return Send(client, txBytes, 0, txBytes.Length);
        }
        ///
        /// 发送一个流数据
        ///
        public bool Send(ClientContext client, byte[] txBytes,int offset, int count)
        {
            try
            {
                //lock (client)
                {
                    if (client == null)
                        return false;
                    if (client.ClientSocket == null)
                        return false;
                    client.txBytes = new byte[count];
                    Array.Copy(txBytes, offset, client.txBytes, 0, count); //复制数据
                    BeforeSendPacketEventArgs beforeSendPacketEventArgs = new BeforeSendPacketEventArgs(client, client.txBytes);
                    OnBeforeSendPacketServerEvent(beforeSendPacketEventArgs);
                    if (beforeSendPacketEventArgs.isCancel)
                        return false;

                    client.SendEventArgs.SetBuffer(client.txBytes, 0, count);
                    bool willRaiseEvent = client.ClientSocket.SendAsync(client.SendEventArgs);
                    if (!willRaiseEvent)
                    {
                        return ProcessSend(client.SendEventArgs);
                    }
                    else
                        return true;
                }
            }
            catch (Exception e)
            {
                //Exception ex = new Exception("send:" + e.Message, e);
                OnErrorServerEvent(new ErrorServerEventArgs(e, client, TCPErrorType.Unkown, "send:"));
                //LastException = e;
            }
            return false;
        }
        
        public void SendToAll(MemoryStream mStream)
        {
            SendToAll(mStream.GetBuffer());
        }
        public void SendToAll(byte[] txBytes)
        {
            SendToAll(txBytes, 0, txBytes.Length);
        }
        public void SendToAll(byte[] txBytes, int offset, int count)
        {
            try
            {
                ClientContext[] ClientList=null;
                m_ClientContextList.CopyList(ref ClientList);
                foreach (ClientContext client in ClientList)
                {                    
                    Send(client, txBytes,offset,count);
                }
            }
            catch (Exception e)
            {
                OnErrorServerEvent(new ErrorServerEventArgs(e, null, TCPErrorType.Unkown));
            }
        }
        ///
        /// 引发接收事件
        ///
        /// 数据
        protected virtual void OnReceiveServerEvent(ReceiveServerEventArgs e)
        {
            if (ReceiveServerEvent != null)
            {
                ReceiveServerEvent(this, e);
            }
        }
        ///
        /// 引发错误事件
        ///
        /// 数据
        protected virtual void OnErrorServerEvent(ErrorServerEventArgs e)
        {
            if (ErrorServerEvent != null)
            {
                ErrorServerEvent(this, e);
            }
        }
        ///
        /// 引发接入事件
        ///
        /// 数据
        protected virtual void OnAcceptServerEvent(AcceptServerEventArgs e)
        {
            if (AcceptServerEvent != null)
            {
                AcceptServerEvent(this, e);
            }
        }
        ///
        /// 引发发送前事件
        ///
        /// 数据
        protected virtual void OnBeforeSendPacketServerEvent(BeforeSendPacketEventArgs e)
        {
            if (BeforeSendPacketServerEvent != null)
            {
                BeforeSendPacketServerEvent(this, e);
            }
        }
        ///
        /// 引发发送完成事件
        ///
        /// 数据
        protected virtual void OnSendCompleteServerEvent(SendCompleteEventArgs e)
        {
            if (SendCompleteServerEvent != null)
            {
                SendCompleteServerEvent(this, e);
            }
        }
        ///
        /// 引发离线事件
        ///
        /// 数据
        protected virtual void OnDisconnectServerEvent(DisconnectEventArgs e)
        {
            if (DisconnectServerEvent != null)
            {
                DisconnectServerEvent(this, e);
            }
        }
        ///
        /// 接收到数据事件
        ///
        public event TCPReceiveEvent ReceiveServerEvent;
        ///
        /// 发生错误事件
        ///
        public event TCPErrorEvent ErrorServerEvent;
        ///
        /// 发生接入事件
        ///
        public event TCPAcceptEvent AcceptServerEvent;
        ///
        /// 发生发送前事件
        ///
        public event TCPBeforeSendPacketEvent BeforeSendPacketServerEvent;
        ///
        /// 发生发送完成事件
        ///
        public event TCPSendCompleteEvent SendCompleteServerEvent;
        ///
        /// 发生离线事件
        ///
        public event TCPDisconnectEvent DisconnectServerEvent;
    }
}
