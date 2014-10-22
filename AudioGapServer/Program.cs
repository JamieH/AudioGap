using System;
using System.IO;
using System.Threading;
using Lidgren.Network;
using NAudio.Wave;
using AudioGap.Shared;

namespace AudioGap.Server
{
    internal class Program
    {
        private const int port = 11000;

        private static BufferedWaveProvider waveProvider = null;
        private static ICodec codec = null;
        private static WaveOut waveOut = new WaveOut();
        private static void Main(string[] args)
        {
            var config = new NetPeerConfiguration("airgap")
            {
                Port = port,
                MaximumConnections = 50,
                ConnectionTimeout = 5f
            };

            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            var server = new NetServer(config);
            server.Start();
            Console.WriteLine("AudioGap Server Started");
            waveOut.DeviceNumber = 0; // TODO: need an option for this

            while (true)
            {
                NetIncomingMessage msg;

                if ((msg = server.ReadMessage()) == null)
                {
                    Thread.Sleep(1);
                    continue;
                }

                switch (msg.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(msg.ReadString());
                        break;

                    case NetIncomingMessageType.StatusChanged:
                        var status = (NetConnectionStatus)msg.ReadByte();
                        string reason = msg.ReadString();
                        Console.WriteLine("New status: " + status + " (" + reason + ")");
                        break;

                    case NetIncomingMessageType.ConnectionApproval:
                        SetupAudio(msg);
                        msg.SenderConnection.Approve();
                        break;

                    case NetIncomingMessageType.Data:
                        if (msg.SenderConnection.Status == NetConnectionStatus.Connected)
                            HandleAudioPacket(msg);
                        break;

                    default:
                        Console.WriteLine("Unhandled type: " + msg.MessageType);
                        break;
                }
            }
        }

        private static void SetupAudio(NetIncomingMessage msg)
        {
            var channels = msg.ReadInt32();
            var rate = msg.ReadInt32();
            var codecName = msg.ReadString();

            var waveFormat = new WaveFormat(rate, channels);

            codec = Codec.Get(codecName);

            Console.WriteLine("Using WaveFormat {0}", waveFormat);
            Console.WriteLine("Using Codec: {0}", codec.Name);

            waveProvider = new BufferedWaveProvider(waveFormat);
            waveOut.Init(waveProvider);
            waveOut.Play();
        }

        private static void HandleAudioPacket(NetIncomingMessage msg)
        {
            Console.WriteLine("Received: {0}", msg.LengthBytes);

            var data = msg.ReadBytes(msg.LengthBytes);
            var decoded = codec.Decode(data);

            waveProvider.AddSamples(decoded, 0, decoded.Length);
        }
    }
}
