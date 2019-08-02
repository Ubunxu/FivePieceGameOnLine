using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class ConstomMessage
    {
        public static ByteBuffer getError(string str)
        {
            ByteBuffer buffer = new ByteBuffer(Protcol.错误信息);
            buffer.writeInt(Protcol.错误信息);
            buffer.writeString(str);
            return buffer;
        }
        public static void sendError(User user,string str)
        {
            user.Send(getError(str));
        }
        public static void SendAllMessage(string str)
        {
            User[] users = UserGroup.ShareUserGroup().Users();
            foreach(User u in users)
            {
                if(u!=null)
                {
                    sendError(u,"服务器崩溃!!!," + str);
                }
            }
        }
        public static void UserExitServer(User user)
        {
           
        }
    }
}
