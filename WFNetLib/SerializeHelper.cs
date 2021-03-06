﻿using System;
using System.Collections.Generic;

using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;
//using System.Runtime.Serialization.Json;

namespace WFNetLib
{
    /// <summary>
    /// Xml序列化
    /// </summary>
    public class XmlSerializeHelper
    {

        public static void Serialize(System.IO.Stream stream, object item,Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            serializer.Serialize(stream, item);
        }

        public static object DeSerialize(System.IO.Stream stream, Type type)
        {
            XmlSerializer serializer = new XmlSerializer(type);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return serializer.Deserialize(stream);
        }
    }

    /// <summary>
    /// 二进制序列化
    /// </summary>
    public class BinarySerializeHelper
    {

        public static void Serialize(System.IO.Stream stream, object item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            formatter.Serialize(stream, item);
        }

        public static object DeSerialize(System.IO.Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            return formatter.Deserialize(stream);
        }
    }

    /// <summary>
    /// Soap序列化
    /// </summary>
    public class SoapSerializeHelper
    {

        public static void Serialize(System.IO.Stream stream, object item)
        {
            SoapFormatter formatter = new SoapFormatter();
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            formatter.Serialize(stream, item);
        }

        public static object DeSerialize(System.IO.Stream stream)
        {
            SoapFormatter formatter = new SoapFormatter();
            return formatter.Deserialize(stream);
        }
    }
}
