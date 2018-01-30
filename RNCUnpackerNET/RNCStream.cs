using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RNCUnpackerNET
{
    /// <inheritdoc />
    /// <summary>
    ///     RNC-aware stream, detects and unpacks RNC-packed content automatically, else passes content through.
    /// </summary>
    public sealed class RNCStream : MemoryStream
    {
#if RNC_UNITY
        private const string Dll = @"RNCUnpacker";
#else
        private const string Dll = @"RNCUnpacker.dll";
#endif

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of <see cref="T:RNCUnpackerNET.RNCStream" />.
        /// </summary>
        /// <param name="sourceStream">
        ///     Source stream, can be either RNC-packed content or not.
        /// </param>
        /// <param name="dispose">
        ///     Dispose <paramref name="sourceStream" />?
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="sourceStream" /> is <c>null</c>.
        /// </exception>
        public RNCStream(Stream sourceStream, bool dispose = true)
        {
            if (sourceStream == null)
                throw new ArgumentNullException(nameof(sourceStream));

            var stream = GetStream(sourceStream);

            stream.CopyTo(this);

            if (dispose)
                sourceStream.Dispose();
        }

        private static Stream GetStream(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            var position = stream.Position;

            var parse = RNCHeader.TryParse(stream, out var header);

            stream.Position = position; // restore position in case of not-RNC

            if (!parse)
                return stream;

            var input = new byte[stream.Length];
            var output = new byte[header.UncompressedSize];

            var read = stream.Read(input, 0, input.Length);
            if (read != input.Length)
                throw new EndOfStreamException("Couldn't read entire stream.");

            var p1 = Marshal.AllocHGlobal(input.Length);
            var p2 = Marshal.AllocHGlobal(output.Length);
            Marshal.Copy(input, 0, p1, input.Length);
            Marshal.Copy(output, 0, p2, output.Length);

            var status = Unpack(p1, p2);
            if (status == RNCStatus.Ok)
                Marshal.Copy(p2, output, 0, output.Length);

            Marshal.FreeHGlobal(p1);
            Marshal.FreeHGlobal(p2);

            if (status != RNCStatus.Ok)
                throw new InvalidOperationException($"Error while unpacking: '{status}'.");

            return new MemoryStream(output);
        }

        [DllImport(Dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "unpack")]
        private static extern RNCStatus Unpack(IntPtr input, IntPtr output);
    }
}