using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace RNCUnpackerNET
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 18)]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public struct RNCHeader
    {
        public readonly string Signature;
        public readonly RNCCompression Compression;
        public readonly uint UncompressedSize;
        public readonly uint CompressedSize;
        public readonly ushort UncompressedSum;
        public readonly ushort CompressedSum;
        public readonly byte Leeway;
        public readonly byte PackChunks;

        private RNCHeader(
            string signature, RNCCompression compression, uint uncompressedSize, uint compressedSize,
            ushort uncompressedSum, ushort compressedSum, byte leeway, byte packChunks)
        {
            Signature = signature;
            Compression = compression;
            UncompressedSize = uncompressedSize;
            CompressedSize = compressedSize;
            UncompressedSum = uncompressedSum;
            CompressedSum = compressedSum;
            Leeway = leeway;
            PackChunks = packChunks;
        }

        public static bool TryParse(Stream stream, out RNCHeader result)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            result = default(RNCHeader);

            var reader = new BinaryReader(stream);

            var signature = Encoding.ASCII.GetString(reader.ReadBytes(3));
            if (signature != "RNC")
                return false;

            var compression = (RNCCompression) reader.ReadByte();
            if (compression != RNCCompression.Method1)
                return false;

            var uncompressedSize = ReadUInt32(reader);
            var compressedSize = ReadUInt32(reader);
            var uncompressedSum = ReadUInt16(reader);
            var compressedSum = ReadUInt16(reader);
            var leeway = reader.ReadByte();
            var packChunks = reader.ReadByte();

            result = new RNCHeader(
                signature, compression, uncompressedSize, compressedSize,
                uncompressedSum, compressedSum, leeway, packChunks);

            return true;
        }

        private static uint ReadUInt32(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var bytes = reader.ReadBytes(4);
            Array.Reverse(bytes);
            var u = BitConverter.ToUInt32(bytes, 0);
            return u;
        }

        private static ushort ReadUInt16(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            var bytes = reader.ReadBytes(2);
            Array.Reverse(bytes);
            var u = BitConverter.ToUInt16(bytes, 0);
            return u;
        }
    }
}