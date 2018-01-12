using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;


//该源码下载自www.51aspx.com(５１ａｓｐｘ．ｃｏｍ)
namespace SQLDBTool
{
    public class DataAccess
    {
        public static string Type; 
        public static string strConn;
        public DataTable GetTables()
        {
            string sql;
            if (Type == "sql")
            {
                sql = "select name as TableName from sysobjects where xtype='U' AND name<>'dtproperties' AND name<>'LoanBank' AND name not like '%asp%' order by name";
                return WFNetLib.ADO.SQLServerOP.DataTableSQL(strConn, sql);
            }
            else if (Type == "access")
            {
                using (OleDbConnection connection = new
                       OleDbConnection(strConn))
                {
                    connection.Open();
                    DataTable schemaTable = connection.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables,
                        new object[] { null, null, null, "TABLE" });
                    return schemaTable;
                }
            }
            else
                return null;
        }
        public DataTable GetColumnsByTable(string tableName)
        {
            if (Type == "sql")
            {
                string sql = "SELECT C.name as ColumnName, C.xtype AS ColumnType,C.length as Length,C.isnullable as Nullable,b.text as sfwerw" +
                                        " FROM sysobjects T,syscolumns C left join syscomments b on C.cdefault=b.id   " +
                                        " WHERE T.id =C.id AND T.xtype='U' AND T.name='" + tableName + "'";
                return WFNetLib.ADO.SQLServerOP.DataTableSQL(strConn, sql);
            }
            else if (Type == "access")
            {
                using (OleDbConnection connection = new
                       OleDbConnection(strConn))
                {
                    connection.Open();
                    DataTable schemaTable = connection.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Columns,
                        new object[] { null, null, tableName, null });
                    return schemaTable;
                }
            }
            else
                return null;
        }
        public DataTable GetPrimaryKeyByTable(string tableName)
        {
            if (Type == "sql")
            {
                WFNetLib.ADO.SQLServerOP sql = new WFNetLib.ADO.SQLServerOP();
                SqlParameter param = new SqlParameter("table_name", tableName);
                return sql.DataSetProcedure(strConn, "sp_pkeys ", new IDataParameter[1] { param }, "x").Tables[0];
            }
            else if (Type == "access")
            {
                using (OleDbConnection connection = new
                       OleDbConnection(strConn))
                {
                    connection.Open();
                    DataTable schemaTable = connection.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Primary_Keys,
                        new object[] { null, null, tableName });
                    return schemaTable;
                }
            }
            else
                return null;
        }
    }
}
//5_1_a_s_p_x.c_o_m
