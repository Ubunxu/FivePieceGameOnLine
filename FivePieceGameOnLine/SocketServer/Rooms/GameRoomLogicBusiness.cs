using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class GameRoomLogicBusiness
    {
        public void do7006(MessageNode node)
        {
            ByteBuffer buffer = node.buffer;
            User user = node.socket.user;
            user.room.updateGameReadyOnCancel(user,1);
        }
        public void do7007(MessageNode node)
        {
            ByteBuffer buffer = node.buffer;
            User user = node.socket.user;
            user.room.updateGameReadyOnCancel(user, -1);
        }

        public void do7999(MessageNode node)
        {
            User user = node.socket.user;
            if (user.room != null)
            {
                user.room.ExitRoom(user);
            }
        }

        public void do7111(MessageNode node)
        {
            ByteBuffer buffer = node.buffer;
            User user = node.socket.user;
            int row = buffer.readInt();//行
            int col = buffer.readInt();//列
            user.room.UserTurnGame(user, row, col);
        }

    }
}
