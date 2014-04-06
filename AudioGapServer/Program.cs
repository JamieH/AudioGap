using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AudioGapServer
{
    class Program
    {
        private const int listenPort = 11000;

        static void Main(string[] args)
        {
            UdpClient server = new UdpClient(listenPort);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, listenPort);

            byte[] data;



            while (true)
            {
                data = server.Receive(ref serverEndPoint);
                Console.WriteLine("Message from: {0}", serverEndPoint.ToString());
            }

            server.Close();

        }
    }
}
