using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;

namespace WFNetLib.WeiXin
{
    public class BaseSupport
    {
        private static AccessToken _access_token=new AccessToken();
        public static ManualResetEvent RefreshEvent=new ManualResetEvent(false);
        static object oLock = new object();
        public static string appID = "";
        public static string appsecret = "";
        public static AccessToken access_token
        {
            get { return _access_token; }
            //set { _ID = value; }
        }
        public static bool RefreshAccessToken(DateTime dt)
        {
            lock (oLock)
            {
                if (dt < _access_token.GetData)
                {
                    //已经更新过了
                    return true;
                }
                WFHttpWebResponse WebResponse = new WFHttpWebResponse();
                string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=";//APPID&secret=APPSECRET";
                url += appID;
                url += "&secret=";
                url += appsecret;
                HttpWebResponse hr = WebResponse.CreateGetHttpResponse(url);
                DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(BaseJsonData));
                MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(WebResponse.Content));
                BaseJsonData calldata = (BaseJsonData)s.ReadObject(stream);
                if (calldata.access_token != null)//获取成功
                {
                    _access_token.GetData = DateTime.Now;
                    _access_token.value = calldata.access_token;
                    _access_token.expires_in = calldata.expires_in;
                }
            }
            return true;
        }
        public static string GetSerIP()
        {
            WFHttpWebResponse WebResponse = new WFHttpWebResponse();
            string url = "https://api.weixin.qq.com/cgi-bin/getcallbackip?access_token=";//APPID&secret=APPSECRET";
            url += _access_token.value;
            HttpWebResponse hr = WebResponse.CreateGetHttpResponse(url);
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(BaseJsonData));
            MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(WebResponse.Content));
            BaseJsonData calldata = (BaseJsonData)s.ReadObject(stream);
            if (calldata.ip_list != null)//获取成功
            {
                return calldata.ip_list;
            }
            else
                return null;
        }
    }
    public class AccessToken
    {
        public string value;
        public DateTime GetData;
        public int expires_in;
        public AccessToken()
        {
            value = "";
            GetData = DateTime.Now.AddDays(-6);
            expires_in = 0;
        }
    }
    [System.Runtime.Serialization.DataContract]
    public class BaseJsonData
    {
        [System.Runtime.Serialization.DataMember]
        public string access_token;
        [System.Runtime.Serialization.DataMember]
        public int expires_in;
        [System.Runtime.Serialization.DataMember]
        public string errmsg;
        [System.Runtime.Serialization.DataMember]
        public int errcode;
        [System.Runtime.Serialization.DataMember]
        public string ip_list;
    }
}
