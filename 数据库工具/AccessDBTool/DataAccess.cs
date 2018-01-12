using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;


//该源码下载自www.51aspx.com(５１ａｓｐｘ．ｃｏｍ)
namespace AccessDBTool
{
    public class DataAccess
    {
        public static string strConn;
        public DataTable GetTables()
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
        public DataTable GetColumnsByTable(string tableName)
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
        public DataTable GetPrimaryKeyByTable(string tableName)
        {
            using (OleDbConnection connection = new
                       OleDbConnection(strConn))
            {
                connection.Open();
                DataTable schemaTable = connection.GetOleDbSchemaTable(
                    OleDbSchemaGuid.Primary_Keys,
                    new object[] { null, null, tableName});
                return schemaTable;
            }
        }
    }
}
//5_1_a_s_p_x.c_o_m
