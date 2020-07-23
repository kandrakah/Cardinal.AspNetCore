using System.IO;
using System.Security.Cryptography;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="Stream"/>
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Extensão para converter o Stream em um vetor de bytes.
        /// </summary>
        /// <param name="stream">Objeto referenciado.</param>
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

        /// <summary>
        /// Extensão que computa um hash de um stream de dados.
        /// </summary>
        /// <param name="source">Objeto referenciado</param>
        /// <param name="hashAlgoritm">Algoritmo utilizado no cálculo de hash. Veja <see cref="HashAlgorithmName"/></param>
        /// <returns>Hash gerado para o stream de dados informado</returns>
        public static byte[] ComputeHash(this Stream source, HashAlgorithmName hashAlgoritm)
        {
            var hasher = default(HashAlgorithm);
            switch (hashAlgoritm.Name)
            {
                case "MD5":
                    hasher = new MD5CryptoServiceProvider();
                    break;
                case "SHA1":
                    hasher = new SHA1CryptoServiceProvider();
                    break;
                case "SHA256":
                    hasher = new SHA256CryptoServiceProvider();
                    break;
                case "SHA384":
                    hasher = new SHA384CryptoServiceProvider();
                    break;
                case "SHA512":
                    hasher = new SHA512CryptoServiceProvider();
                    break;
                default:
                    break;
            }

            return hasher.ComputeHash(source);
        }
    }
}
