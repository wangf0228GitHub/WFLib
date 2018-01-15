using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using WFNetLib.StringFunc;

namespace WFNetLib.Forms
{
    public partial class CommunicationDebugForm : Form
    {
        public bool IsClose=false;
        public bool IsStopShow = false;
        public CommunicationDebugForm()
        {
            InitializeComponent();
        }
#region 接收显示

        private delegate void ShowRxDelegate(string str);
        private void ShowRxDelegateProc(string str)
        {
            if (IsClose || IsStopShow)
                return;
            str = DateTime.Now.ToString("hh:mm:ss:ffff") + ":  "+str;
            tbRx.AppendText(str);
        }
        public void ExternShowRx(string str)
        {
            if (IsClose)
                return;
            ExternShowRx(str, false,true);
        }
        public void ExternShowRx(string str,bool bHex)
        {
            if (IsClose)
                return;
            ExternShowRx(str, bHex, true);
        }
        public void ExternShowRx(string str, bool bHex,bool bNewLine)
        {
            if (IsClose)
                return;
            if (bHex)
                ExternShowRx(new List<byte>(Encoding.ASCII.GetBytes(str)), bNewLine);
            else
            {
                try
                {
                    if (bNewLine)
                        this.Invoke(new ShowRxDelegate(ShowRxDelegateProc), str + "\r\n");
                    else
                        this.Invoke(new ShowRxDelegate(ShowRxDelegateProc), str);
                }
                catch { }
            }
        }
        public void ExternShowRx(List<byte> Tx)
        {
            if (IsClose)
                return;
            ExternShowRx(Tx, true);
        }
        public void ExternShowRx(List<byte> Tx,bool bNewLine)
        {
            if (IsClose)
                return;
            while (!this.IsHandleCreated) ;
            string strB = StringsFunction.byteToHexStr(Tx," ");
            if (bNewLine)
                strB+="\r\n"; 
            try
            {
                this.Invoke(new ShowRxDelegate(ShowRxDelegateProc), strB);
            }
            catch
            {
            	
            }
        }
        public void ExternShowRx(byte[] Tx)
        {
            if (IsClose)
                return;
            ExternShowRx(Tx, true);
        }
        public void ExternShowRx(byte[] Tx, bool bNewLine)
        {
            if (IsClose)
                return;
            while (!this.IsHandleCreated) ;
            string strB = StringsFunction.byteToHexStr(Tx, " ");
            if (bNewLine)
                strB += "\r\n";
            try
            {
                this.Invoke(new ShowRxDelegate(ShowRxDelegateProc), strB);
            }
            catch
            {

            }
        }

#endregion
        
#region 发送显示

        private delegate void ShowTxDelegate(string str);
        private void ShowTxDelegateProc(string str)
        {
            if (IsClose || IsStopShow)
                return;
            str = DateTime.Now.ToString("hh:mm:ss:ffff") + ":  " +str;
            tbTx.AppendText(str);         
        }
        public void ExternShowTx(string str)
        {
            if (IsClose)
                return;
            ExternShowTx(str, false, true);
        }
        public void ExternShowTx(string str, bool bHex)
        {
            if (IsClose)
                return;
            ExternShowTx(str, bHex, true);
        }
        public void ExternShowTx(string str, bool bHex, bool bNewLine)
        {
            if (IsClose)
                return;
            if (bHex)
                ExternShowTx(new List<byte>(Encoding.ASCII.GetBytes(str)), bNewLine);
            else
            {
                try
                {
                    if (bNewLine)
                        this.Invoke(new ShowTxDelegate(ShowTxDelegateProc), str + "\r\n");
                    else
                        this.Invoke(new ShowTxDelegate(ShowTxDelegateProc), str);
                }
                catch
                {
                	
                }                
            }
        }
        public void ExternShowTx(List<byte> Tx)
        {
            if (IsClose)
                return;
            ExternShowTx(Tx, true);
        }
        public void ExternShowTx(List<byte> Tx, bool bNewLine)
        {
            if (IsClose)
                return;
            while (!this.IsHandleCreated) ;
            string strB = StringsFunction.byteToHexStr(Tx, " ");
            if (bNewLine)
                strB += "\r\n";
            try
            {
                this.Invoke(new ShowTxDelegate(ShowTxDelegateProc), strB);
            }
            catch
            {
            	
            }            
        }
        private delegate void CloseDelegate();
        private void CloseProc()
        {
            this.Close();
        }
        public void ExternClose()
        {
            if (IsClose)
                return;
            while (!this.IsHandleCreated) ;
            this.Invoke(new CloseDelegate(CloseProc));
        }

#endregion       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbRx.Text = "";
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            tbTx.Text = "";
        }

        private void CommunicationDebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClose = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            IsStopShow = !IsStopShow;
            if (IsStopShow)
                toolStripButton3.Text = "继续显示";
            else
                toolStripButton3.Text = "停止显示";
        }

    }
}
