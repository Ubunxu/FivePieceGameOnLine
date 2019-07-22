using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class debug
{
    public static void log(params object[] arg)
    {
        
        for (int i = 0; i < arg.Length; i++)
        {
            Console.Write(arg[i].ToString() + ",");
        }
        Console.WriteLine();
    }
    public static void logln(params object[] arg)
    {
        for (int i = 0; i < arg.Length; i++)
        {
            Console.WriteLine(arg[i].ToString());
        }
        //AppDomain.CurrentDomain.ActivationContext.
    }
}