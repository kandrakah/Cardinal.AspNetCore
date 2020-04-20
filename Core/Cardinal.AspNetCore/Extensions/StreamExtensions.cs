using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cardinal.AspNetCore.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ToByteArray(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static Stream FromByteArray(this Stream stream, byte[] array, bool sameInstance = false)
        {
            if (sameInstance)
            {
                stream = new MemoryStream(array);
                return stream;
            }
            else
            {
                return new MemoryStream(array);
            }
        }
    }
}
