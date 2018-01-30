using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

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

        private static ushort ReadUInt16BE(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var bytes = reader.ReadBytes(2);
            Array.Reverse(bytes);
            var u = BitConverter.ToUInt16(bytes, 0);
            return u;
        }

        private static uint ReadUInt32BE(BinaryReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            var bytes = reader.ReadBytes(4);
            Array.Reverse(bytes);
            var u = BitConverter.ToUInt32(bytes, 0);
            return u;
        }

        /// <summary>
        ///     Tries to parse an instance from a stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="result"></param>
        /// <returns>
        ///     <c>true</c> if a header was found and compression scheme is supported.
        /// </returns>
        public static bool TryParse(Stream stream, out RNCHeader result)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            result = default(RNCHeader);

            var reader = new BinaryReader(stream);

            var signature = Encoding.ASCII.GetString(reader.ReadBytes(3));
            if (signature != "RNC")
                return false;

            var compression = (RNCCompression) reader.ReadByte();
            if (compression != RNCCompression.Method1)
                return false;

            var uncompressedSize = ReadUInt32BE(reader);
            var compressedSize = ReadUInt32BE(reader);
            var uncompressedSum = ReadUInt16BE(reader);
            var compressedSum = ReadUInt16BE(reader);
            var leeway = reader.ReadByte();
            var packChunks = reader.ReadByte();

            result = new RNCHeader(
                signature, compression, uncompressedSize, compressedSize,
                uncompressedSum, compressedSum, leeway, packChunks
            );

            return true;
        }
    }
}