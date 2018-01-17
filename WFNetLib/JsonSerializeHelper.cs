using System;
using System.Collections.Generic;

using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.IO;
//using System.Runtime.Serialization.Json;

namespace WFNetLib
{
    /// <summary>
    /// Json序列化
    /// </summary>
    public class JsonSerializeHelper
    {

        public static void Serialize(System.IO.Stream stream, object item, Type type)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(type);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            formatter.WriteObject(stream, item);
        }

        public static object DeSerialize(System.IO.Stream stream, Type type)
        {
            DataContractJsonSerializer formatter = new DataContractJsonSerializer(type);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return formatter.ReadObject(stream);
        }
        public static T DeSerialize<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string Serialize(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }
    }
}
