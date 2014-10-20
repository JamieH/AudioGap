using System.IO;
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
    internal class Program
    {
        private const int listenPort = 11000;

        private static void Main(string[] args)
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
            waveOut.DeviceNumber = 1;
            var waveProvider = new BufferedWaveProvider(codec.RecordFormat);
                        while (true)
            {
                if ((inc = _server.ReadMessage()) == null) continue;
                {
                    if (inc.MessageType == NetIncomingMessageType.Data)
                    {
                        if (inc.LengthBytes <= 100)
                        {
                            try
                            {
                                WaveFormat wf = new WaveFormat(new BinaryReader(new MemoryStream(inc.ReadBytes(inc.LengthBytes))));
                                Console.WriteLine("Using WaveFormat {0}", wf.ToString());
                                waveProvider = new BufferedWaveProvider(wf);
                                waveOut.Init(waveProvider);
                                waveOut.Play();

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else
                        {
                            var data = inc.ReadBytes(inc.LengthBytes);
                            Console.WriteLine(inc.LengthBytes);
                            waveProvider.AddSamples(data, 0, data.Length);
                        }
                    }
                }
            }
        }
    }
}
