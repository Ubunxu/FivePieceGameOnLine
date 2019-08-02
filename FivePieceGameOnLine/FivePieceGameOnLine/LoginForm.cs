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
using util.core;

namespace FivePieceGameOnLine
{
    public partial class LoginForm : Form
    {
        private static LoginForm get_LoginForm = null;
        public static LoginForm Get_LoginForm { get => get_LoginForm; set => get_LoginForm = value; }

        public LoginForm()
        {
            InitializeComponent();
            get_LoginForm = this;
        }

        private void LoginForm_Load_1(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                U3DSocket.shareSocket().ConnectTo("192.168.1.109", 8888, () => {
                    MessageBox.Show("连接成功");
                    EventDispatch.addEventListener(this, "com", this);
                }, (str) => {
                    MessageBox.Show(str);
                }, 10);
            });
        }

        private void LoginButtClick(object sender, EventArgs e)
        {
            //登录界面可以对用户名和密码进行一些简单的判断提示，这样用户体验更好
            SendLoginMessage();
        }

        public void SendLoginMessage()
        {
            ByteBuffer byteBuffer = ByteBuffer.CreateBufferAndType(1001);
            byteBuffer.writeString(this.usernametextBox.Text);
            byteBuffer.writeString(this.passwordtextBox.Text);
            byteBuffer.Send();
        }

        private void RegeditButtClick(object sender, EventArgs e)
        {
            RegeditForm regeditForm = new RegeditForm();
            regeditForm.Show();
            this.Hide();
        }

        private void ExitButtClick(object sender, EventArgs e)
        {
            Application.Exit();
        }


        public void com1001(ByteBuffer buffer)
        {
            //MessageBox.Show(buffer.readString());
            //在服务器下发登录相应指令1001之后，暂停指令消息的执行，直到下个指令的前提条件达到。
            MessageQueue.GetSingletonMessage().StopRead();
            MessageBox.Show("success");
            GameHallForm hallForm = new GameHallForm();
            hallForm.Show();
            this.Hide();
        }

        public void com120(ByteBuffer buffer)
        {
            string error = buffer.readString();
            MessageBox.Show(error);
        }

       
    }
}
