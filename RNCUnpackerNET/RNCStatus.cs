using System.Diagnostics.CodeAnalysis;

namespace RNCUnpackerNET
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum RNCStatus
    {
        Ok,
        FileIsNotRNC,
        HuffmanDecodeError,
        FileSizeMismatch,
        PackedCrcError,
        UnpackedCrcError
    }
}