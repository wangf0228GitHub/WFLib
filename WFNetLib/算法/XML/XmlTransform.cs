using System;
using System.Collections.Generic;

using System.Text;
using System.Web;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using WFNetLib.Strings;
using WFNetLib.StringFunc;

namespace WFNetLib.XML
{
    /// <summary>
    /// XmlTransform 的摘要说明。
    /// </summary>
    public class XmlTransform
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public XmlTransform()
        {
        }

        // Private Field
        private string _xmlSource;
        private string _xslSource;


    #region Public Property
        /// <summary>
        /// 获取或设置XML源文件路径
        /// </summary>
        public string XmlSource
        {
            get { return _xmlSource; }
            set { _xmlSource = value; }
        }

        /// <summary>
        /// 获取或设置XSL源文件路径
        /// </summary>
        public string XslSource
        {
            get { return _xslSource; }
            set { _xslSource = value; }
        }
    #endregion


        /// <summary>
        ///  使用 Xml 和 Xsl(Xslt) 生成静态HTML页面内容
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// using System;
        /// using Seaskyer.XML;
        /// 
        /// public class TestXmlTransform
        /// {
        ///		public TestXmlTransform()
        ///		{
        ///		}
        ///		
        ///		public string TestXmlTrans()
        ///		{
        ///			XmlTransform xt = new XmlTransform();
        ///			xt.xmlSource = "";
        ///			xt.xslSource = "";
        ///		
        ///			string htmlContent = xt.transForm();
        ///			
        ///			return htmlContent;
        ///		}
        ///	}
        ///	]]>
        ///	</code>
        /// </example>
        /// <returns>String</returns>
        public string transForm()
        {
            string strResult = "";
//             string xmlSource = null;
// 
//             HttpRequest Request = HttpContext.Current.Request;
// 
//             try
//             {
//                 if (this.XmlSource.StartsWith("http://"))
//                 {
//                     xmlSource = this.XmlSource;
//                 }
//                 else
//                 {
//                     xmlSource = "http://" + Request.ServerVariables["SERVER_NAME"].ToString() + ":" + Request.ServerVariables["SERVER_PORT"] + Request.ApplicationPath + this.XmlSource;
//                 }
//                 XPathDocument treeDoc = new XPathDocument(xmlSource);
//                 XslTransform treeView = new XslTransform();
//                 treeView.Load(StringsFunction.GetRealFile(this.XslSource));
// 
//                 StringWriter sw = new StringWriter();
//                 treeView.Transform(treeDoc, null, sw, null);
// 
//                 strResult = sw.ToString();
//                 strResult = strResult.Replace("xmlns:asp=\"remove\"", "");
//             }
//             catch (Exception e)
//             {
//                 if (Object.Equals(this.XmlSource, null) || Object.Equals(this.XmlSource, "")) throw new Exception("未指定 Xml 数据源!");
//                 else if (Object.Equals(this.XslSource, null) || Object.Equals(this.XslSource, "")) throw new Exception("未指定 Xsl 数据源!");
//                 else
//                 {
//                     throw new Exception(xmlSource + e.ToString());
//                 }
//             }

            return strResult;
        }
    }
}
