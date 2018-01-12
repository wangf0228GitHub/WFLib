using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WFNetLib
{    
    public class ControlComputer_Win7
    {
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        [DllImport("user32")]
        public static extern void LockWorkStation();
        [DllImport("user32")]
        public static extern int SendMessage(int hWnd, int hMsg, int wParam, int lParam);
        public enum MonitorState
        {
            MonitorStateOn = -1,
            MonitorStateOff = 2,
            MonitorStateStandBy = 1
        }
        public static void ShutDown()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo("shutdown.exe", "-s -t 00");
                System.Diagnostics.Process.Start(startinfo);
            }
            catch { }
        }
        public static void Restart()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startinfo = new System.Diagnostics.ProcessStartInfo("shutdown.exe", "-r -t 00");
                System.Diagnostics.Process.Start(startinfo);
            }
            catch { }
        }
        public static void LogOff()//注销
        {
            try
            {
                ExitWindowsEx(0, 0);
            }
            catch { }
        }
        public static void LockPC()
        {
            try
            {
                LockWorkStation();
            }
            catch { }
        }
        public static void Turnoffmonitor()
        {
            SetMonitorInState(MonitorState.MonitorStateOff);
        }
        private static void SetMonitorInState(MonitorState state)
        {
            SendMessage(0xFFFF, 0x112, 0xF170, (int)state);
        }
    }
    public class SetSystemDateTime
    {
        public static void SetSystemData(DateTime dt)
        {
            SystemTime MySystemTime = new SystemTime();
            MySystemTime.vYear = (ushort)dt.Year;
            MySystemTime.vMonth = (ushort)dt.Month;
            MySystemTime.vDay = (ushort)dt.Day;
            MySystemTime.vHour = (ushort)dt.Hour;
            MySystemTime.vMinute = (ushort)dt.Minute;
            MySystemTime.vSecond = (ushort)dt.Second;
            SetSystemDateTime.SetLocalTime(MySystemTime);
        }
        [DllImportAttribute("Kernel32.dll")]

        public static extern void GetLocalTime(SystemTime st);

        [DllImportAttribute("Kernel32.dll")]

        public static extern void SetLocalTime(SystemTime st);

    }
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class SystemTime
    {

        public ushort vYear;

        public ushort vMonth;

        public ushort vDayOfWeek;

        public ushort vDay;

        public ushort vHour;

        public ushort vMinute;

        public ushort vSecond;

    }
}
