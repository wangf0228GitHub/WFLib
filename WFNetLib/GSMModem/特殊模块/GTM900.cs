using System;
using System.Collections.Generic;

using System.Text;
using WFNetLib.Net;
using System.Threading;

namespace WFNetLib.GSMModem
{
    public partial class GsmModem
    {
//         public string TCPIP;
//         public bool GTM900_InitTCPIP()
//         {
//             return GTM900_InitTCPIP("TCP", "10.0.0.172", 80);
//         }
//         public bool GTM900_InitTCPIP(string strIP, int port)
//         {
//             return GTM900_InitTCPIP("TCP", strIP, port);
//         }
//         public bool GTM900_InitTCPIP(string strType,string strIP,int port)
//         {            
//             if (!SendAT("AT+CGACT=1,1"))//附着GPRS
//             {
//                 OnAddLog("AT+CGACT模块需要重新上电");
//                 return false;
//             } 
//             if (!SendAT("AT%ETCPIP"))//使能TCPIP
//             {
//                 OnAddLog("AT%ETCPIP模块需要重新上电");
//                 return false;
//             } 
//             TCPIP = "AT%IPOPEN=\"" + strType + "\",\"" + strIP + "\"," + port.ToString();
//             if (!SendAT(TCPIP, "CONNECT"))
//             {
//                 OnAddLog("AT%IPOPEN模块需要重新上电");
//                 return false;
//             } 
//             if (!SendAT("AT%IOMODE=1,1,1"))//16进制模式，不使用接收缓存
//             {
//                 OnAddLog("AT%IOMODE模块需要重新上电");
//                 return false;
//             } 
// //             if (!SendAT("AT%IPSP=1000"))//延时1s发送
// //                 return false;
//             return true;
//         }
//         public bool GTM900_CloseTCPIP()
//         {
//             SendAT("AT%IPCLOSE=5", -1);
//             return true;
//         }
//         public object GTM900_TCPIPRead(byte[] tx)
//         {
//             return GTM900_TCPIPRead(tx, TCPDataType.HTTP);
//         }        
//         public object GTM900_TCPIPRead(byte[] tx,TCPDataType datatype)
//         {
//             if (tx.Length > 8192)
//                 return null;
//             int ComTimeOut = Com.ReadTimeout;
//             Com.ReadTimeout = -1;
//             object oRet;
//             int noRead = 0;
//             while (true)
//             {                
//                 switch (datatype)
//                 {
//                     case TCPDataType.HTTP:
//                         oRet = new Http();
//                         break;
//                     default:
//                         Com.ReadTimeout = ComTimeOut;
//                         return null;
//                 }
//                 StringBuilder sb1 = new StringBuilder();
//                 for (int i = 0; i < tx.Length; i++)
//                 {
//                     sb1.Append(tx[i].ToString("X2"));
//                     if (sb1.Length > 1022)
//                     {
//                         string attx = "AT%IPSEND=\"" + sb1.ToString() + "\"";
//                         Com.Write(attx + "\r");    
// #if DEBUG
//                         communicationDebugForm.ExternShowTx(attx);
// #endif
//                         
//                         sb1.Clear();
//                     }
//                 }
//                 if (sb1.Length != 0)
//                 {
//                     string attx = "AT%IPSEND=\"" + sb1.ToString() + "\"";
//                     Com.Write(attx + "\r"); 
// #if DEBUG
//                     communicationDebugForm.ExternShowTx(attx);
// #endif
//                     
//                 }              
//                 string strRx;
//                 string strData;
//                 int len;
//                 int ns;                 
//                 while (true)
//                 {
//                     try
//                     {
//                         strRx = Com.ReadTo("\r\n");
// #if DEBUG
//                         communicationDebugForm.ExternShowRx(strRx);
// #endif
//                         
//                         ns = strRx.IndexOf("%IPDATA");
//                         if (ns != -1)
//                         {
//                             ns += 8;
//                             len = Convert.ToInt32(strRx.Substring(ns, strRx.IndexOf(',', ns) - ns));
//                             ns = strRx.IndexOf('\"', ns) + 1;
//                             strData = strRx.Substring(ns, strRx.IndexOf('\"', ns) - ns);
//                             if (datatype == TCPDataType.HTTP)
//                             {
//                                 Http http = (Http)oRet;
//                                 http.HttpDatas.AddRange(Strings.StringsFunction.strToHexByte(strData, ""));
//                                 int rxState = http.bFinish();
//                                 if (rxState == (int)HttpRxState.Finish)
//                                     break;
//                                 else if (rxState == (int)HttpRxState.LengthOut)
//                                     break;
//                             }
//                         }
//                         else if (strRx.IndexOf("%IPCLOSE") != -1)
//                         {
//                             if (!SendAT("AT%ETCPIP"))//使能TCPIP
//                             {
//                                 OnAddLog("AT%ETCPIP模块需要重新上电");
//                                 return null;
//                             }
//                             TCPIP = "AT%IPOPEN=\"TCP\",\"10.0.0.172\",80";
//                             if (!SendAT(TCPIP, "CONNECT"))
//                             {
//                                 OnAddLog("AT%IPOPEN模块需要重新上电");
//                                 return null;
//                             }                          
//                             throw new Exception("TCPIP超时关闭");
//                         }
//                         else if (strRx.IndexOf("ERROR")!=-1)
//                         {
//                             Thread.Sleep(2000);
//                             throw new Exception("TCPIP未知错误");                            
//                         }                          
// 
//                     }
//                     catch
//                     {
//                         noRead++;
//                         OnAddLog("网页超时，重新请求");
//                         break;
//                     } 
//                 }
//                 if (noRead > 3)
//                     break;
//                 if (datatype == TCPDataType.HTTP)
//                 {
//                     Http http = (Http)oRet;
//                     if (http.bOK)
//                         break;
//                 }                          
//             }
//             Com.ReadTimeout = ComTimeOut;
//             switch (datatype)
//             {
//                 case TCPDataType.HTTP:
//                     Http http = (Http)oRet;
//                     if (http.bOK)
//                         return oRet;
//                     else
//                         return null;
//                 default:
//                     return null;
//             }
//         }
//         public object GTM900_TCPIPRead(string strTx, TCPDataType datatype)
//         {
//             if (strTx.Length > 8192)
//                 return null;
//             byte[] tx = Encoding.ASCII.GetBytes(strTx);
//             return GTM900_TCPIPRead(tx,datatype);
//         }
//         public object GTM900_TCPIPRead(string strTx)
//         {
//             return GTM900_TCPIPRead(strTx, TCPDataType.HTTP);                        
//         }
    }
}
