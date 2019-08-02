using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace SocketServer
{
    /// <summary>
    /// 客户端接点，用来对每一个连接上来的客户端进行操作
    /// </summary>
    [Serializable]
    public class ClientNode
    {
        private Socket socket = null;
        public string name = null;
        public int count = 0;
        public ByteBuffer message = null;
        public User user = null;
        private int header = 0;
        private int reciveCount = 0;
        private MessageQueue mQueue = null;
        byte[] headerByte = new byte[4];
        private bool isSend = false;
        private ConcurrentQueue<ByteBuffer> sendCache = new ConcurrentQueue<ByteBuffer>();

        ByteBuffer buffer = new ByteBuffer(-1, 1024 * 2000);
        public ClientNode(Socket _client)
        {
            mQueue = MessageQueue.CreateMessageQueue();
            this.socket = _client;
            this.name = this.socket.RemoteEndPoint.ToString().Substring(0, this.socket.RemoteEndPoint.ToString().IndexOf(':'));
            this.BeginReciveMessage();
        }
        private void BeginReciveMessage()
        {
            this.socket.BeginReceive(headerByte, this.reciveCount, 1, SocketFlags.None, CallAsyncCallback, this.socket);
        }
        private void CallAsyncCallback(IAsyncResult ar)
        {
            try
            {
                this.socket = (Socket)ar.AsyncState;
                int c = this.socket.EndReceive(ar);
                if (c == 0)
                {
                    //this.socket.Shutdown(SocketShutdown.Both);
                    this.Close();
                    return;
                }
                this.reciveCount += c;
                if (this.header == 0)
                {
                    if (this.reciveCount < 4)
                    {
                        this.BeginReciveMessage();
                    }
                    else
                    {
                        buffer.writeBytes(headerByte);
                        header = buffer.readInt();
                        // debug.log("header:      " + header);
                        buffer.Clear();
                        this.reciveCount = 0;
                        this.socket.BeginReceive(this.buffer.getBuffer(), this.reciveCount, this.header - this.reciveCount, SocketFlags.None, CallAsyncCallback, this.socket);
                    }
                }
                else
                {
                    if (this.reciveCount >= this.header)
                    {

                        //debug.log("ok:   :      " + this.reciveCount,this.header);
                        this.CreateMessage(buffer);
                        buffer.Clear();
                        this.header = 0;
                        this.reciveCount = 0;
                        this.BeginReciveMessage();
                    }
                    else
                    {
                        this.socket.BeginReceive(this.buffer.getBuffer(), this.reciveCount, this.header - this.reciveCount, SocketFlags.None, CallAsyncCallback, this.socket);
                    }
                }
            }
            catch (Exception e) {
                debug.log(this.name + ": "+e.Message);
                this.Close();
            }
        }
        private void CreateMessage(ByteBuffer buffer)
        {
            int type = buffer.readInt();
            ByteBuffer buf = new ByteBuffer(type, header - 4);
            buf.Type = type;
            buf.writeBytes(buffer.getBuffer(), 4, this.header - 4);

            if (type != 1001 && type != 1002)
            {
                if (this.user == null)
                {
                    //用户没登陆
                    this.send(ConstomMessage.getError("用户没登陆"));
                    return;
                }
            }
            MessageNode node;
            node.Type = type;
            node.buffer = buf;
            node.socket = this;
            node.Method = null;
            Server.addReciveTask(node);
        }        
        public void send(ByteBuffer buffer)
        {
            //buffer.Send(this.socket);
            try {
                //正在发送的时候，该客户端断了, 可能会出现对象被释放// && socket.Connected)
                if (socket != null)
                {
                    byte[] lengbs = BitConverter.GetBytes(buffer.Length);
                    socket.Send(lengbs, 0, lengbs.Length, 0);//SocketFlags.None
                    if (this.socket.Poll(10, SelectMode.SelectRead))
                    {
                        this.Close();
                    }
                    else socket.Send(buffer.getBuffer(), 0, buffer.Length, SocketFlags.None);
                }
            }catch(Exception e)
            {
                this.Close();
                debug.logln(this.name + " > 发送消息出错: " + e.Message);
            }
            //buffer.Send(this.socket);
            //sendCache.Enqueue(buffer);  
            //if(!this.isSend)
            //{
            //    this.isSend = true;
            //    this.NextSend();
            //}
        }
        private void NextSend()
        {
            if (this.socket != null)
            {
                if (!sendCache.IsEmpty)
                {
                    ByteBuffer buf;
                    sendCache.TryDequeue(out buf);
                    if (this.socket != null && this.socket.Connected)
                    {
                        try
                        {
                            //发送长度，不需要异步发送
                            //byte[] lengbs = BitConverter.GetBytes(buf.Length);
                            //this.socket.Send(lengbs, 0, lengbs.Length, SocketFlags.None);
                            //this.socket.Send(buf.getBuffer(), 0, buf.Length, SocketFlags.None);
                            //if (!sendCache.IsEmpty)
                            //{
                            //    this.NextSend();
                            //}
                            //else
                            //{
                            //    debug.logln("over...");
                            //    this.isSend = false;
                            //}
                            byte[] lengbs = BitConverter.GetBytes(buf.Length);
                            this.socket.Send(lengbs, 0, lengbs.Length, SocketFlags.None);
                            //异步发送
                            this.socket.BeginSend(buf.getBuffer(), 0, buf.Length, SocketFlags.None, asyncResult =>
                            {
                                int n = this.socket.EndSend(asyncResult);
                                debug.logln("发送到客户端协议: " + buf.Type+"  长度:"+buf.Length+" 状态:  "+ n);
                                if (!sendCache.IsEmpty)
                                {
                                    this.NextSend();
                                }
                                else
                                {
                                    this.isSend = false;
                                }

                            }, this.socket);
                        }
                        catch (Exception e)
                        {
                            debug.logln("发送出错: " + e.Message);
                            this.Close();
                        }
                    }
                }
            }
        }
        
        public Socket getSocket()
        {
            return this.socket;
        }

        public void ClentExit()
        {
            if (this.user != null)
            {
                user.connected = 0;
                UserGroup.ShareUserGroup().RemoveUser(this.user.UserName);
                GameHall.shareGameHall().UserExitGameHall(user);
                debug.logln(this.user.ChinaName + " > 离开了服务器");
                this.user = null;
            }
            if (this.buffer != null)
            {
                this.buffer.Clear();
                this.buffer = null;
            }
        }

        public int one = 0;
        public void Close()
        {
            if (this.socket != null)
            {
                this.socket.Close();
                this.socket = null;
            }
            if(one<1)
            {
                one = 1;
                MessageNode node;
                node.Type = 0;
                node.socket = null;
                node.buffer = null;
                node.Method = ClentExit;
                Server.addReciveTask(node);
            }

        }
    }
}
