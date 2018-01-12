using System.Runtime.InteropServices;

namespace WFNetLib.WinAPI
{
#region 串口API用结构体
    [StructLayout(LayoutKind.Sequential)]
    public struct DCB
    {
        //taken from c struct in platform sdk 
        public int DCBlength; // sizeof(DCB) 
        public int BaudRate; // current baud rate 
        public int fBinary; // binary mode, no EOF check 
        public int fParity; // enable parity checking 
        public int fOutxCtsFlow; // CTS output flow control 
        public int fOutxDsrFlow; // DSR output flow control 
        public int fDtrControl; // DTR flow control type 
        public int fDsrSensitivity; // DSR sensitivity 
        public int fTXContinueOnXoff; // XOFF continues Tx 
        public int fOutX; // XON/XOFF out flow control 
        public int fInX; // XON/XOFF in flow control 
        public int fErrorChar; // enable error replacement 
        public int fNull; // enable null stripping 
        public int fRtsControl; // RTS flow control 
        public int fAbortOnError; // abort on error 
        public int fDummy2; // reserved 
        public ushort wReserved; // not currently used 
        public ushort XonLim; // transmit XON threshold 
        public ushort XoffLim; // transmit XOFF threshold 
        public byte ByteSize; // number of bits/byte, 4-8 
        public byte Parity; // 0-4=no,odd,even,mark,space 
        public byte StopBits; // 0,1,2 = 1, 1.5, 2 
        public char XonChar; // Tx and Rx XON character 
        public char XoffChar; // Tx and Rx XOFF character 
        public char ErrorChar; // error replacement character 
        public char EofChar; // end of input character 
        public char EvtChar; // received event character 
        public ushort wReserved1; // reserved; do not use 
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COMMTIMEOUTS
    {
        public int ReadIntervalTimeout;
        public int ReadTotalTimeoutMultiplier;
        public int ReadTotalTimeoutConstant;
        public int WriteTotalTimeoutMultiplier;
        public int WriteTotalTimeoutConstant;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OVERLAPPED
    {
        public int Internal;
        public int InternalHigh;
        public int Offset;
        public int OffsetHigh;
        public int hEvent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct COMSTAT
    {
        /*public int fCtsHold;
        public int fDsrHold;
        public int fRlsdHold;
        public int fXoffHold;
        public int fXoffSent;
        public int fEof;
        public int fTxim;
        public int fReserved;
        public int cbInQue;
        public int cbOutQue;*/
        // Should have a reverse, i don't know why!!!!!
        public int cbOutQue;
        public int cbInQue;
        public int fReserved;
        public int fTxim;
        public int fEof;
        public int fXoffSent;
        public int fXoffHold;
        public int fRlsdHold;
        public int fDsrHold;
        public int fCtsHold;
    }
#endregion

#region 游戏手柄的参数结构体
    /// <summary>
    /// 游戏手柄的参数信息
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JOYCAPS
    {
        public ushort wMid;
        public ushort wPid;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;
        public int wXmin;
        public int wXmax;
        public int wYmin;
        public int wYmax;
        public int wZmin;
        public int wZmax;
        public int wNumButtons;
        public int wPeriodMin;
        public int wPeriodMax;
        public int wRmin;
        public int wRmax;
        public int wUmin;
        public int wUmax;
        public int wVmin;
        public int wVmax;
        public int wCaps;
        public int wMaxAxes;
        public int wNumAxes;
        public int wMaxButtons;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szRegKey;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szOEMVxD;
    }
#endregion
    
#region 游戏手柄的位置与按钮状态
        /// <summary>
        /// 游戏手柄的位置与按钮状态
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct JOYINFO
        {
            public int wXpos;
            public int wYpos;
            public int wZpos;
            public int wButtons;
        }
        /// <summary>
        /// 游戏手柄的位置与按钮状态
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct JOYINFOEX
        {
            /// <summary>
            /// Size, in bytes, of this structure.
            /// </summary>
            public int dwSize;
            /// <summary>
            /// Flags indicating the valid information returned in this structure. Members that do not contain valid information are set to zero.
            /// </summary>
            public int dwFlags;
            /// <summary>
            /// Current X-coordinate.
            /// </summary>
            public int dwXpos;
            /// <summary>
            /// Current Y-coordinate.
            /// </summary>
            public int dwYpos;
            /// <summary>
            /// Current Z-coordinate.
            /// </summary>
            public int dwZpos;
            /// <summary>
            /// Current position of the rudder or fourth joystick axis.
            /// </summary>
            public int dwRpos;
            /// <summary>
            /// Current fifth axis position.
            /// </summary>
            public int dwUpos;
            /// <summary>
            /// Current sixth axis position.
            /// </summary>
            public int dwVpos;
            /// <summary>
            /// Current state of the 32 joystick buttons. The value of this member can be set to any combination of JOY_BUTTONn flags, where n is a value in the range of 1 through 32 corresponding to the button that is pressed.
            /// </summary>
            public uint dwButtons;
            /// <summary>
            /// Current button number that is pressed.
            /// </summary>
            public int dwButtonNumber;
            /// <summary>
            /// Current position of the point-of-view control. Values for this member are in the range 0 through 35,900. These values represent the angle, in degrees, of each view multiplied by 100. 
            /// </summary>
            public int dwPOV;
            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            public int dwReserved1;
            /// <summary>
            /// Reserved; do not use.
            /// </summary>
            public int dwReserved2;
        }
#endregion        
}
