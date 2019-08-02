using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketServer
{
    /// <summary>
    /// 服务器类
    /// </summary>
    public class Server
    {
        private Socket socket;
        static object lockobj = new object();
        private Dictionary<string, ClientNode> clients = new Dictionary<string, ClientNode>();
        public static Server shareServer = null;
        private static MessageQueue ReciveMessageQueue = MessageQueue.CreateMessageQueue();
        private static SendMessageQueue SendMessageQueue = SendMessageQueue.CreateMessageQueue();
        public Server(string _ip,int port)
        {
            //1:创建一个客户端连接对象
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress ipAddr = Share.IPAddress;

            
            //2:设置客户端的连接IP和端口
            //IPAddress ip = IPAddress.Parse(_ip);
            IPEndPoint iep = new IPEndPoint(ipAddr, port);
            socket.Bind(iep);
            //设置监听最大数
            socket.Listen(100);
            //Console.WriteLine("服务器启动成功 ip:" + ipAddr.ToString() + " port:" + port);
            Console.WriteLine("服务器启动成功 ip:" + ipAddr.ToString() + " port:" + port);
            util.core.EventDispatch.addEventListener("SocketServer.LogicBusiness", "do");
            util.core.EventDispatch.addEventListener("SocketServer.GameHallLogicBusiness", "do");
            util.core.EventDispatch.addEventListener("SocketServer.GameRoomLogicBusiness", "do");
            shareServer = this;
        }
        public void Start()
        {
            socket.BeginAccept(reBackAccept, socket);
        }

        private void reBackAccept(IAsyncResult ar)
        {
            Socket server1 = (Socket)ar.AsyncState;
            Socket client = server1.EndAccept(ar);
            socket.BeginAccept(reBackAccept, socket);
            ClientNode node = new ClientNode(client);
        }
        public static void addReciveTask(MessageNode node)
        {
            ReciveMessageQueue.addTask(node);
        }
        public static void addSendTask(MessageNode node)
        {
            SendMessageQueue.addTask(node);
        }


    }
}
