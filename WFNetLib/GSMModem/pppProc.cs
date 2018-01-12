using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Diagnostics;
using System.Collections;
using WFNetLib.Net;
using System.Net;
using System.Threading;
using WFNetLib.StringFunc;

namespace WFNetLib.GSMModem
{
    public partial class GsmModem
    {
        public PPPStage pppStage=PPPStage.PPPS_None;
        public bool pppAcceptedTheirLCPOptions=false;
        public bool pppAcceptedOurLCPOptions = false;
        public bool pppAcceptedIPOptions = false;
        public bool pppAcceptedIPOptions1 = false;
        public bool pppLogin = false;
        public bool bPPP = false;
        public event intstrEventHandler pppProcLogEvent;//事件声明
        void OnPPPProcLog(string strLog, int type)
        {
            if (pppProcLogEvent != null)
            {
                pppProcLogEvent(this, new intstrEventArgs(type, strLog));
            }
        }
        public bool InitPPPConnect()
        {
            Com.ReadTimeout = 5000;
            ATCommand atComm = new ATCommand(Com);
            if (!atComm.SendAT("ATD*99***1#", "CONNECT"))
                return false;
            bDataTransfer = true;            
            int ComTimeOut = Com.ReadTimeout;
            Com.ReadTimeout = 2000;
            //Com.DataReceived -= COM_DataReceived; //注销事件关联，为发送做准备
            try
            {
                if (pppStage != PPPStage.PPPS_None)
                    return false;
                pppSender(pppLCP);
                if (pppProc(PPPStage.PPPS_IP))
                {
                    bPPP = true;
                    return true;
                }
                else
                {
                    pppClose();
                    pppReset();
                    return false;
                }
            }
            catch (InvalidOperationException ex)//端口被关闭,任务结束
            {
                throw ex;
            }
            catch
            {
                pppClose();
                pppReset();
                return false;
            }
        }
        public bool pppProc(PPPStage ps)
        {
            int n=0;    
            int r=5;            
            while(true)
            {
                pppPocket pppUp=pppReader();
                if(pppUp==null)
                {
                    n++;
                    if(n>r)
                    {
                        pppClose();
                        pppReset();
                        return false;
                    }
                }
                else
                {
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
                }
                switch (pppStage)													
                {								
                    case PPPStage.PPPS_LCP:
                        if (pppAcceptedOurLCPOptions && pppAcceptedTheirLCPOptions)
                        {
                            pppStage = PPPStage.PPPS_PAP;
                            pppPAP.ID = pppLCP.ID;
                            n = 0;
                            r = 30;
                        }
                        else
                            pppSender(pppLCP);
                        break;
                    case PPPStage.PPPS_PAP:
                        if(!pppLogin)
                            pppSender(pppPAP);
                        else
                        {
                            pppStage = PPPStage.PPPS_IPCP;
                            pppIPCP.ID = pppPAP.ID;
                            n = 0;
                            r = 100;
                            Com.ReadTimeout = 5000;
                        }
                        break;
                    case PPPStage.PPPS_IPCP:
                        if (pppAcceptedIPOptions && pppAcceptedIPOptions1)
                        {
                            pppStage = PPPStage.PPPS_IP;                            
                        }
                        else if(!pppAcceptedIPOptions)
                        {
                            pppSender(pppIPCP);
                        }
                        break;
                    case PPPStage.PPPS_IP:
                        break;
                }
                if(pppStage==ps)
                    break;
            }
            return true;
        }
        public List<pppPocket> pppReaderList()
        {
            List<pppPocket> ret = new List<pppPocket>();
            while (true)
            {
                pppPocket p = pppReader();
                if (p != null)
                    ret.Add(p);
                else
                    return ret;
            }
        }
        public pppPocket pppReader()
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
                catch
                {
                    return null;
                }                
                if (b7E)
                {
                    if (rx == 0x7e)
                    {                        
                        try
                        {
                            List<byte> RxList = pppDataTransferConvert.PPPDataDecode(RxTemp);
                            string strB = StringsFunction.byteToHexStr(RxList, " ");
                            OnPPPProcLog(strB, (int)ATCommandLogType.Rx);
                            ushort crc = BytesOP.MakeShort(RxList[RxList.Count - 1], RxList[RxList.Count - 2]);
                            if (Verify.GetVerify_CRC16_CCITT(RxList.ToArray(), RxList.Count - 2) != crc)
                                return null;
                            if (RxList[0] == 0xff || RxList[1] == 0x03)//去掉地址字节，本库中没有
                            {
                                RxList.Remove(0xff);
                                RxList.Remove(0x03);
                            }
                            pppPocket p = pppDataTransferConvert.MakepppPacket(RxList);
                            return p;
                        }
                        catch
                        {
                            return null;
                        }
                        
                    }
                    else
                        RxTemp.Add(rx);
                }
                else
                {
                    if (rx == 0x7e)
                        b7E = true;
                    else
                        Debug.WriteLine(rx.ToString("X2"));
                }
            }            
        }
        public void pppSender(pppPocket p)
        {
            List<byte> tx = pppDataTransferConvert.PPPDataList(p);
            string strB = StringsFunction.byteToHexStr(tx, " ");
            OnPPPProcLog(strB, (int)ATCommandLogType.Tx);          
            tx = pppDataTransferConvert.PPPDataEncode(tx, bLCP);
            Com.Write(tx.ToArray(), 0, tx.Count);
        }
        public bool pppClose()
        {
            if (!bDataTransfer)//if (!bPPP || !bDataTransfer)
                return true;
            int ComTimeOut = Com.ReadTimeout;
            Com.ReadTimeout = 20000;
            bool bComClose=false;
            if (!Com.IsOpen)
            {
                Com.Open();
                bComClose = true;
                Thread.Sleep(4000);
                Com.ReadExisting();
            }
            //Com.DataReceived -= COM_DataReceived; //注销事件关联，为发送做准备            
            pppPocket pD = new pppPocket();
            pD.Protocol = 0xc021;
            pD.ID = 0x01;
            pD.Code = 0x05;
            pD.Datas.Add(0x55);
            pD.Datas.Add(0x73);
            pD.Datas.Add(0x65);
            pD.Datas.Add(0x72);
            pD.Datas.Add(0x20);
            pD.Datas.Add(0x72);
            pD.Datas.Add(0x65);
            pD.Datas.Add(0x71);
            pD.Datas.Add(0x75);
            pD.Datas.Add(0x65);
            pD.Datas.Add(0x73);
            pD.Datas.Add(0x74);            
            pppSender(pD);
            pppProc(PPPStage.PPPS_None);
            string str;
            try
            {
                while (true)
                {
                    str = Com.ReadTo("\r\n");
                    OnPPPProcLog(str, (int)ATCommandLogType.Rx);
                    Debug.WriteLine(Com.PortName + "接收到:" + str);
                    if (str.IndexOf("ERROR") != -1)
                    {
                        return false;
                    }
                    if (str == "NO CARRIER")
                    {
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine(Com.PortName + "未知停止码" + str);
                    }
                }
            }
            catch (System.Exception ex)
            {
                OnAddLog(ex.Message);
                return false;
            }
            finally
            {
                Com.ReadTimeout = ComTimeOut;
                if (bComClose)
                    Com.Close();
                //Com.DataReceived += COM_DataReceived;                
            }
        }
        bool IPCP(pppPocket pppUp)
        {
            if (pppUp == null)
                return false;
            if (pppStage != PPPStage.PPPS_IPCP)
            {
                pppUnkown(pppUp);
                return true;
            }
            pppPocket pppDown;
            bool bSend = false;
            Union_UInt32 x32;
            switch (pppUp.Code)
            {
                case 1://配置请求 
                    pppDown = new pppPocket();
                    pppDown.Protocol = 0x8021;
                    pppDown.Code = 0x04;
                    pppDown.ID = pppUp.ID;
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        byte type = (byte)comm.Key;
                        if ((type != 3) && (type != 129) && (type != 131))
                        {
                            byte[] bs = (byte[])(pppUp.Commands[type]);
                            pppDown.AddCommand(type, bs);
                            bSend = true;
                        }
                    }
                    if (bSend)
                    {
                        pppSender(pppDown);
                        return true;
                    }
                    /************************************************************************/
                    /* 是否需要记录对方IP?                                                  */
                    /************************************************************************/
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        byte type = (byte)comm.Key;
                        byte[] ip = (byte[])(pppUp.Commands[type]);
                        switch (type)
                        {
                            case 3:
                                for (int i = 0; i < 4; i++)
                                    theirIP[i] = ip[i];
                                break;
                            case 129:
                                for (int i = 0; i < 4; i++)
                                    DNS1[i] = ip[i];
                                break;
                            case 131:
                                for (int i = 0; i < 4; i++)
                                    DNS2[i] = ip[i];
                                break;
                        }
                    }
                    pppUp.Code = 0x02;
                    if (pppAcceptedIPOptions)
                    {
                        pppSender(pppUp);
                        pppAcceptedIPOptions1 = true;
                    }
                    return true;
                case 4://拒绝请求
                    pppIPCP.ID++;
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        byte type = (byte)comm.Key;
                        byte[] ip = (byte[])(pppUp.Commands[type]);
                        x32 = new Union_UInt32();
                        x32.ofs_h.ofs_h = ip[0];
                        x32.ofs_h.ofs_l = ip[1];
                        x32.ofs_l.ofs_h = ip[2];
                        x32.ofs_l.ofs_l = ip[3];
                        switch (type)
                        {
                            case 3:
                                if (x32.ofs_32 != 0 && x32.ofs_32!=0xffffffff)
                                {
                                    pppIPCP.RemoveCommand(0x03);
                                    pppIPCP.AddCommand(0x03, 0x00, 0x00, 0x00, 0x00);
                                }
                                else
                                {
                                    pppEnd();
                                    pppReset();
                                }
                                break;
                            case 0x81:
                                if (x32.ofs_32 != 0 && x32.ofs_32 != 0xffffffff)
                                {
                                    pppIPCP.RemoveCommand(0x81);
                                    pppIPCP.AddCommand(0x81, 0x00, 0x00, 0x00, 0x00);
                                }
                                else
                                {
                                    pppIPCP.RemoveCommand(0x81);
                                }
                                break;
                            case 0x83:
                                if (x32.ofs_32 != 0 && x32.ofs_32 != 0xffffffff)
                                {
                                    pppIPCP.RemoveCommand(0x83);
                                    pppIPCP.AddCommand(0x83, 0x00, 0x00, 0x00, 0x00);
                                }
                                else
                                {
                                    pppIPCP.RemoveCommand(0x83);
                                }
                                break;
                        }
                    }
                    return true;
                case 3://服务给分配ip
                    pppIPCP.ID++;
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        byte type = (byte)comm.Key;
                        byte[] ip = (byte[])(pppUp.Commands[type]);
                        switch (type)
                        {
                            case 3:
                                ourIP = new byte[4];
                                for (int i = 0; i < 4; i++)
                                    ourIP[i] = ip[i];
                                break;
                            case 129:
                                for (int i = 0; i < 4; i++)
                                    DNS1[i] = ip[i];
                                break;
                            case 131:
                                for (int i = 0; i < 4; i++)
                                    DNS2[i] = ip[i];
                                break;
                        }
                        if (pppIPCP.Commands.ContainsKey(type))
                        {
                            pppIPCP.RemoveCommand(type);
                            pppIPCP.AddCommand(type, ip);
                        }
                    }
                    return true;
                case 2://服务器同意当前IP配置
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        byte type = (byte)comm.Key;
                        byte[] ip = (byte[])(pppUp.Commands[type]);
                        switch (type)
                        {
                            case 3:
                                for (int i = 0; i < 4; i++)
                                    ourIP[i] = ip[i];
                                break;
                            case 129:
                                for (int i = 0; i < 4; i++)
                                    DNS1[i] = ip[i];
                                break;
                            case 131:
                                for (int i = 0; i < 4; i++)
                                    DNS2[i] = ip[i];
                                break;
                        }
                    }
                    pppAcceptedIPOptions = true;
                    return true;
                default:
                    pppCodeReject(pppUp);
                    return true;
            }
        }
        bool PAP(pppPocket pppUp)
        {
            if (pppUp == null)
                return false;
            if (pppStage != PPPStage.PPPS_PAP && pppStage!=PPPStage.PPPS_IPCP)
            {
                pppUnkown(pppUp);
                return true;
            }
            switch (pppUp.Code)
            {
                case 2://Authenticate-Ack（认证应答）
                    pppLogin = true;                    
                    return true;
                case 3:
                    pppEnd();
                    pppReset();
                    return true;
                case 4:
                    pppEnd();
                    pppReset();
                    return true;
                default:
                    pppCodeReject(pppUp);
                    return true;
            }
        }
        bool LCP(pppPocket pppUp)
        {
            pppStage = PPPStage.PPPS_LCP;
            if (pppUp == null)
                return false;
            pppPocket pppDown;
            switch (pppUp.Code)
            {
                case 5://Terminate-Request
                    pppUp.Code = 0x06;
                    pppSender(pppUp);
                    pppReset();
                    return true;
                case 6://Terminate-Ack
                    pppReset();
                    return true;
                case 8://Protocol-Reject
                    pppEnd();
                    pppReset();
                    return true;
                case 7://Code-Reject
                    pppEnd();
                    pppReset();
                    return true;
                case 9://Echo-Request
                    {
                        Union_UInt32 x32 = new Union_UInt32();
                        x32.ofs_h.ofs_h = pppUp.Datas[0];
                        x32.ofs_h.ofs_l = pppUp.Datas[1];
                        x32.ofs_l.ofs_h = pppUp.Datas[2];
                        x32.ofs_l.ofs_l = pppUp.Datas[3];
                        if (x32.ofs_32 != 0)
                        {
                            byte[] mn = (byte[])(pppLCP.Commands[(byte)0x05]);
                            if (pppUp.Datas[0] != mn[0] ||
                                pppUp.Datas[1] != mn[1] ||
                                pppUp.Datas[2] != mn[2] ||
                                pppUp.Datas[3] != mn[3])
                            {
                                if (pppAcceptedOurLCPOptions)
                                {
                                    pppUp.Datas[0] = mn[0];
                                    pppUp.Datas[1] = mn[1];
                                    pppUp.Datas[2] = mn[2];
                                    pppUp.Datas[3] = mn[3];
                                }
                                else
                                {
                                    pppUp.Datas[0] = 0;
                                    pppUp.Datas[1] = 0;
                                    pppUp.Datas[2] = 0;
                                    pppUp.Datas[3] = 0;
                                }
                                pppUp.Code = 0x10;
                                pppSender(pppUp);
                                return true;
                            }
                            else
                            {
                                pppReset();
                                return true;
                            }
                        }
                        else
                            return true;
                    }
                case 10://Echo-Reply
                    {
                        Union_UInt32 x32 = new Union_UInt32();
                        x32.ofs_h.ofs_h = pppUp.Datas[0];
                        x32.ofs_h.ofs_l = pppUp.Datas[1];
                        x32.ofs_l.ofs_h = pppUp.Datas[2];
                        x32.ofs_l.ofs_l = pppUp.Datas[3];
                        if (x32.ofs_32 != 0)
                        {
                            byte[] mn = (byte[])(pppLCP.Commands[(byte)0x05]);
                            if (pppUp.Datas[0] != mn[0] ||
                                pppUp.Datas[1] != mn[1] ||
                                pppUp.Datas[2] != mn[2] ||
                                pppUp.Datas[3] != mn[3])
                            {                                
                                //是否应该有一些操作？
                                return true;
                            }
                            else
                            {
                                pppReset();
                                return true;
                            }
                        }
                        else
                            return true;
                    }
            }
            if (pppStage == PPPStage.PPPS_None || pppStage == PPPStage.PPPS_Start || pppStage == PPPStage.PPPS_Disconnect)
                return true;            
            switch (pppUp.Code)
            {
                case 0x01:// Configure-Request（匹配请求）
                    //首先找到我们不同意的请求，并给出建议值
                    bool bSend = false;
                    pppDown = new pppPocket();
                    pppDown.Protocol = pppUp.Protocol;
                    pppDown.Code = 0x03;
                    pppDown.ID = pppUp.ID;
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        switch ((byte)comm.Key)
                        {
                            case 5://Magic-Number
                                {
                                    Union_UInt32 x32 = new Union_UInt32();
                                    byte[] mn = (byte[])(pppUp.Commands[(byte)0x05]);
                                    x32.ofs_h.ofs_h = mn[0];
                                    x32.ofs_h.ofs_l = mn[1];
                                    x32.ofs_l.ofs_h = mn[2];
                                    x32.ofs_l.ofs_l = mn[3];
                                    if (x32.ofs_32 != 0)
                                    {
                                        byte[] mn1 = (byte[])(pppLCP.Commands[(byte)0x05]);
                                        if (mn[0] != mn1[0] ||
                                            mn[1] != mn1[1] ||
                                            mn[2] != mn1[2] ||
                                            mn[3] != mn1[3])
                                        {                                            
                                            break;
                                        }
                                        else
                                        {
                                            Random x = new Random();
                                            Union_UInt32 x321 = new Union_UInt32();
                                            x32.ofs_32 = (uint)x.Next();
                                            while (x321.ofs_32 == x32.ofs_32) x32.ofs_32 = (uint)x.Next();
                                            pppDown.AddCommand(0x05, x321.ofs_h.ofs_h, x321.ofs_h.ofs_l, x321.ofs_l.ofs_h, x321.ofs_l.ofs_l);
                                            bSend = true;
                                            break;
                                        }
                                    }
                                    else
                                        break;
                                }
                            case 1://Maximum-Receive-Unit（最大-接收-单元）
                                byte[] mru = (byte[])(pppUp.Commands[(byte)0x01]);
                                ushort w = BytesOP.MakeShort(mru[0], mru[1]);
                                if (w < 128)
                                    w = 128;
                                else if (w > 1500)
                                    w = 1500;
                                else
                                    break;
                                pppDown.AddCommand(0x01, BytesOP.GetHighByte(w),BytesOP.GetLowByte(w));
                                bSend = true;
                                break;
                            case 3://Authentication-Protocol（鉴定-协议）
                                byte[] ap = (byte[])(pppUp.Commands[(byte)0x03]);
                                ushort AP = BytesOP.MakeShort(ap[0], ap[1]);
                                if(AP==0xc023)
                                    break;                                
                                pppDown.AddCommand(0x03, 0xc0,0x23);
                                bSend = true;
                                break;
                        }                        
                    }
                    if (bSend)
                    {
                        pppSender(pppDown);
                        return true;
                    }
                    //去掉我们所不能接受的配置
                    pppDown.Code=0x04;
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        switch ((byte)comm.Key)
                        {
                            case 5://Magic-Number
                                break;
                            case 1://Maximum-Receive-Unit（最大-接收-单元）
                                break;
                            case 2://Async-Control-Character-Map(异步-控制-字符-映射)  
                                break;
                            case 3://Authentication-Protocol（鉴定-协议）
                                break;
                            default:
                                bSend = true;
                                byte[] bs = (byte[])(pppUp.Commands[(byte)comm.Key]);
                                pppDown.AddCommand((byte)comm.Key, bs);
                                break;
                        }
                    }
                    if (bSend)
                    {
                        pppSender(pppDown);
                        pppLCP.ID++;
                        return true;
                    }
                    //到此接受所有的服务器配置
                    pppAcceptedTheirLCPOptions = true;
                    pppUp.Code = 0x02;
                    pppSender(pppUp);
                    return true;
                case 2:// Configure-Ack
                    pppAcceptedOurLCPOptions = true;
                    return true;
                case 3://Configure-Nak
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        switch ((byte)comm.Key)
                        {
                            case 1://Maximum-Receive-Unit（最大-接收-单元）
                                pppLCP.RemoveCommand(0x01);
                                byte[] mru = (byte[])(pppUp.Commands[(byte)0x01]);
                                pppLCP.AddCommand(0x01, mru[0], mru[1]);
                                break;
                            case 5://Magic-Number
                                pppLCP.RemoveCommand(0x05);
                                byte[] mn = (byte[])(pppUp.Commands[(byte)0x05]);
                                pppLCP.AddCommand(0x01, mn[0],mn[1],mn[2],mn[3]);
                                break;
                            case 2://Async-Control-Character-Map(异步-控制-字符-映射)
                                pppLCP.RemoveCommand(0x02);
                                byte[] accm = (byte[])(pppUp.Commands[(byte)0x02]);
                                pppLCP.AddCommand(0x01, accm[0], accm[1], accm[2], accm[3]);
                                break;
                        }
                    }
                    pppAcceptedOurLCPOptions = false;
                    return true;
                case 4://Configure-Reject
                    foreach (DictionaryEntry comm in pppUp.Commands)
                    {
                        switch ((byte)comm.Key)
                        {
                            case 1://Maximum-Receive-Unit（最大-接收-单元）
                                pppLCP.RemoveCommand(0x01);                                
                                break;
                            case 5://Magic-Number
                                pppLCP.RemoveCommand(0x05);
                                break;
                            case 2://Async-Control-Character-Map(异步-控制-字符-映射)
                                pppLCP.RemoveCommand(0x02);
                                break;
                        }
                    }
                    pppAcceptedOurLCPOptions = false;
                    return true;
                default:
                    pppCodeReject(pppUp);
                    return true;
            }
        }
        void pppEnd()
        {
	        switch (pppStage)
	        {
		        case PPPStage.PPPS_None:	
                    break;
		        case PPPStage.PPPS_Start:
                    pppStage = PPPStage.PPPS_None;
                    break;
		        case PPPStage.PPPS_LCP:
		        case PPPStage.PPPS_PAP:
		        case PPPStage.PPPS_IPCP:
		        case PPPStage.PPPS_IP:
                    pppStage = PPPStage.PPPS_Disconnect;
                    break;
		        case PPPStage.PPPS_Disconnect:
                    break;
		        default:
                    pppReset();
                    break;
	        }
        }
        void pppReset()
        {
            pppStage=PPPStage.PPPS_None;
            pppLCP = new pppPocket();
            pppLCP.Protocol = 0xc021;
            pppLCP.ID=1;
            pppLCP.Code = 0x01;
            pppLCP.AddCommand(0x02, 0x00, 0x00, 0x00, 0x00);
            Random x = new Random();
            Union_UInt32 x32 = new Union_UInt32();
            x32.ofs_32 = (uint)x.Next();
            pppLCP.AddCommand(0x05, x32.ofs_h.ofs_h, x32.ofs_h.ofs_l, x32.ofs_l.ofs_h, x32.ofs_l.ofs_l);
            //pppLCP.AddCommand(0x07);
            //pppLCP.AddCommand(0x08);
            pppPAP = new pppPocket();           
            pppPAP.Protocol = 0xc023;
            pppPAP.Code = 0x01;
            pppPAP.ID = 1;
            byte[] Login = new byte[10] { 0x04, 0x6E, 0x6F, 0x6E, 0x65, 0x04, 0x6E, 0x6F, 0x6E, 0x65 };
            pppPAP.Datas.AddRange(Login);

            pppIPCP = new pppPocket();
            pppIPCP.Protocol = 0x8021;
            pppIPCP.Code = 0x01;
            pppIPCP.ID = 1;
            pppIPCP.AddCommand(0x03, 0x00, 0x00, 0x00, 0x00);
            //dIP = new byte[4];
            ourPort = (ushort)x.Next(4097, 4106);
            TCPSeqNum = (uint)x.Next(1000, 5000);
            IPID = 0;//(ushort)x.Next(100, 500);
            pppIPCP.AddCommand(0x81, 0x00, 0x00, 0x00, 0x00);
            pppIPCP.AddCommand(0x83, 0x00, 0x00, 0x00, 0x00);
            DNS1 = new byte[4];
            DNS2 = new byte[4];
            theirIP = new byte[4];
            pppAcceptedTheirLCPOptions=false;
            pppAcceptedOurLCPOptions = false;
            pppAcceptedIPOptions = false;
            pppAcceptedIPOptions1 = false;
            pppLogin = false;
            bLCP = false;
            bDataTransfer = false;
            bPPP = false;
        }
        void pppCodeReject(pppPocket pppUp)
        {
            bLCP = false;
            pppPocket pppDown = new pppPocket();
            pppDown.Protocol = 0xc021;
            pppDown.Code = 0x07;
            pppDown.ID = pppUp.ID;
            for (int i = 0; i < pppUp.Datas.Count; i++)
                pppDown.Datas[i] = pppUp.Datas[i];
            pppSender(pppDown);
        }
        void pppUnkown(pppPocket pppUp)
        {
            bLCP = false;
            pppPocket pppDown = new pppPocket();
            pppDown.Protocol = 0xc021;
            pppDown.Code = 0x08;
            pppDown.ID = (byte)(pppUp.ID+1);
            for (int i = 0; i < pppUp.Datas.Count; i++)
                pppDown.Datas[i] = pppUp.Datas[i];
            pppSender(pppDown);
        }
    }
    public enum PPPStage
    {
        PPPS_None,
        PPPS_Start,
        PPPS_LCP,
        PPPS_PAP,
        PPPS_IPCP,
        PPPS_IP,
        PPPS_Disconnect
    }
}
