using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yoga.Common.FileAct
{
    public class SerializationFile
    {
        public static bool TrySerialize(object obj)
        {
            try
            {
                using (Stream objectStream = new MemoryStream())
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(objectStream, obj);
                }
                return true;
            }
            catch(OutOfMemoryException ex)
            {
                Util.WriteLog(typeof(SerializationFile), ex);
                throw new Exception("内存不足,请检查设置");
            }
            catch (Exception ex)
            {
                
                Util.WriteLog(typeof(SerializationFile), ex);
               // throw new Exception("对象未完全初始化,请检查设置");
            }
            return false;
        }
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="filePath">序列化保存路径</param>
        /// <param name="obj">序列化对象</param>
        public static void SerializeObject(string filePath, object obj)//序列化
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(fileStream, obj);
            }

        }

        /// <summary>
        /// 反序列化文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>反序列化后的对象</returns>
        public static object DeserializeObject(string filePath)
        {
            object obj = new object();
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    obj = b.Deserialize(fileStream);
                }
            }
            catch (Exception ex)
            {
                obj = null;
                Util.WriteLog(typeof(SerializationFile), ex);
                Debug.WriteLine("数据读取失败" + ex.Message);
            }
            return obj;
        }
        /// <summary>
        /// 深度拷贝对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }
        /// <summary>
        /// 深度拷贝对象
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Clone(object obj)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, obj);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream);
            }
        }
    }
}
