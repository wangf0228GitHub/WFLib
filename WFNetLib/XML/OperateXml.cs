using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Xml;
using System.Collections;
using WFNetLib.Strings;
using WFNetLib.StringFunc;

namespace WFNetLib.XML
{
    /// <summary>
	/// OperateXml : 对 Xml 文件的操作。
	/// </summary>
	/// <remarks>
	///  xPath说明
	/// /* 
	///  * 查询属性：	(//sectionName[@propertyName=value])
	///  * 查询子节点：	(//sectionName[childSectionName=value])
	///  * 逐层查询：	(//root/sectionName/child....)
	///  */
	/// </remarks>
    public class OperateXml
    {
        /// <summary>
        /// 公共的构造函数
        /// </summary>
        public OperateXml()
        {
        }

        /// <summary>
        /// 公共构造函数
        /// </summary>
        /// <param name="Path">Xml文件的绝对路径</param>
        public OperateXml(string Path)
        {
            this.filePath = Path;
        }

        // Private Field
        private string _fileContent;
        private string _filePath;
        private string _xPath;
        private OperateXmlMethod _Method;


        #region Public Property
        /// <summary>
        /// XML格式的内容
        /// </summary>
        public string fileContent
        {
            get { return _fileContent; }
            set { _fileContent = value; }
        }
        /// <summary>
        /// XML文件的绝对路径
        /// </summary>
        public string filePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }
        /// <summary>
        /// xPath表达式
        /// </summary>
        public string xPath
        {
            get { return _xPath; }
            set { _xPath = value; }
        }
        /// <summary>
        /// 操作方式
        /// </summary>
        public OperateXmlMethod Method
        {
            get { return _Method; }
            set { _Method = value; }
        }
        #endregion




    # region "XML文件操作"

    #region "新 建"
        /// <summary>
        /// 新建一个空白的XML格式的文件(无根节点)
        /// </summary>
        public void CreateXml()
        {
            // 创建一个同名的临时文件，确保文件夹的存在
            FileOP.Create(this.filePath, FileOPMethod.File);

            // 创建一个 XmlTextWriter 实例
            XmlTextWriter writer = new XmlTextWriter(this.filePath, Encoding.GetEncoding("gb2312"));
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            writer.WriteStartDocument();

            // 释放资源
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// 新建一个XML格式的文件(具有根节点)
        /// </summary>
        /// <param name="rootNodeName">根节点名称</param>
        public void CreateXml(string rootNodeName)
        {
            // 创建一个同名的临时文件，确保文件夹的存在
            FileOP.Create(this.filePath, FileOPMethod.File);

            // 创建一个 XmlTextWriter 实例
            XmlTextWriter writer = new XmlTextWriter(this.filePath, Encoding.GetEncoding("gb2312"));
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 3;

            writer.WriteStartDocument();
            // 开始写根结点
            writer.WriteStartElement(rootNodeName);
            // 结束写根节点
            writer.WriteEndElement();

            writer.WriteEndDocument();
            // 释放资源
            writer.Flush();
            writer.Close();
        }
    #endregion


    # region "添 加"


        /// <summary>
        /// 创建一个子节点到指定的节点下，并为该子节点指定属性或子节点
        /// </summary>
        /// <param name="nodeName">要创建的子节点名称</param>
        /// <param name="dt">
        /// 储存子节点的赋值信息(一个DataTable)
        /// 行1(名称, 名称前含有@字符，则为属性)
        /// 行2(值)</param>
        public void CreateNode(string nodeName, DataTable dt)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);
            XmlNode root = doc.SelectSingleNode(this.xPath);

            // 创建子节点
            XmlElement xe = doc.CreateElement(nodeName);
            XmlElement xeChild = null;
            if (!Object.Equals(dt, null))
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.StartsWith("@"))
                    {
                        string AttributeName = dt.Columns[i].ColumnName.Replace("@", "");
                        // 为该子节点设置属性
                        xe.SetAttribute(AttributeName, dt.Rows[0][i].ToString());
                    }
                    else
                    {
                        xeChild = doc.CreateElement(dt.Columns[i].ColumnName);
                        try
                        {
                            xeChild.InnerXml = dt.Rows[0][i].ToString();
                        }
                        catch
                        {
                            xeChild.InnerText = dt.Rows[0][i].ToString();
                        }
                        xe.AppendChild(xeChild);
                    }
                }
            }
            // 保存子节点设置
            root.AppendChild(xe);

            doc.Save(this.filePath);

            # region "样例"
            /*
			// 构造一个DataTable
			DataTable Dt = new DataTable("testXML");
			DataRow Dr;

			Dt.Columns.Add(new DataColumn("type"));
			Dt.Columns.Add(new DataColumn("name"));
			Dt.Columns.Add(new DataColumn("value"));

			for(int i = 0; i < 4; i++)
			{
				Dr = Dt.NewRow();

				Dr[0] = "1";
				Dr[1] = "name" + i;
				Dr[2] = "value" + i;

				Dt.Rows.Add(Dr);
			}


			Seaskyer.XML.OperateXml oxml = new Seaskyer.XML.OperateXml(Server.MapPath("/test.xml"));
			oxml.CreateXml("dwbs");
		*/
            # endregion
        }


        /// <summary>
        /// 创建一个子节点到指定的节点下，并为该子节点指定属性或子节点，空值也创建
        /// </summary>
        /// <param name="nodeName">要创建的子节点名称</param>
        /// <param name="dt">
        /// 储存子节点的赋值信息(一个DataTable)
        /// 行1(名称, 名称前含有@字符，则为属性)
        /// 行2(值)</param>
        public void CreateNodes(string nodeName, DataTable dt)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);
            XmlNode root = doc.SelectSingleNode(this.xPath);

            // 创建子节点
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                XmlElement xe = doc.CreateElement(nodeName);
                XmlElement xeChild = null;
                if (!Object.Equals(dt, null))
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName.StartsWith("@"))
                        {
                            string AttributeName = dt.Columns[i].ColumnName.Replace("@", "");
                            // 为该子节点设置属性
                            xe.SetAttribute(AttributeName, dt.Rows[j][i].ToString());
                        }
                        else
                        {
                            xeChild = doc.CreateElement(dt.Columns[i].ColumnName);

                            try
                            {
                                xeChild.InnerXml = dt.Rows[j][i].ToString();
                            }
                            catch
                            {
                                xeChild.InnerText = dt.Rows[j][i].ToString();
                            }
                            xe.AppendChild(xeChild);
                        }
                    }
                }
                // 保存子节点设置
                root.AppendChild(xe);
            }

            doc.Save(this.filePath);

        }

        /// <summary>
        /// 创建一个子节点到指定的节点下，并为该子节点指定属性或子节点
        /// </summary>
        /// <param name="nodeName">要创建的子节点名称</param>
        /// <param name="dt">
        /// 储存子节点的赋值信息(一个DataTable)
        /// 行1(名称, 名称前含有@字符，则为属性)
        /// 行2(值)</param>
        /// <param name="CreateNull">是否创建空值的节点或属性</param>
        public void CreateNodes(string nodeName, DataTable dt, bool CreateNull)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);
            XmlNode root = doc.SelectSingleNode(this.xPath);

            // 创建子节点
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                XmlElement xe = doc.CreateElement(nodeName);
                XmlElement xeChild = null;
                if (!Object.Equals(dt, null))
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (dt.Columns[i].ColumnName.StartsWith("@"))
                        {
                            string AttributeName = dt.Columns[i].ColumnName.Replace("@", "");
                            // 为该子节点设置属性
                            if (CreateNull)
                                xe.SetAttribute(AttributeName, dt.Rows[j][i].ToString());
                            else if (dt.Rows[j][i].ToString() != "")
                                xe.SetAttribute(AttributeName, dt.Rows[j][i].ToString());
                        }
                        else
                        {
                            xeChild = doc.CreateElement(dt.Columns[i].ColumnName);

                            if (CreateNull)
                            {
                                try
                                {
                                    xeChild.InnerXml = dt.Rows[j][i].ToString();
                                }
                                catch
                                {
                                    xeChild.InnerText = dt.Rows[j][i].ToString();
                                }
                                xe.AppendChild(xeChild);

                            }
                            else if (dt.Rows[j][i].ToString() != "")
                            {
                                try
                                {
                                    xeChild.InnerXml = dt.Rows[j][i].ToString();
                                }
                                catch
                                {
                                    xeChild.InnerText = dt.Rows[j][i].ToString();
                                }
                                xe.AppendChild(xeChild);
                            }
                        }
                    }
                }
                // 保存子节点设置
                root.AppendChild(xe);
            }

            doc.Save(this.filePath);

        }
    # endregion


    # region "修 改"

        /// <summary>
        /// 修改节点下的属性和字节点的值
        /// </summary>
        /// <param name="parentNodeName"></param>
        /// <param name="type"></param>
        /// <param name="thename"></param>
        /// <param name="thevalue"></param>
        public void ChangeNode(string parentNodeName, int type, string thename, string thevalue)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);

            XmlNodeList list = doc.SelectNodes(parentNodeName);
            foreach (XmlNode l in list)
            {
                XmlElement xe = (XmlElement)l;
                if (type == 0)
                {
                    if (xe.HasAttribute(thename)) xe.SetAttribute(thename, thevalue);
                }
                else if (type == 1)
                {
                    XmlNodeList list1 = xe.ChildNodes;
                    foreach (XmlNode l1 in list1)
                    {
                        XmlElement xe1 = (XmlElement)l1;
                        if (xe1.Name == thename)
                        {
                            try
                            {
                                xe1.InnerXml = thevalue;
                            }
                            catch
                            {
                                xe1.InnerText = thevalue;
                            }
                        }
                    }
                }
            }

            doc.Save(this.filePath);
        }


        /// <summary>
        /// 修改某个节点下的所有属性和一级子节点的值
        /// </summary>
        /// <param name="dt"></param>
        public void ChangeNode(DataTable dt)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);

            XmlNode list = doc.SelectSingleNode(this.xPath);
            if (list == null) return;


            XmlElement xe = (XmlElement)list;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.StartsWith("@"))
                {
                    string AttributeName = dt.Columns[i].ColumnName.Replace("@", "");
                    if (xe.HasAttribute(AttributeName)) xe.SetAttribute(AttributeName, dt.Rows[0][i].ToString());
                }
                else
                {
                    XmlNodeList lists = xe.ChildNodes;
                    foreach (XmlNode l in lists)
                    {
                        XmlElement xe1 = (XmlElement)l;
                        if (xe1.Name == dt.Columns[i].ColumnName)
                        {
                            try
                            {
                                xe1.InnerXml = dt.Rows[0][i].ToString();
                            }
                            catch
                            {
                                xe1.InnerText = dt.Rows[0][i].ToString();
                            }
                        }
                    }
                }
            }

            doc.Save(this.filePath);
        }


    # endregion


    # region "删 除"

        /// <summary>
        /// 删除节点 (传入的 xPath 必须是要删除节点的父节点的xPath)
        /// </summary>
        /// <param name="nodeName">节点名称，可以是单一的，也可以是条件的，如 add, add[@key='aa']</param>
        public void DeleteNode(string nodeName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);

            XmlNode parentNode = doc.SelectSingleNode(this.xPath);
            XmlNode childNode = parentNode.SelectSingleNode(nodeName);

            if (childNode == null) return;

            // 删除节点
            parentNode.RemoveChild(childNode);

            doc.Save(this.filePath);
        }

    # endregion


    # region "提 取"

        /// <summary>
        /// 提取一个节点下所有一级子节点及其所有属性并转化成DataTable保存数据
        /// </summary>
        /// <param name="xlist">节点集合 XmlNodeList</param>
        /// <returns>DataTable</returns>
        public DataTable ConvertXmlNodeListDataTable(XmlNodeList xlist)
        {
            DataTable Dt = new DataTable();
            DataRow Dr;

            for (int i = 0; i < xlist.Count; i++)
            {
                Dr = Dt.NewRow();
                XmlElement xe = (XmlElement)xlist.Item(i);

                for (int j = 0; j < xe.Attributes.Count; j++)
                {
                    if (!Dt.Columns.Contains("@" + xe.Attributes[j].Name))
                    {
                        Dt.Columns.Add("@" + xe.Attributes[j].Name);
                    }

                    Dr["@" + xe.Attributes[j].Name] = xe.Attributes[j].Value;
                }

                for (int j = 0; j < xe.ChildNodes.Count; j++)
                {
                    if (!Dt.Columns.Contains(xe.ChildNodes.Item(j).Name))
                    {
                        Dt.Columns.Add(xe.ChildNodes.Item(j).Name);
                    }

                    Dr[xe.ChildNodes.Item(j).Name] = xe.ChildNodes.Item(j).InnerText;
                }

                Dt.Rows.Add(Dr);
            }

            return Dt;
        }

        /// <summary>
        /// 提取一个节点下所有一级子节点或所有属性并转化成DataTable保存数据
        /// </summary>
        /// <param name="xlist">节点集合 XmlNodeList</param>
        /// <param name="type">0为提取所有属性，1为提取所有子节点</param>
        /// <returns>DataTable</returns>
        public DataTable ConvertXmlNodeListDataTable(XmlNodeList xlist, int type)
        {
            DataTable Dt = new DataTable();
            DataRow Dr;

            for (int i = 0; i < xlist.Count; i++)
            {
                Dr = Dt.NewRow();
                XmlElement xe = (XmlElement)xlist.Item(i);

                if (type == 0)
                {
                    for (int j = 0; j < xe.Attributes.Count; j++)
                    {
                        if (!Dt.Columns.Contains("@" + xe.Attributes[j].Name))
                        {
                            Dt.Columns.Add("@" + xe.Attributes[j].Name);
                        }

                        Dr["@" + xe.Attributes[j].Name] = xe.Attributes[j].Value;
                    }
                }
                else if (type == 1)
                {
                    for (int j = 0; j < xe.ChildNodes.Count; j++)
                    {
                        if (!Dt.Columns.Contains(xe.ChildNodes.Item(j).Name))
                        {
                            Dt.Columns.Add(xe.ChildNodes.Item(j).Name);
                        }

                        Dr[xe.ChildNodes.Item(j).Name] = xe.ChildNodes.Item(j).InnerText;
                    }
                }

                Dt.Rows.Add(Dr);
            }

            return Dt;
        }

        /// <summary>
        /// 提取指定XML文件某个节点的信息(包括属性及其子节点)
        /// </summary>
        /// <param name="nodes">要提取的节点位置</param>
        /// <param name="method">0提取单个节点，1提取所有相关节点</param>
        /// <returns>XmlNodeList</returns>
        public XmlNodeList GetXmlNodeList(string nodes, int method)
        {
            XmlDocument doc = new XmlDocument();

            // 加载XML内容
            if (StringsFunction.CheckValiable(this.fileContent))
                doc.LoadXml(this.fileContent);
            else
                doc.Load(this.filePath);



            XmlNodeList xn = null;

            switch (method)
            {
                case 0:
                    xn = doc.SelectSingleNode(nodes).ChildNodes;
                    break;
                case 1:
                    xn = doc.SelectNodes(nodes);
                    break;
            }

            return xn;
        }

        /// <summary>
        /// 提取单个节点的信息(包括属性及其子节点)
        /// </summary>
        /// <param name="nodes">指定节点</param>
        /// <returns>XmlElement</returns>
        public XmlElement GetXmlElement(string nodes)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);

            XmlElement xe = (XmlElement)doc.SelectSingleNode(nodes);

            return xe;
        }

        /// <summary>
        /// 提取指定节点的指定属性或子节点的值
        /// </summary>
        /// <param name="name">指定属性或子节点</param>
        /// <returns></returns>
        public ArrayList GetXml(string name)
        {
            ArrayList strResult = new ArrayList();
            int i = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);

            XmlNodeList list = doc.SelectNodes(this.xPath);
            foreach (XmlNode l in list)
            {
                XmlElement xe = (XmlElement)l;
                switch (this.Method)
                {
                    case OperateXmlMethod.XmlProperty:
                        if (xe.HasAttribute(name)) strResult.Add(xe.GetAttribute(name));
                        break;

                    case OperateXmlMethod.XmlNodes:
                        XmlNodeList list1 = xe.ChildNodes;
                        foreach (XmlNode l1 in list1)
                        {
                            XmlElement xe1 = (XmlElement)l1;
                            if (xe1.Name == name) strResult.Add(xe1.InnerText);
                        }
                        break;
                }
                i++;
            }
            return strResult;
        }

        /// <summary>
        /// 提取指定节点的指定属性或子节点的值
        /// </summary>
        /// <param name="nodes">指定节点</param>
        /// <param name="type">0为属性，1为节点</param>
        /// <param name="name">指定属性或子节点</param>
        /// <returns></returns>
        public ArrayList GetXml(string nodes, int type, string name)
        {
            ArrayList strResult = new ArrayList();
            int i = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);

            XmlNodeList list = doc.SelectNodes(nodes);
            foreach (XmlNode l in list)
            {
                XmlElement xe = (XmlElement)l;
                switch (type)
                {
                    case 0:
                        if (xe.HasAttribute(name)) strResult.Add(xe.GetAttribute(name));
                        break;
                    case 1:
                        XmlNodeList list1 = xe.ChildNodes;
                        foreach (XmlNode l1 in list1)
                        {
                            XmlElement xe1 = (XmlElement)l1;
                            if (xe1.Name == name) strResult.Add(xe1.InnerText);
                        }
                        break;
                }
                i++;
            }
            return strResult;
        }


        /// <summary>
        /// 检测指定节点的属性或者子节点名称是否存在
        /// </summary>
        /// <param name="Name">指定属性或子节点</param>
        /// <returns></returns>
        public bool CheckXml(string Name)
        {
            bool blResult = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(this.filePath);

            XmlElement xe = (XmlElement)doc.SelectSingleNode(this.xPath);
            switch (this.Method)
            {
                case OperateXmlMethod.XmlProperty:
                    if (xe.HasAttribute(Name)) blResult = true;
                    break;
                case OperateXmlMethod.XmlNodes:
                    for (int i = 0; i < xe.ChildNodes.Count; i++)
                    {
                        if (xe.ChildNodes.Item(i).Name == Name)
                        {
                            blResult = true;
                        }
                    }
                    break;
            }

            return blResult;
        }



    # endregion


    # endregion
    }
}
