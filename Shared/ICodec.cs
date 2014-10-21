using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioGap.Shared
{
    public interface ICodec
    {
        string Name { get; }
        int SampleRate { get; }
        int Channels { get; }
        byte[] Encode(byte[] audioBytes, int length);
        byte[] Decode(byte[] msgBytes);
    }
}
