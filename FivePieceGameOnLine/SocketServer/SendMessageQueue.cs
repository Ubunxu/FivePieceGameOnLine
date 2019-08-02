using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
    public class SendMessageQueue
    {
        public delegate void RCallBack();

        private static  object objLock = new object();
        private bool wait = false;
        private bool runing = false;
        private bool run = true;

        private MessageNode cNode;

        private int msgCount = 0;
        private IAsyncResult arr = null;        

        //private static Queue<ClientNode> queue = new Queue<ClientNode>();
        private ConcurrentQueue<MessageNode> cqueue = new ConcurrentQueue<MessageNode>();


        public static SendMessageQueue CreateMessageQueue()
        {
            return new SendMessageQueue();
        }

        /// <summary>
        /// 消息入队
        /// </summary>
        /// <param name="message"></param>
        public void addTask(MessageNode node)
        {
            lock(this)
            {
                cqueue.Enqueue(node);
            }        
            RunMessage();
        }

        public void RunMessage()
        {
            if(!runing)
            {
                runing = true;
                RCallBack rcb = new RCallBack(Run);
                arr = rcb.BeginInvoke(RunEnd, rcb);
            }            
        }
        public void Run()
        {
            try {
                while (run)
                {
                    if (cqueue.Count > 0)
                    {
                        lock(this)
                        {
                            MessageNode node;
                            bool f = cqueue.TryDequeue(out node);
                            cNode = node;
                            //if (node.socket.user != null)
                            {
                                node.buffer.Send(node.socket.getSocket());
                            }
                        }
                    }
                    //
                    Thread.Sleep(10);
                }
            }catch(Exception e)
            {
                debug.logln("==========================发送消息时出错:==================================");
                if(cNode.socket.user!=null)
                {
                    debug.logln("错误：" + cNode.Type + "   :   " + cNode.socket.user.ChinaName);
                }    
                else
                {
                    debug.logln("错误：" + cNode.Type + "   :   " + cNode.socket.name);
                }       
                debug.log(e.Message);
                debug.logln("===========================================================================");
                run = false;
            }
        }
        private void RunEnd(IAsyncResult ar)
        {
            RCallBack c = (ar.AsyncState as RCallBack);
            c.EndInvoke(ar);            
        }
    }
}
