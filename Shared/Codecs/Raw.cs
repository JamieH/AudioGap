using System;
namespace AudioGap.Shared.Codecs
{
    class Raw : ICodec
    {
        public string Name
        {
            get { return "Raw"; }
        }
        public string DisplayName
        {
            get { return "Raw"; }
        }

        public int SampleRate
        {
            get { return 44100; }
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
