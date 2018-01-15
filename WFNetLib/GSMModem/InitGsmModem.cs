using System;
using System.Collections.Generic;

using System.Text;
using System.Diagnostics;
using System.Threading;
using WFNetLib.Net;

namespace WFNetLib.GSMModem
{
    public partial class GsmModem
    {
        public string strCGSN;//产品序列号 
        public string strCSQ;//信号状态
        public string strCSCA;//+CSCA: "+8613800451500",145,短信服务中心地址应该为13800451500，若不是则修改为这个
        public string strCIMI;//手机卡的IMSI（国际移动台设备标识）
        public string strCCID;//SIM的序列号
        public string strATI;//设备制造商
        AutoResetEvent ComWaitEvent = new AutoResetEvent(false);
//         public bool InitGSMModem()
//         {
//             //             int oldTimeOut = Com.ReadTimeout;
//             //             Com.ReadTimeout = -1;
//             //             Com.DataReceived -= COM_DataReceived; //注销事件关联，为发送做准备
//             //             pppConnect();
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
//                     OnAddLog("未知模块");
//                     return false;
//                 }
//                 strATI = ATResult.Replace("\r\n", "");
//                 if (!SendAT("AT+CGSN"))//产品序列号              
//                 {
//                     OnAddLog("未知产品序列号");
//                     return false;
//                 }
//                 strCGSN = ATResult;
//                 if (!SendAT("AT+CSQ"))//信号状态
//                 {
//                     OnAddLog("未知信号状态");
//                     return false;
//                 }
//                 strCSQ = ATResult.Substring(6);
//                 //SendAT("AT+CSCA?", -1);
//                 SendAT("AT+CSCA?", true);
//                 try
//                 {
//                     strCSCA = ATResult.Substring(ATResult.IndexOf('\"') + 2, ATResult.IndexOf('\"', ATResult.IndexOf('\"') + 2) - ATResult.IndexOf('\"') - 2);
//                 }
//                 catch
//                 {
//                     strCSCA = ATResult;
//                     OnAddLog("未知短信中心" + strCSCA);
//                 }
//                 SendAT("AT+CNMI=2,1,0,0", true);
//                 SendAT("AT+CIMI", true);
//                 //                 if (!SendAT("AT+CNMI=2,1,0,0"))
//                 //                 {
//                 //                     OnAddLog("未知消息提示");
//                 //                     return false;
//                 //                 }                
//                 //                 if (!SendAT("AT+CIMI"))
//                 //                 {
//                 //                     OnAddLog("未知IMSI");
//                 //                     return false;
//                 //                 }
//                 strCIMI = ATResult;
//                 SendAT("AT+CCID?", true);//获得SIM的序列号,可以失败
//                 strCCID = ATResult;
//                 int Retr = 20;
//                 int stat = 1;
//                 while (Retr != 0)
//                 {
//                     Thread.Sleep(2000);
//                     Retr--;
// 
//                     if (!SendAT("AT+CREG?"))//查询是否已经注册上网络
//                     {
//                         OnAddLog("注册网络未知");
//                         return false;
//                     }
//                     stat = Convert.ToInt32(ATResult.Substring(ATResult.IndexOf(',') + 1));
//                     if (stat == 1 || stat == 5)
//                         break;
// 
//                 }
//                 if (stat != 1 && stat != 5)
//                 {
//                     OnAddLog("AT+CREG" + stat.ToString() + "无法注册上网络");
//                     return false;
//                 }
//                 if (!SendAT("AT+CLIP=1"))//来电显示
//                 {
//                     OnAddLog("未知来电显示");
//                     return false;
//                 }
//                 if (!SendAT("AT+CMGF=0"))//SMS 格式,0为PDU
//                 {
//                     OnAddLog("未知短信格式");
//                     return false;
//                 }
//                 if (!SendAT("AT+CPMS=\"SM\",\"SM\""))
//                 {
//                     OnAddLog("未知信息存储");
//                     return false;
//                 }                
//                 switch (apn)
//                 {
//                     case APN.CMWAP:
//                         if (!SendAT("AT+CGDCONT=1,\"IP\",\"CMWAP\""))
//                         {
//                             OnAddLog("无法注册上网络");
//                             return false;
//                         }
//                         break;
//                     case APN.CMNET:
//                         if (!SendAT("AT+CGDCONT=1,\"IP\",\"CMNET\""))
//                         {
//                             OnAddLog("无法注册上网络");
//                             return false;
//                         }
//                         break;
//                 }
//             }
//             catch (System.Exception ex)
//             {
//                 Debug.WriteLine(ex.Message);
//                 // #if DEBUG
//                 //                 communicationDebugForm.ExternShowRx(ex.Message);
//                 // #endif
//                 //communicationDebugForm.Close();
//                 return false;
//             }
//             return true;
//         }
    }
}
