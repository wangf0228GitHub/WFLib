using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Windows.Forms;

/*
1)声明等待窗体变量
WaitingProc wp;
2）初始化等待函数
wp = new WaitingProc();
WaitingProcFunc wpf=new WaitingProcFunc(WaitingPCIE);
wp.Execute(wpf,"等待连接采集卡",WaitingType.With_ConfirmCancel,"未连接到采集卡，取消将退出软件，是否取消？");

3）等待函数
void WaitingPCIE(object LockWatingThread)
{
    while(true)
    {
        pCH368 = CH368.CH367mOpenDevice(1, FALSE, TRUE, 0x00);
        if (pCH368 != (IntPtr)(-1))
        {
            return;
        }
        Thread.Sleep(1000);                
        lock (LockWatingThread)
        {
            wp.SetProcessBarPerformStep();
            if (wp.HasBeenCancelled())
            {
                this.Invoke((EventHandler)(delegate
                {
                    this.Close();                           
                }));
                return;
            }
        }
    }
}
*/
namespace WFNetLib
{
    public delegate void WaitingProcFunc(object LockWatingThread);
    public enum WaitingType
    {
        None,
        WithCancel,
        With_ConfirmCancel
    }
    public class WaitingProc
    {
        private WaitingProcFunc Func;
        private Thread WaitingThread;
        private WaitingForm form;
        public int MaxProgress = 100;
        public int MinProgress = 0;
        public IWin32Window owner=null;
        public WaitingProc()
        {
            MaxProgress = 100;
            MinProgress = 0;
        }
        public WaitingProc(IWin32Window _owner)
        {
            owner = _owner;
            MaxProgress = 100;
            MinProgress = 0;
        }
        public bool Execute(WaitingProcFunc func, string Title, WaitingType type, string ConfirmPrompt)
        {
            Func = func;
            form = new WaitingForm(type);
            form.ConfirmPrompt = ConfirmPrompt;
            form.Text = Title;
            form.progressBar1.Minimum = MinProgress;
            form.progressBar1.Maximum = MaxProgress;
            WaitingThread = new Thread(new ThreadStart(Waitting));
            WaitingThread.Name = "等待执行线程";
            WaitingThread.Start();
            if (owner == null)
                form.ShowDialog();
            else
                form.ShowDialog(owner);
            return !form.bCancelled;
        }
        private void Waitting()
        {
            Func(form.LockWatingThread);
            form.ReadyEvent.WaitOne();
            form.ExternClose();
        }
        public void ExitWatting()
        {
            form.bCancelled = true; 
        }
        public bool HasBeenCancelled()
        {
            return form.bCancelled;
        }
        public void SetProcessBar(int i)
        {
            form.ExternSetProcessBar(i);
        }
        public void SetCursorStyle(Cursor c)
        {
            form.ExternSetCursorStyle(c);
        }
        public void SetTitle(string str)
        {
            form.ExternSetTitle(str);
        }
        public void SetProcessBarRange(int min, int max)
        {
            form.ExternSetProcessBarRange(min, max);
        }
        public void SetProcessBarPerformStep()
        {
            form.ExternSetProcessBarPerformStep();
        }        
    }
    public class WaitSometingForm
    {
        static public bool bGenerateSometing;
        static bool bWaitTimeOut;
        static WaitingProc wp;
        static int waitStep;
        static int waitCount;
        public static bool WaitSometing(int _waitStep, int _waitCount,string strTitle)
        {
            waitStep = _waitStep;
            waitCount = _waitCount;
            wp = new WaitingProc();
            wp.MaxProgress = waitCount;
            WaitingProcFunc wpf1 = new WaitingProcFunc(WaitSometingProc);
            wp.Execute(wpf1, strTitle, WaitingType.None, "");
            if (bWaitTimeOut)
                return false;
            return true;
        }
        public static void WaitSometing_Init()
        {
            bGenerateSometing = false;
            bWaitTimeOut = false;
        }
        static void WaitSometingProc(object LockWatingThread)
        {
            while (true)
            {
                Thread.Sleep(waitStep);
                lock (LockWatingThread)
                {
                    if (bGenerateSometing)
                    {
                        return;
                    }
                    if (waitCount == 0)
                    {
                        bWaitTimeOut = true;
                        return;
                    }
                    waitCount--;                    
                    wp.SetProcessBarPerformStep();
                }
            }
        }
    }
}
