using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace util.file
{
    /// <summary>
    /// 文件操作工厂
    /// </summary>
    public class FFactory
    {


        /// <summary>
        /// 将对象保存到指定路径中,
        /// 注：因为要保的是对象，所以要在保存的类前面要加上序列化[Serializable]，
        /// 序列化:就是把一个整体打散成若干个颗粒
        /// 如:
        /// [Serializable]
        /// public class Goods
        /// {
        /// }
        /// </summary>
        /// <param name="path">要保存的路径，如：C:\a.obj,a.obj是自己的取的文件名和扩展名，可以随便取，没有的路径都会自动生成</param>
        /// 注：路径可以随便写，会自动生成:如:C:\A\B\C\test.obj 程序会自动生成A,B,C目录
        /// <param name="obj">要保存的对象,object:所有类的父基类</param>
        public static void SaveObject(string path,object obj)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@""+path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            //将对象写入到本地
            formatter.Serialize(stream, obj);
            stream.Close();
            Console.WriteLine("["+path+"]文件保存成功...");
        }
        public static void SaveObject(string path, byte[] bs)
        {
            File.WriteAllBytes(path, bs);
            Console.WriteLine("[" + path + "]文件保存成功...");
        }
        public static byte[] ReadFile(string path)
        {
            return File.ReadAllBytes(path);
        }
        /// <summary>
        /// 从指定的路径中取出文件，返回指定的类型
        /// T:是指泛型
        /// Read<Hero>那么所有的T都会被换成Hero类
        /// 如：Hero h = FFactory.Read<Hero>("C:\hero.obj");
        /// </summary>
        /// <typeparam name="T">返回指定的类型</typeparam>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public static T ReadObject<T>(string path)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(@"" + path, FileMode.Open, FileAccess.Read, FileShare.None);
                T myObj = (T)formatter.Deserialize(stream);
                stream.Close();
                return myObj;
            }
            catch (Exception){
                Console.WriteLine("文件读取出错，或没有找到");             
            }
            return default(T);
        }
        /// <summary>
        /// 将一个字符串内容写入到指定的目录文件中
        /// </summary>
        /// <param name="path">要写入的目录包括文件名</param>
        /// <param name="content">要写入的内容</param>
        /// <param name="append">是否要追加还是覆盖</param>
        public static void SaveText(string path,string content,bool append = false)
        {
            if (append)
            {
                //追加到原来内容的后面
                File.AppendAllText(path, content);
            }
            else
            {
                //覆盖原来的内容
                File.WriteAllText(path, content);
            }
        }
        /// <summary>
        /// 从指定的目录中读取文件内容
        /// </summary>
        /// <param name="path">要读取的目录</param>
        /// <returns></returns>
        public static string ReadText(string path)
        {
            string str =  File.ReadAllText(path);
            return str;
        }

        /// <summary>
        /// 判断指定的文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            return File.Exists(path);
        }
    }
}
