using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public struct MessageNode
    {
        public int Type;
        public ByteBuffer buffer;
        public ClientNode socket;
        public Action Method;
    }
}
