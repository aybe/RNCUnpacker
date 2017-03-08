using System.Diagnostics.CodeAnalysis;

namespace RNCUnpackerNET
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public enum RNCStatus
    {
        Ok,
        FileIsNotRNC,
        HufDecodeError,
        FileSizeMismatch,
        PackedCrcError,
        UnpackedCrcError
    }
}