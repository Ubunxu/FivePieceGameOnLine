using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class Algorithm
    {
        private int hi = 0;
        private int[,] arr = {
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
            {0,0,0,0,0,0,0,0,0,0,-1,0,0,0,1},
            {0,0,0,0,0,0,0,0,0,0,0,1,0,0,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,1,0,1 },
            {0,0,0,0,0,0,0,0,0,0,0,0,0,1,1 },
            {1,1,1,1,1,0,0,0,0,0,0,0,0,0,1 }
        };

        //public void start()
        //{
        //    for (int i = 0; i < 15; i++)
        //    {
        //        for (int j = 0; j < 15; j++)
        //        {
        //            arr[i, j] = 0;
        //        }
        //    }
        //}
        
        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        /// <param name="flg">黑子，白子（-1，1）</param>
        public bool isWin(int x, int y, int flg)
        {
            arr[x, y] = flg;

            for (int i = -1; i < 3; i++)
            {
                nnn(x, y, flg, i);
            }
            if(flg>0)
            {
                return hi > 0;
            }
            if(flg<0)
            {
                return hi < 0;
            }
            return false;
        }

        public void nnn(int x, int y, int flg, int n)
        {
            int h = 1;
            int xi = 0;
            int yi = 0;
            bool h_1 = true;
            bool h_2 = true;
            for (int i = 1; i < 5; i++)
            {
                xi = (n >= 0 ? x + i : x);
                yi = (n >= 0 ? (n >= 1 ? (n >= 2 ? y + i : y - i) : y) : y + i);
                if (yi >= 0 && yi < 15 && xi >= 0 && xi < 15 && flg == arr[xi, yi] &&h_1)
                {
                    h++;
                }
                else
                {
                    h_1 = false;
                }
                xi = (n >= 0 ? x - i : x);
                yi = (n >= 0 ? (n >= 1 ? (n >= 2 ? y - i : y + i) : y) : y - i);
                if (yi >= 0 && yi < 15 && xi >= 0 && xi < 15 && flg == arr[xi, yi] && h_2)
                {
                    h++;
                }
                else
                {
                    h_2 = false;
                }
            }
            if (h == 5)
            {
                hi = flg;
            }
        }
    }
}
