using System;
using System.Collections.Generic;

using System.Text;
using WFNetLib.Net;
using System.Threading;
using System.Diagnostics;
using System.Net;
using WFNetLib.StringFunc;

namespace WFNetLib.GSMModem
{
    public partial class GsmModem
    {
        public APN apn = APN.CMWAP;
        public List<byte> TCPTxData;
        TCPState tcpState;
        public TCPDataRecombine TCPData;
        public WebHeaderCollection httpHeader = new WebHeaderCollection();
        public CookieCollection cookies = new CookieCollection();
        public string url;
        public delegate string delegateProcessURL(string URL);
        public delegateProcessURL ProcessURL;        
        public List<byte> PostTx;
        public Single finishper=100;
        public event strEventHandler IPFinishPerEvent;//事件声明
        void OnIPFinishPer(string strLog)
        {
            if (IPFinishPerEvent != null)
            {
                IPFinishPerEvent(this, new strEventArgs(strLog));
            }
        }
        public event intstrEventHandler IPProcLogEvent;//事件声明
        void OnIPProcLog(string strLog, int type)
        {
            if (IPProcLogEvent != null)
            {
                IPProcLogEvent(this, new intstrEventArgs(type, strLog));
            }
        }
        public bool TCPRead(List<byte> tx)
        {
            bool ret;
            Debug.WriteLine(tx.Count);
            Debug.Write(Encoding.ASCII.GetString(tx.ToArray()));
            TCPTxData = tx;            
//             IPPacket ip = NewIPPacket();
//             TCPSeqNum++;
//             TCPPacket tcp = (TCPPacket)ip.oProtocolContent;
//             tcp.TCPOptions = new List<byte>();
//             tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
//             tcp.TCPOptions.Add(0x04);
//             tcp.TCPOptions.Add(0x04);
//             tcp.TCPOptions.Add(0x00);
//             tcp.DataOffset++;
//             tcp.TCPFlags = 0x02;//同步
//             IPSender(ip);
//             tcpState = TCPState.SYN;
            tcpState = TCPState.SYN;
            int ComTimeOut = Com.ReadTimeout;
            Com.ReadTimeout = 300;
            //Com.DataReceived -= COM_DataReceived; //注销事件关联，为发送做准备
            ret=Read();
            Com.ReadTimeout = ComTimeOut;
            //Com.DataReceived += COM_DataReceived;
            //ComWaitEvent.WaitOne();            
            return ret;
        }
        public bool TCPRead()
        {
            return TCPRead("GET");
        }
        public bool TCPRead(string Method)
        {
            string tx = HttpStatic.HttpHeaderToStr(Method,url, httpHeader, cookies);
            string str1;
            bool ret;
            List<byte> btx=new List<byte>(Encoding.ASCII.GetBytes(tx));
            if (Method == "POST")
            {
                btx.AddRange(PostTx);
            }
            Com.ReadTimeout = 300;
            ret=TCPRead(btx);
            string str;
            if (ret)//成功
            {
                object o = TCPData.oTCPDatas;
                if (o == null)
                    return false;
                switch (TCPData.datatype)
                {
                    case TCPDataType.HTTP:
                        Http http = (Http)o;
                        switch (http.ServerState)
                        {
                            case 200:
                                if (http.ContentDatas.Count == 0)
                                    return true;
                                str = HttpStatic.ContentToStr(http);
                                int x = str.IndexOf("onevent type=\"onenterforward\"");
                                if (x != -1)
                                {
                                    x = str.IndexOf("go href=\"", x);                                
                                    x += "go href=\"".Length;
                                    //OnAddLog("获得跳转页");
                                    //httpHeader[HttpRequestHeader.Referer] = url;
                                    url = StringsFunction.strDecode(str.Substring(x, str.IndexOf('\"', x) - x));
                                    if (http.Cookies.Count != 0)
                                    {
                                        foreach (Cookie c in http.Cookies)
                                        {
                                            if (cookies[c.Name] != null)
                                                cookies[c.Name].Value = c.Value;
                                            else
                                                cookies.Add(c);
                                        }
                                    }
                                    return TCPRead(Method);
                                }
                                x = str.IndexOf("<meta http-equiv=\"Refresh\"");
                                if (x != -1)
                                {
                                    x = str.IndexOf("<meta http-equiv=\"Refresh\"", x);
                                    x += "<meta http-equiv=\"Refresh\"".Length;
                                    x = str.IndexOf("content=\"");
                                    if(x==-1)
                                        break;
                                    x = x + "content=\"".Length;
                                    str1 = str.Substring(x, str.IndexOf('"', x) - x);
                                    x = str1.IndexOf(';');
                                    if(x==-1)
                                        break;
                                    str1 = str1.Substring(x + ";url=".Length);
                                    //OnAddLog("获得跳转页");
                                    httpHeader[HttpRequestHeader.Referer] = url;
                                    url = StringsFunction.strDecode(str1);
                                    if (http.Cookies.Count != 0)
                                    {
                                        foreach (Cookie c in http.Cookies)
                                        {
                                            if (cookies[c.Name] != null)
                                                cookies[c.Name].Value = c.Value;
                                            else
                                                cookies.Add(c);
                                        }
                                    }
                                    httpHeader.Remove(HttpRequestHeader.ContentLength);
                                    httpHeader.Remove(HttpRequestHeader.ContentType);
                                    return TCPRead();
                                }
                                break;
                            case 302://转移
                                try
                                {
                                    url = http.httpHeader[HttpResponseHeader.Location];
                                    url = ProcessURL(url);
                                    if (http.Cookies.Count != 0)
                                    {
                                        foreach (Cookie c in http.Cookies)
                                        {
                                            if (cookies[c.Name] != null)
                                                cookies[c.Name].Value = c.Value;
                                            else
                                                cookies.Add(c);
                                        }
                                    }
                                    httpHeader[HttpRequestHeader.Host] = "";
                                    return TCPRead(Method);
                                }
                                catch (System.Exception ex)
                                {
                                    Debug.WriteLine(ex.Message);
                                    return false;
                                }
                        }
                        break;
                    default:
                        return true;
                }
            }
            return ret;
        }
        void IPSender(IPPacket ip)
        {
            //Thread.Sleep(1000);
            TCPPacket tcpUp = (TCPPacket)ip.oProtocolContent;            
            List<byte>tx = pppDataTransferConvert.IPDataEncode(ip);
            //while (Com.DsrHolding) ;
            //while (!Com.CtsHolding) ; 
            Com.Write(tx.ToArray(), 0, tx.Count);
            OnIPProcLog("编号:" + tcpUp.InitialSeqNumber.ToString(), (int)ATCommandLogType.Tx);
            OnIPProcLog("回应编号:" + tcpUp.AckSeqNumber.ToString(), (int)ATCommandLogType.Tx);
            OnIPProcLog("标志:" + tcpUp.TCPFlags.ToString("X2"), (int)ATCommandLogType.Tx);                     
        }
//         void IPSender(IPPacket ip,bool b7E)
//         {
//             //Thread.Sleep(1000);
//             TCPPacket tcpUp = (TCPPacket)ip.oProtocolContent;            
//             List<byte> tx = pppDataTransferConvert.IPDataEncode(ip);
//             if (!b7E)
//                 tx.RemoveAt(0);
//             while (Com.DsrHolding) ;
//             while (!Com.CtsHolding) ;
//             Com.Write(tx.ToArray(), 0, tx.Count);
//             communicationDebugForm.ExternShowTx(tx);
//             communicationDebugForm.ExternShowTx("编号:" + tcpUp.InitialSeqNumber.ToString());
//             communicationDebugForm.ExternShowTx("回应编号:" + tcpUp.AckSeqNumber.ToString());
//             communicationDebugForm.ExternShowTx("标志:" + tcpUp.TCPFlags.ToString("X2"));
//         }
        IPPacket NewIPPacket()
        {
            IPPacket ip = new IPPacket(ourIP, dIP);
            ip.Identification = IPID++;
            ip.Protocol = 0x06;
            TCPPacket tcp = (TCPPacket)ip.oProtocolContent;
            tcp.SourcePort = ourPort;
            tcp.DastinationPort = dPort;
            //tcp.InitialSeqNumber = TCPSeqNum;
            return ip;
        }
        void OnTCPDataProc(TCPDataEventArgs tcpEx)
        {
            if (TCPDataProc != null)
                TCPDataProc(this, tcpEx);
        }
        bool Read()
        {
            IPPacket ipUp;
            IPPacket ip;
            TCPPacket tcp;
            bool bFinish = false;
            ip = NewIPPacket();
            tcp = (TCPPacket)ip.oProtocolContent;
            tcp.TCPOptions = new List<byte>();
            tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
            tcp.TCPOptions.Add(0x04);
            tcp.TCPOptions.Add(0x04);
            tcp.TCPOptions.Add(0x00);
            tcp.DataOffset++;
            tcp.TCPFlags = 0x02;//同步
            IPSender(ip);
            int err = 0;
            int retry = 0;
            while (true)
            {
                ipUp=IPReader();
                if (ipUp != null)
                {
                    err = 0;
                    TCPPacket tcpUp = (TCPPacket)ipUp.oProtocolContent;
                    if (tcpUp == null)
                        continue;
                    if (tcpUp.DastinationPort != ourPort)
                        continue;
                    switch (tcpUp.TCPFlags)
                    {
                        case 0x12:
                            if (tcpState == TCPState.SYN)//之前正在处于请求连接状态
                            {
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;                                
                                tcp.TCPFlags = 0x10;
                                IPSender(ip);
                                tcp.TCPFlags = 0x18;//push数据
                                TCPSeqNum += (uint)TCPTxData.Count;
                                foreach (byte b in TCPTxData)
                                {
                                    tcp.Datas.Add(b);
                                }
                                IPSender(ip);
                                TCPData = new TCPDataRecombine(TCPDataType.HTTP);
                                TCPData.InitSeqNum = tcp.AckSeqNumber;
                                tcpState = TCPState.TransData;
                            }
                            else if (tcpState == TCPState.FIN)
                            {
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;                               
                                tcp.TCPFlags = 0x10;
                                IPSender(ip);
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.TCPOptions = new List<byte>();
                                tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x00);
                                tcp.DataOffset++;
                                tcp.TCPFlags = 0x02;//同步
                                IPSender(ip);
                                tcpState = TCPState.SYN;
                            }
                            else if (tcpState == TCPState.TransData)
                            {
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;                                
                                tcp.TCPFlags = 0x10;
                                IPSender(ip);
                            }
                            else
                            {
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;                                
                                tcp.TCPFlags = 0x14;
                                IPSender(ip);
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.TCPOptions = new List<byte>();
                                tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x00);
                                tcp.DataOffset++;
                                tcp.TCPFlags = 0x02;//同步
                                IPSender(ip);
                                Debug.WriteLine(Com.PortName + "TCP未知命令交互:" + tcpState.ToString() + "  " + tcpUp.TCPFlags.ToString("X2"));
                            }
                            break;
                        case 0x10:                            
                        case 0x18:
                            if (tcpState == TCPState.WaitFIN)
                            {
                                OnIPProcLog("成功关闭连接!", (int)ATCommandLogType.Rx);  
                                tcpState = TCPState.WaitFIN1;
                                return true;
                            }
                            else if (tcpState == TCPState.TransData)
                            {
                                if (tcpUp.Datas.Count != 0)
                                {                                    
                                    int rxState = TCPData.bFinish(tcpUp.InitialSeqNumber, tcpUp.Datas,finishper);
                                    if (finishper < 100)
                                    {
                                        switch (TCPData.datatype)
                                        {
                                            case TCPDataType.HTTP:
                                                Http http = (Http)TCPData.oTCPDatas;
                                                OnIPFinishPer(http.per.ToString());
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    if (rxState==(int)HttpRxState.Finish)//数据接收完成
                                    {
                                        tcp.TCPFlags = 0x11;
                                        TCPSeqNum++;
                                        tcpState = TCPState.WaitFIN;
                                        IPSender(ip);
                                        bFinish = true;
                                        OnIPProcLog("数据接收完成!", (int)ATCommandLogType.Rx);
                                        
                                    }
                                    else if (rxState==(int)HttpRxState.LengthOut)
                                    {                                        
                                        ip = NewIPPacket();
                                        tcp = (TCPPacket)ip.oProtocolContent;
                                        tcp.TCPOptions = new List<byte>();
                                        tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
                                        tcp.TCPOptions.Add(0x04);
                                        tcp.TCPOptions.Add(0x04);
                                        tcp.TCPOptions.Add(0x00);
                                        tcp.DataOffset++;
                                        tcp.TCPFlags = 0x02;//同步
                                        IPSender(ip);
                                        tcpState = TCPState.SYN;
                                    }
                                    else
                                    {                                        
                                        Debug.WriteLine(Com.PortName + "还需要的数据量为:" + rxState.ToString());
                                        ip = NewIPPacket();
                                        tcp = (TCPPacket)ip.oProtocolContent;
                                        tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                        tcp.AckSeqNumber = tcpUp.InitialSeqNumber + (uint)tcpUp.Datas.Count;
                                        tcp.TCPFlags = 0x10;
                                        IPSender(ip);
                                    }
                                }
                            }
                            else
                            {
//                                 ip = NewIPPacket();
//                                 tcp = (TCPPacket)ip.oProtocolContent;
//                                 tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
//                                 tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;
//                                 tcp.TCPFlags = 0x14;
//                                 IPSender(ip);
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.TCPOptions = new List<byte>();
                                tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x00);
                                tcp.DataOffset++;
                                tcp.TCPFlags = 0x02;//同步
                                IPSender(ip);
//                                 Debug.WriteLine(Com.PortName + "TCP未知命令交互:" + tcpState.ToString() + "  " + tcpUp.TCPFlags.ToString("X2"));
                                Debug.WriteLine(Com.PortName + "TCP未知命令交互:" + tcpState.ToString() + "  " + tcpUp.TCPFlags.ToString("X2"));
                            }
                            break;
                        case 0x11:
                            if (tcpState == TCPState.WaitFIN)
                            {
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;
                                tcp.TCPFlags = 0x10;
                                IPSender(ip);
                                OnIPProcLog("成功关闭连接!11111111111111111111", (int)ATCommandLogType.Rx);                         
                                return true;                              
                            }                            
                            else
                            {
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;
                                tcp.TCPFlags = 0x10;
                                IPSender(ip);
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.TCPOptions = new List<byte>();
                                tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x04);
                                tcp.TCPOptions.Add(0x00);
                                tcp.DataOffset++;
                                tcp.TCPFlags = 0x02;//同步
                                IPSender(ip);
                                tcpState = TCPState.SYN;
                            }
                            break;
                        case 0x04:                            
                            if (tcpState == TCPState.WaitFIN || tcpState==TCPState.WaitFIN1)
                            {
//                                 ip = NewIPPacket();
//                                 tcp = (TCPPacket)ip.oProtocolContent;
//                                 tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
//                                 tcp.AckSeqNumber = tcpUp.InitialSeqNumber;
//                                 tcp.TCPFlags = 0x10;
//                                 IPSender(ip);
//                                 tcp.TCPFlags = 0x11;
//                                 TCPSeqNum++;
//                                 tcpState = TCPState.WaitFIN;
//                                 IPSender(ip);
                                OnIPProcLog("彻底关闭连接", (int)ATCommandLogType.Rx);      
                                return true;
                            }
                            else
                            {
                                Debug.WriteLine(Com.PortName + "TCP未知命令交互:" + tcpState.ToString() + "  " + tcpUp.TCPFlags.ToString("X2"));
//                                 if(tcpState==TCPState.SYN)
//                                 {
//                                     ip = NewIPPacket();
//                                     tcp = (TCPPacket)ip.oProtocolContent;
//                                     tcp.TCPOptions = new List<byte>();
//                                     tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
//                                     tcp.TCPOptions.Add(0x04);
//                                     tcp.TCPOptions.Add(0x04);
//                                     tcp.TCPOptions.Add(0x00);
//                                     tcp.DataOffset++;
//                                     tcp.TCPFlags = 0x02;//同步
//                                     IPSender(ip);
//                                     tcpState = TCPState.SYN;
//                                 }
                            }
                            break;
                        case 0x14:                            
                            if (tcpState == TCPState.WaitFIN || tcpState == TCPState.WaitFIN1)
                            {
                                OnIPProcLog("彻底关闭连接", (int)ATCommandLogType.Rx);   
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber;
                                tcp.TCPFlags = 0x10;
                                IPSender(ip);
                                return true;
                            }
                            else
                            {
                                Debug.WriteLine(Com.PortName + "TCP未知命令交互:" + tcpState.ToString() + "  " + tcpUp.TCPFlags.ToString("X2"));
//                                 if (tcpState == TCPState.SYN)
//                                 {
//                                     ip = NewIPPacket();
//                                     tcp = (TCPPacket)ip.oProtocolContent;
//                                     tcp.TCPOptions = new List<byte>();
//                                     tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
//                                     tcp.TCPOptions.Add(0x04);
//                                     tcp.TCPOptions.Add(0x04);
//                                     tcp.TCPOptions.Add(0x00);
//                                     tcp.DataOffset++;
//                                     tcp.TCPFlags = 0x02;//同步
//                                     IPSender(ip);
//                                     tcpState = TCPState.SYN;
//                                 }
                                //                                 ip = NewIPPacket();
//                                 tcp = (TCPPacket)ip.oProtocolContent;
//                                 tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
//                                 tcp.AckSeqNumber = tcpUp.InitialSeqNumber;
//                                 tcp.TCPFlags = 0x10;
//                                 IPSender(ip);
//                                 tcpState = TCPState.SYN;
                            }                            
                            break;
                        case 0x01:
                            if(tcpState==TCPState.WaitFIN1)
                            {
                                ip = NewIPPacket();
                                tcp = (TCPPacket)ip.oProtocolContent;
                                tcp.InitialSeqNumber = tcpUp.AckSeqNumber;
                                tcp.AckSeqNumber = tcpUp.InitialSeqNumber + 1;
                                tcp.TCPFlags = 0x10;
                                IPSender(ip);
                                OnIPProcLog("成功关闭连接!11111111111111111111", (int)ATCommandLogType.Rx);   
                                return true;
                            }   
                            else
                                break;
                    }
                }
                else
                {
                    err++;
                    if (err > 30)
                    {
                        if (bFinish)
                            return true;
                        bFinish = false;
                        retry++;
                        if (retry > 12)
                        {                            
                            break;
                        }
                        OnIPProcLog("TCP超时重试" + (retry + 1).ToString(), (int)ATCommandLogType.TCPTimeOut);
                        err = 0;
                        ip = NewIPPacket();
                        tcp = (TCPPacket)ip.oProtocolContent;
                        tcp.TCPOptions = new List<byte>();
                        tcp.TCPOptions.Add(0x02);//段字节数最多为1024：0x4000
                        tcp.TCPOptions.Add(0x04);
                        tcp.TCPOptions.Add(0x04);
                        tcp.TCPOptions.Add(0x00);
                        tcp.DataOffset++;
                        tcp.TCPFlags = 0x02;//同步
                        IPSender(ip);
                        tcpState = TCPState.SYN;
                    }
                }
            }
            return false;
        }
        IPPacket IPReader()
        {
            bool b7E = false;
            byte rx;
            List<byte> RxTemp = new List<byte>();            
            while (true)
            {
                try
                {
                    rx = (byte)Com.ReadByte();
                }
                catch (InvalidOperationException ex)//端口被关闭,任务结束
                {
                    throw ex;
                }
                catch
                {                    
                    return null;
                }
                if (b7E)
                {
                    if (rx == 0x7e)
                    {
                        List<byte> RxList = pppDataTransferConvert.PPPDataDecode(RxTemp);
// #if DEBUG
//                         communicationDebugForm.ExternShowRx(RxList);
// #endif
                        
//                         ushort crc = BytesOP.MakeShort(RxList[RxList.Count - 1], RxList[RxList.Count - 2]);
//                         if (Verify.GetVerify_CRC16_CCITT(RxList.ToArray(), RxList.Count - 2) != crc)
//                             return null;
//                         if (RxList[0] == 0xff || RxList[1] == 0x03)//去掉地址字节，本库中没有
//                         {
//                             RxList.Remove(0xff);
//                             RxList.Remove(0x03);
//                         }
                        IPPacket ipUp;
                        try
                        {
                            ipUp = IPPacket.MakeIPPacket(RxList, 4);
                        }
                        catch
                        {
                            pppPocket pppUp;
                            pppUp=pppDataTransferConvert.MakepppPacket(RxList,2);
                            switch (pppUp.Protocol)
                            {
                                case 0xc021:
                                    LCP(pppUp);
                                    break;
                                case 0xc023:
                                    PAP(pppUp);
                                    break;
                                case 0x8021:
                                    IPCP(pppUp);
                                    break;
                            }
                            Debug.WriteLine(Com.PortName + StringsFunction.byteToHexStr(RxTemp.ToArray(), " "));
                            return null;
                        }
                        TCPPacket tcpUp = (TCPPacket)ipUp.oProtocolContent;
                        if(tcpUp!=null)
                        {
                            OnIPProcLog("编号:" + tcpUp.InitialSeqNumber.ToString(), (int)ATCommandLogType.Rx);
                            OnIPProcLog("回应编号:" + tcpUp.AckSeqNumber.ToString(), (int)ATCommandLogType.Rx);
                            OnIPProcLog("标志:" + tcpUp.TCPFlags.ToString("X2"), (int)ATCommandLogType.Rx);
                        }                       
                        return ipUp;
                    }
                    else
                        RxTemp.Add(rx);
                }
                else
                {
                    if (rx == 0x7e)
                        b7E = true;
                    else
                    {
                        OnIPProcLog("未知接收:" + rx.ToString("X2"), (int)ATCommandLogType.Rx);
                    }
                }
                
            }
        }
    }
}
