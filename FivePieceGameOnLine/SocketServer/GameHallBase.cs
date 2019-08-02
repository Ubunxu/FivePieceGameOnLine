using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{

    public  abstract class GameHallBase : Group<string, User>
    {
        //房间管理 
        RoomManager rm = RoomManager.shareRoomManager();

        /// <summary>
        /// 玩家进入大厅
        /// 1：要发送大厅所有信息到玩家,还要将在大厅房间的所有信息发给进来的人,
        /// 2：同时需要把该玩家发给当前大厅的其它玩家,包括房间玩家,玩家进入房间后，依然在大厅保留
        /// 3：线程队列操作，不需要加锁
        /// </summary>
        /// <param name="user"></param>
        public void addGameHall(User user)
        {            
            this.SendToHall(user);//通知其它人，有人进入到大厅
            this.Add(user.UserName, user);
            this.HallToSend(user);            
        }

        public void removeGameHall(User user)
        {
            if (this.ContainsKey(user.UserName))
            {
                this.Remove(user.UserName);                
                this.UserExitHall(user);
            }
        }
        private void UserExitHall(User user)
        {
            ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.玩家离开大厅);//6555
            buffer.writeString(user.UserName);
            sendAllMessage(buffer);
        }
        //把某个玩家的消息发给大厅所有人
        private void SendToHall(User user)
        {
            ByteBuffer bf = ByteBuffer.CreateByteBufferType(Protcol.某个玩家的所有信息给大厅所有玩家);//6551
            bf.writeString(user.UserName);
            bf.writeString(user.ChinaName);
            bf.writeString(user.Image);
            sendAllMessage(bf);

        }
        //把大厅所有信息发给某个玩家
        private void HallToSend(User user)
        {
            ByteBuffer bf = ByteBuffer.CreateByteBufferType(Protcol.大厅所有信息给某个玩家,2000);//6550
            //1: 先发大厅所有人信息
            User[] users = this.Values();
            foreach (User u in users)
            {
                //要判断玩家是否中途退出
                if (u != null)
                {
                    bf.writeString(u.UserName);
                    bf.writeString(u.ChinaName);
                    //bf.writeString(u.Image);
                }
            }
            user.Send(bf);

            //2: 再发房间信息 : 6210
            ByteBuffer roomBuffer = rm.RoomInfos();
            user.Send(roomBuffer);
        }
        public void sendAllMessage(ByteBuffer buf)
        {
            User[] user = this.Values();
            foreach (User u in user)
            {
                //要判断玩家是否中途退出
                if (u != null)
                {
                    u.Send(buf);
                }
            }
        }
        
    }
}
