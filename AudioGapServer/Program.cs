using AudioGap;
using NAudio.Wave;
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
            var codec = new Codec();

            UdpClient server = new UdpClient(listenPort);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, listenPort);

            var reconstructed = new List<byte>();
            
            var waveOut = new WaveOut();
            var waveProvider = new BufferedWaveProvider(codec.RecordFormat);
            waveOut.Init(waveProvider);
            waveOut.Play();

            int byteArrayLength=0;
            int gotMessages=0;
            while (true)
            {
                var data = server.Receive(ref serverEndPoint);
                if (data.Length <= 4)
                {
                    waveProvider.ClearBuffer();
                    if (gotMessages != byteArrayLength)
                        Console.WriteLine("Error: Not enough packets before we got our size packet, got {0}, expected {1}", gotMessages, byteArrayLength);

                    reconstructed = new List<byte>();
                    gotMessages = 0;
                    int i = BitConverter.ToInt32(data, 0);
                    byteArrayLength = i;
                    Console.WriteLine("New message: {0}", i);
                }
                else
                {
                    Console.WriteLine(data.Length);
                    //I know I'm not putting it through the codec but if I do it just sounds worse:/
                    waveProvider.AddSamples(data, 0, data.Length);
                    
                    //byte[] encoded = codec.Decode(data, 0, data.Length); //using the codec
                    //waveProvider.AddSamples(encoded, 0, encoded.Length); //perhaps I should only add the samples once I reconstucted what ever is being sent? :/

                    reconstructed.AddRange(data);
                    gotMessages++;
                }
            }

            server.Close();

        }
    }
}
