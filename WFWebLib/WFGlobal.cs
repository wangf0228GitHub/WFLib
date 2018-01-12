using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace WFWebLib
{
    public class WFGlobal
    {
        public static void ShowAlart(System.Web.UI.Page page, string msg)
        {
            msg = msg.Replace("'", "‘");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", "<script language='javascript' defer>alert('" + msg.ToString() + "');</script>");
        }
        public static void ShowAlertAndRedirect(System.Web.UI.Page page,string msg, string url)
        {
            msg = msg.Replace("'", "‘");
            page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "<script>setTimeout(function(){alert('" + msg + "');document.location.href='" + url + "'},50);</script>");
            //page.ClientScript.RegisterStartupScript(page.GetType(), "alert", "<script>alert('" + msg + "');document.location.href='" + url + "';</script>");
        } 
        static public TValue ParseValue<TValue>(string value)
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(TValue));
            return (TValue)converter.ConvertFromInvariantString(value);
        }
        static public void GetMaxMinIndex(double[] values, out int minIndex, out int maxIndex)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            minIndex = maxIndex = 0;
            double minimum = Double.MaxValue;
            double maximum = Double.MinValue;

            for (int i = 0; i < values.Length; ++i)
            {
                double currentValue = values[i];

                if (currentValue < minimum)
                {
                    minimum = currentValue;
                    minIndex = i;
                }

                if (currentValue > maximum)
                {
                    maximum = currentValue;
                    maxIndex = i;
                }
            }
        }
        static public void GetMaxMinValue(double[] values, out double minimum, out double maximum)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            minimum = Double.MaxValue;
            maximum = Double.MinValue;

            for (int i = 0; i < values.Length; ++i)
            {
                double currentValue = values[i];

                if (currentValue < minimum)
                    minimum = currentValue;

                if (currentValue > maximum)
                    maximum = currentValue;
            }
        }
        static public double GetAverageValue(double[] values)
        {
            if (values == null)
                throw new ArgumentNullException("values");

            double sum = 0.0;
            for (int i = 0; i < values.Length; ++i)
                sum += values[i];

            return sum / values.Length;
        }
        /// <summary>
        /// 初始化程序执行时间
        /// </summary>
        public static DateTime dtStartTime = DateTime.MinValue;


        /// <summary>
        /// 系统运行时信息，用于调试
        /// </summary>
        public static StringBuilder writeLine = new StringBuilder();
    }
}
