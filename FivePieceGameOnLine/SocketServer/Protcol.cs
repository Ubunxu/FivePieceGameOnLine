using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Protcol
    {
        public static int 错误信息 = 120;
        public static int 提示信息 = 110;

        public static int 用户注册 = 1002;
        public static int 用户登陆 = 1001;
        public static int 用户加入And离开服务器 = 1003;
        public static int 用户加入服务器= 1004;

        public static int 用户群聊 = 2001;
        public static int 用户私聊 = 2002;

        public static int 查看在线用户 = 5001;

        public static int 房间信息 = 6001;
        public static int 所有房间信息 = 6210;


        public static int 更新大厅某个房间信息 = 6211;
        public static int 大厅所有信息给某个玩家 = 6550;
        public static int 某个玩家的所有信息给大厅所有玩家 = 6551;
        public static int 玩家离开大厅 = 6555;
        

        public static int 玩家第一次进入房间 = 7000;
        public static int 玩家进入房间 = 7001;
        public static int 玩家离开房间 = 7999;
        public static int 房间状态 = 7005;
        public static int 房间密码 = 7017;
        public static int 通知玩家房间有密码 = 7018;



        public static int 游戏准备 = 7006;
        public static int 取消准备 = 7007;
        public static int 游戏开始 = 7100;

        public static int 玩家走棋 = 7111;
        public static int 游戏结束 = 7119;

    }
}
