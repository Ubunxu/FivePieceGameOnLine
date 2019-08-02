using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class TaskManager
    {
        List<Task> tasks = null;
        public TaskManager(int n)
        {
            tasks = new List<Task>(n);
            
        }
        private void initTask(int c)
        {
            for(int i=0;i< c;i++)
            {
                Task t = new Task(() => { });
            }
        }

    }
}
