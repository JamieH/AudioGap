using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AudioGap;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioGapClient
{
    class Network
    {
        static Codec codec = new Codec();

        static UdpClient udpClient = new UdpClient();
        public static void connect(IPEndPoint endpoint, MMDevice device)
        {
            IWaveIn waveIn = new WasapiLoopbackCapture(device);
           
            waveIn.DataAvailable += SendData;
            waveIn.StartRecording();

            udpClient.Connect(endpoint);
        }

        static void SendData(object sender, WaveInEventArgs e)
        {
            byte[] encoded = codec.Encode(e.Buffer, 0, e.BytesRecorded);
            udpClient.Send(encoded, encoded.Length);
        }
    }
}
