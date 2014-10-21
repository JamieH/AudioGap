using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSpeex;

namespace AudioGap.Shared.Codecs
{
    class Speex_32khz : ICodec
    {
        public static SpeexEncoder encoder = new SpeexEncoder(BandMode.UltraWide);
        public static SpeexDecoder decoder = new SpeexDecoder(BandMode.UltraWide);

        public string Name
        {
            get { return "Speex Ultra Wide Band (32kHz)"; }
        }

        public int SampleRate
        {
            get { return 32000; }
        }

        public int Channels
        {
            get { return 1; }
        }

        public byte[] Encode(byte[] audioBytes, int length)
        {

            // convert to short
            short[] data = new short[length / 2];
            Buffer.BlockCopy(audioBytes, 0, data, 0, length);
            var encodedData = new byte[length * 1000];
            // note: the number of samples per frame must be a multiple of encoder.FrameSize
            var encodedBytes = encoder.Encode(data, 0, data.Length, encodedData, 0, encodedData.Length);
            byte[] tm = new byte[encodedBytes];

            Buffer.BlockCopy(encodedData, 0, tm, 0, encodedBytes);

            return tm;
        }

        public byte[] Decode(byte[] msgBytes)
        {
            short[] decodedFrame = new short[102400]; // should be the same number of samples as on the capturing side
            var len = decoder.Decode(msgBytes, 0, msgBytes.Length, decodedFrame, 0, false);

            byte[] data = new byte[decodedFrame.Length * 2];
            Buffer.BlockCopy(msgBytes, 0, data, 0, len);

            return data;
        }
    }
}
