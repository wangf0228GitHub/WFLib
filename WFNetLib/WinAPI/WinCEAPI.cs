using System;
using System.Collections.Generic;

using System.Text;
using System.Runtime.InteropServices;

namespace WFNetLib.WinAPI
{
    class WinCEAPI
    {
        [DllImport("coredll")]
        public static extern int CreateFile(
        string lpFileName, // file name 
        uint dwDesiredAccess, // access mode 
        int dwShareMode, // share mode 
        int lpSecurityAttributes, // SD 
        int dwCreationDisposition, // how to create 
        int dwFlagsAndAttributes, // file attributes 
        int hTemplateFile // handle to template file 
        );


        [DllImport("coredll")]
        public static extern bool GetCommState(
        int hFile, // handle to communications device 
        ref DCB lpDCB // device-control block 
        );

        [DllImport("coredll")]
        public static extern bool BuildCommDCB(
        string lpDef, // device-control string 
        ref DCB lpDCB // device-control block 
        );

        [DllImport("coredll")]
        public static extern bool SetCommState(
        int hFile, // handle to communications device 
        ref DCB lpDCB // device-control block 
        );

        [DllImport("coredll")]
        public static extern bool GetCommTimeouts(
        int hFile, // handle to comm device 
        ref COMMTIMEOUTS lpCommTimeouts // time-out values 
        );

        [DllImport("coredll")]
        public static extern bool SetCommTimeouts(
        int hFile, // handle to comm device 
        ref COMMTIMEOUTS lpCommTimeouts // time-out values 
        );

        [DllImport("coredll")]
        public static extern bool ReadFile(
        int hFile, // handle to file 
        byte[] lpBuffer, // data buffer 
        int nNumberOfBytesToRead, // number of bytes to read 
        ref int lpNumberOfBytesRead, // number of bytes read 
        ref OVERLAPPED lpOverlapped // overlapped buffer 
        );

        [DllImport("coredll")]
        public static extern bool WriteFile(
        int hFile, // handle to file 
        byte[] lpBuffer, // data buffer 
        int nNumberOfBytesToWrite, // number of bytes to write 
        ref int lpNumberOfBytesWritten, // number of bytes written 
        ref OVERLAPPED lpOverlapped // overlapped buffer 
        );

        [DllImport("coredll")]
        public static extern bool CloseHandle(
        int hObject // handle to object 
        );

        [DllImport("coredll")]
        public static extern bool ClearCommError(
        int hFile, // handle to file
        ref int lpErrors,
        ref COMSTAT lpStat
        );

        [DllImport("coredll")]
        public static extern bool PurgeComm(
        int hFile, // handle to file
        uint dwFlags
        );

        [DllImport("coredll")]
        public static extern bool SetupComm(
        int hFile,
        int dwInQueue,
        int dwOutQueue
        );       
    }

}
