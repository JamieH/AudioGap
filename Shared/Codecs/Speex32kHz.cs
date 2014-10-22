using System;
using NSpeex;

namespace AudioGap.Shared.Codecs
{
    class Speex32kHz : ICodec
    {
        private SpeexEncoder _encoder;
        private SpeexDecoder _decoder;

        private byte[] _buffer;
        private int _bufferLength;

        public Speex32kHz()
        {
            _encoder = new SpeexEncoder(BandMode.UltraWide);
            _encoder.Quality = 10;

            _decoder = new SpeexDecoder(BandMode.UltraWide);

            _buffer = new byte[SampleRate];
            _bufferLength = 0;
        }

        public string Name
        {
            get { return "Speex32kHz"; }
        }

        public string DisplayName
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
            if (_bufferLength + length > _buffer.Length)
                Array.Resize(ref _buffer, _buffer.Length + SampleRate);

            // copy input to buffer
            Buffer.BlockCopy(audioBytes, 0, _buffer, _bufferLength, length);
            _bufferLength += length;

            // the number of samples per frame must be a multiple of encoder.FrameSize
            var sampleCount = ((_bufferLength / sizeof(short)) / _encoder.FrameSize) * _encoder.FrameSize;
            var sampleCountBytes = sampleCount * sizeof(short);
            var samples = new short[sampleCount];

            // take out the samples we want to encode
            Buffer.BlockCopy(_buffer, 0, samples, 0, sampleCountBytes);
            Buffer.BlockCopy(_buffer, sampleCountBytes, _buffer, 0, _bufferLength - sampleCountBytes);
            _bufferLength -= sampleCountBytes;

            // encode samples
            var encodedData = new byte[length];
            var encodedBytes = _encoder.Encode(samples, 0, samples.Length, encodedData, 0, encodedData.Length);

            // remove excess bytes
            var trimmed = new byte[encodedBytes];
            Buffer.BlockCopy(encodedData, 0, trimmed, 0, encodedBytes);

            return trimmed;
        }

        public byte[] Decode(byte[] msgBytes)
        {
            var decodedFrame = new short[msgBytes.Length * 320];
            var len = _decoder.Decode(msgBytes, 0, msgBytes.Length, decodedFrame, 0, false);

            var data = new byte[len * 2];
            Buffer.BlockCopy(decodedFrame, 0, data, 0, data.Length);

            return data;
        }
    }
}
