using System;
using System.Collections.Generic;

using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using System.Management;
using System.IO;
using System.Web.Security;
using System.Data;
using Microsoft.VisualBasic;
using WFNetLib.Strings.CryptoService;

namespace WFNetLib.StringFunc
{
    public partial class StringsFunction 
    {
    #region 正则表达式的使用

        /// <summary>
        /// 判断输入的字符串是否完全匹配正则
        /// </summary>
        /// <param name="RegexExpression">正则表达式</param>
        /// <param name="str">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsValiable(string RegexExpression, string str)
        {
            bool blResult = false;

            Regex rep = new Regex(RegexExpression, RegexOptions.IgnoreCase);

            //blResult = rep.IsMatch(str);
            Match mc = rep.Match(str);

            if (mc.Success)
            {
                if (mc.Value == str) blResult = true;
            }


            return blResult;
        }
        /// <summary>
        /// 判断输入的用户名是否符合规则，用户名由minL-maxL位的字母或数字组成或下划线
        /// </summary>
        /// <param name="minL">最短长度</param>
        /// <param name="maxL">最大长度</param>
        /// <param name="str">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsValiable_UserName(string str, int minL, int maxL)
        {
            bool blResult = false;
            string RegexExpression = string.Format(@"^\w{{{0},{1}}}$", minL, maxL);
            Regex rep = new Regex(RegexExpression, RegexOptions.IgnoreCase);

            //blResult = rep.IsMatch(str);
            Match mc = rep.Match(str);

            if (mc.Success)
            {
                if (mc.Value == str) blResult = true;
            }


            return blResult;
        }
        /// <summary>
        /// 判断输入的用户名是否符合规则，用户名由4-16位的字母或数字组成或下划线
        /// </summary>
        /// <param name="str">待判断的字符串</param>
        /// <returns></returns>
        public static bool IsValiable_UserName(string str)
        {
            return IsValiable_UserName(str, 4, 16);
        }

        /// <summary>
        /// 转换代码中的URL路径为绝对URL路径
        /// </summary>
        /// <param name="sourceString">源代码</param>
        /// <param name="replaceURL">替换要添加的URL</param>
        /// <returns>string</returns>
        public static string convertURL(string sourceString, string replaceURL)
        {
            Regex rep = new Regex(" (src|href|background|value)=('|\"|)([^('|\"|)http://].*?)('|\"| |>)");
            sourceString = rep.Replace(sourceString, " $1=$2" + replaceURL + "$3$4");

            return sourceString;
        }

        /// <summary>
        /// 获取代码中所有图片的以HTTP开头的URL地址
        /// </summary>
        /// <param name="sourceString">代码内容</param>
        /// <returns>ArrayList</returns>
        public static ArrayList GetImgFileUrl(string sourceString)
        {
            ArrayList imgArray = new ArrayList();

            Regex r = new Regex("<IMG(.*?)src=('|\"|)(http://.*?)('|\"| |>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection mc = r.Matches(sourceString);
            for (int i = 0; i < mc.Count; i++)
            {
                if (!imgArray.Contains(mc[i].Result("$3")))
                {
                    imgArray.Add(mc[i].Result("$3"));
                }
            }

            return imgArray;
        }

        /// <summary>
        /// 获取代码中所有文件的以HTTP开头的URL地址
        /// </summary>
        /// <param name="sourceString">代码内容</param>
        /// <returns>ArrayList</returns>
        public static Hashtable getFileUrlPath(string sourceString)
        {
            Hashtable url = new Hashtable();

            Regex r = new Regex(" (src|href|background|value)=('|\"|)(http://.*?)('|\"| |>)",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

            MatchCollection mc = r.Matches(sourceString);
            for (int i = 0; i < mc.Count; i++)
            {
                if (!url.ContainsValue(mc[i].Result("$3")))
                {
                    url.Add(i, mc[i].Result("$3"));
                }
            }

            return url;
        }

        /// <summary>
        /// 获取一条SQL语句中的所参数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static ArrayList SqlParame(string sql)
        {
            ArrayList list = new ArrayList();
            Regex r = new Regex(@"@(?<x>[0-9a-zA-Z]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection mc = r.Matches(sql);
            for (int i = 0; i < mc.Count; i++)
            {
                list.Add(mc[i].Result("$1"));
            }

            return list;
        }

        /// <summary>
        /// 获取一条SQL语句中的所参数
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns></returns>
        public static ArrayList OracleParame(string sql)
        {
            ArrayList list = new ArrayList();
            Regex r = new Regex(@":(?<x>[0-9a-zA-Z]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection mc = r.Matches(sql);
            for (int i = 0; i < mc.Count; i++)
            {
                list.Add(mc[i].Result("$1"));
            }

            return list;
        }

        /// <summary>
        /// 将HTML代码转化成纯文本
        /// </summary>
        /// <param name="sourceHTML">HTML代码</param>
        /// <returns></returns>
        public static string convertText(string sourceHTML)
        {
            string strResult = "";
            Regex r = new Regex("<(.*?)>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection mc = r.Matches(sourceHTML);

            if (mc.Count == 0)
            {
                strResult = sourceHTML;
            }
            else
            {
                strResult = sourceHTML;

                for (int i = 0; i < mc.Count; i++)
                {
                    strResult = strResult.Replace(mc[i].ToString(), "");
                }
            }

            return strResult;
        }
        #endregion

    #region 对字符串的加密/解密

        /// <summary>
        /// 对字符串进行适应 ServU 的 MD5 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string strServUPWD(string str)
        {
            string strResult = "";
            strResult = RandomSTR(2);
            str = strResult + str;
            str = NoneEncrypt(str, 1);
            str = strResult + str;

            return str;
        }

        /// <summary>
        /// 对字符串进行加密（不可逆）
        /// </summary>
        /// <param name="Password">要加密的字符串</param>
        /// <param name="Format">加密方式,0 is SHA1,1 is MD5</param>
        /// <returns></returns>
        public static string NoneEncrypt(string Password, int Format)
        {
            string strResult = "";
            switch (Format)
            {
                case 0:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "SHA1");
                    break;
                case 1:
                    strResult = FormsAuthentication.HashPasswordForStoringInConfigFile(Password, "MD5");
                    break;
                default:
                    strResult = Password;
                    break;
            }

            return strResult;
        }


        /// <summary>
        /// 对字符串进行加密
        /// </summary>
        /// <param name="Passowrd">待加密的字符串</param>
        /// <returns>string</returns>
        public static string Encrypt(string Passowrd)
        {
            string strResult = "";

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(Passowrd, true, 2);
            strResult = FormsAuthentication.Encrypt(ticket).ToString();

            return strResult;
        }


        /// <summary>
        /// 对字符串进行解密
        /// </summary>
        /// <param name="Passowrd">已加密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string Passowrd)
        {
            string strResult = "";

            strResult = FormsAuthentication.Decrypt(Passowrd).Name.ToString();

            return strResult;
        }
        /// <summary>
        /// 可选择使用 DES/RSA 双向加密函数
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <param name="keyValue">对称加密密钥(密钥长度能且只能是8个字母) </param>
        /// <param name="privateKeyFilePath">非对称加密的私钥文件地址</param>
        /// <returns>string</returns>
        public static string EncryptString(string str, string keyValue, string privateKeyFilePath)
        {
            string strResult = str;

            if (!StringsFunction.CheckValiable(str)) return strResult;


            // 判断是否指定了对称加密的密钥， 指定则采用 DES 对称加密算法进行加密
            if (StringsFunction.CheckValiable(keyValue))
            {
                DESCrypto dc = new DESCrypto();
                strResult = dc.EncryptString(strResult, keyValue, keyValue);
            }


            // 判断非对称加密的私钥文件是否指定，指定则使用非对称加密算法（RSA）对其进行二次加密
            if (StringsFunction.CheckValiable(privateKeyFilePath))
            {
                string privateKey = FileOP.ReadFile(GetRealFile(privateKeyFilePath));
                Regex r = new Regex(@"<Modulus>(.*?)</Exponent>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                MatchCollection mc = r.Matches(privateKey);
                string publicKey = "<RSAKeyValue><Modulus>" + mc[0].Groups[1].Value + "</Exponent></RSAKeyValue>";


                RSACrypto rc = new RSACrypto();
                strResult = rc.RSAEncrypt(publicKey, strResult);

            }

            return strResult;
        }

        /// <summary>
        /// 可选择使用 DES/RSA 双向解密函数
        /// </summary>
        /// <param name="str">待解密的字符串</param>
        /// <param name="privateKeyFilePath">非对称加密的私钥文件地址</param>
        /// <param name="keyValue">对称加密密钥(密钥长度能且只能是8个字母)</param>
        /// <returns>string</returns>
        public static string DecryptString(string str, string privateKeyFilePath, string keyValue)
        {
            string strResult = str;

            if (!StringsFunction.CheckValiable(str)) return strResult;


            // 判断非对称加密的私钥文件是否指定，指定则使用非对称加密算法（RSA）对其进行解密
            if (StringsFunction.CheckValiable(privateKeyFilePath))
            {
                string privateKey = FileOP.ReadFile(GetRealFile(privateKeyFilePath));

                RSACrypto rc = new RSACrypto();
                strResult = rc.RSADecrypt(privateKey, strResult);
            }


            // 判断是否指定了对称加密的密钥， 指定则采用 DES 对称加密算法进行解密
            if (StringsFunction.CheckValiable(keyValue))
            {
                DESCrypto dc = new DESCrypto();
                strResult = dc.DecryptString(strResult, keyValue);
            }

            return strResult;
        }
    #endregion
        /// <summary>
        /// 获取一个目录的绝对路径（适用于WEB应用程序）
        /// </summary>
        /// <param name="folderPath">目录路径</param>
        /// <returns></returns>
        public static string GetRealPath(string folderPath)
        {
            string strResult = "";

            if (folderPath.IndexOf(":\\") > 0)
            {
                strResult = AddLast(folderPath, "\\");
            }
            else
            {
                if (folderPath.StartsWith("~/"))
                {
                    strResult = AddLast(System.Web.HttpContext.Current.Server.MapPath(folderPath), "\\");
                }
                else
                {
                    string webPath = System.Web.HttpContext.Current.Request.ApplicationPath + "/";
                    strResult = AddLast(System.Web.HttpContext.Current.Server.MapPath(webPath + folderPath), "\\");
                }
            }

            return strResult;
        }

        /// <summary>
        /// 获取一个文件的绝对路径（适用于WEB应用程序）
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>string</returns>
        public static string GetRealFile(string filePath)
        {
            string strResult = "";

            //strResult = ((file.IndexOf(@":\") > 0 || file.IndexOf(":/") > 0) ? file : System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/" + file));
            strResult = ((filePath.IndexOf(":\\") > 0) ?
                filePath :
                System.Web.HttpContext.Current.Server.MapPath(filePath));

            return strResult;
        }
    }
}
