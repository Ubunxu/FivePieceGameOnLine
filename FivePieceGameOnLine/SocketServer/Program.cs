using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace SocketServer
{
    public delegate int MyAction();

    class Program
    {
        static int COL = 15;
        static int ROW = 15;
        static string input = null;
        static object sobj = new object();
        static int i = 0;

        private static readonly ManualResetEvent Moinst = new ManualResetEvent(false);
        private static int[,] wzq =
       {
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,-1,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,-1,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,-1,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,-1,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,-1,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,-1,0,0,0,0,0 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},
            {0,0,0,0,0,0,0,0,0,0,0,1,0,0,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,1,1 },
            {1,1,1,1,0,1,0,0,0,0,0,0,0,0,1 }
        };

        static void Main(string[] args)
        {
            StartServer();

           
            Console.ReadKey();
        }
        public static bool is5inRow(int[,] arr, int x, int y, int value)
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
      

        static void StartServer()
        {
            //while (true)
            //{
                //int port = int.Parse(Console.ReadLine());
                Server server = new Server("192.168.1.102", 8888);
                server.Start();
            //}
        }
        

    }
}
