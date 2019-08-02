namespace FivePieceGameOnLine
{
    partial class LoginForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usernametextBox = new System.Windows.Forms.TextBox();
            this.passwordtextBox = new System.Windows.Forms.TextBox();
            this.regeditButt = new System.Windows.Forms.Button();
            this.loginButt = new System.Windows.Forms.Button();
            this.exitButt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(141, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(141, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "密  码:";
            // 
            // usernametextBox
            // 
            this.usernametextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usernametextBox.Location = new System.Drawing.Point(192, 65);
            this.usernametextBox.Name = "usernametextBox";
            this.usernametextBox.Size = new System.Drawing.Size(152, 23);
            this.usernametextBox.TabIndex = 2;
            this.usernametextBox.Text = "xjy";
            // 
            // passwordtextBox
            // 
            this.passwordtextBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordtextBox.Location = new System.Drawing.Point(192, 120);
            this.passwordtextBox.Name = "passwordtextBox";
            this.passwordtextBox.PasswordChar = '●';
            this.passwordtextBox.Size = new System.Drawing.Size(152, 23);
            this.passwordtextBox.TabIndex = 3;
            this.passwordtextBox.Text = "123456";
            // 
            // regeditButt
            // 
            this.regeditButt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.regeditButt.Location = new System.Drawing.Point(93, 184);
            this.regeditButt.Name = "regeditButt";
            this.regeditButt.Size = new System.Drawing.Size(79, 23);
            this.regeditButt.TabIndex = 4;
            this.regeditButt.Text = "注册";
            this.regeditButt.UseVisualStyleBackColor = true;
            this.regeditButt.Click += new System.EventHandler(this.RegeditButtClick);
            // 
            // loginButt
            // 
            this.loginButt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginButt.Location = new System.Drawing.Point(212, 184);
            this.loginButt.Name = "loginButt";
            this.loginButt.Size = new System.Drawing.Size(79, 23);
            this.loginButt.TabIndex = 5;
            this.loginButt.Text = "登录";
            this.loginButt.UseVisualStyleBackColor = true;
            this.loginButt.Click += new System.EventHandler(this.LoginButtClick);
            // 
            // exitButt
            // 
            this.exitButt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.exitButt.Location = new System.Drawing.Point(332, 184);
            this.exitButt.Name = "exitButt";
            this.exitButt.Size = new System.Drawing.Size(79, 23);
            this.exitButt.TabIndex = 6;
            this.exitButt.Text = "退出";
            this.exitButt.UseVisualStyleBackColor = true;
            this.exitButt.Click += new System.EventHandler(this.ExitButtClick);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 341);
            this.Controls.Add(this.exitButt);
            this.Controls.Add(this.loginButt);
            this.Controls.Add(this.regeditButt);
            this.Controls.Add(this.passwordtextBox);
            this.Controls.Add(this.usernametextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "LoginForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.LoginForm_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernametextBox;
        private System.Windows.Forms.TextBox passwordtextBox;
        private System.Windows.Forms.Button regeditButt;
        private System.Windows.Forms.Button loginButt;
        private System.Windows.Forms.Button exitButt;
    }
}

