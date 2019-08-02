using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using util.core;
using util.net;
using System.IO;

namespace FivePieceGameOnLine
{
    public partial class RegeditForm : Form
    {
        private byte[] imgByte = null;
        private string imgName = "";
        public RegeditForm()
        {
            InitializeComponent();
            EventDispatch.addEventListener(this, "com");
        }
        private void RegeditForm_Load(object sender, EventArgs e)
        {

        }
        private void RegeditFormClosed(object sender, FormClosedEventArgs e)
        {
            //注册两次报错处理方式，进行完善，里面的代码没有完善
            EventDispatch.Remove(this, "com");
            LoginForm.Get_LoginForm.Show();
        }

        private void EnterButtClick(object sender, EventArgs e)
        {
            if (ValidateInfo())
            {
                SendRegeditMessage();
            }
        }

        public bool ValidateInfo()
        {
            if (!this.passwordTextBox.Text.Equals(this.pwdAgainTextBox.Text))
            {
                //
                MessageBox.Show("两次密码输入不一致");
                return false;
            }
            return true;
        }

        public void SendRegeditMessage()
        {
            ByteBuffer buffer = ByteBuffer.CreateBufferTypeAndLength(1002, 1024 * 2000);
            buffer.writeString(this.userAccountTextBox.Text);
            buffer.writeString(this.nickNameTextBox.Text);
            buffer.writeString(this.sexComboBox.Text);
            buffer.writeInt(int.Parse(this.ageTextBox.Text));
            buffer.writeString(this.passwordTextBox.Text);

            //上传头像的数据
            buffer.writeInt(this.imgByte == null ? -1 : 1);
            if (this.imgByte != null)
            {
                buffer.writeString(imgName.Substring(imgName.IndexOf('.')));
                buffer.writeInt(imgByte.Length);
                buffer.writeBytes(imgByte);
            }

            buffer.Send();
        }


        private void ResetButtClick(object sender, EventArgs e)
        {
            this.userAccountTextBox.Text = "";
            this.nickNameTextBox.Text = "";
            this.passwordTextBox.Text = "";
            this.pwdAgainTextBox.Text = "";
            this.sexComboBox.Text = "<性别>";
            this.ageTextBox.Text = "";
            this.imgPictureBox.Image = null;
            this.imgPositionLable.Text = "图片位置:";
            this.imgSizeLable.Text = "图片大小:";
        }

       

        private void UploadImgButtClick(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG" + "|All Files (*.*)|*.*";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = this.openFileDialog1.FileName;
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    if (fs.Length > 1000 * 1000)
                    {
                        MessageBox.Show("图片不能超过1M");
                        return;
                    }
                    imgByte = new Byte[fs.Length];
                    this.imgName = path.Substring(path.LastIndexOf("\\") + 1);
                    this.imgPositionLable.Text = "图片位置:" + this.imgName;
                    this.imgSizeLable.Text = "图片大小:" + (imgByte.Length / 1024) + " KB  ";
                    fs.Read(imgByte, 0, imgByte.Length);
                    fs.Close();

                    Image m = Image.FromFile(path);
                    this.imgPictureBox.Image = m;
                }
                catch (Exception)
                {
                }
            }
        }


        public void com1002(ByteBuffer buffer)
        {
            Console.WriteLine("我注册成功了！！！！");
            //int t1 = buffer.readInt();
            MessageBox.Show("注册成功");
        }


    }
}
