using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace RNCUnpacker.NET
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