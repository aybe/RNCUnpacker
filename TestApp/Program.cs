using System.IO;
using RNCUnpackerNET;

namespace TestApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var files = new[]
            {
                "packed.dat",
                "unpacked.dat"
            };

            foreach (var file in files)
            {
                var input = File.OpenRead(file);

                var stream = new RNCStream(input);

                using (var output = File.Create(string.Format("{0}.output", file)))
                {
                    stream.WriteTo(output);
                }
            }
        }
    }
}