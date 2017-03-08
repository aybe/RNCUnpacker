using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RNCUnpackerNET
{
    public sealed class RNCStream : MemoryStream
    {
        public RNCStream(Stream stream, bool disposeStream = true)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var stream1 = GetStream(stream);
            stream1.CopyTo(this);

            if (disposeStream)
                stream.Dispose();
        }

        [DllImport(@"RNCUnpacker.dll",
            CallingConvention = CallingConvention.Cdecl, EntryPoint = "unpack")]
        private static extern RNCStatus unpack(IntPtr input, IntPtr output);

        private static Stream GetStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var position = stream.Position;

            RNCHeader header;
            var tryParse = RNCHeader.TryParse(stream, out header);

            stream.Position = position;

            if (!tryParse)
                return stream;

            var input = new byte[stream.Length];
            var output = new byte[header.UncompressedSize];

            var read = stream.Read(input, 0, input.Length);
            if (read != input.Length)
                throw new EndOfStreamException("Couldn't read entire stream.");

            var pInput = Marshal.AllocHGlobal(input.Length);
            var pOutput = Marshal.AllocHGlobal(output.Length);
            Marshal.Copy(input, 0, pInput, input.Length); // clear
            Marshal.Copy(output, 0, pOutput, output.Length); // clear

            var status = unpack(pInput, pOutput);
            if (status == RNCStatus.Ok)
                Marshal.Copy(pOutput, output, 0, output.Length);

            Marshal.FreeHGlobal(pInput);
            Marshal.FreeHGlobal(pOutput);

            if (status != RNCStatus.Ok)
            {
                var message = string.Format("An error occurred while unpacking: {0}", status);
                throw new InvalidOperationException(message);
            }

            var memoryStream = new MemoryStream(output);
            return memoryStream;
        }
    }
}