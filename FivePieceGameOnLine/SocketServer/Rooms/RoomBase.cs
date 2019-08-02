using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    [Serializable]
    public abstract class RoomBase
    {
        private int roomid;
        private string roomname;
        private int currentCount = 0;
        protected const int MAX_COUNT = 2;
        private bool isWatch = false;//是否允许围观
        private string password = null;
        private RoomState state = RoomState.Idle;
        protected bool Gameing = false;
        private Group<string, User> group = Group<string,User>.CreateGroup();            

        public RoomBase(int id,string name= null)
        {
            this.roomid = id;
            this.Roomname = name;
        }
        public bool Add(User user)
        {
            if (currentCount >= MAX_COUNT)
            {
                //if (!this.isWatch)
                //{
                //    return false;
                //}
                return false;
            }            
            group.Add(user.UserName, user);
            this.currentCount++;
            this.state = RoomState.Adding;
            return true;
        }
        public void Remove(User user)
        {
            this.currentCount--;
            group.Remove(user.UserName);
            if(this.currentCount<=0)
            {
                this.state = RoomState.Idle;
            }          
        }
        public void sendAllMessage(ByteBuffer buffer)
        {
            User[] users = group.Values();
            foreach(User u in users)
            {
                if(u!= null)
                {
                    u.Send(buffer);
                }
            }
        }
        public void sendSingleMessage(User user,ByteBuffer buffer)
        {
            user.Send(buffer);
        }
        public User[] getUsers()
        {
            return this.group.Values();
        }       
        public bool Contains(User user)
        {
            return this.group.ContainsKey(user.UserName);
        }
        public int CurrentCount
        {
            get
            {
                return currentCount;
            }

            set
            {
                currentCount = value;
            }
        }
        public bool IsWatch
        {
            get
            {
                return isWatch;
            }

            set
            {
                isWatch = value;
            }
        }

        public string Password
        {
            get
            {
                return password==null?"":password;
            }

            set
            {
                password = value;
            }
        }

        protected RoomState State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        public int Roomid
        {
            get
            {
                return roomid;
            }
        }

        public string Roomname
        {
            get
            {
                return roomname;
            }

            set
            {
                roomname = value;
            }
        }
    }
}
