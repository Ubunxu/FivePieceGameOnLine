using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    [Serializable]
    public class Room : RoomBase
    {
        bool left = false;
        bool right = false;
        bool isWoner = true;
        int readyCount = 0;
        RoomGame game = null;

        public Room(int id,string name):base(id, name)
        {
            game = new RoomGame(this);      
        }
        public bool AddRoom(User user)
        {
            if(base.Add(user))
            {
                user.room = this;
                this.setDesk(user,true);
                debug.log(user.ChinaName + " 加入房间成功");
                this.RoomTosSend(user);
                this.SendToRoomer(user);
                this.SendRoomState();
                user.roomOwner = isWoner;
                if(isWoner)
                {
                    isWoner = false;
                }          
                return true;
            }
            return false;
        }
        public void updateGameReadyOnCancel(User user,int flg)
        {
            this.readyCount += flg;
            user.Ready = flg;
            sendGameReadyOrCancel(user, flg);
            if (readyCount == MAX_COUNT)
            {
                //游戏开始中
                this.State = RoomState.Ring;
                //发送游戏开始准备
                this.sendGameStart();
                this.SendRoomState();

            }
        }
        public void UserTurnGame(User user,int row,int col)
        {
            if (this.GameCheck())
            {
                this.game.GameTurn(user, row, col);
            }
            else
            {
                //debug.logln("this.readyCount: " + this.readyCount + "  ,userLength: " + this.getUsers().Length);
                sendAllMessage(ConstomMessage.getError("房间数据异常，请退出房间，重新进入"));
                this.GameOver();
            }
        }
        public bool GameCheck()
        {
            if (this.readyCount < MAX_COUNT) return false;
            if (this.getUsers().Length < MAX_COUNT) return false;
            return true;
        }

        public void ExitRoom(User user)
        {
            if(user.Ready>0)this.readyCount--;
            user.Ready = -1;
            setDesk(user, false);
            this.State = RoomState.Adding;
            base.Remove(user);
            updateRoomOwner(user);

            //如果正在游戏中,提前gameOver
            if (this.State == RoomState.Ring)
            {                
                this.GameOver();
            }
            this.ExitUserByteBuffer(user);//7999
            this.game.Clear();
            GameHall.shareGameHall().UpdateRoomInfomation(user, -1);            
            user.room = null;            
            this.SendRoomState();
        }

        public void GameOver(ByteBuffer buf=null)
        {
            if(buf!=null)
            {
                this.sendAllMessage(buf);
            }
            else
            {
                //非正常退出
                ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.游戏结束);
                buffer.writeInt(-1);//正常退出 1，非正常退出 -1;        
                sendAllMessage(buffer);
               // debug.logln("非正常 退出");
            }            
            this.CancelAllReady();
            this.State = RoomState.Idle;
        }
        public int getRoomState()
        {
            if (this.State == RoomState.Ring) return 1;
            if (this.State == RoomState.Adding) return 0;
            return -1;
        }
        public bool isFully()
        {
            return this.CurrentCount == MAX_COUNT;
        }
        private void updateRoomOwner(User user)
        {
            //如果房间没有人了
            if (this.CurrentCount<=0)
            {
                isWoner = true;
                this.Password = null;
                return;
            }
            //如果离开的这个人是房主，那么换个新人做房主
            if(user.roomOwner)
            {
                foreach(User u in this.getUsers())
                {
                    u.roomOwner = true;
                    break;
                }
            }
        }
        private void sendGameStart()
        {
            int index = new Random().Next(MAX_COUNT);
            User user = this.getUsers()[index];
            ByteBuffer buf = ByteBuffer.CreateByteBufferType(Protcol.游戏开始);
            buf.writeString(user.UserName);
            sendAllMessage(buf);
           // debug.logln("游戏开始");
            this.game.Start(user,index);
        }
        private void sendGameReadyOrCancel(User user,int flg)
        {
            ByteBuffer buf = ByteBuffer.CreateByteBufferType(flg > 0 ? Protcol.游戏准备 : Protcol.取消准备);
            buf.writeString(user.UserName);
            buf.writeInt(flg);
            sendAllMessage(buf);
        }

        //把某个玩家的消息发给大厅所有人
        private void SendToRoomer(User  user)
        {
            ByteBuffer bf = ByteBuffer.CreateByteBufferType(Protcol.玩家进入房间);//7001
            bf.writeString(user.UserName);
            bf.writeString(user.ChinaName);
            bf.writeInt(user.DeskPos);
            bf.writeString(user.Image);
            sendAllMessage(bf);

        }
        //把房间所有信息发给某个玩家
        private void RoomTosSend(User user)
        {
                ByteBuffer bf = ByteBuffer.CreateByteBufferType(Protcol.玩家第一次进入房间, 1000);//7000
                bf.writeInt(this.Roomid);
                bf.writeString(this.Roomname);
                bf.writeString(this.Password);
                
                User[] users = this.getUsers();
                bf.writeInt(users.Length);

                foreach (User u in users)
                {
                    //要判断玩家是否中途退出
                    if (u != null)
                    {
                        bf.writeString(u.UserName);
                        bf.writeString(u.ChinaName);
                        bf.writeInt(u.DeskPos);
                        bf.writeString(u.Image);
                        bf.writeInt(u.Ready);
                    }
                }
                user.Send(bf);
        }
        private void ExitUserByteBuffer(User user)
        {
            ByteBuffer bf = ByteBuffer.CreateByteBufferType(Protcol.玩家离开房间);//7999
            bf.writeString(user.UserName);
            sendAllMessage(bf);
        }



        public void CancelAllReady()
        {
            this.readyCount = 0;
            this.State = RoomState.Adding;

            User[] users = this.getUsers();
            foreach(User u in users)
            {
                u.Ready = -1;
                sendGameReadyOrCancel(u, -1);
            }            
        }

        public void SendRoomState()
        {
            int type = this.State == RoomState.Ring ? 1 : -1;
            if(this.State == RoomState.Adding)
            {
                type = 0;
            }
            ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.房间状态);//7005
            buffer.writeInt(this.Roomid);
            buffer.writeInt(type);
            GameHall.shareGameHall().sendAllMessage(buffer);
        }


        //设置玩家坐在左边还是右边
        private void setDesk(User user,bool isAdd = true)
        {
            if (isAdd)
            {
                if (!left)
                {
                    user.DeskPos = -1;
                    left = true;
                }
                else
                {
                    right = true;
                    user.DeskPos = 1;
                }
            }
            else
            {
                if(user.DeskPos<0)
                {
                    left = false;                   
                }
                else
                {
                    right = false;
                }
            }
       }

    }
}
