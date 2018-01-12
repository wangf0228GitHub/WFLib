using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace WFNetLib.TCP
{
    public class TCPSyncClient
    {
        public string TCPServerName = "127.0.0.1";
        public int TCPServerPort = 8001;
        public int myPort = -1;
        public string myIP = "";
        private ClientContext clientContext;
        public SaveDataProcessCallbackDelegate SaveDataProcessCallback = null;
        public int ReadTimeOut=500;
        public int SendTimeOut=500;
        public int ReadRetry = 3;
        public Exception LastException;
        ///
        /// 连接服务器
        ///
        public Boolean Conn()
        {
            try
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (myPort > 0)
                {
                    IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(myIP), myPort);
                    socket.Bind(localEndPoint);
                }
//                 IPAddress ipAddress = IPAddress.Parse(TCPServerName);
//                 IPEndPoint remoteEP = new IPEndPoint(ipAddress, TCPServerPort);
                socket.Connect(TCPServerName,TCPServerPort);
                clientContext = new ClientContext(socket);
                clientContext.netStream.ReadTimeout = ReadTimeOut;
                clientContext.netStream.WriteTimeout = SendTimeOut;
                return true;
            }
            catch (SocketException e)
            {
                LastException = e;
//                 switch (e.ErrorCode)
//                 {
//                     case 10061:
//                         OnErrorClientEvent(new ErrorServerEventArgs(e, TCPErrorType.CannotConnect));
//                         break;
//                     case 10060:
//                         OnErrorClientEvent(new ErrorServerEventArgs(e, TCPErrorType.CannotConnect));
//                         break;
//                 }
            }
            catch (Exception e)
            {
                LastException = e;
                //OnErrorClientEvent(new ErrorServerEventArgs(e, TCPErrorType.Unkown));
            }
            return false;
        }
        public object ReceivePacket()
        {
            int readLen = 0;
            try
            {
                int retry=ReadRetry;
                while (retry != 0)
                {
                    retry--;
                    readLen = clientContext.netStream.Read(clientContext.tempbuffer, 0, ClientContext.BUFFER_SIZE);
                    object o = SaveDataProcessCallback(clientContext.tempbuffer,ref clientContext.netDataBuffer,ref clientContext.netDataOffset, readLen);
                    if (o != null)
                        return o;
                }
                
            }
            catch (System.Exception ex)
            {
                LastException = ex;
            }
            return null;
        }
        public bool SendPacket(byte[] txBytes)
        {
            try
            {
                clientContext.netStream.Write(txBytes, 0, txBytes.Length);
                return true;
            }
            catch// (System.Exception ex)
            {
            	
            }
            return false;
        }
        public void Close()
        {
            try
            {
                clientContext.ClientSocket.Shutdown(SocketShutdown.Both);
                clientContext.ClientSocket.Close();
            }
            catch (SystemException ex)
            {
                //OnErrorClientEvent(new ErrorServerEventArgs(ex, TCPErrorType.Unkown));
                LastException = ex;
            }
        }
    }
}
