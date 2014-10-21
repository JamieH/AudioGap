using System;
using System.IO;
using System.Threading;
using Lidgren.Network;
using NAudio.Wave;

namespace AudioGapServer
{
    internal class Program
    {
        private const int Port = 11000;

        private static void Main(string[] args)
        {
            var config = new NetPeerConfiguration("airgap")
            {
                Port = Port,
                MaximumConnections = 50,
                ConnectionTimeout = 5f
            };

            var server = new NetServer(config);
            server.Start();

            Console.WriteLine("AudioGap Server Started");

            var waveOut = new WaveOut();
            waveOut.DeviceNumber = 0; // TODO: need an option for this

            BufferedWaveProvider waveProvider = null;

            while (true)
            {
                NetIncomingMessage msg;

                if ((msg = server.ReadMessage()) == null)
                {
                    Thread.Sleep(1);
                    continue;
                }

                if (msg.MessageType != NetIncomingMessageType.Data)
                    continue;

                if (waveProvider == null)
                {
                    var msgBytes = msg.ReadBytes(msg.LengthBytes);
                    var msgReader = new BinaryReader(new MemoryStream(msgBytes));
                    var waveFormat = new WaveFormat(msgReader);

                    Console.WriteLine("Using WaveFormat {0}", waveFormat);

                    waveProvider = new BufferedWaveProvider(waveFormat);
                    waveOut.Init(waveProvider);
                    waveOut.Play();
                }
                else
                {
                    Console.WriteLine("Received: {0}", msg.LengthBytes);

                    var data = msg.ReadBytes(msg.LengthBytes);
                    waveProvider.AddSamples(data, 0, data.Length);
                }
            }
        }
    }
}
