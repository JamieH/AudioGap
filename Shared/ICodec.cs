namespace AudioGap.Shared
{
    public interface ICodec
    {
        string Name { get; }
        string DisplayName { get; }

        int SampleRate { get; }
        int Channels { get; }

        byte[] Encode(byte[] audioBytes, int length);
        byte[] Decode(byte[] msgBytes);
    }
}
