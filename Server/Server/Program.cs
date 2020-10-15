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

            //for (var i = 0; i < 100500; i++)
            //{
            //    Console.Write(i);
            //}

            //I realized that this is a useless function, do not forget to remove it, along with the repository
            while (true)
            {
                Console.WriteLine("Max go to NA xui");
                //Sorry
            }
        }

    }
}
