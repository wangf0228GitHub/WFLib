using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WFNetLib
{
#region 游戏手柄的常数
    public class Joy
    {
    #region 按钮定义
        public const int JOY_BUTTON1 = 0x0001;

        public const int JOY_BUTTON2 = 0x0002;

        public const int JOY_BUTTON3 = 0x0004;

        public const int JOY_BUTTON4 = 0x0008;

        public const int JOY_BUTTON5 = 0x0010;

        public const int JOY_BUTTON6 = 0x0020;

        public const int JOY_BUTTON7 = 0x0040;

        public const int JOY_BUTTON8 = 0x0080;

        public const int JOY_BUTTON9 = 0x0100;

        public const int JOY_BUTTON10 = 0x0200;

        //Button up/down
        public const int JOY_BUTTON1CHG = 0x0100;

        public const int JOY_BUTTON2CHG = 0x0200;

        public const int JOY_BUTTON3CHG = 0x0400;

        public const int JOY_BUTTON4CHG = 0x0800;
    #endregion    

    #region 手柄Id定义
        /// <summary>
        /// 主游戏手柄Id
        /// </summary>
        public const int JOYSTICKID1 = 0;
        /// <summary>
        /// 副游戏手柄Id
        /// </summary>
        public const int JOYSTICKID2 = 1;
    #endregion

    #region 错误号定义
        /// <summary>
        /// 没有错误
        /// </summary>
        public const int JOYERR_NOERROR = 0;
        /// <summary>
        /// 参数错误
        /// </summary>
        public const int JOYERR_PARMS = 165;
        /// <summary>
        /// 无法正常工作
        /// </summary>
        public const int JOYERR_NOCANDO = 166;
        /// <summary>
        /// 操纵杆未连接 
        /// </summary>
        public const int JOYERR_UNPLUGGED = 167;
    #endregion 
    }
#endregion
#region 游戏手柄的事件参数
    public class JoystickEventArgs : EventArgs
    {
        /// <summary>
        /// 游戏手柄的事件参数
        /// </summary>
        /// <param name="joystickId">手柄Id</param>
        /// <param name="buttons">按钮</param>
        public JoystickEventArgs(int joystickId, JoystickButtons buttons)
        {
            this.JoystickId = joystickId;
            this.Buttons = buttons;
        }
        /// <summary>
        /// 手柄Id
        /// </summary>
        public int JoystickId { get; private set; }
        /// <summary>
        /// 按钮
        /// </summary>
        public JoystickButtons Buttons { get; private set; }
    }

    /// <summary>
    /// 游戏手柄的按钮定义
    /// </summary>
    [Flags]
    public enum JoystickButtons
    {
        //没有任何按钮
        None = 0x0,
        UP = 0x01,
        Down = 0x02,
        Left = 0x04,
        Right = 0x08,
        B1 = 0x10,
        B2 = 0x20,
        B3 = 0x40,
        B4 = 0x80,
        B5 = 0x100,
        B6 = 0x200,
        B7 = 0x400,
        B8 = 0x800,
        B9 = 0x1000,
        B10 = 0x2000
    }
#endregion
    
#region 游戏手柄的事件
    public class Joystick : IMessageFilter, IDisposable
    {
    #region 事件定义
        /// <summary>
        /// 按钮被单击
        /// </summary>
        public event EventHandler<JoystickEventArgs> Click;
        /// <summary>
        /// 按钮被弹起
        /// </summary>
        public event EventHandler<JoystickEventArgs> ButtonUp;
        /// <summary>
        /// 按钮已被按下
        /// </summary>
        public event EventHandler<JoystickEventArgs> ButtonDown;
        /// <summary>
        /// 触发单击事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnClick(JoystickEventArgs e)
        {
            EventHandler<JoystickEventArgs> h = this.Click;
            if (h != null) 
                h(this, e);
        }
        /// <summary>
        /// 触发按钮弹起事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnButtonUp(JoystickEventArgs e)
        {
            EventHandler<JoystickEventArgs> h = this.ButtonUp;
            if (h != null) 
                h(this, e);
        }
        /// <summary>
        /// 触发按钮按下事件
        /// </summary>
        /// <param name="e"></param>
        protected void OnButtonDown(JoystickEventArgs e)
        {
            EventHandler<JoystickEventArgs> h = this.ButtonDown;
            if (h != null) 
                h(this, e);
        }
        /// <summary>
        /// 是否已注册消息
        /// </summary>
        private bool IsRegister = false;
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="hWnd">需要捕获手柄消息的窗口</param>
        /// <param name="joystickId">要捕获的手柄Id</param>
        public bool Register(IntPtr hWnd, int joystickId)
        {
            bool flag = false;
            int result = 0;
            WinAPI.JOYCAPS caps = new WinAPI.JOYCAPS();
            if (WinAPI.Win32API.joyGetNumDevs() != 0)
            {
                //拥有手柄.则判断手柄状态
                result = WinAPI.Win32API.joyGetDevCaps(joystickId, ref caps, Marshal.SizeOf(typeof(WinAPI.JOYCAPS)));
                if (result == Joy.JOYERR_NOERROR)
                {
                    //手柄处于正常状态
                    flag = true;
                }
            }
            if (flag)
            {
                //注册消息
                if (!this.IsRegister)
                {
                    Application.AddMessageFilter(this);
                }
                this.IsRegister = true;

                result = WinAPI.Win32API.joySetCapture(hWnd, joystickId, caps.wPeriodMin * 2, false);
                if (result != Joy.JOYERR_NOERROR)
                {
                    flag = false;
                }
            }
            return flag;
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <param name="joystickId"></param>
        public void UnRegister(int joystickId)
        {
            if (this.IsRegister)
            {
                WinAPI.Win32API.joyReleaseCapture(joystickId);
            }
        }
    #endregion

    #region 消息处理
        #region IMessageFilter 成员
            /// <summary>
            /// 处理系统消息.
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            bool IMessageFilter.PreFilterMessage(ref Message m)
            {
                bool flag = false;
                if (m.HWnd != IntPtr.Zero && (m.WParam != IntPtr.Zero || m.LParam != IntPtr.Zero))
                {
                    Action<JoystickEventArgs> action = null;
                    JoystickButtons buttons = JoystickButtons.None;
                    int joystickId = -1;
                    switch (m.Msg)
                    {
                        case WinAPI.APIConstant.MM_JOY1MOVE:
                        case WinAPI.APIConstant.MM_JOY2MOVE:
                            //单击事件
                            buttons = GetButtonsStateFromMessageParam(m.WParam.ToInt32(), m.LParam.ToInt32());
                            action = this.OnClick;
                            joystickId = (m.Msg == WinAPI.APIConstant.MM_JOY1MOVE ? Joy.JOYSTICKID1 : Joy.JOYSTICKID2);
                            break;
                        case WinAPI.APIConstant.MM_JOY1BUTTONDOWN:
                        case WinAPI.APIConstant.MM_JOY2BUTTONDOWN:
                            //按钮被按下
                            buttons = GetButtonsPressedStateFromMessageParam(m.WParam.ToInt32(), m.LParam.ToInt32());
                            action = this.OnButtonDown;
                            joystickId = (m.Msg == WinAPI.APIConstant.MM_JOY1BUTTONDOWN ? Joy.JOYSTICKID1 : Joy.JOYSTICKID2);
                            break;
                        case WinAPI.APIConstant.MM_JOY1BUTTONUP:
                        case WinAPI.APIConstant.MM_JOY2BUTTONUP:
                            //按钮被弹起
                            buttons = GetButtonsPressedStateFromMessageParam(m.WParam.ToInt32(), m.LParam.ToInt32());
                            action = this.OnButtonUp;
                            joystickId = (m.Msg == WinAPI.APIConstant.MM_JOY1BUTTONUP ? Joy.JOYSTICKID1 : Joy.JOYSTICKID2);
                            break;
                    }
                    if (action != null && joystickId != -1 && buttons != JoystickButtons.None)
                    {
                        //阻止消息继续传递
                        flag = true;
                        //触发事件
                        action(new JoystickEventArgs(joystickId, buttons));
                    }
                }
                return flag;
            }
        #endregion
        /// <summary>
        /// 根据消息的参数获取按钮的状态值
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private JoystickButtons GetButtonsStateFromMessageParam(int wParam, int lParam)
        {
            JoystickButtons buttons = JoystickButtons.None;
            if ((wParam & Joy.JOY_BUTTON1) == Joy.JOY_BUTTON1)
            {
                buttons |= JoystickButtons.B1;
            }
            if ((wParam & Joy.JOY_BUTTON2) == Joy.JOY_BUTTON2)
            {
                buttons |= JoystickButtons.B2;
            }
            if ((wParam & Joy.JOY_BUTTON3) == Joy.JOY_BUTTON3)
            {
                buttons |= JoystickButtons.B3;
            }
            if ((wParam & Joy.JOY_BUTTON4) == Joy.JOY_BUTTON4)
            {
                buttons |= JoystickButtons.B4;
            }
            if ((wParam & Joy.JOY_BUTTON5) == Joy.JOY_BUTTON5)
            {
                buttons |= JoystickButtons.B5;
            }
            if ((wParam & Joy.JOY_BUTTON6) == Joy.JOY_BUTTON6)
            {
                buttons |= JoystickButtons.B6;
            }
            if ((wParam & Joy.JOY_BUTTON7) == Joy.JOY_BUTTON7)
            {
                buttons |= JoystickButtons.B7;
            }
            if ((wParam & Joy.JOY_BUTTON8) == Joy.JOY_BUTTON8)
            {
                buttons |= JoystickButtons.B8;
            }
            if ((wParam & Joy.JOY_BUTTON9) == Joy.JOY_BUTTON9)
            {
                buttons |= JoystickButtons.B9;
            }
            if ((wParam & Joy.JOY_BUTTON10) == Joy.JOY_BUTTON10)
            {
                buttons |= JoystickButtons.B10;
            }

            GetXYButtonsStateFromLParam(lParam, ref buttons);

            return buttons;
        }
        /// <summary>
        /// 根据消息的参数获取按钮的按压状态值
        /// </summary>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private JoystickButtons GetButtonsPressedStateFromMessageParam(int wParam, int lParam)
        {
            JoystickButtons buttons = JoystickButtons.None;
            if ((wParam & Joy.JOY_BUTTON1CHG) == Joy.JOY_BUTTON1CHG)
            {
                buttons |= JoystickButtons.B1;
            }
            if ((wParam & Joy.JOY_BUTTON2CHG) == Joy.JOY_BUTTON2CHG)
            {
                buttons |= JoystickButtons.B2;
            }
            if ((wParam & Joy.JOY_BUTTON3CHG) == Joy.JOY_BUTTON3CHG)
            {
                buttons |= JoystickButtons.B3;
            }
            if ((wParam & Joy.JOY_BUTTON4CHG) == Joy.JOY_BUTTON4CHG)
            {
                buttons |= JoystickButtons.B4;
            }

            GetXYButtonsStateFromLParam(lParam, ref buttons);

            return buttons;
        }
        /// <summary>
        /// 获取X,Y轴的状态
        /// </summary>
        /// <param name="lParam"></param>
        /// <param name="buttons"></param>
        private void GetXYButtonsStateFromLParam(int lParam, ref JoystickButtons buttons)
        {
            //处理X,Y轴
            int x = lParam & 0x0000FFFF;                //低16位存储X轴坐标
            int y = (int)((lParam & 0xFFFF0000) >> 16); //高16位存储Y轴坐标(不直接移位是为避免0xFFFFFF)
            int m = 0x7EFF;                             //中心点的值,
            if (x > m)
            {
                buttons |= JoystickButtons.Right;
            }
            else if (x < m)
            {
                buttons |= JoystickButtons.Left;
            }
            if (y > m)
            {
                buttons |= JoystickButtons.Down;
            }
            else if (y < m)
            {
                buttons |= JoystickButtons.UP;
            }
        }
    #endregion

    #region IDisposable 成员

        public void Dispose()
        {
            Application.RemoveMessageFilter(this);
        }

    #endregion
    }
#endregion
    #region JOYINFOEX.dwFlags值的定义
    public class JOYINFOEX_dwFlags
    {    
        public const long JOY_RETURNX = 0x1;
        public const long JOY_RETURNY = 0x2;
        public const long JOY_RETURNZ = 0x4;
        public const long JOY_RETURNR = 0x8;
        public const long JOY_RETURNU = 0x10;
        public const long JOY_RETURNV = 0x20;
        public const long JOY_RETURNPOV = 0x40;
        public const long JOY_RETURNBUTTONS = 0x80;
        public const long JOY_RETURNRAWDATA = 0x100;
        public const long JOY_RETURNPOVCTS = 0x200;
        public const long JOY_RETURNCENTERED = 0x400;
        public const long JOY_USEDEADZONE = 0x800;
        public const long JOY_RETURNALL = (JOY_RETURNX | JOY_RETURNY | JOY_RETURNZ | JOY_RETURNR | JOY_RETURNU | JOY_RETURNV | JOY_RETURNPOV | JOY_RETURNBUTTONS);
        public const long JOY_CAL_READALWAYS = 0x10000;
        public const long JOY_CAL_READRONLY = 0x2000000;
        public const long JOY_CAL_READ3 = 0x40000;
        public const long JOY_CAL_READ4 = 0x80000;
        public const long JOY_CAL_READXONLY = 0x100000;
        public const long JOY_CAL_READYONLY = 0x200000;
        public const long JOY_CAL_READ5 = 0x400000;
        public const long JOY_CAL_READ6 = 0x800000;
        public const long JOY_CAL_READZONLY = 0x1000000;
        public const long JOY_CAL_READUONLY = 0x4000000;
        public const long JOY_CAL_READVONLY = 0x8000000;
    }
    #endregion
}
