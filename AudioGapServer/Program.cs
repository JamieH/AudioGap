using AudioGap;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using Lidgren.Network;
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
            var codec = new Codec();


            var config = new NetPeerConfiguration("airgap")
            {
                Port = listenPort,
                MaximumConnections = 50,
                ConnectionTimeout = 5f
            };
            var _server = new NetServer(config);
            _server.Start();

            Console.WriteLine(@"AudioGap Server Started");
            
            NetIncomingMessage inc; // Incoming Message

            var waveOut = new WaveOut();
            var waveProvider = new BufferedWaveProvider(codec.RecordFormat);
            waveOut.Init(waveProvider);
            waveOut.Play();
            waveProvider.BufferLength = 20000000;
            while (true)
            {
                if ((inc = _server.ReadMessage()) == null) continue;
                {
                    if (inc.MessageType == NetIncomingMessageType.Data)
                    {
                        
                        Console.WriteLine(inc.Data.Length);
                        
                        waveProvider.AddSamples(inc.Data, 0, inc.Data.Length);
                    }
                }
        }            
            //var reconstructed = new List<byte>();
            


            //int byteArrayLength=0;
            //int gotMessages=0;
            //while (true)
            //{
            //    var data = server.Receive(ref serverEndPoint);
            //    if (data.Length <= 4)
            //    {
            //        waveProvider.ClearBuffer();
            //        if (gotMessages != byteArrayLength)
            //            Console.WriteLine("Error: Not enough packets before we got our size packet, got {0}, expected {1}", gotMessages, byteArrayLength);

            //        reconstructed = new List<byte>();
            //        gotMessages = 0;
            //        int i = BitConverter.ToInt32(data, 0);
            //        byteArrayLength = i;
            //        Console.WriteLine("New message: {0}", i);
            //    }
            //    else
            //    {
            //        Console.WriteLine(data.Length);
            //        //I know I'm not putting it through the codec but if I do it just sounds worse:/
            //        waveProvider.AddSamples(data, 0, data.Length);



            //        reconstructed.AddRange(data);
            //        gotMessages++;
            //    }
            //}

            //server.Close();

        }
    }
}
