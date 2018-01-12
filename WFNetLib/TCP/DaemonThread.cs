using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WFNetLib.TCP
{
    class DaemonThread : Object
    {
        private Thread m_thread;
        private TCPAsyncServer m_asyncSocketServer;

        public DaemonThread(TCPAsyncServer asyncSocketServer)
        {
            m_asyncSocketServer = asyncSocketServer;
            m_thread = new Thread(DaemonThreadStart);
            m_thread.Start();
        }

        public void DaemonThreadStart()
        {
            while (m_thread.IsAlive)
            {
                ClientContext[] clientList = null;
                m_asyncSocketServer.ClientContextList.CopyList(ref clientList);
                for (int i = 0; i < clientList.Length; i++)
                {
                    if (!m_thread.IsAlive)
                        break;
                    try
                    {
                        if ((DateTime.Now - clientList[i].ActiveDateTime).Milliseconds > m_asyncSocketServer.SocketTimeOutMS) //超时Socket断开
                        {
                            lock (clientList[i])
                            {
                                m_asyncSocketServer.CloseClientSocket(clientList[i]);
                            }
                        }
                    }                    
                    catch// (Exception E)
                    {
//                         Program.Logger.ErrorFormat("Daemon thread check timeout socket error, message: {0}", E.Message);
//                         Program.Logger.Error(E.StackTrace);
                    }
                }

                for (int i = 0; i < 60 * 1000 / 10; i++) //每分钟检测一次
                {
                    if (!m_thread.IsAlive)
                        break;
                    Thread.Sleep(10);
                }
            }
        }

        public void Close()
        {
            m_thread.Abort();
            m_thread.Join();
        }
    }
}
