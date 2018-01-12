using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WFNetLib.ADO.DBToolGenerate
{
    public class Generate
    {
        public string table_name;
        public DataTable dtColomns;
        public DataTable dtPrimaryKey;
        public string PrimaryKey = string.Empty;
        public string PrimaryKeyType = null;
        public object PrimaryKeyTypeNum = null;
        public string type;
        public string ColumnName;
        public string ColumnType;
        public string ColumnDefault;
        public bool bWeb;
        public bool bDLL;
        public Generate()
        {

        }
        public string Getpropertity(string ns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;");
            sb.Append(System.Environment.NewLine);
            //sb.Append("using System.Collections.Generic;");
            //sb.Append(System.Environment.NewLine);
            //sb.Append("using System.Text;");
            //sb.Append(System.Environment.NewLine);
            sb.Append("using System.Data;");
            sb.Append(System.Environment.NewLine);
            if(!bDLL)
            {
                sb.Append("using System.Windows.Forms;");
                sb.Append(System.Environment.NewLine);
            }            
            sb.Append("using System.Text;");
            sb.Append(System.Environment.NewLine);
            if (type == "access")
            {
                sb.Append("using System.Data.OleDb;");
                sb.Append(System.Environment.NewLine);
            }
            else if (type == "sql")
            {
                sb.Append("using System.Data.SqlClient;");
                sb.Append(System.Environment.NewLine);
            }
            sb.Append(System.Environment.NewLine);
            sb.Append("namespace " + ns);
            sb.Append(System.Environment.NewLine);
            sb.Append("{");
            sb.Append(System.Environment.NewLine);
            sb.Append("     [Serializable]");
            sb.Append(System.Environment.NewLine);
            sb.Append("     public class " + table_name + "Data");
            sb.Append(System.Environment.NewLine);
            sb.Append("     {");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                sb.Append("         public const string " + dr[ColumnName].ToString().ToUpperInvariant() + "Field =@\"" + dr[ColumnName].ToString() + "\";");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                if (dr[ColumnName].ToString() == PrimaryKey)//当前列为主键
                {
                    sb.Append("         public const string PRIMARYKEY_Field" + "=@\"" + dr[ColumnName].ToString() + "\";");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                }
            }

            //字段
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                sb.Append("         private " + GetTypeByID(dr[ColumnType]) + " _" + dr[ColumnName].ToString() + ";");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                if (dr[ColumnName].ToString() == PrimaryKey)//当前列为主键
                {
                    PrimaryKeyType = GetTypeByID(dr[ColumnType]);
                    PrimaryKeyTypeNum = dr[ColumnType];
                }
            }
            //属性
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                sb.Append("         public " + GetTypeByID(dr[ColumnType]) + " " + dr[ColumnName].ToString());
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             get{ return _" + dr[ColumnName].ToString() + ";}");
                sb.Append(System.Environment.NewLine);
                sb.Append("             set{ _" + dr[ColumnName].ToString() + "=value;}");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
            }
            //初始化
            sb.Append("         public " + table_name + "Data()");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                if (type == "access")
                {
                    if (GetInitByID(dr[ColumnType]) != "")
                    {
                        if ((Boolean)dr["COLUMN_HASDEFAULT"])
                        {
                            sb.Append("             " + "_" + dr[ColumnName].ToString() + "=" + access.GetDefaultByID(dr[ColumnType],dr[ColumnDefault].ToString()) + ";");
                        }
                        else
                            sb.Append("             " + "_" + dr[ColumnName].ToString() + "=" + GetInitByID(dr[ColumnType]) + ";");
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (type == "sql")
                {
                    if (dr[ColumnDefault].ToString() != "")
                    {
                        sb.Append("             " + "_" + dr[ColumnName].ToString() + "=" + sql.GetDefaultByID(dr[ColumnType],dr[ColumnDefault].ToString()) + ";");
                    }
                    else if (GetInitByID(dr[ColumnType]) != "")
                        sb.Append("             " + "_" + dr[ColumnName].ToString() + "=" + GetInitByID(dr[ColumnType]) + ";");
                    else
                    {
                        continue;
                    }

                }
                sb.Append(System.Environment.NewLine);
            }
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            sb.Append("     }");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            sb.Append("     public class " + table_name + "DataFactory");
            sb.Append(System.Environment.NewLine);
            sb.Append("     {");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* Reader读取                                                           */
            /************************************************************************/
            sb.Append("         public static " + table_name + "Data Construct(IDataReader reader)");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             " + table_name + "Data data = new " + table_name + "Data();");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                if (dr[ColumnType].ToString() == "36")
                    sb.Append("             try{  data." + dr[ColumnName].ToString() + " = (Guid)" + "(reader[\"" + dr[ColumnName] + "\"]) ;} catch { } ");
                else
                    sb.Append("             try{  data." + dr[ColumnName].ToString() + " = Convert.To" + GetTypeByID(dr[ColumnType]) + "(reader[\"" + dr[ColumnName] + "\"])  ;} catch { } ");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);

            }
            sb.Append("             return data;");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* 主键取                                                               */
            /************************************************************************/
            if (PrimaryKeyType!=null)
            {
                sb.Append("         public static " + PrimaryKeyType + " GetPrimaryKey(DataTable dt)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "return GetPrimaryKey(dt,0);");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);

                sb.Append("         public static " + PrimaryKeyType + " GetPrimaryKey(DataTable dt,int rowIndex)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + PrimaryKeyType + " PrimaryKey=" + GetInitByID(PrimaryKeyTypeNum) + ";");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "if(dt.Rows.Count<rowIndex+1)");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("                 " + "return " + GetInitByID(PrimaryKeyTypeNum) + ";");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "DataRow dr=dt.Rows[rowIndex];");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "if(dr==null)");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("                 " + "return " + GetInitByID(PrimaryKeyTypeNum) + ";");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                if (PrimaryKeyType == "Guid")
                    sb.Append("             try{ PrimaryKey" + " = (Guid)" + "(dr[" + table_name + "Data.PRIMARYKEY_Field]) ;} catch { } ");
                else
                    sb.Append("             try{ PrimaryKey" + " = Convert.To" + PrimaryKeyType + "(dr[" + table_name + "Data.PRIMARYKEY_Field]) ;} catch { } ");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "return PrimaryKey;");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
            }
            /************************************************************************/
            /* 表读取                                                               */
            /************************************************************************/
            sb.Append("         public static " + table_name + "Data Construct(DataTable dt)");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             " + "return Construct(dt,0);");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* 反相读取                                                             */
            /************************************************************************/
            sb.Append("         public static " + table_name + "Data Construct(DataTable dt,int rowIndex)");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             " + table_name + "Data data = new " + table_name + "Data();");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            sb.Append("             " + "if(dt.Rows.Count<rowIndex+1)");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            sb.Append("                 " + "return null;");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            sb.Append("             " + "DataRow dr=dt.Rows[rowIndex];");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            sb.Append("             " + "if(dr==null)");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            sb.Append("                 " + "return null;");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                //sb.Append("             data." + dr[ColumnName].ToString() + " = dr[\"" + dr[ColumnName] + "\"];");
                if (dr[ColumnType].ToString() == "36")
                    sb.Append("             try{ data." + dr[ColumnName].ToString() + " = new Guid(" + "dr[\"" + dr[ColumnName] + "\"].ToString()) ;} catch { } ");
                else
                    sb.Append("             try{ data." + dr[ColumnName].ToString() + " = Convert.To" + GetTypeByID(dr[ColumnType]) + "(dr[\"" + dr[ColumnName] + "\"]) ;} catch { } ");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
            }
            sb.Append("             return data;");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* DataGridView反相读取                                                 */
            /************************************************************************/
            if (bWeb)
            {

            }
            else
            {
                sb.Append("         public static " + table_name + "Data Construct(DataGridView dgv,int rowIndex)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + table_name + "Data data = new " + table_name + "Data();");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "if(dgv.Rows.Count<rowIndex+1)");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("                 " + "return null;");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "DataGridViewCellCollection dr=dgv.Rows[rowIndex].Cells;");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + "if(dr==null)");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("                 " + "return null;");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                for (int i = 0; i < dtColomns.Rows.Count; i++)
                {
                    DataRow dr = dtColomns.Rows[i];
                    //sb.Append("             data." + dr[ColumnName].ToString() + " = dr[\"" + dr[ColumnName] + "\"];");
                    if (dr[ColumnType].ToString() == "36")
                        sb.Append("             try{ data." + dr[ColumnName].ToString() + " = new Guid(" + "dr[\"" + dr[ColumnName] + "\"].ToString()) ;} catch { } ");
                    else
                        sb.Append("             try{ data." + dr[ColumnName].ToString() + " = Convert.To" + GetTypeByID(dr[ColumnType]) + "(dr[\"" + dr[ColumnName] + "\"]) ;} catch { } ");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                }
                sb.Append("             return data;");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
            }
            /************************************************************************/
            /* DataGridView反相主键读取                                                 */
            /************************************************************************/
            if (PrimaryKeyType!=null)
            {
                if (bWeb)
                {

                }
                else
                {
                    sb.Append("         public static " + PrimaryKeyType + " GetPrimaryKey(DataGridView dgv,int rowIndex)");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("         {");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             " + PrimaryKeyType + " PrimaryKey=" + GetInitByID(PrimaryKeyTypeNum) + ";");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             " + "if(dgv.Rows.Count<rowIndex+1)");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                    sb.Append("                 " + "return " + GetInitByID(PrimaryKeyTypeNum) + ";");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             " + "DataGridViewCellCollection dr=dgv.Rows[rowIndex].Cells;");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             " + "if(dr==null)");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                    sb.Append("                 " + "return " + GetInitByID(PrimaryKeyTypeNum) + ";");
                    sb.Append(System.Environment.NewLine);
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             " + "for (int i = 0; i < dgv.Columns.Count; i++)");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             " + "{");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("                 " + "if(dgv.Columns[i].DataPropertyName==" + table_name + "Data.PRIMARYKEY_Field)");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("                 " + "{");
                    sb.Append(System.Environment.NewLine);
                    if (PrimaryKeyType == "Guid")
                        sb.Append("                     try{ PrimaryKey" + " = (Guid)" + "(dr[i].Value) ;} catch { } ");
                    else
                        sb.Append("                     try{ PrimaryKey" + " = Convert.To" + PrimaryKeyType + "(dr[i].Value) ;} catch { } ");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("                         " + "return PrimaryKey;");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("                 " + "}");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             " + "}");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             return PrimaryKey;");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("         }");
                    sb.Append(System.Environment.NewLine);
                }
            }
            /************************************************************************/
            /* DataRow反相读取                                                      */
            /************************************************************************/
            sb.Append("         public static " + table_name + "Data Construct(DataRow dr)");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             " + table_name + "Data data = new " + table_name + "Data();");
            sb.Append(System.Environment.NewLine);
            sb.Append(System.Environment.NewLine);
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                //sb.Append("             data." + dr[ColumnName].ToString() + " = dr[\"" + dr[ColumnName] + "\"];");
                if (dr[ColumnType].ToString() == "36")
                    sb.Append("             try{ data." + dr[ColumnName].ToString() + " = new Guid(" + "dr[\"" + dr[ColumnName] + "\"].ToString()) ;} catch { } ");
                else
                    sb.Append("             try{ data." + dr[ColumnName].ToString() + " = Convert.To" + GetTypeByID(dr[ColumnType]) + "(dr[\"" + dr[ColumnName] + "\"]) ;} catch { } ");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
            }
            sb.Append("             return data;");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* DataRow反相主键读取                                                 */
            /************************************************************************/
            if (PrimaryKeyType!=null)
            {
                sb.Append("         public static " + PrimaryKeyType + " GetPrimaryKey(DataRow dr)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             " + PrimaryKeyType + " PrimaryKey=" + GetInitByID(PrimaryKeyTypeNum) + ";");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                if (PrimaryKeyType == "Guid")
                    sb.Append("             try{ PrimaryKey" + " = (Guid)" + "(dr[" + table_name + "Data.PRIMARYKEY_Field]) ;} catch { } ");
                else
                    sb.Append("             try{ PrimaryKey" + " = Convert.To" + PrimaryKeyType + "(dr[" + table_name + "Data.PRIMARYKEY_Field]) ;} catch { } ");
                sb.Append(System.Environment.NewLine);
                sb.Append("                 " + "return PrimaryKey;");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
            }
           
            /************************************************************************/
            /* 类结束                                                               */
            /************************************************************************/
            sb.Append("     }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* 数据库操作                                                           */
            /************************************************************************/
            sb.Append("     public class " + table_name + "DataDBOption");
            sb.Append(System.Environment.NewLine);
            sb.Append("     {");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* 插入                                                                 */
            /************************************************************************/
            sb.Append("         public static int " + "Insert(" + table_name + "Data d)");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             StringBuilder sql=new StringBuilder();");
            sb.Append(System.Environment.NewLine);
            sb.Append("             sql.Append(\"insert into "+table_name+" (\");");
            sb.Append(System.Environment.NewLine);
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                if (dr[ColumnName].ToString() == PrimaryKey)
                    continue;
                sb.Append("             sql.Append(\""+dr[ColumnName].ToString());
                if(i!=dtColomns.Rows.Count-1)
                {
                    sb.Append(", ");
                }
                sb.Append("\");");
                sb.Append(System.Environment.NewLine);
            }
            sb.Append("             sql.Append(\") values (\");");
            sb.Append(System.Environment.NewLine);
            for (int i = 0; i < dtColomns.Rows.Count; i++)
            {
                DataRow dr = dtColomns.Rows[i];
                if (dr[ColumnName].ToString() == PrimaryKey)
                    continue;
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(dr[ColumnType])+"\");");
                sb.Append(System.Environment.NewLine);
                if (type == "access")
                {
                    if (dr[ColumnType].ToString() == "7")//datetime
                        sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString(\"yyyy-MM-dd HH:mm:ss\"));");
                    else if (dr[ColumnType].ToString() == "11") //bool
                    {
                        sb.Append("             if(d." + dr[ColumnName].ToString() + ")");
                        sb.Append(System.Environment.NewLine);
                        sb.Append("                 sql.Append(\"1\");");
                        sb.Append(System.Environment.NewLine);
                        sb.Append("             else");
                        sb.Append(System.Environment.NewLine);
                        sb.Append("                 sql.Append(\"0\");");
                    }
                    else
                        sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString());");
                }
                else if (type == "sql")
                {
                    if (dr[ColumnType].ToString() == "61")//datetime
                        sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString(\"yyyy-MM-dd HH:mm:ss\"));");
                    else if (dr[ColumnType].ToString() == "104") //bool
                    {
                        sb.Append("             if(d." + dr[ColumnName].ToString() + ")");
                        sb.Append(System.Environment.NewLine);
                        sb.Append("                 sql.Append(\"1\");");
                        sb.Append(System.Environment.NewLine);
                        sb.Append("             else");
                        sb.Append(System.Environment.NewLine);
                        sb.Append("                 sql.Append(\"0\");");
                    }
                    else
                        sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString());");
                }                
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(dr[ColumnType]) + "\");");
                sb.Append(System.Environment.NewLine);
                if (i != dtColomns.Rows.Count - 1)
                {
                    sb.Append("             sql.Append(\", \");");
                    sb.Append(System.Environment.NewLine);
                }
            }
            sb.Append("             sql.Append(\")\");");
            sb.Append(System.Environment.NewLine);
            if(type=="access")
                sb.Append("             return WFNetLib.ADO.AccessOP.NonQuerySQL(sql.ToString());");
            else if(type=="sql")
                sb.Append("             return WFNetLib.ADO.SQLServerOP.NonQuerySQL(sql.ToString());");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* 更新                                                                 */
            /************************************************************************/
            if (PrimaryKeyType != null)
            {
                sb.Append("         public static int " + "Update(" + table_name + "Data d)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             StringBuilder sql=new StringBuilder();");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"update " + table_name + " set \");");
                sb.Append(System.Environment.NewLine);
                for (int i = 0; i < dtColomns.Rows.Count; i++)
                {
                    DataRow dr = dtColomns.Rows[i];
                    if (dr[ColumnName].ToString() == PrimaryKey)
                        continue;
                    sb.Append("             sql.Append(\"" + dr[ColumnName].ToString() + "=\");");
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             sql.Append(\"" + GetHeaderOPByID(dr[ColumnType]) + "\");");
                    sb.Append(System.Environment.NewLine);
                    if (type == "access")
                    {
                        if (dr[ColumnType].ToString() == "7")//datetime
                            sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString(\"yyyy-MM-dd HH:mm:ss\"));");
                        else if (dr[ColumnType].ToString() == "11") //bool
                        {
                            sb.Append("             if(d." + dr[ColumnName].ToString() + ")");
                            sb.Append(System.Environment.NewLine);
                            sb.Append("                 sql.Append(\"1\");");
                            sb.Append(System.Environment.NewLine);
                            sb.Append("             else");
                            sb.Append(System.Environment.NewLine);
                            sb.Append("                 sql.Append(\"0\");");
                        }
                        else
                            sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString());");
                    }
                    else if (type == "sql")
                    {
                        if (dr[ColumnType].ToString() == "61")//datetime
                            sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString(\"yyyy-MM-dd HH:mm:ss\"));");
                        else if (dr[ColumnType].ToString() == "104") //bool
                        {
                            sb.Append("             if(d." + dr[ColumnName].ToString() + ")");
                            sb.Append(System.Environment.NewLine);
                            sb.Append("                 sql.Append(\"1\");");
                            sb.Append(System.Environment.NewLine);
                            sb.Append("             else");
                            sb.Append(System.Environment.NewLine);
                            sb.Append("                 sql.Append(\"0\");");
                        }
                        else
                            sb.Append("             sql.Append(d." + dr[ColumnName].ToString() + ".ToString());");
                    }
                    sb.Append(System.Environment.NewLine);
                    sb.Append("             sql.Append(\"" + GetHeaderOPByID(dr[ColumnType]) + "\");");
                    sb.Append(System.Environment.NewLine);
                    if (i != dtColomns.Rows.Count - 1)
                    {
                        sb.Append("             sql.Append(\", \");");
                        sb.Append(System.Environment.NewLine);
                    }
                }
                sb.Append("             sql.Append(\" where " + PrimaryKey + "=\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(d." + PrimaryKey + ".ToString());");
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                if (type == "access")
                    sb.Append("             return WFNetLib.ADO.AccessOP.NonQuerySQL(sql.ToString());");
                else if (type == "sql")
                    sb.Append("             return WFNetLib.ADO.SQLServerOP.NonQuerySQL(sql.ToString());");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
            }
            /************************************************************************/
            /*  删除                                                                */
            /************************************************************************/
            if (PrimaryKeyType != null)
            {
                sb.Append("         public static int " + "Delete(" + table_name + "Data d)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             StringBuilder sql=new StringBuilder();");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"delete from " + table_name + " \");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\" where " + PrimaryKey + "=\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(d." + PrimaryKey + ".ToString());");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                if (type == "access")
                    sb.Append("             return WFNetLib.ADO.AccessOP.NonQuerySQL(sql.ToString());");
                else if (type == "sql")
                    sb.Append("             return WFNetLib.ADO.SQLServerOP.NonQuerySQL(sql.ToString());");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
            }
            /************************************************************************/
            /*  查询table                                                           */
            /************************************************************************/
            sb.Append("         public static DataTable " + "DataTableSelect()");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             StringBuilder sql=new StringBuilder();");
            sb.Append(System.Environment.NewLine);
            sb.Append("             sql.Append(\"select * from " + table_name + " \");");
            sb.Append(System.Environment.NewLine);
            if (type == "access")
                sb.Append("             return WFNetLib.ADO.AccessOP.DataTableSQL(sql.ToString());");
            else if (type == "sql")
                sb.Append("             return WFNetLib.ADO.SQLServerOP.DataTableSQL(sql.ToString());");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /*  查询table                                                           */
            /************************************************************************/
            if (PrimaryKeyType != null)
            {
                sb.Append("         public static DataTable " + "DataTableSelect(" + table_name + "Data d)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             StringBuilder sql=new StringBuilder();");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"select * from " + table_name + " where " + PrimaryKey + "=\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(d." + PrimaryKey + ".ToString());");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                if (type == "access")
                    sb.Append("             return WFNetLib.ADO.AccessOP.DataTableSQL(sql.ToString());");
                else if (type == "sql")
                    sb.Append("             return WFNetLib.ADO.SQLServerOP.DataTableSQL(sql.ToString());");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
            }
            /************************************************************************/
            /*  查询类                                                          */
            /************************************************************************/
            if (PrimaryKeyType != null)
            {
                sb.Append("         public static "+ table_name +"Data Get(" + PrimaryKeyType + " _id)");
                sb.Append(System.Environment.NewLine);
                sb.Append("         {");
                sb.Append(System.Environment.NewLine);
                sb.Append("             StringBuilder sql=new StringBuilder();");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"select * from " + table_name + " where " + PrimaryKey + "=\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(_id.ToString());");
                sb.Append(System.Environment.NewLine);
                sb.Append("             sql.Append(\"" + GetHeaderOPByID(PrimaryKeyTypeNum) + "\");");
                sb.Append(System.Environment.NewLine);
                if (type == "access")
                    sb.Append("             DataTable dt = WFNetLib.ADO.AccessOP.DataTableSQL(sql.ToString());");
                else if (type == "sql")
                    sb.Append("             DataTable dt = WFNetLib.ADO.SQLServerOP.DataTableSQL(sql.ToString());");
                sb.Append(System.Environment.NewLine);
                sb.Append("             if(dt.Rows.Count==0)");
                sb.Append(System.Environment.NewLine);
                sb.Append("                 return null;");
                sb.Append(System.Environment.NewLine);
                sb.Append("             else");
                sb.Append(System.Environment.NewLine);
                sb.Append("                 return " + table_name + "DataFactory.Construct(dt.Rows[0]);");
                sb.Append(System.Environment.NewLine);
                sb.Append("         }");
                sb.Append(System.Environment.NewLine);
            }
            /************************************************************************/
            /*  查询DataSet                                                         */
            /************************************************************************/
            sb.Append("         public static DataSet " + "DataSetSelect()");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             StringBuilder sql=new StringBuilder();");
            sb.Append(System.Environment.NewLine);
            sb.Append("             sql.Append(\"select * from " + table_name + " \");");
            sb.Append(System.Environment.NewLine);
            if (type == "access")
                sb.Append("             return WFNetLib.ADO.AccessOP.DataSetSQL(sql.ToString());");
            else if (type == "sql")
                sb.Append("             return WFNetLib.ADO.SQLServerOP.DataSetSQL(sql.ToString());");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /*  查询                                                                */
            /************************************************************************/
            if(type=="access")
                sb.Append("         public static OleDbDataReader " + "ReaderSelect()");
            else if(type=="sql")
                sb.Append("         public static SqlDataReader " + "ReaderSelect(" + table_name + "Data d)");
            sb.Append(System.Environment.NewLine);
            sb.Append("         {");
            sb.Append(System.Environment.NewLine);
            sb.Append("             StringBuilder sql=new StringBuilder();");
            sb.Append(System.Environment.NewLine);
            sb.Append("             sql.Append(\"select * from " + table_name + " \");");
            sb.Append(System.Environment.NewLine);
            if (type == "access")
                sb.Append("             return WFNetLib.ADO.AccessOP.ReaderExecuteSQL(sql.ToString());");
            else if (type == "sql")
                sb.Append("             return WFNetLib.ADO.SQLServerOP.ReaderExecuteSQL(sql.ToString());");
            sb.Append(System.Environment.NewLine);
            sb.Append("         }");
            sb.Append(System.Environment.NewLine);
            sb.Append("     }");
            sb.Append(System.Environment.NewLine);
            /************************************************************************/
            /* 结束                                                                 */
            /************************************************************************/
            sb.Append("}");            
            return sb.ToString();
        }

        private string GetInitByID(object p)
        {
            if (type == "access")
            {
                return access.GetInitByID(p);
            }
            else if (type == "sql")
            {
                return sql.GetInitByID(p);
            }
            return "";
        }

        private string GetTypeByID(object p)
        {
            if (type == "access")
            {
                return access.GetTypeByID(p);
            }
            else if (type == "sql")
            {
                return sql.GetTypeByID(p);
            }
            return "";
        }
        private string GetHeaderOPByID(object p)
        {
            if (type == "access")
            {
                return access.GetHeaderOPByID(p);
            }
            else if (type == "sql")
            {
                return sql.GetHeaderOPByID(p);
            }
            return "";
        }
    }
}
