using System;
using System.IO;
using System.Net;
using Lidgren.Network;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using AudioGap.Shared;

namespace AudioGap.Client
{
    class Network
    {
        private static NetClient _client;
        private static WasapiLoopbackCapture _waveIn;

        private static WaveFormat _sourceFormat;
        private static WaveFormat _targetFormat;
        private static ICodec _codec;

        public static void Connect(IPEndPoint endpoint, MMDevice device, ICodec codec)
        {
            var config = new NetPeerConfiguration("airgap");

            _client = new NetClient(config);
            _client.RegisterReceivedCallback(MessageReceived);

            _client.Start();

            _waveIn = new WasapiLoopbackCapture(device);
            _codec = codec;

            _sourceFormat = _waveIn.WaveFormat;
            _targetFormat = new WaveFormat(_codec.SampleRate, _codec.Channels); // format to convert to
            
            _waveIn.DataAvailable += SendData;
            _waveIn.RecordingStopped += (sender, args) => Console.WriteLine("Stopped");
            // TODO: RecordingStopped is called when you change the audio device settings, should recover from that

            NetOutgoingMessage formatMsg = _client.CreateMessage();
            formatMsg.Write(_targetFormat.Channels);
            formatMsg.Write(_targetFormat.SampleRate);
            formatMsg.Write(codec.Name);

            _client.Connect(endpoint, formatMsg);
        }

        private static void MessageReceived(object state)
        {
            NetIncomingMessage msg;
            while ((msg = _client.ReadMessage()) != null)
            {
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

                        switch (status)
                        {
                            case NetConnectionStatus.Connected:
                                _waveIn.StartRecording();
                                break;
                            case NetConnectionStatus.Disconnected:
                                {
                                    Environment.Exit(0); // TODO: show status on UI
                                    break;
                                }
                        }
                        break;

                    default:
                        Console.WriteLine("Unhandled type: " + msg.MessageType);
                        break;
                }
                _client.Recycle(msg);
            }
        }

        private static void SendData(object sender, WaveInEventArgs e)
        {
            if (e.BytesRecorded == 0)
                return;

            // buffer contains all the samples for all the channels
            // it looks like this for 16bit stereo:
            // [L1][L1][R1][R1][L2][L2][R2][R2]...
            var buffer = e.Buffer;

            var bytesPerSample = (_sourceFormat.BitsPerSample / 8) * _sourceFormat.Channels;

            // to change sample rate we must skip/repeat samples, this number tells when to do that
            var inputRatio = (float)_sourceFormat.SampleRate / _targetFormat.SampleRate;

            // combine left and right channels if we're moving down to mono
            var combineLandR = _targetFormat.Channels == 1 && _sourceFormat.Channels >= 2;

            var a = 0f;
            var p = 0;

            // loop through the samples, "i" will be set to the first byte of the first channel
            for (int i = 0; i < e.BytesRecorded; i += bytesPerSample)
            {
                while (a <= 1)
                {
                    for (var j = 0; j < _targetFormat.Channels; j++)
                    {
                        // TODO: this *might* not work for everyone, some drivers may give us shorts, who knows
                        var sample = BitConverter.ToSingle(buffer, i + sizeof(float) * j);

                        if (combineLandR)
                        {
                            sample += BitConverter.ToSingle(buffer, i + sizeof(float) * (j + 1));
                            sample /= 2;
                        }

                        var sampleShort = (short)(sample * short.MaxValue);

                        // TODO: using the same buffer wont work for people who use lower quality audio than the target format
                        buffer[p++] = (byte)(sampleShort & 0xFF);
                        buffer[p++] = (byte)(sampleShort >> 8);
                    }

                    a += inputRatio;
                }

                a -= 1;
            }

            var enc = _codec.Encode(buffer, p);

            NetOutgoingMessage msg = _client.CreateMessage(enc.Length);
            msg.Write(enc, 0, enc.Length); // p contains the length of the useful buffer data

            _client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered);

            Console.WriteLine("Sent: {0}", enc.Length);
        }
    }
}
