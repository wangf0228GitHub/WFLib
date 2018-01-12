using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.XML
{
    /// <summary>
    /// XML文档的操作类型
    /// </summary>
    public enum OperateXmlMethod : int
    {
        /// <summary>
        /// 仅操作属性
        /// </summary>
        XmlProperty = 0,

        /// <summary>
        /// 仅操作节点
        /// </summary>
        XmlNodes,

        /// <summary>
        /// 属性和节点都操作
        /// </summary>
        All
    }
}
