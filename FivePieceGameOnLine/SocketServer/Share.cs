using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Share
    {
        public static String USER_SAVE_PATH = "C:/Users/ASUS/Desktop/我的文件夹/实训老师文件/SocketServer/Users/";
        //public static String USER_SAVE_IMAGE_PATH = "C:/Users/ASUS/Desktop/我的文件夹/实训老师文件/Users/";
        public static String USER_SAVE_IMAGE_PATH = "F:/Tomcat8/apache-tomcat-8.5.39-windows-x64/apache-tomcat-8.5.39/webapps/ROOT/userImages/";

        public static IPAddress IPAddress
        {
            get
            {
                string myIpAdddr = "192.168.1.110";

                //IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
                //IPAddress ipAddr = ipHost.AddressList[0];
                IPAddress ipAddr = IPAddress.Parse(myIpAdddr);
                
                return ipAddr;
            }
        }
    }
}
