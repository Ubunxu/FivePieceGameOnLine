using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using util.core;
using util.net;
using System.Windows.Forms;

namespace FivePieceGameOnLine
{
    class GameHallOrderLogic
    {
        
        private GameHallForm hallForm = null;

        //listView列表中的在线用户(账号   用户名字)
        private Dictionary<string, string> userDic = new Dictionary<string, string>();
        //保存房间  几桌游戏
        private Dictionary<string, RoomForm> roomDic = new Dictionary<string, RoomForm>();
        public GameHallOrderLogic(GameHallForm frame)
        {
            hallForm = frame;
            EventDispatch.addEventListener(this, "com");
        }


        /// <summary>
        /// 查看所有在线用户
        /// </summary>
        /// <param name="buffer"></param>

        public void com5001(ByteBuffer buffer)
        {
            string str = buffer.readString();
            string[] userInfo = str.Split('@');
            foreach (string users in userInfo)
            {
                string[] user = users.Split('#');
                if (userDic.ContainsKey(user[0]) == false)
                    userDic.Add(user[0], user[1]);
            }
            ShowAllUserInfo();
        }

        private void ShowAllUserInfo()
        {
            List<string> userNameList = userDic.Keys.ToList<string>();
            List<string> userCNameList = userDic.Values.ToList<string>();

            for (int i = 0; i < userDic.Count; ++i)
            {
                ListViewItem viewItem = new ListViewItem();
                viewItem.Text = userNameList[i];
                viewItem.SubItems.Add(userCNameList[i]);
                hallForm.usersListView.Items.Add(viewItem);
            }
            //一开始用户在线人数
            hallForm.onLineUserNum.Text = userDic.Count + "";
        }



        /// <summary>
        /// 更新在线用户
        /// </summary>
        /// <param name="buffer"></param>
        public void com1003(ByteBuffer buffer)
        {
            //hallForm.SelectAllListV.BeginUpdate();
            int flag = buffer.readInt();
            string _username = buffer.readString();
            string _userCName = buffer.readString();
            ListViewItem viewItem = new ListViewItem();
            viewItem.Text = _username;
            viewItem.SubItems.Add(_userCName);
            if (flag < 0)//有人下线
            {
                for (int i = 0; i < hallForm.usersListView.Items.Count; ++i)
                {
                    //获取每一项的text属性
                    if (hallForm.usersListView.Items[i].Text.Equals(_username)) { hallForm.usersListView.Items.RemoveAt(i); }
                }
                hallForm.chatListBox.Items.Add("↓↓↓↓{" + _userCName + "[" + _username + "]}下线了！！！");
            }
            else//有人上线
            {
                if (!hallForm.usersListView.Items.ContainsKey(_username))
                    hallForm.usersListView.Items.Add(viewItem);
                hallForm.chatListBox.Items.Add("↑↑↑↑{" + _userCName + "[" + _username + "]}上线了！！！");
            }

            //用户在线人数
            hallForm.onLineUserNum.Text = hallForm.usersListView.Items.Count + "";
            //hallForm.SelectAllListV.EndUpdate();
        }


        /// <summary>
        /// 接受群发指令
        /// </summary>
        /// <param name="buffer"></param>
        public void com2001(ByteBuffer buffer)
        {
            ShowReceiveMessage(buffer, "<Q>");

        }
        /// <summary>
        /// 接受私聊内容
        /// </summary>
        /// <param name="buffer"></param>
        public void com2002(ByteBuffer buffer)
        {
            ShowReceiveMessage(buffer, "<S>");
        }
        public void ShowReceiveMessage(ByteBuffer buffer, string flag = "")
        {
            string _username = buffer.readString();
            string _userCName = buffer.readString();
            string _chatContent = buffer.readString();
            string str = flag + "{" + _userCName + "[" + _username + "]}：" + _chatContent;
            hallForm.chatListBox.Items.Add(str);
        }


        /// <summary>
        /// 接收服务器的房间内容消息
        /// </summary>
        /// <param name="buffer"></param>
        public void com6210(ByteBuffer buffer)
        {
            while (buffer.Available > 0)
            {
                int roomId = buffer.readInt();
                string roomName = buffer.readString();
                RoomForm room = new RoomForm();
                room.SetRoomNum(roomId.ToString());
                room.SetRoomName(roomName);

                string password = buffer.readString();
                int playerInfoLength = buffer.readInt();
                if (playerInfoLength > 0)
                {
                    string userCname = buffer.readString();
                    int roomPos = buffer.readInt();//每个房间的左右两个位置
                    string isHaveImg = buffer.readString();//接受图片  “null”:没有
                    if (isHaveImg.Equals("null"))
                    {
                        isHaveImg = null;
                    }

                    room.SetUserName(userCname, isHaveImg, roomPos);
                    playerInfoLength--;
                }
                roomDic.Add(roomId.ToString(), room);
                hallForm.roomContainer.Controls.Add(room);
                //room.Parent = hallForm.roomContains;
            }
        }

        /// <summary>
        /// 接收服务器消息， 更新大厅某个房间的信息变化
        /// </summary>
        /// <param name="buffer"></param>
        public void com6211(ByteBuffer buffer)
        {
            int roomId = buffer.readInt();
            string userName = buffer.readString();

            int roomPos = buffer.readInt();//每个房间的左右两个位置
            string isHaveImg = buffer.readString();//接受图片  “null”:没有

            if (isHaveImg.Equals("null"))
            {
                isHaveImg = null;
            }
            RoomForm room = roomDic[roomId.ToString()];
            room.SetUserName(userName, isHaveImg, roomPos);

        }

        /// <summary>
        /// 大厅用户列表，某一个用户进入大厅，
        /// 相当于更新大厅用户列表中的加入用户
        /// </summary>
        /// <param name="buffer"></param>

        public void com6551(ByteBuffer buffer)
        {
            string _name = buffer.readString();
            string _cname = buffer.readString();
            ListViewItem item = new ListViewItem();
            item.Text = _name;
            item.SubItems.Add(_cname);
            hallForm.usersListView.Items.Add(item);
        }

        /// <summary>
        /// 玩家第一次进入大厅的时候，将大厅所有的玩家的消息发给自己
        /// 相当于之前的获取所有用户列表的功能一样
        /// </summary>
        /// <param name="buffer"></param>
        public void com6550(ByteBuffer buffer)
        {
            while (buffer.Available > 0)
            {
                string _name = buffer.readString();
                string _cname = buffer.readString();
                ListViewItem item = new ListViewItem();
                item.Text = _name;
                item.SubItems.Add(_cname);
                hallForm.usersListView.Items.Add(item);
            }
            //MessageQueue.GetSingletonMessage().StopRead();
        }

        /// <summary>
        /// 某个玩家退出大厅，
        /// 相当于之前的更新用户列表中减少用户
        /// </summary>
        /// <param name="buffer"></param>

        public void com6555(ByteBuffer buffer)
        {
            string _uname = buffer.readString();
            int count = hallForm.usersListView.Items.Count;

            for (int i = 0; i < count; ++i)
            {
                string s = hallForm.usersListView.Items[i].Text;
                if (s.Equals(_uname))
                {
                    hallForm.usersListView.Items.RemoveAt(i);
                    break;
                }
            }
        }

    
    }
}
