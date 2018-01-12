using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WFNetLib.WFSession
{
    /// <summary>
	/// WFSessionCollection : 自定义类 Session 操作类的集合。
	/// </summary>
    public class WFSessionCollection
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oSession"></param>
        public void Add(WFSession oSession)
        {
            //InnerList.Add(oSession);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oSession"></param>
        public void Remove(WFSession oSession)
        {
            //InnerList.Remove(oSession);
        }

        // Private Field
        private string _tableName = "UserInfo";
        private string _filePath = string.Empty;


        #region Public Property
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
        public string filePath
        {
            set
            {
                _filePath = value;
            }
            get
            {
                if (this._filePath == string.Empty)
                    throw new Exception("未指定路径");
                return _filePath;
            }
        }




        /// <summary>
        /// 
        /// </summary>
        public WFSession this[string tableName]
        {
            get
            {
                WFSession oSession = new WFSession();
                //oSession.tableName = tableName;
                return oSession;
            }
        }
        #endregion
    }
}
