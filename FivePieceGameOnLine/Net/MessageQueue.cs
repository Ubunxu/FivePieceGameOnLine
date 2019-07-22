using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using util.core;
using util.net;

namespace util.net
{
    /// <summary>
    /// 消息队列，
    /// 一直接受来自服务器发送的消息，然后执行的时候一步一步执行，
    /// 如果要暂停，则暂停执行，如果要继续执行，那么就立即开始执行
    /// </summary>
    class MessageQueue
    {
        //消息队列
        ConcurrentQueue<ByteBuffer> queue = new ConcurrentQueue<ByteBuffer>();
        //信号
        EventWaitHandle handle = new AutoResetEvent(false);
        //是否等待
        private bool isWait = false;
        //是否继续读取执行消息
        private bool isGoRead = false;

        //是否在运行
        private bool isRunning = false;
        private static MessageQueue _mq = null;
        public static MessageQueue GetSingletonMessage()
        {
             if(_mq == null)
            {
                _mq = new MessageQueue();
            }
            return _mq;
        }
        
        public void AddMessageQue(ByteBuffer buffer)
        {
            Console.WriteLine(buffer.Type);

            this.queue.Enqueue(buffer);
            if (this.isWait)
            {
                handle.Set();//下令停止等待
            }

            if (isRunning)
            {
                isRunning = true;
                this.RunTaskAsyn();
            }

        }
        /// <summary>
        /// 下面的是异步编程
        /// </summary>
        public void RunTaskAsyn()
        {
            if (!isGoRead)
            {
                Action ac = new Action(RunTask);
                ac.BeginInvoke(RunTaskEnd, ac);
            }

        }

        public void RunTask()
        {
            if (this.queue.IsEmpty)
            {
                this.isWait = true;
                handle.WaitOne();
                this.isWait = false;
            }
            ByteBuffer buf;  //放在这里的作用？？这里是当做返回值来用
            this.queue.TryDequeue(out buf);
            EventDispatch.dispatchEvent(buf.Type, buf);
        }

        public void RunTaskEnd(IAsyncResult ar)
        {
            Action a = ar.AsyncState as Action;
            a.EndInvoke(ar);
            RunTaskAsyn();
        }
        //停止读取消息
        public void StopRead()
        {
            this.isGoRead = true;
        }
        //继续读取消息
        public void RecoverRead()
        {
            this.isGoRead = false;
            this.RunTaskAsyn();
        }


    }
}
