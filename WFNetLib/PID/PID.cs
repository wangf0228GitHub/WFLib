using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib.PID
{
    public class PID
    {
        public PIDParam pidParam;
        public PID()
        {
            pidParam=new PIDParam();
        }
        public float PIDCalc(float pv)
        {
            float err;   
            float pterm, dterm, result;   
            pidParam.pv=pv;
            err = (pidParam.sp) - (pidParam.pv);
            if (Math.Abs(err) > pidParam.deadband)
            {
                pterm = pidParam.pgain * err;
                pidParam.integral += pidParam.igain * err;
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
                dterm = (err - pidParam.last_error) * pidParam.dgain;
                result = pterm + pidParam.integral + dterm;
            }
            else
                result = 0;//pidParam.integral;   
            pidParam.last_error = err;   
            return (result); 
        }
    }
    public class PIDParam
    {
        public float pv; //当前值
        public float sp; //目标值
        public float integral;//积分结果
        public float pgain;//比例
        public float igain;//积分
        public float dgain;//微分
        public float deadband;//无需调节的范围
        public float last_error;//上次偏差
        public PIDParam()
        {
            pv = 0;
            sp = 0;
            integral = 0;
            pgain = 0;
            igain = 0;
            dgain = 0;
            deadband = 0;
            last_error = 0;
        }
    };   
}
