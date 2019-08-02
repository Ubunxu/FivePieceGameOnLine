using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class ServerMessageEventListener
    {
        private MessageQueue queue = null;
        private static ServerMessageEventListener listener = null;
        private ServerMessageEventListener()
        {
            queue = new MessageQueue();
        }
        public static ServerMessageEventListener ShareServerMessageEventListener()
        {
            if (listener == null) listener = new ServerMessageEventListener();
            return listener;
        }
       
    }
}
