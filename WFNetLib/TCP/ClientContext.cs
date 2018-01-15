using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using WFNetLib.PacketProc;
using System.Collections;

namespace WFNetLib.TCP
{
    public class ClientContextPool
    {
        private Queue<ClientContext> m_pool;

        public ClientContextPool(int capacity)
        {
            m_pool = new Queue<ClientContext>(capacity);
        }

        public void Push(ClientContext item)
        {
            if (item == null)
            {
                throw new ArgumentException("Items added to a AsyncSocketUserToken cannot be null");
            }
            lock (m_pool)
            {
                m_pool.Enqueue(item);
            }
        }

        public ClientContext Pop()
        {
            lock (m_pool)
            {
                return m_pool.Dequeue();
            }
        }

        public int Count
        {
            get { return m_pool.Count; }
        }
    }

    public class ClientContextList : Object
    {
        private List<ClientContext> m_list;

        public ClientContextList()
        {
            m_list = new List<ClientContext>();
        }

        public void Add(ClientContext client)
        {
            lock (m_list)
            {
                m_list.Add(client);
            }
        }
        public void Remove(ClientContext client)
        {
            lock (m_list)
            {
                m_list.Remove(client);
            }
        }

        public void CopyList(ref ClientContext[] array)
        {
            lock (m_list)
            {
                array = new ClientContext[m_list.Count];
                m_list.CopyTo(array);
            }
        }
    }
    public class ClientContext :Object
    {
        protected SocketAsyncEventArgs m_receiveEventArgs;
        public SocketAsyncEventArgs ReceiveEventArgs { get { return m_receiveEventArgs; } set { m_receiveEventArgs = value; } }
        protected byte[] m_asyncReceiveBuffer;
        protected SocketAsyncEventArgs m_sendEventArgs;
        public SocketAsyncEventArgs SendEventArgs { get { return m_sendEventArgs; } set { m_sendEventArgs = value; } }


        protected Socket m_ClientSocket;
        public Socket ClientSocket
        {
            get
            {
                return m_ClientSocket;
            }
            set
            {
                m_ClientSocket = value;
                if (m_ClientSocket != null) //清理缓存
                {
                    tempbuffer = new byte[BUFFER_SIZE];
                    netStream = new NetworkStream(ClientSocket);
                    clientEndPoint = (IPEndPoint)m_ClientSocket.RemoteEndPoint;
                    key = ClientSocket.RemoteEndPoint.ToString();
                    ConnectDateTime = DateTime.Now;
                    ActiveDateTime = DateTime.Now;
                    RxDateTime = DateTime.Now;
                    TxDateTime = DateTime.Now;
                    m_receiveEventArgs.AcceptSocket = m_ClientSocket;
                    m_sendEventArgs.AcceptSocket = m_ClientSocket;
                }               
            }
        }

        protected DateTime m_ConnectDateTime;
        public DateTime ConnectDateTime { get { return m_ConnectDateTime; } set { m_ConnectDateTime = value; } }
        protected DateTime m_ActiveDateTime;
        public DateTime ActiveDateTime { get { return m_ActiveDateTime; } set { m_ActiveDateTime = value; } }

        protected DateTime m_RxDateTime;
        public DateTime RxDateTime { get { return m_RxDateTime; } set { m_RxDateTime = value; } }
        protected DateTime m_TxDateTime;
        public DateTime TxDateTime { get { return m_TxDateTime; } set { m_TxDateTime = value; } }

        public SaveDataProcessCallbackDelegate SaveDataProcessCallback = null;



        public delegate void TimerOutCallbackDelegate(ClientContext client);
        public static TimerOutCallbackDelegate SendOutCallback = null;
        public static TimerOutCallbackDelegate ReciveOutCallback = null;
        /// <summary>
        /// 缓冲区大小1024字节
        /// </summary>
        public static Int32 BUFFER_SIZE = 1024;
        ///
        /// 缓存
        ///
        public byte[] tempbuffer=null;

        public byte[] txBytes;
        /// <summary>
        /// 接收网络数据的缓冲区
        /// </summary>
        public Byte[] netDataBuffer = new Byte[BUFFER_SIZE * 64];
        ///
        /// 网络流
        /// 
        public NetworkStream netStream = null;
        
        ///
        /// 数据包大小
        ///
        //public long packSize = 0;
        ///
        /// 计数器
        ///
        public int netDataOffset = 0;
        ///
        /// 客户端IP 地址和端口
        /// 
        public IPEndPoint clientEndPoint;

        public string key;
        public object DataPacket;
        public ClientContext(Socket client)
        {
            m_ClientSocket = client;
            tempbuffer = new byte[BUFFER_SIZE];
            netStream = new NetworkStream(ClientSocket);          
            clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
            ConnectDateTime = DateTime.Now;
            ActiveDateTime = DateTime.Now;
            RxDateTime = DateTime.Now;
            TxDateTime = DateTime.Now;
            DataPacket = null;
            m_receiveEventArgs = new SocketAsyncEventArgs();
            m_receiveEventArgs.UserToken = this;
            m_sendEventArgs = new SocketAsyncEventArgs();
            m_sendEventArgs.UserToken = this;
        }
        public ClientContext()
        {
            m_ClientSocket = null;
            tempbuffer = new byte[BUFFER_SIZE];
            //netStream = new NetworkStream(ClientSocket);
            ConnectDateTime = DateTime.Now;
            ActiveDateTime = DateTime.Now;
            RxDateTime = DateTime.Now;
            TxDateTime = DateTime.Now;
            DataPacket = null;
            m_receiveEventArgs = new SocketAsyncEventArgs();
            m_receiveEventArgs.SetBuffer(tempbuffer,0,tempbuffer.Length);
            m_receiveEventArgs.UserToken = this;
            m_sendEventArgs = new SocketAsyncEventArgs();
            m_sendEventArgs.UserToken = this;
            m_sendEventArgs.SetBuffer(tempbuffer, 0, tempbuffer.Length);
        }    
    }
    public class ClientAndPacket
    {
        public ClientContext Client;
        public byte[] txBytes;
        public ClientAndPacket(ClientContext client, byte[] txBytes)
        {
            Client = client;
            this.txBytes=txBytes;
        }
    }
}
