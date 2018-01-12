using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Windows.Forms;
using WFNetLib.StringFunc;

namespace WFNetLib.Net
{
    public class HttpStatic
    {

        public static CookieCollection GetCookies(string strCookies)
        {
            CookieCollection Cookies = new CookieCollection();
            foreach (string str in strCookies.Split(','))
            {
                Cookie c = new Cookie();
                c = GetCookie(str);
                if (c != null)
                {
                    if (Cookies[c.Name] != null)
                        Cookies[c.Name].Value = c.Value;
                    else
                        Cookies.Add(c);
                }
            }
            return Cookies;
        }
        public static Cookie GetCookie(string cookie)
        {        
            try
            {
                string[] cookieP = cookie.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                Cookie c = new Cookie();
                foreach (string p in cookieP)
                {
                    if (p.ToLower().IndexOf("comment") != -1)
                        c.Comment = p.Substring(p.IndexOf("=") + 1);
                    else if (p.ToLower().IndexOf("commenturi") != -1)
                        c.CommentUri = new Uri(p.Substring(p.IndexOf("=") + 1));
                    else if (p.ToLower().IndexOf("discard") != -1)
                        c.Discard = Convert.ToBoolean(p.Substring(p.IndexOf("=") + 1));
                    else if (p.ToLower().IndexOf("domain") != -1)
                        c.Domain = p.Substring(p.IndexOf("=") + 1);
                    else if (p.ToLower().IndexOf("expired") != -1)
                        c.Expired = Convert.ToBoolean(p.Substring(p.IndexOf("=") + 1));
                    else if (p.ToLower().IndexOf("expires") != -1)
                        c.Expires = Convert.ToDateTime(p.Substring(p.IndexOf("=") + 1));
                    else if (p.ToLower().IndexOf("httponly") != -1)
                        c.HttpOnly = false;//Convert.ToBoolean(p.Substring(p.IndexOf("=") + 1));
                    else if (p.ToLower().IndexOf("path") != -1)
                        c.Path = p.Substring(p.IndexOf("=") + 1);
                    else if (p.ToLower().IndexOf("Port") != -1)
                        c.Port = p.Substring(p.IndexOf("=") + 1);
                    else if (p.ToLower().IndexOf("secure") != -1)
                    {
                        if (p.IndexOf("=") == -1)
                            c.Secure = true;
                        else
                            c.Secure = Convert.ToBoolean(p.Substring(p.IndexOf("=") + 1));
                    }
                    //                             else if (p.ToLower().IndexOf("TimeStamp") != -1)
                    //                                 c.TimeStamp = Convert.ToDateTime(p.Substring(p.IndexOf("=") + 1));
                    else if (p.ToLower().IndexOf("Version") != -1)
                        c.Version = Convert.ToInt32(p.Substring(p.IndexOf("=") + 1));
                    else
                    {
                        c.Name = p.Substring(0, p.IndexOf("="));
                        c.Value = p.Substring(p.IndexOf("=") + 1);
                    }
                }
                return c;
            }
            catch
            {
                return null;
            }
        }
        public static string GetHref(string strContent,string href)
        {
            int x=0,x1;
            while(true)
            {
                x = strContent.IndexOf(href,x);
                if (x == -1)
                    return string.Empty;
                x1 = strContent.LastIndexOf("<a", x);
                if (x1 == -1)
                {
                    x+=href.Length;
                    continue ;
                }
                x1 = strContent.IndexOf("href=\"", x1);
                if (x == -1)
                {
                    x += href.Length;
                    continue;
                }
                x1 += "href=\"".Length;
                break;
            }
            return StringsFunction.strDecode(strContent.Substring(x1, strContent.IndexOf('\"', x1) - x1));           
        }
        public static string ContentToStr(Http http)
        {
            string str="";
            if (string.IsNullOrEmpty(http.httpHeader[HttpResponseHeader.ContentEncoding]))
            {
                Encoding ContentTypeCharset = GetCharset(http.httpHeader);
                if(ContentTypeCharset!=null)
                {
                    str = ContentTypeCharset.GetString(http.ContentDatas.ToArray());
                    Debug.WriteLine(str);
                }
                else
                {
                    str = Encoding.Default.GetString(http.ContentDatas.ToArray());
                    Debug.WriteLine(str);
                }
            }
            else if (http.httpHeader[HttpResponseHeader.ContentEncoding] == "gzip")
            {
                MemoryStream ms = new MemoryStream(http.ContentDatas.ToArray(), 0, http.ContentDatas.Count);
                GZipStream gzip = new GZipStream(ms, CompressionMode.Decompress);
                //string str = http.ContentEncoding.GetString(http.ContentDatas.ToArray());
                Encoding ContentTypeCharset = GetCharset(http.httpHeader);
                StreamReader reader;
                if (ContentTypeCharset != null)
                    reader = new StreamReader(gzip, ContentTypeCharset);
                else
                    reader = new StreamReader(gzip);
                str = reader.ReadToEnd();
                Debug.Write(str);
            }
            return str;
        }
        public static TreeNode HttpGetContentTree(string str)
        {
            TreeNode tnc = new TreeNode();            
            Hashtable ht = HttpGetContentList(str);
            if (ht.Count == 0)
                return null;
            string key, text;
            foreach (DictionaryEntry comm in ht)
            {
                text = (string)comm.Key;
                key = (string)comm.Value;
                TreeNode tnc1=tnc.Nodes.Add(key,text);                
                HttpGetContentTree(key,tnc1);                
            }
            return tnc; 
        }
        public static string HttpMakeContent(TreeNode tn)
        {
            string str="";
            foreach (TreeNode t in tn.Nodes)
            {
                str=str+"<";
                str = str + t.Text;
                str=str+">";
                if (t.Nodes.Count == 0)
                    str = str + t.Name;
                else
                    str = str + HttpMakeContent(t);
                str = str + "</";
                str = str + t.Text;
                str = str + ">";
            }
            return str;
        }
        public static void HttpGetContentTree(string str,TreeNode tn)
        {
            Hashtable ht = HttpGetContentList(str);
            if (ht.Count == 0)
                return;
            string key, text;
            foreach (DictionaryEntry comm in ht)
            {
                text = (string)comm.Key;
                key = (string)comm.Value;
                TreeNode tnc1 = tn.Nodes.Add(key, text);
                HttpGetContentTree(key,tnc1);                
            }
        }
        public static Hashtable HttpGetContentList(string str)
        {
            Hashtable ht = new Hashtable();
            string s1,s2,s3;
            int i1=0,i2,i3,i4;
            while (true)
            {
                i1 = str.IndexOf('<',i1);
                if(i1==-1)
                    break;
                i2 = str.IndexOf('>',i1+1);
                if(i2==-1)
                    break;
                s1 = str.Substring(i1 + 1, i2 - i1-1);
                if (s1[s1.Length - 1] == '/')
                {
                    i1 = i2 + 1;
                    ht.Add(s1.Substring(0, s1.Length - 1), "");
                    continue;
                }
                s2 = "";
                i2++;
                i3 = i2;
                while (true)
                {
                    i3 = str.IndexOf("</",i3);
                    if (i3 == -1)
                        break;                    
                    i4 = str.IndexOf('>', i3 + 2);
                    if (i4 == -1)
                        break;                    
                    s3 = str.Substring(i3 + 2, i4 - i3 - 2);
                    if (s3 == s1)
                    {
                        s2 = str.Substring(i2, i3 - i2);
                        ht.Add(s1, s2);
                        i1 = i4 + 1;
                        break;
                    }
                    else
                    {
                        i3 = i4 + 1;
                    }
                }
            }
            return ht;
        }
        public static string HttpMakeContent(Hashtable ht,string key)
        {
            string str = "";
            str = "<" + key + ">";
            str += (string)ht[key];
            str += "</" + key + ">";
            return str;
        }
        public static string HttpHeaderToStr(string uri, WebHeaderCollection httpHeader, CookieCollection cookies)
        {
            return HttpHeaderToStr("GET", uri, httpHeader, cookies);
        }
        public static string HttpHeaderToStr(string Method,string uri,WebHeaderCollection httpHeader,CookieCollection cookies)
        {
            
//             if (string.IsNullOrEmpty(httpHeader[HttpRequestHeader.Host]))
//             {
                int x = uri.IndexOf("http://");
                if (x != -1)
                {
                    x += 7;
                    if (uri.IndexOf('/', x) == -1)
                    {
                        httpHeader[HttpRequestHeader.Host] = uri.Substring(x);
                        uri = "/";
                    }
                    else
                    {
                        string sx = uri.Substring(x, uri.IndexOf('/', x) - x);
                        httpHeader[HttpRequestHeader.Host] = sx;
                        uri = uri.Substring(sx.Length + 7);
                    }
                }
            //}
            StringBuilder sb = new StringBuilder();
            sb.Append(Method);
            sb.Append(" ");
            sb.Append(uri);
            sb.Append(" HTTP/1.1\r\n");
            foreach (string h in httpHeader.AllKeys)
            {
                sb.Append(h + ": ");
                String[] values = httpHeader.GetValues(h);
                foreach (string v in values)
                {
                    sb.Append(v + ",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("\r\n");
            }
            if (cookies.Count != 0)
            {
                sb.Append("Cookie: ");
                foreach (Cookie c in cookies)
                {
                    sb.Append(c.Name);
                    sb.Append("=");
                    sb.Append(c.Value);
                    sb.Append(";");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("\r\n");
            }
            sb.Append("\r\n");           
            return sb.ToString();
        }
        public static int GetHttpHeader(string http)
        {
            int ret=http.IndexOf("\r\n\r\n");
            if (ret != -1)
                return ret + 4;
            else
                return -1;
        }
        public static string GetFiledID(string httpHeader,string strFiled)
        {
            int index = httpHeader.IndexOf(strFiled);
            if (index == -1)
                return string.Empty;
            index += strFiled.Length;
            index += 2;
            int end=httpHeader.IndexOf("\r\n",index);
            string ret = httpHeader.Substring(index, end - index);
            return ret;
        }
        public static int GetServerState(string httpHeader)
        {
            return Convert.ToInt32(httpHeader.Substring(9, 3));
        }
        public static Encoding GetCharset(string httpHeader)
        {
            string type = GetFiledID(httpHeader, "Content-Type");
            if (string.IsNullOrEmpty(type) || type.IndexOf("charset=") == -1 || type.IndexOf(";") == -1)
                return null;
            string Charset = type.Split(';')[1];
            Charset=Charset.ToLower();
            int index = Charset.IndexOf("charset=");
            string ret;
            if (index == -1)
                return null;
            ret = Charset.Substring(index + 8);
            try
            {
                return Encoding.GetEncoding(ret);
            }
            catch
            {
                Debug.WriteLine(ret);
                return null;
            }
        }
        public static Encoding GetCharset(WebHeaderCollection httpHeader)
        {
            string type = httpHeader[HttpResponseHeader.ContentType];
            if (string.IsNullOrEmpty(type) || type.IndexOf("charset=") == -1 || type.IndexOf(";")==-1)
                return null;
            string Charset = type.Split(';')[1];
            Charset = Charset.ToLower();
            int index = Charset.IndexOf("charset=");
            string ret;
            if (index == -1)
                return null;
            ret = Charset.Substring(index + 8);
            try
            {
                return Encoding.GetEncoding(ret);
            }
            catch
            {
                Debug.WriteLine(ret);
                return null;
            }
        }
        public static string GetContentType(string httpHeader)
        {
            string type = GetFiledID(httpHeader, "Content-Type");
            if (string.IsNullOrEmpty(type))
                return "";
            return type.Split(';')[0];
        }
        public static string GetContentType(WebHeaderCollection httpHeader)
        {
            string type = httpHeader[HttpResponseHeader.ContentType];
            if (string.IsNullOrEmpty(type))
                return "";
            return type.Split(';')[0];
        } 
    }
    public class Http
    {
        public Encoding ContentTypeCharset;
        public string ContentType;
        public List<byte> ContentDatas;
        public List<byte> HttpDatas;
        public string strHttpHeader="";
        public int ContentLength;
        public string ContentEncoding;
        public CookieCollection Cookies;
        public WebHeaderCollection httpHeader;
        public int ServerState;
        int HeaderEnd;
        public bool bOK=false;
        public Single per;
        public Http()
        {
            ContentDatas = new List<byte>();
            HttpDatas = new List<byte>();
            httpHeader = null;
            Cookies = new CookieCollection();
        }
#if DEBUG
        bool bFirstHeader = true;
#endif       
        public int bFinish()
        {
            return bFinish(100);
        }
        public int bFinish(Single FinishPer)
        {
            string strHttp;
            if (httpHeader == null)
            {
                strHttp = Encoding.ASCII.GetString(HttpDatas.ToArray(), 0, HttpDatas.Count);
                int ret = strHttp.IndexOf("\r\n\r\n");
                if (ret != -1)
                {
                    HeaderEnd= ret + 4;
                    strHttpHeader = strHttp.Substring(0, HeaderEnd);
                    Debug.Write(strHttpHeader);
                    ServerState=Convert.ToInt32(strHttpHeader.Substring(9, 3));
                    string[] headers = strHttpHeader.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    httpHeader=new WebHeaderCollection();
                    for (int i = 1; i < headers.Length; i++)
                        httpHeader.Add(headers[i]);
                    int index = strHttpHeader.IndexOf("Set-Cookie");
                    if (index != -1)
                    {                        
                        while (true)
                        {                            
                            index += 12;
                            string cookie = strHttpHeader.Substring(index, strHttpHeader.IndexOf("\r\n", index) - index);
                            Cookie c = HttpStatic.GetCookie(cookie);
                            if (c != null)
                                Cookies.Add(c);
                            index = strHttpHeader.IndexOf("Set-Cookie", index);
                            if (index == -1)
                                break;
                        }
                    }                    
                }
                else
                    return (int)HttpRxState.NoFinish;
            } 
#if DEBUG
            if (bFirstHeader)
            {
                bFirstHeader = false;
                
            }
#endif            
            string strTemp = httpHeader[HttpResponseHeader.ContentLength];
            if (strTemp != string.Empty)
            {
                ContentLength = Convert.ToInt32(strTemp);
                if (ContentLength == (HttpDatas.Count - HeaderEnd))//数据完成了
                {
                    ContentTypeCharset = HttpStatic.GetCharset(httpHeader);
                    ContentType = HttpStatic.GetContentType(httpHeader);
                    ContentDatas = new List<byte>();
                    for (int i = 0; i < (HttpDatas.Count - HeaderEnd); i++)
                    {
                        ContentDatas.Add(HttpDatas[HeaderEnd + i]);
                    }
                    bOK = true;
                    return (int)HttpRxState.Finish;
                }
                else if(FinishPer!=100)
                {
                    per = (Single)((HttpDatas.Count - HeaderEnd) * 100) / (Single)ContentLength;
                    if (per > FinishPer)
                    {
                        ContentTypeCharset = HttpStatic.GetCharset(httpHeader);
                        ContentType = HttpStatic.GetContentType(httpHeader);
                        ContentDatas = new List<byte>();
                        for (int i = 0; i < (HttpDatas.Count - HeaderEnd); i++)
                        {
                            ContentDatas.Add(HttpDatas[HeaderEnd + i]);
                        }
                        bOK = true;
                        return (int)HttpRxState.Finish;
                    }
                    return ContentLength - (HttpDatas.Count - HeaderEnd);
                }
                else
                    return ContentLength - (HttpDatas.Count - HeaderEnd);
            }
            else//没有数据长度
            {
                strTemp = HttpStatic.GetFiledID(strHttpHeader, "Content-Type");
                if (strTemp == string.Empty)
                {
                    throw new Exception("未知http协议，没有办法结束");
                }               
            }
            return (int)HttpRxState.UnKnow;
        }
    }
    public enum HttpRxState
    {
        UnKnow=-3,
        LengthOut=-2,
        NoFinish=-1,
        Finish=0
    }
}
