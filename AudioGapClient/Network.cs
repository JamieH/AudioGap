using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using AudioGap;
using Lidgren.Network;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioGapClient
{
    class Network
    {
        static Codec codec = new Codec();
        private static NetClient _netClient;
        private static WasapiLoopbackCapture waveIn;

        public static void connect(IPEndPoint endpoint, MMDevice device)
        {
            var config = new NetPeerConfiguration("airgap");
            _netClient = new NetClient(config);
            _netClient.Start();
            _netClient.Connect(endpoint);



            waveIn = new WasapiLoopbackCapture(device);
            waveIn.DataAvailable += SendData;            
            waveIn.StartRecording();
        }

        private static void SendData(object sender, WaveInEventArgs e)
        {
            MemoryStream sendStream = new MemoryStream(e.BytesRecorded);

            for (int i = 0; i < e.BytesRecorded / 4; i++)
            {

                float sample = BitConverter.ToSingle(e.Buffer, i * 4);
                short sampleShort = (short)(sample * 32768);
                sendStream.Write(BitConverter.GetBytes(sampleShort), 0, 2);
            }

            NetOutgoingMessage msg = _netClient.CreateMessage();
            var dat = codec.Encode(sendStream.GetBuffer(), 0, sendStream.GetBuffer().Length);
            msg.Write(dat);
            _netClient.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("sending {0}", dat.Length);

        }
    }
}
