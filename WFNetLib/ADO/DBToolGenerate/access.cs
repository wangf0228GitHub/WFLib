using System;
using System.Collections.Generic;

using System.Text;

namespace WFNetLib.ADO.DBToolGenerate
{
    public class access
    {
        public static String GetTypeByID(object o)
        {
            string type;
            switch (int.Parse(o.ToString()))
            { //case常量 值 说明 //case 0x2000 // :p = AdArray //（不适用于 ADOX。） 0x2000 一个标志值，通常与另一个数据类型常量组合，指示该数据类型的数组。 
                case 20: type = "Int64"; break;// "adBigInt 20 指示一个八字节的有符号整数 (DBTYPE_I8)。"; 
                case 128: type = "byte[]"; break;//"adBinary 128 指示一个二进制值 (DBTYPE_BYTES)。"; 
                case 11: type = "Boolean"; break;//"adBoolean 指示一个布尔值 (DBTYPE_BOOL)。";                     
                case 8: type = "String"; break;// "adBSTR 8 指示以 Null 终止的字符串 (Unicode) (DBTYPE_BSTR)。"; 
                //case 136: type="String";break; "adChapter 136 指示一个四字节的子集值，标识子行集合中的行 (DBTYPE_HCHAPTER)。"; 
                case 129: type = "String"; break;//"adChar129 指示一个字符串值 (DBTYPE_STR)。"; 
                case 6: type = "Decimal"; break; //"adCurrency 6 指示一个货币值 (DBTYPE_CY)。货币是一个定点数字，小数点右侧有四位数字。该值存储为八字节、范围为 10,000 的有符号整数。"; 
                case 7: type = "DateTime"; break; //"adDate 7 指示日期值 (DBTYPE_DATE)。日期保存为双精度数，数字的整数部分是从 1899 年 12 月 30 日算起的天数，小数部分是一天当中的片段时间。"; 
                case 133: type = "DateTime"; break; // "adDBDate 133 指示日期值 (yyyymmdd) (DBTYPE_DBDATE)。"; 
                case 134: type = "TimeSpan"; break; // "adDBTime 134 指示时间值 (hhmmss) (DBTYPE_DBTIME)。"; 
                case 135: type = "DateTime"; break; //"adDBTimeStamp 135 指示日期/时间戳（yyyymmddhhmmss 加十亿分之一的小数）(DBTYPE_DBTIMESTAMP)。"; 
                case 14: type = "Decimal"; break; // "adDecimal 14 指示具有固定精度和范围的确切数字值 (DBTYPE_DECIMAL)。";
                case 5: type = "Double"; break; // "adDouble 5 指示一个双精度浮点值 (DBTYPE_R8)。"; 
                case 0: type = ""; break; // "adEmpty 0 指定没有值 (DBTYPE_EMPTY)。"; 
                case 10: type = "Exception"; break; // "adError 10 指示一个 32 位的错误代码 (DBTYPE_ERROR)。"; 
                case 64: type = "DateTime"; break; // "adFileTime 64 指示一个 64 位的值，表示从 1601 年 1 月 1 日开始的 100 个十亿分之一秒间隔的数量 (DBTYPE_FILETIME)。"; 
                case 72: type = "Guid"; break; // "adGUID 72 指示全局唯一标识符 (GUID) (DBTYPE_GUID)。"; 
                //case 9: p = "adIDispatch 9 指示指向 COM 对象上 IDispatch 接口的指针 (DBTYPE_IDISPATCH)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 3: type = "Int32"; break; // " adInteger 3 指示一个四字节的有符号整数 (DBTYPE_I4)。"; 
                //case 13: p = "adIUnknown 13 指示指向 COM 对象上 IUnknown 接口的指针 (DBTYPE_IUNKNOWN)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 205: type = "byte[]"; break; // "adLongVarBinary 205 指示一个长二进制值（仅限于 Parameter 对象）。"; 
                case 201: type = "String"; break; // " adLongVarChar 201 指示一个长字符串值（仅限于 Parameter 对象）。"; 
                case 203: type = "String"; break; // "adLongVarWChar 203 指示一个以 Null 终止的长 Unicode 字符串值（仅限于 Parameter 对象）。"; 
                case 131: type = "Decimal"; break; // "adNumeric 131 指示具有固定精度和范围的确切数字值 (DBTYPE_NUMERIC)。"; 
                case 138: type = "Object"; break; // "adPropVariant 138 指示一个 Automation PROPVARIANT (DBTYPE_PROP_VARIANT)。"; 
                case 4: type = "Single"; break; // "adSingle 4 指示一个单精度浮点值 (DBTYPE_R4)。"; 
                case 2: type = "Int16"; break; // "adSmallInt 2 指示一个双字节的有符号整数 (DBTYPE_I2)。"; 
                case 16: type = "SByte"; break; // "adTinyInt 16 指示一个单字节的有符号整数 (DBTYPE_I1)。"; 
                case 21: type = "UInt64"; break; // " adUnsignedBigInt 21 指示一个八字节的无符号整数 (DBTYPE_UI8)。"; 
                case 19: type = "UInt32"; break; // "adUnsignedInt 19 指示一个四字节的无符号整数 (DBTYPE_UI4)。"; 
                case 18: type = "Uint16"; break; // "adUnsignedSmallInt 18 指示一个双字节的无符号整数 (DBTYPE_UI2)。"; 
                case 17: type = "Byte"; break; // "adUnsignedTinyInt 17 指示一个单字节的无符号整数 (DBTYPE_UI1)。"; 
                //case 132: type= "adUserDefined 132 指示一个用户定义的变量 (DBTYPE_UDT)。"; 
                case 204: type = "byte[]"; break; //"adVarBinary 204 指示一个二进制值（仅限于 Parameter 对象）。"; 
                case 200: type = "String"; break; // "adVarChar 200 指示一个字符串值（仅限于 Parameter 对象）。"; 
                case 12: type = "Object"; break; // "adVariant 12 指示一个 Automation Variant (DBTYPE_VARIANT)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 139: type = "Decimal"; break; // "adVarNumeric 139 指示一个数字值（仅限于 Parameter 对象）。"; 
                case 202: type = "String"; break; // "adVarWChar 202 指示一个以 Null 终止的 Unicode 字符串（仅限于 Parameter 对象）。"; 
                case 130: type = "String"; break; // "adWChar 130 指示一个以 Null 终止的 Unicode 字符串 (DBTYPE_WSTR)。"; }
                default:
                    type = "";
                    break;
            }
            return type;
        }

        public static string GetInitByID(object o)
        {
            string init;
            switch (int.Parse(o.ToString()))
            {
                case 20: init = "0"; break;// "adBigInt 20 指示一个八字节的有符号整数 (DBinit_I8)。"; 
                //case 128: init = "byte[]"; break;//"adBinary 128 指示一个二进制值 (DBinit_BYTES)。"; 
                case 11: init = "false"; break;//"adBoolean 指示一个布尔值 (DBinit_BOOL)。";                     
                case 8: init = "\"\""; break;// "adBSTR 8 指示以 Null 终止的字符串 (Unicode) (DBinit_BSTR)。"; 
                //case 136: init="String";break; "adChapter 136 指示一个四字节的子集值，标识子行集合中的行 (DBinit_HCHAPTER)。"; 
                case 129: init = "\"\""; break;//"adChar129 指示一个字符串值 (DBinit_STR)。"; 
                case 6: init = "0"; break; //"adCurrency 6 指示一个货币值 (DBinit_CY)。货币是一个定点数字，小数点右侧有四位数字。该值存储为八字节、范围为 10,000 的有符号整数。"; 
                case 7: init = "DateTime.Now"; break; //"adDate 7 指示日期值 (DBinit_DATE)。日期保存为双精度数，数字的整数部分是从 1899 年 12 月 30 日算起的天数，小数部分是一天当中的片段时间。"; 
                case 133: init = "DateTime.Now"; break; // "adDBDate 133 指示日期值 (yyyymmdd) (DBinit_DBDATE)。"; 
                case 134: init = "TimeSpan.MinValue"; break; // "adDBTime 134 指示时间值 (hhmmss) (DBinit_DBTIME)。"; 
                case 135: init = "DateTime.Now"; break; //"adDBTimeStamp 135 指示日期/时间戳（yyyymmddhhmmss 加十亿分之一的小数）(DBinit_DBTIMESTAMP)。"; 
                case 14: init = "0"; break; // "adDecimal 14 指示具有固定精度和范围的确切数字值 (DBinit_DECIMAL)。";
                case 5: init = "0"; break; // "adDouble 5 指示一个双精度浮点值 (DBinit_R8)。"; 
                case 0: init = ""; break; // "adEmpty 0 指定没有值 (DBinit_EMPTY)。"; 
                //case 10: init = "Exception"; break; // "adError 10 指示一个 32 位的错误代码 (DBinit_ERROR)。"; 
                case 64: init = "DateTime.Now"; break; // "adFileTime 64 指示一个 64 位的值，表示从 1601 年 1 月 1 日开始的 100 个十亿分之一秒间隔的数量 (DBinit_FILETIME)。"; 
                case 72: init = "Guid.NewGuid()"; break; // "adGUID 72 指示全局唯一标识符 (GUID) (DBinit_GUID)。"; 
                //case 9: p = "adIDispatch 9 指示指向 COM 对象上 IDispatch 接口的指针 (DBinit_IDISPATCH)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 3: init = "0"; break; // " adInteger 3 指示一个四字节的有符号整数 (DBinit_I4)。"; 
                //case 13: p = "adIUnknown 13 指示指向 COM 对象上 IUnknown 接口的指针 (DBinit_IUNKNOWN)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                //case 205: init = "byte[]"; break; // "adLongVarBinary 205 指示一个长二进制值（仅限于 Parameter 对象）。"; 
                case 201: init = "\"\""; break; // " adLongVarChar 201 指示一个长字符串值（仅限于 Parameter 对象）。"; 
                case 203: init = "\"\""; break; // "adLongVarWChar 203 指示一个以 Null 终止的长 Unicode 字符串值（仅限于 Parameter 对象）。"; 
                case 131: init = "0"; break; // "adNumeric 131 指示具有固定精度和范围的确切数字值 (DBinit_NUMERIC)。"; 
                case 138: init = "null"; break; // "adPropVariant 138 指示一个 Automation PROPVARIANT (DBinit_PROP_VARIANT)。"; 
                case 4: init = "0"; break; // "adSingle 4 指示一个单精度浮点值 (DBinit_R4)。"; 
                case 2: init = "0"; break; // "adSmallInt 2 指示一个双字节的有符号整数 (DBinit_I2)。"; 
                case 16: init = "0"; break; // "adTinyInt 16 指示一个单字节的有符号整数 (DBinit_I1)。"; 
                case 21: init = "0"; break; // " adUnsignedBigInt 21 指示一个八字节的无符号整数 (DBinit_UI8)。"; 
                case 19: init = "0"; break; // "adUnsignedInt 19 指示一个四字节的无符号整数 (DBinit_UI4)。"; 
                case 18: init = "0"; break; // "adUnsignedSmallInt 18 指示一个双字节的无符号整数 (DBinit_UI2)。"; 
                case 17: init = "0"; break; // "adUnsignedTinyInt 17 指示一个单字节的无符号整数 (DBinit_UI1)。"; 
                //case 132: init= "adUserDefined 132 指示一个用户定义的变量 (DBinit_UDT)。"; 
                //case 204: init = "byte[]"; break; //"adVarBinary 204 指示一个二进制值（仅限于 Parameter 对象）。"; 
                case 200: init = "\"\""; break; // "adVarChar 200 指示一个字符串值（仅限于 Parameter 对象）。"; 
                case 12: init = "null"; break; // "adVariant 12 指示一个 Automation Variant (DBinit_VARIANT)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 139: init = "0"; break; // "adVarNumeric 139 指示一个数字值（仅限于 Parameter 对象）。"; 
                case 202: init = "\"\""; break; // "adVarWChar 202 指示一个以 Null 终止的 Unicode 字符串（仅限于 Parameter 对象）。"; 
                case 130: init = "\"\""; break; // "adWChar 130 指示一个以 Null 终止的 Unicode 字符串 (DBinit_WSTR)。"; }
                default:
                    init = "";
                    break;
            }
            return init;
        }
        public static string GetDefaultByID(object o, string str)
        {
            string ret;
            switch (int.Parse(o.ToString()))
            {
                case 20: ret = str; break;// "adBigInt 20 指示一个八字节的有符号整数 (DBinit_I8)。"; 
                //case 128: init = "byte[]"; break;//"adBinary 128 指示一个二进制值 (DBinit_BYTES)。"; 
                case 11:
                    if (str == "1")
                        ret = "true";
                    else
                        ret = "false";
                    break;//"adBoolean 指示一个布尔值 (DBinit_BOOL)。";                     
                case 8: ret = "\"" + str + "\""; break;// "adBSTR 8 指示以 Null 终止的字符串 (Unicode) (DBinit_BSTR)。"; 
                //case 136: init="String";break; "adChapter 136 指示一个四字节的子集值，标识子行集合中的行 (DBinit_HCHAPTER)。"; 
                case 129: ret = "\"" + str + "\""; break;//"adChar129 指示一个字符串值 (DBinit_STR)。"; 
                case 6: ret = str; break; //"adCurrency 6 指示一个货币值 (DBinit_CY)。货币是一个定点数字，小数点右侧有四位数字。该值存储为八字节、范围为 10,000 的有符号整数。"; 
                case 7: ret = "DateTime.Now"; break; //"adDate 7 指示日期值 (DBinit_DATE)。日期保存为双精度数，数字的整数部分是从 1899 年 12 月 30 日算起的天数，小数部分是一天当中的片段时间。"; 
                case 133: ret = "DateTime.Now"; break; // "adDBDate 133 指示日期值 (yyyymmdd) (DBinit_DBDATE)。"; 
                case 134: ret = "TimeSpan.MinValue"; break; // "adDBTime 134 指示时间值 (hhmmss) (DBinit_DBTIME)。"; 
                case 135: ret = "DateTime.Now"; break; //"adDBTimeStamp 135 指示日期/时间戳（yyyymmddhhmmss 加十亿分之一的小数）(DBinit_DBTIMESTAMP)。"; 
                case 14: ret = str; break; // "adDecimal 14 指示具有固定精度和范围的确切数字值 (DBinit_DECIMAL)。";
                case 5: ret = str; break; // "adDouble 5 指示一个双精度浮点值 (DBinit_R8)。"; 
                case 0: ret = ""; break; // "adEmpty 0 指定没有值 (DBinit_EMPTY)。"; 
                //case 10: init = "Exception"; break; // "adError 10 指示一个 32 位的错误代码 (DBinit_ERROR)。"; 
                case 64: ret = "DateTime.Now"; break; // "adFileTime 64 指示一个 64 位的值，表示从 1601 年 1 月 1 日开始的 100 个十亿分之一秒间隔的数量 (DBinit_FILETIME)。"; 
                case 72: ret = "Guid.NewGuid()"; break; // "adGUID 72 指示全局唯一标识符 (GUID) (DBinit_GUID)。"; 
                //case 9: p = "adIDispatch 9 指示指向 COM 对象上 IDispatch 接口的指针 (DBinit_IDISPATCH)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 3: ret = str; break; // " adInteger 3 指示一个四字节的有符号整数 (DBinit_I4)。"; 
                //case 13: p = "adIUnknown 13 指示指向 COM 对象上 IUnknown 接口的指针 (DBinit_IUNKNOWN)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                //case 205: init = "byte[]"; break; // "adLongVarBinary 205 指示一个长二进制值（仅限于 Parameter 对象）。"; 
                case 201: ret = "\"" + str + "\""; break; // " adLongVarChar 201 指示一个长字符串值（仅限于 Parameter 对象）。"; 
                case 203: ret = "\"" + str + "\""; break; // "adLongVarWChar 203 指示一个以 Null 终止的长 Unicode 字符串值（仅限于 Parameter 对象）。"; 
                case 131: ret = str; break; // "adNumeric 131 指示具有固定精度和范围的确切数字值 (DBinit_NUMERIC)。"; 
                case 138: ret = "null"; break; // "adPropVariant 138 指示一个 Automation PROPVARIANT (DBinit_PROP_VARIANT)。"; 
                case 4: ret = str; break; // "adSingle 4 指示一个单精度浮点值 (DBinit_R4)。"; 
                case 2: ret = str; break; // "adSmallInt 2 指示一个双字节的有符号整数 (DBinit_I2)。"; 
                case 16: ret = str; break; // "adTinyInt 16 指示一个单字节的有符号整数 (DBinit_I1)。"; 
                case 21: ret = str; break; // " adUnsignedBigInt 21 指示一个八字节的无符号整数 (DBinit_UI8)。"; 
                case 19: ret = str; break; // "adUnsignedInt 19 指示一个四字节的无符号整数 (DBinit_UI4)。"; 
                case 18: ret = str; break; // "adUnsignedSmallInt 18 指示一个双字节的无符号整数 (DBinit_UI2)。"; 
                case 17: ret = str; break; // "adUnsignedTinyInt 17 指示一个单字节的无符号整数 (DBinit_UI1)。"; 
                //case 132: init= "adUserDefined 132 指示一个用户定义的变量 (DBinit_UDT)。"; 
                //case 204: init = "byte[]"; break; //"adVarBinary 204 指示一个二进制值（仅限于 Parameter 对象）。"; 
                case 200: ret = "\"" + str + "\""; break; // "adVarChar 200 指示一个字符串值（仅限于 Parameter 对象）。"; 
                case 12: ret = "null"; break; // "adVariant 12 指示一个 Automation Variant (DBinit_VARIANT)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 139: ret = str; break; // "adVarNumeric 139 指示一个数字值（仅限于 Parameter 对象）。"; 
                case 202: ret = "\"" + str + "\""; break; // "adVarWChar 202 指示一个以 Null 终止的 Unicode 字符串（仅限于 Parameter 对象）。"; 
                case 130: ret = "\"" + str + "\""; break; // "adWChar 130 指示一个以 Null 终止的 Unicode 字符串 (DBinit_WSTR)。"; }
                default:
                    ret = "";
                    break;
            }
            return ret;
        }
        public static string GetHeaderOPByID(object o)
        {
            string init;
            switch (int.Parse(o.ToString()))
            {
                case 20: init = ""; break;// "adBigInt 20 指示一个八字节的有符号整数 (DBinit_I8)。"; 
                //case 128: init = "byte[]"; break;//"adBinary 128 指示一个二进制值 (DBinit_BYTES)。"; 
                case 11: init = ""; break;//"adBoolean 指示一个布尔值 (DBinit_BOOL)。";                     
                case 8: init = "'"; break;// "adBSTR 8 指示以 Null 终止的字符串 (Unicode) (DBinit_BSTR)。"; 
                //case 136: init="String";break; "adChapter 136 指示一个四字节的子集值，标识子行集合中的行 (DBinit_HCHAPTER)。"; 
                case 129: init = "'"; break;//"adChar129 指示一个字符串值 (DBinit_STR)。"; 
                case 6: init = ""; break; //"adCurrency 6 指示一个货币值 (DBinit_CY)。货币是一个定点数字，小数点右侧有四位数字。该值存储为八字节、范围为 10,000 的有符号整数。"; 
                case 7: init = "#"; break; //"adDate 7 指示日期值 (DBinit_DATE)。日期保存为双精度数，数字的整数部分是从 1899 年 12 月 30 日算起的天数，小数部分是一天当中的片段时间。"; 
                case 133: init = "#"; break; // "adDBDate 133 指示日期值 (yyyymmdd) (DBinit_DBDATE)。"; 
                case 134: init = "#"; break; // "adDBTime 134 指示时间值 (hhmmss) (DBinit_DBTIME)。"; 
                case 135: init = "#"; break; //"adDBTimeStamp 135 指示日期/时间戳（yyyymmddhhmmss 加十亿分之一的小数）(DBinit_DBTIMESTAMP)。"; 
                case 14: init = ""; break; // "adDecimal 14 指示具有固定精度和范围的确切数字值 (DBinit_DECIMAL)。";
                case 5: init = ""; break; // "adDouble 5 指示一个双精度浮点值 (DBinit_R8)。"; 
                case 0: init = ""; break; // "adEmpty 0 指定没有值 (DBinit_EMPTY)。"; 
                //case 10: init = "Exception"; break; // "adError 10 指示一个 32 位的错误代码 (DBinit_ERROR)。"; 
                case 64: init = "#"; break; // "adFileTime 64 指示一个 64 位的值，表示从 1601 年 1 月 1 日开始的 100 个十亿分之一秒间隔的数量 (DBinit_FILETIME)。"; 
                case 72: init = "'"; break; // "adGUID 72 指示全局唯一标识符 (GUID) (DBinit_GUID)。"; 
                //case 9: p = "adIDispatch 9 指示指向 COM 对象上 IDispatch 接口的指针 (DBinit_IDISPATCH)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 3: init = ""; break; // " adInteger 3 指示一个四字节的有符号整数 (DBinit_I4)。"; 
                //case 13: p = "adIUnknown 13 指示指向 COM 对象上 IUnknown 接口的指针 (DBinit_IUNKNOWN)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                //case 205: init = "byte[]"; break; // "adLongVarBinary 205 指示一个长二进制值（仅限于 Parameter 对象）。"; 
                case 201: init = "'"; break; // " adLongVarChar 201 指示一个长字符串值（仅限于 Parameter 对象）。"; 
                case 203: init = "'"; break; // "adLongVarWChar 203 指示一个以 Null 终止的长 Unicode 字符串值（仅限于 Parameter 对象）。"; 
                case 131: init = ""; break; // "adNumeric 131 指示具有固定精度和范围的确切数字值 (DBinit_NUMERIC)。"; 
                case 138: init = ""; break; // "adPropVariant 138 指示一个 Automation PROPVARIANT (DBinit_PROP_VARIANT)。"; 
                case 4: init = ""; break; // "adSingle 4 指示一个单精度浮点值 (DBinit_R4)。"; 
                case 2: init = ""; break; // "adSmallInt 2 指示一个双字节的有符号整数 (DBinit_I2)。"; 
                case 16: init = ""; break; // "adTinyInt 16 指示一个单字节的有符号整数 (DBinit_I1)。"; 
                case 21: init = ""; break; // " adUnsignedBigInt 21 指示一个八字节的无符号整数 (DBinit_UI8)。"; 
                case 19: init = ""; break; // "adUnsignedInt 19 指示一个四字节的无符号整数 (DBinit_UI4)。"; 
                case 18: init = ""; break; // "adUnsignedSmallInt 18 指示一个双字节的无符号整数 (DBinit_UI2)。"; 
                case 17: init = ""; break; // "adUnsignedTinyInt 17 指示一个单字节的无符号整数 (DBinit_UI1)。"; 
                //case 132: init= "adUserDefined 132 指示一个用户定义的变量 (DBinit_UDT)。"; 
                //case 204: init = "byte[]"; break; //"adVarBinary 204 指示一个二进制值（仅限于 Parameter 对象）。"; 
                case 200: init = "'"; break; // "adVarChar 200 指示一个字符串值（仅限于 Parameter 对象）。"; 
                case 12: init = ""; break; // "adVariant 12 指示一个 Automation Variant (DBinit_VARIANT)。"; //注意 ADO 目前不支持这种数据类型。使用它可能导致不可预料的结果。 
                case 139: init = ""; break; // "adVarNumeric 139 指示一个数字值（仅限于 Parameter 对象）。"; 
                case 202: init = "'"; break; // "adVarWChar 202 指示一个以 Null 终止的 Unicode 字符串（仅限于 Parameter 对象）。"; 
                case 130: init = "'"; break; // "adWChar 130 指示一个以 Null 终止的 Unicode 字符串 (DBinit_WSTR)。"; }
                default:
                    init = "";
                    break;
            }
            return init;
        }
    }
}
