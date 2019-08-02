using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using util.net;
using System.Threading;

namespace FivePieceGameOnLine
{
    public partial class GameHallForm : Form
    {
        private GameHallOrderLogic orderLogic;
        //被双击的listview的子项的两列的名称
        private string useritemName = "";
        private string useritemCname = "";

        public GameHallForm()
        {
            InitializeComponent();
            this.orderLogic = new GameHallOrderLogic(this);
        }

        private void GameFrame_Load(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                Thread.Sleep(300);
                MessageQueue.GetSingletonMessage().RecoverRead();
            });
        }

        private void GameFrame_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoginForm.Get_LoginForm.Show();
        }

        /// <summary>
        /// 双击listview用户列表中的用户的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllListV_DoubleClick(object sender, EventArgs e)
        {
            //将选项设置为私聊  (要先将单选按钮的checked状态修改，然后这样的修改就不会受之前的默认的checked的影响)
            this.privateChatButt.Checked = true;
            ListView listView = (ListView)sender;
            useritemName = listView.SelectedItems[0].SubItems[0].Text;
            useritemCname = listView.SelectedItems[0].SubItems[1].Text;
            this.sendMessageTips.Text = "正在给[" + useritemCname + "]发送消息";
        }
        /// <summary>
        /// 群聊单选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void QLradioButton_CheckedChanged(object sender, EventArgs e)
        {
            this.sendMessageTips.Text = "正在给[所有用户]发消息";
        }

        /// <summary>
        /// 私聊单选按钮点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SLradioButton_Click(object sender, EventArgs e)
        {
            if (this.sendMessageTips.Text.Equals("正在给[所有用户]发消息"))
            {
                this.groupChatButt.Checked = true;
                MessageBox.Show("请选择私聊用户！", "提示");
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButt_Click(object sender, EventArgs e)
        {
            if (this.groupChatButt.Checked)
            {
                //群聊
                ByteBuffer byteBuffer = ByteBuffer.CreateBufferAndType(2001);
                byteBuffer.writeString(this.sendMessageTextBox.Text);
                this.sendMessageTextBox.Text = "";
                byteBuffer.Send();
            }
            else if (this.privateChatButt.Checked)
            {
                //私聊
                ByteBuffer byteBuffer2 = ByteBuffer.CreateBufferAndType(2002);
                byteBuffer2.writeString(useritemName);//用户名，不是中文名
                byteBuffer2.writeString(this.sendMessageTextBox.Text);
                this.chatListBox.Items.Add("@{" + useritemCname + "[" + useritemName + "]}：" + this.sendMessageTextBox.Text);
                this.sendMessageTextBox.Text = "";
                byteBuffer2.Send();
            }
        }


        /*加载自定义桌面
        /// <summary>
        /// 加载自定义桌面
        /// </summary>
        public void LoadDesks()
        {
            for(int i = 0; i < 20; ++i)
            {
                Desk desk = new Desk();
                this.dictRoom.Add(1001 + i + "", desk);
                this.deskContains.Controls.Add(desk);
            }

            foreach (Control control in this.deskContains.Controls)
            {
                control.Margin = new Padding(3);//设置每个控件之间的距离
            }
            UpdateRoomInfo(1001+""); 

        }

        public void UpdateRoomInfo(string roomId)
        {
            this.dictRoom[roomId].SetRoomName("漱芳斋");
        }

    */


    }
}
