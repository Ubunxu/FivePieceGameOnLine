using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
    public class MessageQueue
    {
        //public delegate void RCallBack();

        private static  object objLock = new object();
        private bool wait = false;
        private bool runing = false;
        private bool run = true;

        private MessageNode cNode;

        private int msgCount = 0;
        private IAsyncResult arr = null;

        private Action runAction;
        //private static Queue<ClientNode> queue = new Queue<ClientNode>();
        private ConcurrentQueue<MessageNode> cqueue = new ConcurrentQueue<MessageNode>();
       
        //信号
        private EventWaitHandle Moinst = new AutoResetEvent(false);

        public static MessageQueue CreateMessageQueue()
        {
            return new MessageQueue();
        }

        /// <summary>
        /// 消息入队
        /// </summary>
        /// <param name="message"></param>
        public void addTask(MessageNode node)
        {
             cqueue.Enqueue(node);
  
            if(this.wait)
            {
                Moinst.Set();
            }
            if(!this.runing)
            {
                runing = true;
                runAction = new Action(doRun);
                this.RunTask();
            }
        }
        public void RunTask()
        {
            arr = runAction.BeginInvoke(RunEnd, runAction);
        }
        private void RunEnd(IAsyncResult ar)
        {
            runAction.EndInvoke(ar);
            if(this.cqueue.IsEmpty)
            {
                this.wait = true;
                Moinst.WaitOne();
                this.wait = false;
            }
            //Thread.Sleep(1);
            this.RunTask();
        }
        public void doRun()
        {
            
            MessageNode node;
            bool f = cqueue.TryDequeue(out node);

            cNode = node;

            if (node.Method != null)
            {
                node.Method();
            }
            else
            {
                debug.logln("准备执行: " + (node.socket.user != null ? node.socket.user.ChinaName : node.socket.name) + " 的协议,Type: "+node.Type);
                util.core.EventDispatch.dispatchEvent(node.Type, node);
            }
            //debug.logln(node.socket.name + " < " + (node.socket.user != null ? node.socket.user.ChinaName : "") + " : " + node.Type + " > - 执行第[ " + (++msgCount) + " ]条协议");
        }

        public void Run()
        {
            try {
                while (run)
                {
                    if (!cqueue.IsEmpty)
                    {
                       // lock(this)
                        //{
                            MessageNode node;
                            bool f = cqueue.TryDequeue(out node);
                            cNode = node;
                            util.core.EventDispatch.dispatchEvent(node.Type, node);
                            debug.logln("===============还有剩余消息:   " + cqueue.Count);
                            //debug.logln(node.socket.name + " < " + (node.socket.user != null ? node.socket.user.ChinaName : "") + " : " + node.Type + " > - 执行第[ " + (++msgCount) + " ]条协议");
                       // }
                    }
                    //
                    Thread.Sleep(1);
                }
            }catch(Exception e)
            {
                debug.log(e.Message);
                debug.logln("==========================================================");
                debug.logln("警告: 发生了严重错误!!! <"+cNode.socket.user.ChinaName+" : "+ cNode.Type+">");
                debug.logln("==========================================================");
                cNode.socket.Close();
                ConstomMessage.SendAllMessage("原因:[" + cNode.socket.user.ChinaName + "]执行了 > " + cNode.Type + " 协议");
                run = false;
            }
        }
        
    }
}
