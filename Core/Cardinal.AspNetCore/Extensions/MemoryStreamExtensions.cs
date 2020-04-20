using System.IO;

namespace Cardinal.AspNetCore.Extensions
{
    public static class MemoryStreamExtensions
    {
        public static void WriteToFile(this MemoryStream stream, string filePath)
        {
            var bytes = stream.ToArray();
            File.WriteAllBytes(filePath, bytes);
        }
    }
}
