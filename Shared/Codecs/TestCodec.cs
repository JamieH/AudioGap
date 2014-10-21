using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioGap.Shared.Codecs
{
    class TestCodec : ICodec
    {
        public string Name
        {
            get { return "Test Codec"; }
        }

        public int SampleRate
        {
            get { return 0; }
        }

        public int Channels
        {
            get { return 2; }
        }

        public byte[] Encode(byte[] audioBytes, int length)
        {
            var d = new byte[length];
            Array.Copy(audioBytes,d, length);

            return d;
        }

        public byte[] Decode(byte[] msgBytes)
        {
            return msgBytes;
        }
    }
}
