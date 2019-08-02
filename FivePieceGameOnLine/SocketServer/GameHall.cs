using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class GameHall : GameHallBase
    {
        private static GameHall hall = null;
        public  static GameHall shareGameHall()
        {
            if (hall == null) hall = new GameHall();
            return hall;
        }
        public void addGameHall(User user)
        {
            base.addGameHall(user);
        }
        public void UserExitGameHall(User user)
        {
            base.removeGameHall(user);
            if (user.room != null)
            {
                user.room.ExitRoom(user);
            }
        }
        public void addRoom(int roomId,User user,string pwd=null)
        {
            if(user.room!= null)
            {
                user.Send(ConstomMessage.getError("你已经在别的房间，请先退出，再加入"));
                return;
            }
            bool f = RoomManager.shareRoomManager().addRoom(roomId, user,pwd);
            if(f)
            {
                UpdateRoomInfomation(user,1);
            }
        }
        public void setRoomPassword(int roomId, User user, string pwd)
        {
            RoomManager.shareRoomManager().setRoomPassword(roomId, user, pwd);
        }

        public void SendSingleUser(User user,ByteBuffer buf)
        {
            user.Send(buf);
        }

        public void UpdateRoomInfomation(User user,int flg)
        {
            if(flg>0)
            {
                //加入成功,更新大厅信息
                ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.更新大厅某个房间信息);//6211
                buffer.writeInt(flg);
                buffer.writeInt(user.room.Roomid);
                buffer.writeString(user.ChinaName);
                buffer.writeInt(user.DeskPos);
                buffer.writeString(user.Image);
                sendAllMessage(buffer);
            }
            else
            {
                //加入成功,更新大厅信息
                ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.更新大厅某个房间信息);//6211
                buffer.writeInt(flg);
                buffer.writeInt(user.room.Roomid);
                buffer.writeInt(user.DeskPos);
                sendAllMessage(buffer);
            }
        }

        public void QunLiaoMessage(User user,ByteBuffer buf)
        {
            ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.用户群聊,500);
            string msg = buf.readString();
            buffer.writeString(user.UserName);
            buffer.writeString(user.ChinaName);
            buffer.writeString(msg);
            sendAllMessage(buffer);
        }

        public void SiLiaoMessage(User user,ByteBuffer buf)
        {
            ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.用户私聊,1024);
            string userName = buf.readString();//取出用户,s要发给谁的
            string msg = buf.readString();//要说什么 

            buffer.writeString(user.UserName);
            buffer.writeString(user.ChinaName);
            buffer.writeString(msg);

            User user1 = this[userName];
            if(user1!=null)
            {
                user1.Send(buffer);
            }
        }

    }
}
