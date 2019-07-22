using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FivePieceGameOnLine
{
    public partial class ChessBorderForm : Form
    {
        private int[,] pieceLocation = new int[15, 15];//每个点的状态  -1：黑子 1：白子，  0：无棋子
        private int pieceState = -1;//落子的状态，  -1：黑子   1：白子
        //private Dictionary<Point, int> pieceDic = new Dictionary<Point, int>();//保存棋盘上的点有棋子的位置以及点的状态(0:黑子  1：白子)

        public ChessBorderForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 双击棋盘会进行画线，我们的棋子的落点位置就是根据这个原理来得到的
        /// 棋子的位置就是该交叉线的方框位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChessBorderFormDClick(object sender, MouseEventArgs e)
        {
            Graphics graphics = this.chessboard.CreateGraphics();
            for (int i = 0; i < 15; i++)
            {
                graphics.DrawLine(Pens.Red, new Point(0, i * 35 + 5), new Point(this.chessboard.Width, i * 35 + 5));
                graphics.DrawLine(Pens.Red, new Point(i * 35 + 5, 0), new Point(i * 35 + 5, this.chessboard.Height));
            }
        }
        /// <summary>
        /// 鼠标的移动时的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ChessBorderMMove(object sender, MouseEventArgs e)
        {
            int posx = e.Location.X;
            int posy = e.Location.Y;
            this.lable_posxy.Text =posx + ", " + posy;
        }
        /// <summary>
        /// 单击棋盘，可以进行落子
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChessBorderClick(object sender, MouseEventArgs e)
        {
            this.LoadChess(e);
        }
        /// <summary>
        /// 加载棋盘，包括其中的棋子以及棋盘格点的状态
        /// </summary>
        /// <param name="e"></param>
        private void LoadChess(MouseEventArgs e)
        {
            //鼠标点击的行和列
            int row = (e.Location.Y - 5) / 35;
            int col = (e.Location.X - 5) / 35;
            this.lable_posrc.Text = row + ", " + col;
            if (this.pieceLocation[row, col] == 0)
            {
                InitPiece(row, col);
                if (this.JudgeFivePiece(row, col, -this.pieceState))
                {
                    MessageBox.Show(this.pieceState == 1 ? "恭喜黑方获胜" : "恭喜白方获胜");
                }
            }
            else
            {
                MessageBox.Show("此位置有棋子");
            }
        }
        /// <summary>
        /// 初始化棋子，其中已经进行了黑白棋切换的判断
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        private void InitPiece(int row, int col)
        {
            PictureBox piece = new PictureBox();
            if (this.pieceState == -1)
            {
                piece.Image = Properties.Resources.hd;
                //改变棋盘格点的状态  
                pieceLocation[row, col] = -1;
                this.pieceState = 1;
            }
            else
            {
                piece.Image = Properties.Resources.bd;
                //改变棋盘格点的状态  
                pieceLocation[row, col] = 1;
                this.pieceState = -1;
            }
            piece.Size = new Size(30, 30);
            piece.Parent = this.chessboard;
            piece.BackColor = Color.Transparent;
            piece.SizeMode = PictureBoxSizeMode.StretchImage;
            //                                棋子图片的宽度
            int vx = col * 35 + 35 / 2 + 5 - piece.Width / 2;
            //                                棋子图片的高度
            int vy = row * 35 + 35 / 2 + 5 - piece.Height / 2;
            piece.Location = new Point(vx, vy);//棋子落点的位置
            //pieceDic.Add(piece.Location, System.Math.Abs(this.pieceState - 1));
        }
        /// <summary>
        /// 判断棋盘上棋子是否成五个
        /// </summary>
        private bool JudgeFivePiece(int x, int y, int _piece)//_piece：2是黑子  1是白子
        {
            int count = 1;//棋子计数 
            int posX = 0, posY = 0;//水平和竖直方向位置

            //判断水平方向：
            //左边
            for (posX = x - 1; posX >= 0; --posX)
            {
                if (this.pieceLocation[posX, y] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            //右边
            for (posX = x + 1; posX < 15; ++posX)
            {
                if (this.pieceLocation[posX, y] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                    break;
                }
            }

            //竖直方向：
            //上边
            for (posY = y - 1; posY >= 0; --posY)
            {
                if (this.pieceLocation[x, posY] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            //下边
            for (posY = y + 1; posY < 15; ++posY)
            {
                if (this.pieceLocation[x, posY] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                    break;
                }
            }

            //反对角线方向
            //左上边
            for (posX = x - 1, posY = y - 1; posX >= 0 && posY >= 0; --posX, --posY)
            {
                if (this.pieceLocation[posX, posY] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            //右下边
            for (posX = x + 1, posY = y + 1; posX < 15 && posY < 15; ++posX, ++posY)
            {
                if (this.pieceLocation[posX, posY] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                    break;
                }
            }

            //正对角线方向
            //右上
            for (posX = x + 1, posY = y - 1; posX < 15 && posY >= 0; ++posX, --posY)
            {
                if (this.pieceLocation[posX, posY] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    break;
                }
            }
            //左下
            for (posX = x - 1, posY = y + 1; posX >= 0 && posY < 15; --posX, ++posY)
            {
                if (this.pieceLocation[posX, posY] == _piece)
                {
                    ++count;
                    if (count >= 5)
                    {
                        return true;
                    }
                }
                else
                {
                    count = 1;
                    break;
                }
            }

            return false;
        }

       
    }
}
