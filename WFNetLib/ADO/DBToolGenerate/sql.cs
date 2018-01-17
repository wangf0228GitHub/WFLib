using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib.ADO.DBToolGenerate
{
    public class sql
    {
        public static String GetTypeByID(object o)
        {
            string type;
            switch (o.ToString())
            {
                case "127"://bigint
                    type = "Int64";
                    break;
                case "104"://bit
                    type = "Boolean";
                    break;
                case "175"://Char
                    type = "String";
                    break;
                case "61"://datetime
                    type = "DateTime";
                    break;
                case "106"://Decimal
                    type = "Decimal";
                    break;
                case "62"://Float
                    type = "Double";
                    break;
                case "56"://Int
                    type = "Int32";
                    break;
                case "60"://Money
                    type = "Decimal";
                    break;
                case "239"://NChar
                    type = "String";
                    break;
                case "99"://NText
                    type = "String";
                    break;
                case "231"://NVarChar
                    type = "String";
                    break;
                case "59"://Real
                    type = "Single";
                    break;
                case "36"://UniqueIdentifier
                    type = "Guid";
                    break;
                case "58"://SmallDateTime
                    type = "DateTime";
                    break;
                case "52"://SmallInt
                    type = "Int16";
                    break;
                case "122"://SmallMoney
                    type = "Decimal";
                    break;
                case "35"://Text
                    type = "String";
                    break;
                case "48"://TinyInt
                    type = "Byte";
                    break;
                case "167"://VarChar
                    type = "String";
                    break;
                //                 case "98" ://Variant
                //                     type="Object";                    
                //                     break;               
                default:
                    type = "";
                    break;
            }
            return type;
        }

        public static string GetInitByID(object o)
        {
            string init;
            switch (o.ToString())
            {
                case "127"://bigint
                    init = "0";
                    break;
                case "104"://bit
                    init = "false";
                    break;
                case "175"://Char
                    init = "\"\"";
                    break;
                case "61"://datetime
                    init = "DateTime.Now";
                    break;
                case "106"://Decimal
                    init = "0";
                    break;
                case "62"://Float
                    init = "0";
                    break;
                case "56"://Int
                    init = "0";
                    break;
                case "60"://Money
                    init = "0";
                    break;
                case "239"://NChar
                    init = "\"\"";
                    break;
                case "99"://NText
                    init = "\"\"";
                    break;
                case "231"://NVarChar
                    init = "\"\"";
                    break;
                case "59"://Real
                    init = "0";
                    break;
                case "36"://UniqueIdentifier
                    init = "Guid.Empty";
                    break;
                case "58"://SmallDateTime
                    init = "DateTime.Now";
                    break;
                case "52"://SmallInt
                    init = "0";
                    break;
                case "122"://SmallMoney
                    init = "0";
                    break;
                case "35"://Text
                    init = "\"\"";
                    break;
                case "48"://TinyInt
                    init = "0";
                    break;
                case "167"://VarChar
                    init = "\"\"";
                    break;
                //                 case "98" ://Variant
                //                     type="Object";                    
                //                     break;               
                default:
                    init = "";
                    break;
            }
            return init;
        }
        public static string GetDefaultByID(object o,string str)
        {
            string ret;
            str = str.Replace("(", "");
            str = str.Replace(")", "");
            str = str.Replace("'", "");
            switch (o.ToString())
            {
                case "127"://bigint
                    ret = str;
                    break;
                case "104"://bit
                    if (str == "1")
                        ret = "true";
                    else
                        ret = "false";
                    break;
                case "175"://Char
                    ret = "\""+str+"\"";
                    break;
                case "61"://datetime
                    ret = "DateTime.Now";
                    break;
                case "106"://Decimal
                    ret = str;
                    break;
                case "62"://Float
                    ret = str;
                    break;
                case "56"://Int
                    ret = str;
                    break;
                case "60"://Money
                    ret = str;
                    break;
                case "239"://NChar
                    ret = "\""+str+"\"";
                    break;
                case "99"://NText
                    ret = "\"" + str + "\"";
                    break;
                case "231"://NVarChar
                    ret = "\"" + str + "\"";
                    break;
                case "59"://Real
                    ret = str;
                    break;
                case "36"://UniqueIdentifier
                    ret = "Guid.Empty";
                    break;
                case "58"://SmallDateTime
                    ret = "DateTime.Now";
                    break;
                case "52"://SmallInt
                    ret = str;
                    break;
                case "122"://SmallMoney
                    ret = str;
                    break;
                case "35"://Text
                    ret = "\""+str+"\"";
                    break;
                case "48"://TinyInt
                    ret = str;
                    break;
                case "167"://VarChar
                    ret = "\""+str+"\"";
                    break;
                //                 case "98" ://Variant
                //                     type="Object";                    
                //                     break;               
                default:
                    ret = "";
                    break;
            }
            return ret;
        }
        public static string GetHeaderOPByID(object o)
        {
            string ret;
            switch (o.ToString())
            {
                case "127"://bigint
                    ret = "";
                    break;
                case "104"://bit
                    ret = "";
                    break;
                case "175"://Char
                    ret = "'";
                    break;
                case "61"://datetime
                    ret = "'";
                    break;
                case "106"://Decimal
                    ret = "";
                    break;
                case "62"://Float
                    ret = "";
                    break;
                case "56"://Int
                    ret = "";
                    break;
                case "60"://Money
                    ret = "";
                    break;
                case "239"://NChar
                    ret = "'";
                    break;
                case "99"://NText
                    ret = "'";
                    break;
                case "231"://NVarChar
                    ret = "'";
                    break;
                case "59"://Real
                    ret = "";
                    break;
                case "36"://UniqueIdentifier
                    ret = "'";
                    break;
                case "58"://SmallDateTime
                    ret = "#";
                    break;
                case "52"://SmallInt
                    ret = "";
                    break;
                case "122"://SmallMoney
                    ret = "";
                    break;
                case "35"://Text
                    ret = "'";
                    break;
                case "48"://TinyInt
                    ret = "";
                    break;
                case "167"://VarChar
                    ret = "'";
                    break;
                //                 case "98" ://Variant
                //                     type="Object";                    
                //                     break;               
                default:
                    ret = "";
                    break;
            }
            return ret;
        }
    }
}
