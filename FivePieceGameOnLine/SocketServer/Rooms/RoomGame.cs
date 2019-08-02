using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class RoomGame
    {
        private const int ROW = 15;
        private const int COL = 15;
        private const int COUNT = 5;

        private bool turn = true;
        private int userIndex = 0;
        private User[] users = null;
        //private int[,] wzq = new int[ROW, COL];//我方，白方，1， 对方，黑方，-1
        private int[,] arr = new int[15, 15];// };
        private Room parent = null;
        private User mainUser = null;

        Dictionary<string, Node> dict = new Dictionary<string, Node>();
        public RoomGame(Room _parent)
        {
            this.parent = _parent;
        }
        public void Start(User _mainUser,int index)
        {
            this.mainUser = _mainUser;
            this.userIndex = index;
            this.users = this.parent.getUsers();
            //debug.logln("游戏开始2： " + mainUser.UserName + ",  " + userIndex + "  userLeng: " + users.Length);
        }

        public void GameTurn(User user,int row,int col)
        {
            int value = user == this.mainUser ? 1 : -1;
            arr[row, col] = value;
            this.SendRowCol(user, row, col);
            this.turn = !this.turn;
            bool isOk = this.isComplate(row, col, value);
            if(isOk)
            {
                this.GameOver(user,value);
            }
        }
       

        public void Clear()
        {
            for(int i=0;i<ROW;i++)
            {
                for(int j=0;j<COL;j++)
                {
                    arr[i, j] = 0;
                }
            }
            this.turn = true;
            this.userIndex = 0;
            this.users = this.parent.getUsers();
        }
        public void GameOver(User user,int value)
        {            
            ByteBuffer buffer = ByteBuffer.CreateByteBufferType(Protcol.游戏结束);
            buffer.writeInt(1);//正常退出 1，非正常退出 -1;            
            if (user != null)
            {
                buffer.writeString(user.UserName);
                buffer.writeString(user.UserName);
                //全网通靠
                //GameHall.shareGameHall().sendAllMessage(ConstomMessage.getError("房间数据异常，请退出房间，重新进入"));
            }
            this.parent.GameOver(buffer);
            this.Clear();
        }
        private void SendRowCol(User user, int row, int col)
        {
            ByteBuffer buf = ByteBuffer.CreateByteBufferType(Protcol.玩家走棋);
            buf.writeString(user.UserName);
            buf.writeInt(row);
            buf.writeInt(col);
            buf.writeInt(turn ? 1 : -1);
            string nextName = this.nextUser().UserName;
            buf.writeString(nextName);
            this.parent.sendAllMessage(buf);
        }

        private User nextUser()
        {
            this.userIndex++;
            if (this.userIndex >= this.users.Length)
            {
                this.userIndex = 0;
            }
            return this.users[userIndex];
        }
        public bool isComplate(int x, int y, int value)
        {
            int[] DX = { 1, 0, 1, 1 };
            int[] DY = { 0, 1, 1, -1 };
            //DX[0] DY[0] 表示X 轴正向检查是否在一行
            //DX[1] DY[1] 表示Y 轴正向检查是否在一行
            //DX[2] DY[2] 表示右上 轴正向检查是否在一行
            //DX[3] DY[3] 表示右下 轴正向检查是否在一行
            int k, c1, c2, xx, yy;
            for (k = 0; k < 4; k++)
            {
                c1 = c2 = 0;
                // 如果当前棋子类型与本方向上的棋子是同一类则，继续判断
                // 正方向逐个比较
                if (x + DX[k] >= 0 && x + DX[k] <= 14 && y + DY[k] >= 0 && y + DY[k] <= 14)
                {
                    for (xx = x + DX[k], yy = y + DY[k]; arr[xx, yy] == value; xx += DX[k], yy += DY[k])
                    {
                        c1++;
                        if (xx + DX[k] >= 0 && xx + DX[k] <= 14 && yy + DY[k] >= 0 && yy + DY[k] <= 14)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                // 相反方向逐个比较
                if (x - DX[k] >= 0 && x - DX[k] <= 14 && y - DY[k] >= 0 && y - DY[k] <= 14)
                {
                    for (xx = x - DX[k], yy = y - DY[k]; arr[xx, yy] == value; xx -= DX[k], yy -= DY[k])
                    {
                        c2++;
                        if (xx - DX[k] >= 0 && xx - DX[k] <= 14 && yy - DY[k] >= 0 && yy - DY[k] <= 14)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                if (c1 + c2 >= 4)
                {
                    return true;
                }
            }
            return false;
        }

    }
    


    class Node
    {
        public int row = 0;
        public int col = 0;
        public int count = 0;//当前有多少个棋子
        public int type = 1;//白棋,0:黑棋
        public Node next = null;//下一个棋子
        public Node befor = null;//前一个棋子
        public string name = null;
    }
    
}
