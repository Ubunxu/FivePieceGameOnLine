using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class GameHallLogicBusiness
    {
        //当玩家第一次进入大厅时 获得所有房间信息  
        public void do6210(MessageNode node)
        {
            //User user = node.socket.user;
            //ByteBuffer buf = RoomManager.shareRoomManager().RoomInfos();
           // user.Send(buf);
        }
        //玩家加入房间
        public void do6310(MessageNode node)
        {
            User user = node.socket.user;
            ByteBuffer buffer = node.buffer;
            GameHall.shareGameHall().addRoom(buffer.readInt(), user,buffer.readString());
        }
        //房间密码设置
        public void do7017(MessageNode node)
        {
            User user = node.socket.user;
            ByteBuffer buffer = node.buffer;
            GameHall.shareGameHall().setRoomPassword(buffer.readInt(), user, buffer.readString());
        }


        //群聊
        public void do2001(MessageNode node)
        {
            User user = node.socket.user;
            ByteBuffer buffer = node.buffer;
            GameHall.shareGameHall().QunLiaoMessage(user,buffer);
        }

        //私聊
        public void do2002(MessageNode node)
        {
            User user = node.socket.user;
            ByteBuffer buffer = node.buffer;
            GameHall.shareGameHall().SiLiaoMessage(user, buffer);
        }


    }
}
