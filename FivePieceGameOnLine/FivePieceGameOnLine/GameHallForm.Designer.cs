namespace FivePieceGameOnLine
{
    partial class GameHallForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.roomContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.chatListBox = new System.Windows.Forms.ListBox();
            this.sendMessageTips = new System.Windows.Forms.Label();
            this.groupChatButt = new System.Windows.Forms.RadioButton();
            this.privateChatButt = new System.Windows.Forms.RadioButton();
            this.usersListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.onLineUserNum = new System.Windows.Forms.Label();
            this.sendMessageTextBox = new System.Windows.Forms.TextBox();
            this.sendMessageButt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // roomContainer
            // 
            this.roomContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.roomContainer.Location = new System.Drawing.Point(14, 20);
            this.roomContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.roomContainer.Name = "roomContainer";
            this.roomContainer.Size = new System.Drawing.Size(384, 258);
            this.roomContainer.TabIndex = 0;
            // 
            // chatListBox
            // 
            this.chatListBox.FormattingEnabled = true;
            this.chatListBox.ItemHeight = 17;
            this.chatListBox.Location = new System.Drawing.Point(16, 320);
            this.chatListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chatListBox.Name = "chatListBox";
            this.chatListBox.Size = new System.Drawing.Size(262, 157);
            this.chatListBox.TabIndex = 1;
            // 
            // sendMessageTips
            // 
            this.sendMessageTips.AutoSize = true;
            this.sendMessageTips.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendMessageTips.ForeColor = System.Drawing.Color.Red;
            this.sendMessageTips.Location = new System.Drawing.Point(284, 320);
            this.sendMessageTips.Name = "sendMessageTips";
            this.sendMessageTips.Size = new System.Drawing.Size(148, 17);
            this.sendMessageTips.TabIndex = 2;
            this.sendMessageTips.Text = "正在给[所有用户]发送消息";
            // 
            // groupChatButt
            // 
            this.groupChatButt.AutoSize = true;
            this.groupChatButt.Checked = true;
            this.groupChatButt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupChatButt.Location = new System.Drawing.Point(286, 351);
            this.groupChatButt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupChatButt.Name = "groupChatButt";
            this.groupChatButt.Size = new System.Drawing.Size(50, 21);
            this.groupChatButt.TabIndex = 3;
            this.groupChatButt.TabStop = true;
            this.groupChatButt.Text = "群聊";
            this.groupChatButt.UseVisualStyleBackColor = true;
            // 
            // privateChatButt
            // 
            this.privateChatButt.AutoSize = true;
            this.privateChatButt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.privateChatButt.Location = new System.Drawing.Point(284, 380);
            this.privateChatButt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.privateChatButt.Name = "privateChatButt";
            this.privateChatButt.Size = new System.Drawing.Size(50, 21);
            this.privateChatButt.TabIndex = 3;
            this.privateChatButt.Text = "私聊";
            this.privateChatButt.UseVisualStyleBackColor = true;
            // 
            // usersListView
            // 
            this.usersListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.usersListView.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.usersListView.FullRowSelect = true;
            this.usersListView.GridLines = true;
            this.usersListView.Location = new System.Drawing.Point(432, 20);
            this.usersListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.usersListView.Name = "usersListView";
            this.usersListView.Size = new System.Drawing.Size(144, 258);
            this.usersListView.TabIndex = 4;
            this.usersListView.UseCompatibleStateImageBehavior = false;
            this.usersListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "用户名";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "昵称";
            this.columnHeader2.Width = 70;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(463, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "在线用户";
            // 
            // onLineUserNum
            // 
            this.onLineUserNum.AutoSize = true;
            this.onLineUserNum.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.onLineUserNum.ForeColor = System.Drawing.Color.Red;
            this.onLineUserNum.Location = new System.Drawing.Point(543, 286);
            this.onLineUserNum.Name = "onLineUserNum";
            this.onLineUserNum.Size = new System.Drawing.Size(15, 17);
            this.onLineUserNum.TabIndex = 6;
            this.onLineUserNum.Text = "0";
            // 
            // sendMessageTextBox
            // 
            this.sendMessageTextBox.Location = new System.Drawing.Point(287, 430);
            this.sendMessageTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sendMessageTextBox.Multiline = true;
            this.sendMessageTextBox.Name = "sendMessageTextBox";
            this.sendMessageTextBox.Size = new System.Drawing.Size(239, 40);
            this.sendMessageTextBox.TabIndex = 7;
            // 
            // sendMessageButt
            // 
            this.sendMessageButt.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendMessageButt.Location = new System.Drawing.Point(546, 430);
            this.sendMessageButt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sendMessageButt.Name = "sendMessageButt";
            this.sendMessageButt.Size = new System.Drawing.Size(65, 40);
            this.sendMessageButt.TabIndex = 8;
            this.sendMessageButt.Text = "发送";
            this.sendMessageButt.UseVisualStyleBackColor = true;
            // 
            // GameHallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 479);
            this.Controls.Add(this.sendMessageButt);
            this.Controls.Add(this.sendMessageTextBox);
            this.Controls.Add(this.onLineUserNum);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.usersListView);
            this.Controls.Add(this.privateChatButt);
            this.Controls.Add(this.groupChatButt);
            this.Controls.Add(this.sendMessageTips);
            this.Controls.Add(this.chatListBox);
            this.Controls.Add(this.roomContainer);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "GameHallForm";
            this.Text = "GameHallForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label sendMessageTips;
        private System.Windows.Forms.RadioButton groupChatButt;
        private System.Windows.Forms.RadioButton privateChatButt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox sendMessageTextBox;
        private System.Windows.Forms.Button sendMessageButt;
        internal System.Windows.Forms.ListView usersListView;
        internal System.Windows.Forms.Label onLineUserNum;
        internal System.Windows.Forms.ListBox chatListBox;
        internal System.Windows.Forms.FlowLayoutPanel roomContainer;
    }
}