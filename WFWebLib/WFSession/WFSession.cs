using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using WFNetLib.Strings;
using WFNetLib.XML;
using System.Xml;

namespace WFNetLib.WFSession
{
    /// <summary>
    /// WFSession : 自定义类 Session 操作类。
    /// </summary>
    public class WFSession
    {
        /// <summary>
		/// 析构函数，自动执行更改
		/// </summary>
		~WFSession()
		{
			this.AcceptChanges();
		}

		#region 构造函数
		/// <summary>
		/// 构造函数
		/// </summary>
		public WFSession()
		{
			this.filePath = StringsFunction.GetRealPath("~/UserInfos/") + "test.config";
			this.WFSession_Load();
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="filePath"></param>
		public WFSession(string filePath)
		{
			this.filePath = filePath;
			this.WFSession_Load();
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="tableName"></param>
		public WFSession(string filePath, string tableName)
		{
			this.filePath	= filePath;
			this.tableName	= tableName;
			this.WFSession_Load();
		}

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="filePath"></param>
		/// <param name="tableName"></param>
		/// <param name="rootName"></param>
		public WFSession(string filePath, string tableName, string rootName)
		{
			this.filePath	= filePath;
			this.tableName	= tableName;
			this.rootName	= rootName;
			this.WFSession_Load();
		}

		/// <summary>
		/// 自定义添加值，将需要添加的键及其键值保存到 Hashtable 中，以便统一添加
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Value"></param>
		/// <returns></returns>
		private void AddedCollection(string Name, object Value)
		{
			if( this.IsAddedCollection.Contains(Name) ) this.IsAddedCollection.Remove(Name);

			this.IsAddedCollection.Add(Name, Value);
		}

		/// <summary>
		/// 自定义修改值，将已经修改过的键及其键值保存到 Hashtable 中，以便统一修改
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Value"></param>
		/// <returns></returns>
		private void ChangedCollection(string Name, object Value)
		{
			if( this.IsChangedCollection.Contains(Name) ) this.IsChangedCollection.Remove(Name);

			this.IsChangedCollection.Add(Name, Value);
		}


		/// <summary>
		/// 从 XML 文件中获取数据并转换成 DataTable 数据集
		/// </summary>
		/// <param name="xPath"></param>
		/// <returns>DataTable</returns>
		private DataTable ConvertXmlToDataTable(string xPath)
		{
			OperateXml oxml		= new OperateXml();
			oxml.filePath		= this.filePath;

			XmlNodeList xlist	= oxml.GetXmlNodeList(xPath, 1);

			return oxml.ConvertXmlNodeListDataTable(xlist, 1);
		}

		private void WFSession_Load()
		{
            if (FileOP.IsExist(this.filePath, FileOPMethod.File))
			{
				// 文件存在，将当前 XML 文件中的数据读入到 DataSet 中
				// ds.ReadXml(this.filePath, XmlReadMode.DiffGram);
				DataTable dt = ConvertXmlToDataTable(this.rootName);

				for( int i = 0; i < dt.Columns.Count; i++ )
				{
					DataTable dd = ConvertXmlToDataTable("//" + this.rootName + "/" + dt.Columns[i].ColumnName);
					dd.TableName = dt.Columns[i].ColumnName;

					this.SessionDs.Tables.Add(dd);
				}

				WFGlobal.writeLine.Append("公有表 " + dt.Columns.Count + " 个\r\n");
			}
			else
			{
				// 文件不存在，开始创建
				OperateXml oXml = new OperateXml();
				oXml.filePath	= this.filePath;
				oXml.CreateXml(this.rootName);

				DataTable dt	= new DataTable(this.tableName);
				DataRow dr;

				dr = dt.NewRow();
				dt.Rows.Add(dr);
				this.SessionDs.Tables.Add(dt);

				oXml.xPath	= this.rootName;
				oXml.CreateNode(this.tableName, dt);
			}


			if( this.SessionDs.Tables.Contains(this.tableName) )
			{
			}
			else
			{
				// 当前操作的表不存在，创建该表
				DataTable dt	= new DataTable(this.tableName);
				DataRow dr;

				dr = dt.NewRow();
				dt.Rows.Add(dr);
				this.SessionDs.Tables.Add(dt);

				OperateXml oXml = new OperateXml();
				oXml.filePath	= this.filePath;
				oXml.xPath	= this.rootName;
				oXml.CreateNode(this.tableName, dt);
			}
		}
		#endregion

		// Private Fields
		private string		_rootName	= "UserInfos";	// 默认根节点名称
		private string		_tableName	= "UserInfo";	// 默认操作表名称
		private string		_filePath	= string.Empty;
		private DataSet		SessionDs	= new DataSet();
		private Hashtable	IsChangedCollection = new Hashtable();
		private Hashtable	IsAddedCollection	= new Hashtable();


		#region Public Property
		/// <summary>
		/// 当前操作的自定义 Session 所属的根节点的名称
		/// </summary>
		public string rootName
		{
			set
			{
				_rootName = value;
			}
			get
			{
				return _rootName;
			}
		}

		/// <summary>
		/// 当前操作的自定义 Session 所属的名称
		/// </summary>
		public string tableName
		{
			set
			{
				_tableName = value;
			}
			get
			{
				return _tableName;
			}
		}

		/// <summary>
		/// 自定义 Session 信息保存的路径
		/// </summary>
		private string filePath
		{
			set
			{
				_filePath = value;
			}
			get
			{
				if( this._filePath == string.Empty )
					throw new Exception("未指定路径");
				return _filePath;
			}
		}

		/// <summary>
		/// 获取当前表的所拥有的键的数目
		/// </summary>
		public int Count
		{
			get
			{
				return this.SessionDs.Tables[this.tableName].Columns.Count;
			}
		}

		/// <summary>
		/// 获取或设置当前用户配置信息
		/// </summary>
		public object this[string name]
		{
			get
			{
				return this.Get(name);
			}
			set
			{
				this.Set(name, value);
			}
		}

		#endregion


		#region Pulic Method


		/// <summary>
		/// 向当前表中添加键及其值，如果存在则修改该键值。
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="obj"></param>
		public void Add(string Name, object obj)
		{
			DataTable dt = this.SessionDs.Tables[this.tableName];

			if( dt.Columns.Contains(Name) )
			{
				this.Set(Name, obj);
			}
			else
			{
				dt.Columns.Add(Name);

				if( dt.Rows.Count == 0 )
				{
					DataRow dr;
					dr = dt.NewRow();
					dr[Name] = obj;
					dt.Rows.Add(dr);
				}
				else
				{
					dt.Rows[0][Name] = obj;
				}

				this.AddedCollection(Name, obj);
			}
		}

		/// <summary>
		/// 获取当前表中指定列名称的值
		/// </summary>
		/// <param name="Name"></param>
		/// <returns></returns>
		public object Get(string Name)
		{
			DataTable dt = this.SessionDs.Tables[this.tableName];

			if( dt.Columns.Contains(Name) )
			{
				return dt.Rows[0][Name];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获取指定 tableName 表中指定列名称的值
		/// </summary>
		/// <param name="tableName"></param>
		/// <param name="Name"></param>
		/// <returns></returns>
		public object Get(string tableName, string Name)
		{
			DataTable dt = this.SessionDs.Tables[tableName];

			if( dt.Columns.Contains(Name) )
			{
				return dt.Rows[0][Name];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 设置当前表中指定列名称的值，如果不存在则添加该键值
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="obj"></param>
		public void Set(string Name, object obj)
		{
			DataTable dt = this.SessionDs.Tables[this.tableName];

			if( dt.Columns.Contains(Name) )
			{
				dt.Rows[0][Name] = obj;

				this.ChangedCollection(Name, obj);
			}
			else
			{
				this.Add(Name, obj);
			}
		}

		/// <summary>
		/// 当前操作表中是否含有指定的列名称
		/// </summary>
		/// <param name="Name"></param>
		public bool Contains(string Name)
		{
			return this.SessionDs.Tables[this.tableName].Columns.Contains(Name);
		}

		/// <summary>
		/// 从当前表中移除指定列名称的列及其值
		/// </summary>
		/// <param name="Name"></param>
		public void Remove(string Name)
		{
			// 开始移除指定的配置信息
			OperateXml oXml = new OperateXml();
			oXml.filePath	= this.filePath;
			oXml.xPath		= "//" + this.rootName + "/" + this.tableName;

			// 开始删除
			oXml.DeleteNode(Name);
		}

		/// <summary>
		/// 处理或接受信息的变更
		/// </summary>
		public void AcceptChanges()
		{
			#region 处理添加
			if( this.IsAddedCollection.Count > 0 )
			{
				foreach(string Name in this.IsAddedCollection.Keys)
				{
					DataTable dt	= new DataTable();

					// 开始添加
					OperateXml oXml = new OperateXml();
					oXml.filePath	= this.filePath;
					oXml.xPath		= "//" + this.rootName + "/" + this.tableName;

					oXml.CreateNode(Name, dt);

					this.IsChangedCollection.Add(Name, this.IsAddedCollection[Name]);
				}

				this.IsAddedCollection.Clear();
			}

			#endregion

			#region 处理修改

			if( this.IsChangedCollection.Count > 0 )
			{
				DataTable dt	= new DataTable();
				DataRow dr;

				dr = dt.NewRow();

				foreach(string Name in this.IsChangedCollection.Keys)
				{
					dt.Columns.Add(Name);	dr[Name] = this.IsChangedCollection[Name];
				}
				dt.Rows.Add(dr);

				// 开始修改
				OperateXml oXml = new OperateXml();
				oXml.filePath	= this.filePath;
				oXml.xPath		= "//" + this.rootName + "/" + this.tableName;

				oXml.ChangeNode(dt);
#if DEBUG
				WFGlobal.writeLine.Append("改写当前自定义 Session 信息，路径: " + this.filePath + "，表名: " + this.tableName + "。\r\n");
#endif

				this.IsChangedCollection.Clear();
			}
			#endregion

		}
		#endregion
    }
}
