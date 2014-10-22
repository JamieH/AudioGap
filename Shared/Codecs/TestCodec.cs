using System;

namespace AudioGap.Shared.Codecs
{
    class TestCodec : ICodec
    {
        public string Name
        {
            get { return "TestCodec"; }
        }

        public string DisplayName
        {
            get { return "Test Codec"; }
        }

        public int SampleRate
        {
            get { return 11025; }
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
