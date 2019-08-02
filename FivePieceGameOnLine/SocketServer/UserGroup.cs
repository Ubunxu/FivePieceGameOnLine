using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class UserGroup
    {
        private static UserGroup _u = null;
        private Dictionary<string, User> users = new Dictionary<string, User>();
        public static UserGroup ShareUserGroup()
        {
            if (_u == null) _u = new UserGroup();
            return _u;
        }

        public bool addUser(User user)
        {            
            lock(this)
            {
                if (this.users.ContainsKey(user.UserName))
                {
                    return false;
                }
               // SendMessage(user,1);
                this.users.Add(user.UserName, user);
            }
            return true;
        }

        public User RemoveUser(string userName)
        {
            
            lock(this)
            {
                User c = null;
                if (this.users.ContainsKey(userName))
                {
                    c = this.users[userName];
                    this.users.Remove(userName);
                   // SendMessage(c,-1);
                    return c;
                }
            }
            return null;
            
        }

        public User getUser(string userName)
        {
            lock(this)
            {
                if (this.users.ContainsKey(userName))
                {
                    return this.users[userName];
                }
            }
            return null;
        }

        public User[] Users()
        {
            lock(this)
            {
                return this.users.Values.ToArray<User>();
            }
        }

        public bool ContansUser(string usreName)
        {
            return this.users.ContainsKey(usreName);
        }

        public void SendMessage(User user,int v)
        {
            ByteBuffer buffer = null;
            if (v<0)
            {
                buffer = new ByteBuffer(Protcol.用户加入And离开服务器);
                buffer.writeInt(Protcol.用户加入And离开服务器);
            }
            else
            {
                buffer = new ByteBuffer(Protcol.用户加入And离开服务器);
                buffer.writeInt(Protcol.用户加入And离开服务器);
            }
            buffer.writeInt(v);
            buffer.writeString(user.UserName);
            buffer.writeString(user.ChinaName);

            User[] users = this.Users();
            foreach (User u in users)
            {
                u.Send(buffer);
            }
        }        
    }
}
