using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace WFNetLib.WeiXin
{    
    public class MessageProc
    {
        public Encoding encoding;
        public wxMessage GetMessage(string postStr)
        {
            if (_queue.Count >= 50)
            {
                _queue = _queue.Where(q => { return q.CreateTime.AddSeconds(20) > DateTime.Now; }).ToList();//保留20秒内未响应的消息
            }            
            wxMessage ret = null;// new wxMessage();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(new System.IO.MemoryStream(encoding.GetBytes(postStr)));
            ret = GetCommMessage(xmldoc);
            if (ret.MsgType == "event")//事件类型消息推荐使用FromUserName + CreateTime 排重
            {
                if (_queue.FirstOrDefault(m => { return (m.MsgFlag == ret.CreateTime) && (m.FromUser == ret.FromUserName); }) == null)
                {
                    _queue.Add(new BaseMsg
                    {
                        CreateTime = DateTime.Now,
                        FromUser = ret.FromUserName,
                        MsgFlag = ret.CreateTime
                    });
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if (_queue.FirstOrDefault(m => { return m.MsgFlag == ret.MsgId; }) == null)
                {
                    _queue.Add(new BaseMsg
                    {
                        CreateTime = DateTime.Now,
                        FromUser = ret.FromUserName,
                        MsgFlag = ret.MsgId
                    });
                }
                else
                {
                    return null;
                }

            }
            switch (ret.MsgType)
            {
                case "event":
                    //responseContent = EventHandle(xmldoc);//事件处理
                    ret.Event = xmldoc.SelectSingleNode("/xml/Event").InnerText.ToLower();
                    ret.EventKey = xmldoc.SelectSingleNode("/xml/EventKey").InnerText;
                    break;
                case "text":
                    //responseContent = TextHandle(xmldoc);//接受文本消息处理
                    ret.Content = xmldoc.SelectSingleNode("/xml/Content").InnerText;
                    ret.MsgId = xmldoc.SelectSingleNode("/xml/MsgId").InnerText;
                    break;
                default:
                    break;
            }
            return ret;
        }
        public wxMessage GetCommMessage(XmlDocument xmldoc)
        {
            wxMessage ret =  new wxMessage();
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            XmlNode CreateTime = xmldoc.SelectSingleNode("/xml/CreateTime");
            XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");        
            
            ret.ToUserName = ToUserName.InnerText;
            ret.FromUserName = FromUserName.InnerText;
            ret.CreateTime = CreateTime.InnerText;
            ret.MsgType = MsgType.InnerText.ToLower();            
            return ret;
        }
        public string MakeTxMsg(wxMessage wxMsg)
        {
            return MakeTxMsg(wxMsg, true);
        }
        public string MakeTxMsg(wxMessage wxMsg,bool bACK)
        {
            string ret="";
            string ToUserName;
            string FromUserName;
            if (bACK)
            {
                ToUserName = wxMsg.FromUserName;
                FromUserName = wxMsg.ToUserName;
            }
            else
            {
                ToUserName = wxMsg.ToUserName;
                FromUserName = wxMsg.FromUserName;
            }
            switch (wxMsg.MsgType)
            {
                case "text":
                    ret = string.Format(ReplyType.Message_Text,
                    ToUserName,
                    FromUserName,
                    DateTime.Now.Ticks,
                    wxMsg.Content);
                    break;
                case "news":
                    string Articles = "";
                    for (int i = 0; i < wxMsg.ArticleCount; i++)
                    {
                        Articles += string.Format(ReplyType.Message_News_Item,
                             wxMsg.NewsItemList[i].Title,
                             wxMsg.NewsItemList[i].Description,
                             wxMsg.NewsItemList[i].PicUrl,
                             wxMsg.NewsItemList[i].Url);
                    }
                    ret = string.Format(ReplyType.Message_News_Main,
                    ToUserName,
                    FromUserName,
                    DateTime.Now.Ticks,
                    wxMsg.ArticleCount.ToString(),
                    Articles);
                    break;
            }
            return ret;
        }
        public static List<BaseMsg> _queue = new List<BaseMsg>();

    }
    public class BaseMsg
    {
        /// <summary>
        /// 发送者标识
        /// </summary>
        public string FromUser { get; set; }
        /// <summary>
        /// 消息表示。普通消息时，为msgid，事件消息时，为事件的创建时间
        /// </summary>
        public string MsgFlag { get; set; }
        /// <summary>
        /// 添加到队列的时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
    public class wxMessage
    {
        public string ToUserName;//	 接收方微信号
        public string FromUserName;//	 发送方微信号，若为普通用户，则是一个OpenID
        public string CreateTime;//	 消息创建时间
        public string MsgType;//	 消息类型，link        

        public string MsgId;//	 消息id，64位整型

        //文本消息用
        public string Content;//	 文本消息内容

        //链接消息用
        public string Title;//	 消息标题
        public string Description;//	 消息描述
        public string Url;//	 消息链接

        //自定义菜单事件
        public string Event;//事件类型，CLICK;事件类型，VIEW
        public string EventKey;//事件KEY值，与自定义菜单接口中KEY值对应;事件KEY值，设置的跳转URL

        //图文消息
        public int ArticleCount;// 图文消息个数，限制为10条以内
        public wxMessageNewsItem[] NewsItemList;
    }
    public class wxMessageNewsItem
    {
        public string Title;
        public string Description;
        public string PicUrl;
        public string Url;
    }
    //回复类型
    public class ReplyType
    {
        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[{3}]]></Content>
                            </xml>";
            }
        }
        /// <summary>
        /// 图文消息主体
        /// </summary>
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[news]]></MsgType>
                            <ArticleCount>{3}</ArticleCount>
                            <Articles>
                            {4}
                            </Articles>
                            </xml> ";
            }
        }
        /// <summary>
        /// 图文消息项
        /// </summary>
        public static string Message_News_Item
        {
            get
            {
                return @"<item>
                            <Title><![CDATA[{0}]]></Title> 
                            <Description><![CDATA[{1}]]></Description>
                            <PicUrl><![CDATA[{2}]]></PicUrl>
                            <Url><![CDATA[{3}]]></Url>
                            </item>";
            }
        }
    }
}
