namespace FivePieceGameOnLine
{
    partial class ChessBorderForm
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
            this.chessboard = new System.Windows.Forms.PictureBox();
            this.lable_posxy = new System.Windows.Forms.Label();
            this.lable_posrc = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chessboard)).BeginInit();
            this.SuspendLayout();
            // 
            // chessboard
            // 
            this.chessboard.Image = global::FivePieceGameOnLine.Properties.Resources.qp;
            this.chessboard.Location = new System.Drawing.Point(5, 4);
            this.chessboard.Name = "chessboard";
            this.chessboard.Size = new System.Drawing.Size(535, 535);
            this.chessboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.chessboard.TabIndex = 0;
            this.chessboard.TabStop = false;
            this.chessboard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ChessBorderClick);
            this.chessboard.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ChessBorderFormDClick);
            this.chessboard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChessBorderMMove);
            // 
            // lable_posxy
            // 
            this.lable_posxy.AutoSize = true;
            this.lable_posxy.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable_posxy.Location = new System.Drawing.Point(625, 63);
            this.lable_posxy.Name = "lable_posxy";
            this.lable_posxy.Size = new System.Drawing.Size(25, 17);
            this.lable_posxy.TabIndex = 1;
            this.lable_posxy.Text = "0,0";
            // 
            // lable_posrc
            // 
            this.lable_posrc.AutoSize = true;
            this.lable_posrc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lable_posrc.Location = new System.Drawing.Point(649, 122);
            this.lable_posrc.Name = "lable_posrc";
            this.lable_posrc.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lable_posrc.Size = new System.Drawing.Size(25, 17);
            this.lable_posrc.TabIndex = 2;
            this.lable_posrc.Text = "0,0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(539, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "鼠标位置(x,y):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(539, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "鼠标行列(row,col):";
            // 
            // ChessBorderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 543);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lable_posrc);
            this.Controls.Add(this.lable_posxy);
            this.Controls.Add(this.chessboard);
            this.Name = "ChessBorderForm";
            this.Text = "ChessBorderForm";
            ((System.ComponentModel.ISupportInitialize)(this.chessboard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox chessboard;
        private System.Windows.Forms.Label lable_posxy;
        private System.Windows.Forms.Label lable_posrc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}