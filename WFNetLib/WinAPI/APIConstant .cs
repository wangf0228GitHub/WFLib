using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.WinAPI
{
    public class APIConstant
    {
        public static readonly uint GENERIC_READ =0x80000000;
        public static readonly uint GENERIC_WRITE = 0x40000000;
        public static readonly uint GENERIC_EXECUTE = 0x20000000;
        public static readonly uint GENERIC_ALL = 0x10000000;

        public static readonly int CREATE_NEW = 1;
        public static readonly int CREATE_ALWAYS = 2;
        public static readonly int OPEN_EXISTING = 3;
        public static readonly int OPEN_ALWAYS = 4;
        public static readonly int TRUNCATE_EXISTING = 5;

        public static readonly int INVALID_HANDLE_VALUE = -1;

        //
        // PURGE function flags.
        //
        public static readonly uint PURGE_TXABORT = 0x0001;  // Kill the pending/current writes to the comm port.
        public static readonly uint PURGE_RXABORT = 0x0002; // Kill the pending/current reads to the comm port.
        public static readonly uint PURGE_TXCLEAR = 0x0004;  // Kill the transmit queue if there.
        public static readonly uint PURGE_RXCLEAR = 0x0008; // Kill the typeahead buffer if there.

        public static readonly int SW_HIDE = 0;
        public static readonly int SW_SHOWNORMAL = 1;
        public static readonly int SW_NORMAL = 1;
        public static readonly int SW_SHOWMINIMIZED = 2;
        public static readonly int SW_SHOWMAXIMIZED = 3;
        public static readonly int SW_MAXIMIZE = 3;
        public static readonly int SW_SHOWNOACTIVATE = 4;
        public static readonly int SW_SHOW = 5;
        public static readonly int SW_MINIMIZE = 6;
        public static readonly int SW_SHOWMINNOACTIVE = 7;
        public static readonly int SW_SHOWNA = 8;
        public static readonly int SW_RESTORE = 9;
        public static readonly int SW_SHOWDEFAULT = 10;
        public static readonly int SW_FORCEMINIMIZE = 11;
        public static readonly int SW_MAX = 11;

        public const int WM_DEVICECHANGE = 0x0219; 
        public const int DBT_DEVICEARRIVAL = 0x8000;
        public const int DBT_CONFIGCHANGECANCELED = 0x0019; 
        public const int DBT_CONFIGCHANGED = 0x0018; 
        public const int DBT_CUSTOMEVENT = 0x8006;
        public const int DBT_DEVICEQUERYREMOVE = 0x8001;
        public const int DBT_DEVICEQUERYREMOVEFAILED = 0x8002; 
        public const int DBT_DEVICEREMOVECOMPLETE = 0x8004; 
        public const int DBT_DEVICEREMOVEPENDING = 0x8003; 
        public const int DBT_DEVICETYPESPECIFIC = 0x8005; 
        public const int DBT_DEVNODES_CHANGED = 0x0007; 
        public const int DBT_QUERYCHANGECONFIG = 0x0017;
        public const int DBT_USERDEFINED = 0xFFFF;

        public const int MB_ICONEXCLAMATION = 48;
#region 手柄消息定义
        public const int MM_JOY1MOVE = 0x3A0;
        public const int MM_JOY2MOVE = 0x3A1;
        public const int MM_JOY1BUTTONDOWN = 0x3B5;
        public const int MM_JOY2BUTTONDOWN = 0x3B6;
        public const int MM_JOY1BUTTONUP = 0x3B7;
        public const int MM_JOY2BUTTONUP = 0x3B8;
#endregion

    }
}
