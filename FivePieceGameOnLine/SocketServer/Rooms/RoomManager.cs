using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using util.file;

namespace SocketServer
{
    public class RoomManager
    {

        private Group<int, Room> rooms = new Group<int,Room>();
        private static RoomManager rm = null;
        public static RoomManager shareRoomManager()
        {
            if (rm == null)
            {
                rm = new RoomManager();
                rm.initRoom(20);
            }
            return rm;
        }
        private void initRoom(int count)
        {
            string s = "另一个次元#天使界#紫云亭#蝴蝶梦#烟味缭绕丶迷惑谁的心╰╮#淑女闺房#离歌无言只是太悲つ#怡然居#游子居凌#莪卜怕輸わ#遗忘过去╮珍惜现在#如花美眷似水流#落寞☆偝影つ#我是为谁而来#白衣染霜华#陌·兮#给力的男人#绝不放弃#ゞ伱的爱我全权代理#再見亦是決別";
            string[] s1 = s.Split('#');
            for (int i=0;i< count;i++)
            {
                Room r = new Room(1001+i,s1[i]);
                rooms.Add(r.Roomid, r);
            }
        }
        public bool addRoom(int roomId,User user,string password)
        {
            Room r = this.rooms[roomId];
            //有房间密码
            if (r.Password!=null && r.Password.Length>0)
            {
                if(password.Length<=0)
                {
                    ByteBuffer buf = ByteBuffer.CreateByteBufferType(Protcol.通知玩家房间有密码);
                    buf.writeInt(roomId);
                    user.Send(buf);
                    return false;
                }
                if(!r.Password.Equals(password))
                {                    
                    user.Send(ConstomMessage.getError("房间密码不对"));
                    return false;
                }
            }
            bool f = r.AddRoom(user);  
            if(!f)
            {
                user.Send(ConstomMessage.getError("房间已满"));
            }         
            return f;
        }
        public void setRoomPassword(int roomId, User user, string pwd)
        {
            Room r = this.rooms[roomId];
            if(!user.roomOwner)
            {
                user.Send(ConstomMessage.getError("你不是房主，不能设置密码"));
                return;
            }
            if(r.getRoomState()==1)
            {
                user.Send(ConstomMessage.getError("游戏正在进行中，不能设置房间密码"));
                return;
            }
            if(!r.Contains(user))
            {
                user.Send(ConstomMessage.getError("警告：只能设置自己所在房间的密码!"));
                return;
            }
            r.Password = pwd;
            user.Send(ConstomMessage.getError("房间密码成功设置为:["+pwd+"]请牢记"));
        }


        public ByteBuffer RoomInfos()
        {
            ByteBuffer buf = ByteBuffer.CreateByteBufferType(Protcol.所有房间信息, 100*40);//6210
            Room[] rms = rooms.Values();
            foreach(Room r in rms)
            {
                buf.writeInt(r.Roomid);
                buf.writeString(r.Roomname);               
                buf.writeString(r.Password);
                buf.writeInt(r.getRoomState());
                User[] us = r.getUsers();
                buf.writeInt(us.Length);//0
                if(us.Length>0)
                {                    
                    foreach (User u in us)
                    {
                        buf.writeString(u.ChinaName);
                        buf.writeInt(u.DeskPos);
                        buf.writeString(u.Image);
                    }
                }              
            }
            return buf;
        }
        
        public ByteBuffer RoomInfos(int roomId)
        {
            bool isExt = false;
            ByteBuffer buf = ByteBuffer.CreateByteBufferForLenth(100);
            buf.writeInt(Protcol.更新大厅某个房间信息);//6211
            Room r = rooms[roomId];
            //foreach (Room r in rms)
            {
                buf.writeInt(r.Roomid);
                buf.writeString(r.Roomname);                
                buf.writeString(r.Password);
                User[] us = r.getUsers();
                buf.writeInt(us.Length);
                if(us.Length>0)
                {                    
                    foreach (User u in us)
                    {
                        buf.writeString(u.ChinaName);
                        buf.writeInt(u.Image == null ? -1 : 1);
                        if (u.Image != null)
                        {
                            if (!isExt)
                            {
                                isExt = true;
                                buf.ExpansionCapacity(1000 * 1000 * us.Length + buf.Length);
                            }
                            byte[] files = FFactory.ReadFile(Share.USER_SAVE_PATH + "imgs/" + u.Image);
                            buf.writeInt(files.Length);
                            buf.writeBytes(files);
                        }
                    }
                } 
            }
            return buf;
        }
    }
}
