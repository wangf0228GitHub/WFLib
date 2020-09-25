using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib.PID
{
    public class incrementPID
    {
        public double uk; //输出值
        public double sp; //目标值
        public double pgain;//比例
        public double igain;//积分
        public double dgain;//微分
        public double deadband;//无需调节的范围
        public double ek1;//上次偏差
        public double ek2;//上次偏差
        public bool bOk;
        public incrementPID()
        {
            sp = 0;
            pgain = 0;
            igain = 0;
            dgain = 0;
            deadband = 0;
            ek1 = 0;
            ek2 = 0;
            bOk = false;
        }
        public void ResetPIDParam()
        {
            ek1 = 0;
            ek2 = 0;
            bOk = false;
        }
        public double PIDCalc(double pv)
        {
            double ek;  
            ek = sp - pv;
            if (Math.Abs(ek) > deadband)
            {
                bOk = false;
                uk = uk + pgain * (ek - ek1) + igain * ek + dgain * (ek - 2 * ek1 + ek2);
                uk = uk + pgain * (ek - ek1);// 只调节P
                uk = uk + pgain * (ek - ek1) + igain * ek;//只调节PI                
            }
            else
                bOk = true;
            ek2 = ek1;
            ek1 = ek;
            return uk; 
        }
    }
    public class positionPID
    {
        public double uk; //当前值
        public bool bOk;
        public double sp; //目标值
        public double integral;//积分结果
        public double pgain;//比例
        public double igain;//积分
        public double dgain;//微分
        public double deadband;//无需调节的范围
        public double last_error;//上次偏差
        public positionPID()
        {
            uk = 0;
            sp = 0;
            integral = 0;
            pgain = 0;
            igain = 0;
            dgain = 0;
            deadband = 0;
            last_error = 0;
        }
        public void ResetPIDParam()
        {
            integral = 0;
            last_error = 0;
        }
        public double PIDCalc(double pv)
        {
            double err;
            double pterm, dterm, result;
            err = sp - pv;
            if (Math.Abs(err) > deadband)
            {
                bOk = false;
                pterm = pgain * err;
                integral += igain * err;
                //                 if (pterm > 100 || pterm < -100)   
                //                 {   
                //                     pidParam.integral = 0;   //偏差过大不进行I调节
                //                 }   
                //                 else   
                //                 {   
                //                     pidParam.integral += pidParam.igain * err;   
                //                     if (pidParam.integral > 100.0f)   
                //                     {   
                //                         pidParam.integral = 100.0f;   
                //                     }   
                //                     else if (pidParam.integral < 0.0) 
                //                         pidParam.integral = 0.0f;   
                //                 }   
                dterm = (err - last_error) * dgain;
                result = pterm + integral + dterm;
                uk = result;
            }
            else
            {
                bOk = true;
                result = uk;//pidParam.integral;   
            }
            last_error = err;
            return (result);
        }
    }
}
