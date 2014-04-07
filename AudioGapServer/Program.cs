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


            int byteArrayLength=0;
            int gotMessages=0;
            while (true)
            {
                data = server.Receive(ref serverEndPoint);
                if (data.Length <= 4)
                {
                    if (gotMessages != byteArrayLength)
                    {
                        Console.WriteLine("Error: Not enough packets before we got our size packet, got {0}, expected {1}", gotMessages, byteArrayLength);
                        gotMessages = 0;
                    }
                    else
                        gotMessages = 0;
                    int i = BitConverter.ToInt32(data, 0);
                    byteArrayLength = i;
                    Console.WriteLine("New message: {0}", i);
                }
                else
                {
                    Console.WriteLine(data.Length);
                    gotMessages++;
                }
            }

            server.Close();

        }
    }
}
