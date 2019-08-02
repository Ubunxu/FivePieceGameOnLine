using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SocketServer;

namespace util.core
{
    public class EventDispatch
    {
        static Dictionary<int, Node> dict = new Dictionary<int, Node>();

        public static void addEventListener(string className, string priex = "do")
        {
            //1：根据字符串类名 获得该类的类型
            Type type = Type.GetType(className);//线程.A, 线程.B
                                                //2：创建对象
            object parent = Activator.CreateInstance(type);//new A();
                                                    //获得所有方法,返回一个方法数组
                                                    //MethodInfo m = type.GetMethod("do1001");
            MethodInfo[] ms = type.GetMethods();
            //取出所有指定开头的方法
            for (int i = 0; i < ms.Length; i++)
            {
                MethodInfo mth = ms[i];
                if (mth.Name.StartsWith(priex))
                {
                    Node node = new Node(parent,mth);
                    int sName = int.Parse(mth.Name.Substring(priex.Length));
                    dict.Add(sName, node);
                    debug.logln(mth.Name);
                }
            }
        }
        public static void dispatchEvent(int methodName,params object[] objs)
        {
            if (dict.ContainsKey(methodName))
            {
                Node node = dict[methodName];
                node.Run(objs);
            }
            else
            {
                debug.logln("["+ ((MessageNode)objs[0]).Type +"]错误的协议类型： " + methodName);
            }
        }
    }
    class Node
    {
        public object parent;
        public MethodInfo method;

        public Node(object parent, MethodInfo method)
        {
            this.parent = parent;
            this.method = method;
        }
        public void Run(params object[] objs)
        {
            this.method.Invoke(this.parent, objs);
        }
    }
}