namespace FivePieceGameOnLine
{
    partial class RoomForm
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pic_center_img = new System.Windows.Forms.PictureBox();
            this.pic_right_img = new System.Windows.Forms.PictureBox();
            this.pic_left_img = new System.Windows.Forms.PictureBox();
            this.lab_roomName = new System.Windows.Forms.Label();
            this.lab_left_player = new System.Windows.Forms.Label();
            this.lab_right_player = new System.Windows.Forms.Label();
            this.lab_gameState = new System.Windows.Forms.Label();
            this.linkLab_joinRoom = new System.Windows.Forms.LinkLabel();
            this.lab_roomNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_center_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_right_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_left_img)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_center_img
            // 
            this.pic_center_img.Image = global::FivePieceGameOnLine.Properties.Resources.qp;
            this.pic_center_img.Location = new System.Drawing.Point(60, 19);
            this.pic_center_img.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_center_img.Name = "pic_center_img";
            this.pic_center_img.Size = new System.Drawing.Size(44, 58);
            this.pic_center_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_center_img.TabIndex = 1;
            this.pic_center_img.TabStop = false;
            // 
            // pic_right_img
            // 
            this.pic_right_img.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_right_img.Image = global::FivePieceGameOnLine.Properties.Resources.headImg1;
            this.pic_right_img.Location = new System.Drawing.Point(121, 19);
            this.pic_right_img.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_right_img.Name = "pic_right_img";
            this.pic_right_img.Size = new System.Drawing.Size(37, 36);
            this.pic_right_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_right_img.TabIndex = 0;
            this.pic_right_img.TabStop = false;
            // 
            // pic_left_img
            // 
            this.pic_left_img.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pic_left_img.Image = global::FivePieceGameOnLine.Properties.Resources.headImg1;
            this.pic_left_img.Location = new System.Drawing.Point(7, 19);
            this.pic_left_img.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pic_left_img.Name = "pic_left_img";
            this.pic_left_img.Size = new System.Drawing.Size(37, 36);
            this.pic_left_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_left_img.TabIndex = 0;
            this.pic_left_img.TabStop = false;
            // 
            // lab_roomName
            // 
            this.lab_roomName.AutoSize = true;
            this.lab_roomName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_roomName.ForeColor = System.Drawing.Color.Blue;
            this.lab_roomName.Location = new System.Drawing.Point(58, 0);
            this.lab_roomName.Name = "lab_roomName";
            this.lab_roomName.Size = new System.Drawing.Size(44, 17);
            this.lab_roomName.TabIndex = 2;
            this.lab_roomName.Text = "天鹰阁";
            // 
            // lab_left_player
            // 
            this.lab_left_player.AutoSize = true;
            this.lab_left_player.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_left_player.Location = new System.Drawing.Point(7, 59);
            this.lab_left_player.Name = "lab_left_player";
            this.lab_left_player.Size = new System.Drawing.Size(39, 17);
            this.lab_left_player.TabIndex = 3;
            this.lab_left_player.Text = "玩家1";
            // 
            // lab_right_player
            // 
            this.lab_right_player.AutoSize = true;
            this.lab_right_player.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_right_player.Location = new System.Drawing.Point(122, 59);
            this.lab_right_player.Name = "lab_right_player";
            this.lab_right_player.Size = new System.Drawing.Size(39, 17);
            this.lab_right_player.TabIndex = 4;
            this.lab_right_player.Text = "玩家2";
            // 
            // lab_gameState
            // 
            this.lab_gameState.AutoSize = true;
            this.lab_gameState.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_gameState.ForeColor = System.Drawing.Color.Maroon;
            this.lab_gameState.Location = new System.Drawing.Point(-1, 104);
            this.lab_gameState.Name = "lab_gameState";
            this.lab_gameState.Size = new System.Drawing.Size(53, 17);
            this.lab_gameState.TabIndex = 5;
            this.lab_gameState.Text = "游戏中...";
            // 
            // linkLab_joinRoom
            // 
            this.linkLab_joinRoom.AutoSize = true;
            this.linkLab_joinRoom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLab_joinRoom.LinkColor = System.Drawing.Color.Red;
            this.linkLab_joinRoom.Location = new System.Drawing.Point(115, 105);
            this.linkLab_joinRoom.Name = "linkLab_joinRoom";
            this.linkLab_joinRoom.Size = new System.Drawing.Size(46, 17);
            this.linkLab_joinRoom.TabIndex = 6;
            this.linkLab_joinRoom.TabStop = true;
            this.linkLab_joinRoom.Text = "进入->";
            // 
            // lab_roomNum
            // 
            this.lab_roomNum.AutoSize = true;
            this.lab_roomNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lab_roomNum.Location = new System.Drawing.Point(46, 83);
            this.lab_roomNum.Name = "lab_roomNum";
            this.lab_roomNum.Size = new System.Drawing.Size(75, 17);
            this.lab_roomNum.TabIndex = 7;
            this.lab_roomNum.Text = "房间号:1001";
            // 
            // RoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lab_roomNum);
            this.Controls.Add(this.linkLab_joinRoom);
            this.Controls.Add(this.lab_gameState);
            this.Controls.Add(this.lab_right_player);
            this.Controls.Add(this.lab_left_player);
            this.Controls.Add(this.lab_roomName);
            this.Controls.Add(this.pic_center_img);
            this.Controls.Add(this.pic_right_img);
            this.Controls.Add(this.pic_left_img);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RoomForm";
            this.Size = new System.Drawing.Size(165, 122);
            ((System.ComponentModel.ISupportInitialize)(this.pic_center_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_right_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_left_img)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_left_img;
        private System.Windows.Forms.PictureBox pic_right_img;
        private System.Windows.Forms.PictureBox pic_center_img;
        private System.Windows.Forms.Label lab_roomName;
        private System.Windows.Forms.Label lab_left_player;
        private System.Windows.Forms.Label lab_right_player;
        private System.Windows.Forms.Label lab_gameState;
        private System.Windows.Forms.LinkLabel linkLab_joinRoom;
        private System.Windows.Forms.Label lab_roomNum;
    }
}
