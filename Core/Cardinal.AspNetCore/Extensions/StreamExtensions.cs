using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para Stream.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Extensão para converter o Stream em um vetor de bytes.
        /// </summary>
        /// <param name="stream">Este stream</param>
        /// <returns>Vetor de bytes referente ao stream.</returns>
        public static byte[] ToByteArray(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Extensão para converter um vetor de bytes em um stream.
        /// </summary>
        /// <param name="stream">Este stream.</param>
        /// <param name="array">Vetor de bytes.</param>
        /// <param name="sameInstance"></param>
        /// <returns>Stream carregado com o vetor de bytes informado.</returns>
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
