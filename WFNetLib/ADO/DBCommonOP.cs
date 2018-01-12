using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Collections;

namespace WFNetLib.ADO
{
    public enum DBType
    {
        Access,
        SQL
    }    
    public class DBCommonOP
    {
        public static DBType DataBaseType = DBType.SQL;
        public static int NonQuerySQL(string SQLString)
        {
            switch (DataBaseType)
            {
                case DBType.SQL:
                    return SQLServerOP.NonQuerySQL(SQLString);
                case DBType.Access:
                    return AccessOP.NonQuerySQL(SQLString);
            }
            return 0;
        }
        public static void NonQuerySQL_Tran(ArrayList SQLStringList)
        {
            switch (DataBaseType)
            {
                case DBType.SQL:
                    SQLServerOP.NonQuerySQL_Tran(SQLStringList);
                    break;
                case DBType.Access:
                    AccessOP.NonQuerySQL_Tran(SQLStringList);
                    break;
            }
        }
        public static int NonQuerySQL(string Conn, string SQLString, params object[] cmdParms)
        {
            switch(DataBaseType)
            {
                case DBType.SQL:
                    return SQLServerOP.NonQuerySQL(Conn, SQLString, (SqlParameter[])cmdParms);
                case DBType.Access:
                    return AccessOP.NonQuerySQL(Conn, SQLString, (OleDbParameter[])cmdParms);
            }
            return 0;
        }
    }
}
