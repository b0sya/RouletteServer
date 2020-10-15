using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start();

            var i = 0;
            while (true)
            {
                i++;
                if (i > 100500)
                    break;
            }
        }

    }
}
