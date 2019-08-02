using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using util.net;

namespace FivePieceGameOnLine
{
    public partial class RoomForm : UserControl
    {

        private string roomName = null;
        private string roomId = null;
        private bool leftSitDown = false;

        public RoomForm()
        {
            InitializeComponent();
        }




        public void SetRoomName(string name)
        {
            this.roomName = name;
            this.lab_roomName.Text = name;
        }

        public void SetRoomNum(string _roomId)
        {
            this.roomId = _roomId;
            this.lab_roomNum.Text = "房间号:" + _roomId;
        }
        /// <summary>
        /// 设置玩家的名字到房间中，是玩家的中文名字
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="imgRrl"></param>
        /// <param name="leftOrRight">加入房间的玩家的是在桌子的左边还是右边， -1是代表左边，  大于零是代表右边</param>
        public void SetUserName(string uName, string imgRrl = null, int leftOrRight = -1)
        {
            //如果左边没有人
            if (leftOrRight < 0)
            {
                this.lab_left_player.Text = uName;
                if (imgRrl != null)
                {
                    this.pic_left_img.LoadAsync(imgRrl);
                }
                this.leftSitDown = true;
            }
            else
            {
                this.lab_right_player.Text = uName;
                if (imgRrl != null)
                {
                    this.pic_right_img.LoadAsync(imgRrl);
                }
            }
        }

        public void setUserImage(string imgUrl, int leftOrRight = -1)
        {
            if (leftOrRight < 0)
            {
                this.pic_left_img.LoadAsync(imgUrl);
            }
            else
            {
                this.pic_right_img.LoadAsync(imgUrl);
            }
        }


        public void SetUserName(string uName, Image img = null)
        {
            //如果左边没有人
            if (!this.leftSitDown)
            {
                this.lab_left_player.Text = uName;
                if (img != null)
                {
                    this.pic_left_img.Image = img;
                }
                this.leftSitDown = true;
            }
            else
            {
                this.lab_right_player.Text = uName;
                if (img != null)
                {
                    this.pic_right_img.Image = img;
                }
            }
        }

        public void SetRoomState(string str)
        {
            this.lab_gameState.Text = str;
        }




        private void join_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            JoinRoom();
        }

        /// <summary>
        /// 加入房间
        /// </summary>
        /// 
        public void JoinRoom()
        {
            ByteBuffer buffer = ByteBuffer.CreateBufferAndType(6310);
            buffer.writeInt(int.Parse(this.roomId));
            //buffer.writeString(this.roomName);
            buffer.Send();
        }
    }
}
