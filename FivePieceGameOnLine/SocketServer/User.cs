using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using util.file;

namespace SocketServer
{
    [Serializable]
    public class User
    {
        private int uid;
        private string userName;
        private string chinaName;
        private string userSex;
        private int userAge;
        private string password;
        private string duanwei = "青铜IV";
        private int _money = 50;
        private string text = "这个人什么都没有说";

        private int deskPos = -1;//-1:左边，1：右边
        private int ready = -1;
        private string image = null;

        public bool roomOwner = false;
        public int connected = 0;//是否连接

        public Room room = null;
        private ClientNode socket = null;
        private List<string> msgList = new List<string>();
        List<User> frends = new List<User>();
        public User(string userName,String chinaName, string userSex, int userAge, string password)
        {
            this.userName = userName;
            this.chinaName = chinaName;
            this.userSex = userSex;
            this.userAge = userAge;
            this.password = password;
        }
        public void BindSocket(ClientNode _socket)
        {
            this.socket = _socket;
            _socket.user = this;
            this.connected = 1;//表示连接
        }
        public void Send(ByteBuffer buffer)
        {
            if (this.connected > 0)
            {
                this.socket.send(buffer);
            }
        }
        
























        public int Uid
        {
            get
            {
                return uid;
            }

            set
            {
                uid = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string UserSex
        {
            get
            {
                return userSex;
            }

            set
            {
                userSex = value;
            }
        }

        public int UserAge
        {
            get
            {
                return userAge;
            }

            set
            {
                userAge = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string ChinaName
        {
            get
            {
                return chinaName;
            }

            set
            {
                chinaName = value;
            }
        }

        public string Image
        {
            get
            {
                if (image == null) return "null";
                return "http://" + Share.IPAddress.ToString() + ":8080/userImages/" + this.image;
            }

            set
            {
                image = value;
            }
        }

        public int DeskPos
        {
            get
            {
                return deskPos;
            }

            set
            {
                deskPos = value;
            }
        }

        public int Ready
        {
            get
            {
                return ready;
            }

            set
            {
                ready = value;
            }
        }

        public string Duanwei
        {
            get
            {
                return duanwei;
            }

            set
            {
                duanwei = value;
            }
        }

        public int Money
        {
            get
            {
                return _money;
            }

            set
            {
                _money = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        ByteBuffer imgBuf=null;
        public ByteBuffer ReadImageBuffer()
        {
            if (this.image != null)
            {
                if (imgBuf == null)
                {
                    imgBuf = this.getImageBuffer();
                }
            }
            return imgBuf;
        }
        public ByteBuffer getImageBuffer()
        {
            byte[] files = FFactory.ReadFile(Share.USER_SAVE_PATH + "imgs/" + this.Image);
            ByteBuffer buf = new ByteBuffer(-1, files.Length+4);//125679
            buf.writeInt(files.Length);
            buf.writeBytes(files);
            return buf;
        }
    }
}
