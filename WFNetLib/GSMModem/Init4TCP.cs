using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Diagnostics;

namespace WFNetLib.GSMModem
{
    public partial class GsmModem
    {  
//         public bool Init4TCP()
//         {            
//             try
//             {
//                 int ComTimeOut = Com.ReadTimeout;
//                 Com.ReadTimeout = 1000;
//                 while (true)
//                 {
//                     try
//                     {
//                         Com.ReadTo("\r\n");
//                     }
//                     catch
//                     {
//                         break;
//                     }
//                 }
//                 Com.ReadTimeout = ComTimeOut;
//                 if (!SendAT("ATE0"))
//                 {
//                     OnAddLog("此路是否上电?");
//                     return false;
//                 }
//                 if (!SendAT("ATI"))
//                 {
//                     OnAddLog("ATI请重试");
//                     return false;
//                 }
//                 strATI = ATResult.Replace("\r\n", "");
// //                 strATI = "";
// //                 if (strATI == "HUAWEIGTM900")
// //                 {
// //                     if (!SendAT("ATZ1"))
// //                     {
// //                         OnAddLog("ATZ1请重试");
// //                         return false;
// //                     }
// //                     ComTimeOut = Com.ReadTimeout;
// //                     Com.ReadTimeout = 10000;
// //                     while (true)
// //                     {
// //                         try
// //                         {
// //                             Com.ReadTo("\r\n");
// //                         }
// //                         catch
// //                         {
// //                             break;
// //                         }
// //                     }
// //                     Com.ReadTimeout = ComTimeOut;
// //                 }
//                 int Retr = 20;
//                 int stat = 1;
//                 while (Retr != 0)
//                 {
//                     Thread.Sleep(2000);
//                     Retr--;
// 
//                     if (!SendAT("AT+CREG?"))//查询是否已经注册上网络
//                     {
//                         OnAddLog("AT+CREG请重试");
//                         return false;
//                     }
//                     stat = Convert.ToInt32(ATResult.Substring(ATResult.IndexOf(',') + 1));
//                     if (stat == 1 || stat == 5)
//                         break;
// 
//                 }
//                 if (stat != 1 && stat != 5)
//                 {
//                     OnAddLog("AT+CREG"+stat.ToString()+"无法注册上网络");
//                     return false;
//                 } 
//                 switch (apn)
//                 {
//                     case APN.CMWAP:
//                         if (!SendAT("AT+CGDCONT=1,\"IP\",\"CMWAP\""))
//                         {
//                             OnAddLog("AT+CGDCONT无法注册上网络");
//                             return false;
//                         } 
//                         break;
//                     case APN.CMNET:
//                         if (!SendAT("AT+CGDCONT=1,\"IP\",\"CMNET\""))
//                         {
//                             OnAddLog("AT+CGDCONT无法注册上网络");
//                             return false;
//                         } 
//                         break;
//                 }
//                 if (!SendAT("AT+CGACT=0,1"))//附着GPRS
//                 {
//                     OnAddLog("AT+CGACT无法注册上网络");
//                     return false;
//                 }
// //                 if (strATI == "HUAWEIGTM900")
// //                 {
// //                     if (!GTM900_InitTCPIP())
// //                     {
// //                         OnAddLog("TCPIP初始化失败");
// //                         return false;
// //                     }
// //                     OnAddLog("TCPIP初始化成功");
// //                 }
// //                 else
//                 {
//                     if (!InitPPPConnect())
//                     {
//                         OnAddLog("拨号失败");
//                         return false;
//                     }
//                     OnAddLog("拨号成功");
//                 }
//             }
//             catch (System.Exception ex)
//             {
//                 Debug.WriteLine(Com.PortName + ex.Message);
//                 //communicationDebugForm.Close();
//                 return false;
//             }
//             return true;
//         }
    }
}
