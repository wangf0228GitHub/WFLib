using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WFNetLib.WinAPI
{
    public class Win32API
    {
#region Kernel32.dll
        [DllImport("Kernel32.dll")]
        public static extern int CreateFile(
        string lpFileName, // file name 
        uint dwDesiredAccess, // access mode 
        int dwShareMode, // share mode 
        int lpSecurityAttributes, // SD 
        int dwCreationDisposition, // how to create 
        int dwFlagsAndAttributes, // file attributes 
        int hTemplateFile // handle to template file 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool GetCommState(
        int hFile, // handle to communications device 
        ref DCB lpDCB // device-control block 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool BuildCommDCB(
        string lpDef, // device-control string 
        ref DCB lpDCB // device-control block 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool SetCommState(
        int hFile, // handle to communications device 
        ref DCB lpDCB // device-control block 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool GetCommTimeouts(
        int hFile, // handle to comm device 
        ref COMMTIMEOUTS lpCommTimeouts // time-out values 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool SetCommTimeouts(
        int hFile, // handle to comm device 
        ref COMMTIMEOUTS lpCommTimeouts // time-out values 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool ReadFile(
        int hFile, // handle to file 
        byte[] lpBuffer, // data buffer 
        int nNumberOfBytesToRead, // number of bytes to read 
        ref int lpNumberOfBytesRead, // number of bytes read 
        ref OVERLAPPED lpOverlapped // overlapped buffer 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool WriteFile(
        int hFile, // handle to file 
        byte[] lpBuffer, // data buffer 
        int nNumberOfBytesToWrite, // number of bytes to write 
        ref int lpNumberOfBytesWritten, // number of bytes written 
        ref OVERLAPPED lpOverlapped // overlapped buffer 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool CloseHandle(
        int hObject // handle to object 
        );

        [DllImport("Kernel32.dll")]
        public static extern bool ClearCommError(
        int hFile, // handle to file
        ref int lpErrors,
        ref COMSTAT lpStat
        );

        [DllImport("Kernel32.dll")]
        public static extern bool PurgeComm(
        int hFile, // handle to file
        uint dwFlags
        );

        [DllImport("Kernel32.dll")]
        public static extern bool SetupComm(
        int hFile,
        int dwInQueue,
        int dwOutQueue
        );

        // 声明读写INI文件的API函数
        [DllImport("Kernel32.dll")]
        public static extern long WritePrivateProfileString(
            string section,
            string key,
            string val,
            string filePath
            );

        [DllImport("Kernel32.dll")]
        public static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath
            );
        // 声明读写INI文件的API函数
        [DllImport("Kernel32.dll")]
        public static extern long WritePrivateProfileSection(
            string section,
            StringBuilder val,
            string filePath
            );

        [DllImport("Kernel32.dll")]
        public static extern long GetPrivateProfileSection(
            string section,
            StringBuilder retVal,
            int size,
            string filePath
            );
        [DllImport("Kernel32.dll")]
        public static extern bool Beep(int frequency, int duration);
#endregion
#region Ws2_32.dll
        [DllImport("Ws2_32.dll")]
        public static extern Int32 inet_addr(string ip);
        [DllImport("Ws2_32.dll")]
        public static extern string inet_ntoa(uint ip);
        [DllImport("Ws2_32.dll")]
        public static extern uint htonl(uint ip);
        [DllImport("Ws2_32.dll")]
        public static extern uint ntohl(uint ip);
        [DllImport("Ws2_32.dll")]
        public static extern ushort htons(ushort ip);
        [DllImport("Ws2_32.dll")]
        public static extern ushort ntohs(ushort ip);
#endregion    
#region Winmm.dll
        /// <summary>
        /// 检查系统是否配置了游戏端口和驱动程序。如果返回值为零，表明不支持操纵杆功能。如果返回值不为零，则说明系统支持游戏操纵杆功能。
        /// </summary>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyGetNumDevs();

        /// <summary>
        /// 获取某个游戏手柄的参数信息
        /// </summary>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <param name="pjc"></param>
        /// <param name="cbjc">JOYCAPS结构的大小</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyGetDevCaps(int uJoyID, ref JOYCAPS pjc, int cbjc);

        /// <summary>
        /// 向系统申请捕获某个游戏杆并定时将该设备的状态值通过消息发送到某个窗口 
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <param name="uPeriod">每隔给定的轮询间隔就给应用程序发送有关游戏杆的信息。这个参数是以毫妙为单位的轮询频率。</param>
        /// <param name="fChanged">是否允许程序当操纵杆移动一定的距离后才接受消息</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joySetCapture(IntPtr hWnd, int uJoyID, int uPeriod, bool fChanged);

        /// <summary>
        /// 释放操纵杆的捕获
        /// </summary>
        /// <param name="uJoyID">指定游戏杆(0-15)，它可以是JOYSTICKID1或JOYSTICKID2</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyReleaseCapture(int uJoyID);
        /// 获取操纵杆位置和按钮状态
        /// </summary>
        /// <param name="uJoyID"></param>
        /// <param name="pji"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyGetPos(int uJoyID, ref JOYINFO pji);
        /// <summary>
        /// 获取操纵杆位置和按钮状态
        /// </summary>
        /// <param name="uJoyID"></param>
        /// <param name="pji"></param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        public static extern int joyGetPosEx(int uJoyID, ref JOYINFOEX pji); 

#endregion
#region User32.dll
        [DllImport("User32.dll ", EntryPoint = "SetParent")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        [DllImport("User32.dll ", EntryPoint = "ShowWindow")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32", EntryPoint = "FindWindowA")]
        public static extern int FindWindow(
            string lpClassName,
            string lpWindowName
            );
        [DllImport("user32", EntryPoint = "FindWindowExA")]
        public static extern int FindWindowEx(
            int hwndParent,
            int hwndChildAfter,
            string lpszClass,		//窗口类
            string lpszWindow		//窗口标题
            );
        //枚举屏幕上所有顶级窗口（不会枚举子窗口，除了一些有WS_CHILD的顶级窗口）
        //API回调函数（在c＃中，一个委托实例代表一个函数的指针（入口地址））
        public delegate bool WNDENUMPROC(int hwnd, int lParam);
        [DllImport("user32")]
        public static extern bool EnumWindows(
            WNDENUMPROC lpEnumFunc,
            int lParam
            );


        public const int MB_ICONEXCLAMATION = 48;
        [DllImport("user32.dll", EntryPoint = "MessageBeep")]
        public static extern bool MessageBeep(uint uType);
#endregion
#region hid.dll
        //获得GUID
        [DllImport("hid.dll")]
        public static extern void HidD_GetHidGuid(ref Guid HidGuid);
        //释放设备
        [DllImport("hid.dll")]
        static public extern bool HidD_FreePreparsedData(ref IntPtr PreparsedData);
#endregion
#region setupapi.dll
        //过滤设备，获取需要的设备
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid, uint Enumerator, IntPtr HwndParent, DIGCF Flags);

        //获取设备，true获取到
        [DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean SetupDiEnumDeviceInterfaces(IntPtr hDevInfo, IntPtr devInfo, ref Guid interfaceClassGuid, UInt32 memberIndex, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData);

        public struct SP_DEVICE_INTERFACE_DATA
        {
            public int cbSize;
            public Guid interfaceClassGuid;
            public int flags;
            public int reserved;
        }
        // 获取接口的详细信息 必须调用两次 第1次返回长度 第2次获取数据 
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref SP_DEVICE_INTERFACE_DATA deviceInterfaceData, IntPtr deviceInterfaceDetailData,
            int deviceInterfaceDetailDataSize, ref int requiredSize, SP_DEVINFO_DATA deviceInfoData);
        [StructLayout(LayoutKind.Sequential)]
        public class SP_DEVINFO_DATA
        {
            public int cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
            public Guid classGuid = Guid.Empty; // temp
            public int devInst = 0; // dumy
            public int reserved = 0;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            internal int cbSize;
            internal short devicePath;
        }
        public enum DIGCF
        {
            DIGCF_DEFAULT = 0x1,
            DIGCF_PRESENT = 0x2,
            DIGCF_ALLCLASSES = 0x4,
            DIGCF_PROFILE = 0x8,
            DIGCF_DEVICEINTERFACE = 0x10
        }
#endregion
    }
}
