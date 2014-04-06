using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AudioGapClient
{
    class Network
    {
        static UdpClient udpClient = new UdpClient();
        public static void connect(IPEndPoint endpoint)
        {


            udpClient.Connect(endpoint);
        }


    }
}
