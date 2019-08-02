using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using util.file;

namespace SocketServer
{
    /// <summary>
    /// 逻辑业务
    /// </summary>
    public class LogicBusiness
    {
        private UserGroup g = UserGroup.ShareUserGroup();

        //用户登陆
        public void do1001(MessageNode node)
        {
            ClientNode client = node.socket;
            ByteBuffer buffer = node.buffer;

            string name = buffer.readString();
            if(g.ContansUser(name))
            {
                client.send(ConstomMessage.getError("该用户已在别处登陆，服务器拒绝登陆！"));
                return;
            }
            if(!FFactory.Exists(Share.USER_SAVE_PATH + name))
            {
                client.send(ConstomMessage.getError("用户名错误！"));
                return;
            }
            User user = FFactory.ReadObject<User>(Share.USER_SAVE_PATH + name);
            string pwd = buffer.readString();
            if(!user.Password.Equals(pwd))
            {
                client.send(ConstomMessage.getError("密码错误！"));   
            }
            else
            {
                user.BindSocket(client);
                g.addUser(user);

                ByteBuffer buf = new ByteBuffer();
                buf.writeInt(Protcol.用户登陆);
                buf.writeString(user.UserName);
                buf.writeString(user.ChinaName);
                //debug.logln(user.Money, user.Duanwei, user.Image, user.Text);
                buf.writeInt(user.Money);
                buf.writeString(user.Duanwei);
                buf.writeString(user.Image);
                buf.writeString(user.Text);

                client.send(buf);


                GameHall.shareGameHall().addGameHall(user);

                //debug.logln(user.ChinaName + " 登陆成功");
                
                //user.ReadImageBuffer();//先把图片读出来

            }
        }
        //注册 
        public void do1002(MessageNode node)
        {
            ClientNode client = node.socket;
            ByteBuffer buff = node.buffer;

            string name = buff.readString();
            if(g.ContansUser(name) || FFactory.Exists(Share.USER_SAVE_PATH + name))
            {
                //存在
                client.send(ConstomMessage.getError("用户名已存在"));
                client = null;
                return;
            }
            string chinaName = buff.readString();
            string sex = buff.readString();
            int age = buff.readInt();
            string pwd = buff.readString();
            
            User user = new User(name, chinaName, sex, age, pwd);            
            //是否有图片上传
            int im = buff.readInt();
            if (im > 0)
            {
                string imgExt = buff.readString();
                string imgName = name + this.getImageName() + imgExt;
                user.Image = imgName;
                ByteBuffer imgbs = buff.readBuffer();
                FFactory.SaveObject(Share.USER_SAVE_IMAGE_PATH + imgName,imgbs.getBuffer());
            }
            FFactory.SaveObject(Share.USER_SAVE_PATH + name, user);
            //g.addUser(user);

            debug.logln("用户注册成功 : " + name + " 密码: " + pwd);
            ByteBuffer buf = ByteBuffer.CreateByteBufferType(Protcol.用户注册);
            //buf.writeInt(Protcol.用户注册);
            //buf.writeInt(1);
            client.send(buf);
        }

        public void _do2001(MessageNode node)
        {
            ClientNode client = node.socket;
            ByteBuffer buff = node.buffer;
            string wrod = buff.readString();
            ByteBuffer buf = new ByteBuffer(Protcol.用户群聊);
            buf.writeInt(Protcol.用户群聊);
            buf.writeString(client.user.UserName);
            buf.writeString(client.user.ChinaName);
            buf.writeString(wrod);

            User[] users = g.Users();
            foreach(User u in users)
            {
                if (u != null)
                {
                    u.Send(buf);
                }
            }

          

        }
        //私聊
        public void _do2002(MessageNode node)
        {
            ClientNode client = node.socket;
            ByteBuffer buff = node.buffer;
            string dfUser = buff.readString();
            string wrod = buff.readString();
            User user = g.getUser(dfUser);
            if (user != null)
            {
                ByteBuffer buf = new ByteBuffer(Protcol.用户私聊);
                buf.writeInt(Protcol.用户私聊);
                buf.writeString(client.user.UserName);
                buf.writeString(client.user.ChinaName);
                buf.writeString(wrod);
                user.Send(buf);
            }
        }
       
        private string getImageName()
        {
            int t = (DateTime.Now.Millisecond);
            string s1 = ((char)(new Random(t).Next(90 - 65) + 65)).ToString();
            int t2 = (DateTime.Now.Millisecond);
            string s2 = ((char)(new Random().Next(122 - 97) + 97)).ToString();
            string m = s1 + t + s2 + t2;
            return m;
        }
    }
}
