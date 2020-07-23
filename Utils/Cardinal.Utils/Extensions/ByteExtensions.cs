using System;
using System.Security.Cryptography;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="byte"/>.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Extensão que computa um hash de um vetor de bytes.
        /// </summary>
        /// <param name="bytes">Objeto referenciado</param>
        /// <param name="hashAlgoritm">Algoritmo utilizado no cálculo de hash. Veja <see cref="HashAlgorithmName"/></param>
        /// <returns>Hash gerado para o byte array informado</returns>
        public static byte[] ComputeHash(this byte[] bytes, HashAlgorithmName hashAlgoritm)
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

            return hasher.ComputeHash(bytes);
        }

        /// <summary>
        /// Extensão para conversão de um vetor de bytes em Base64;
        /// </summary>
        /// <param name="bytes">Objeto referenciado.</param>
        /// <returns>Valor em Base64 do vetor de bytes informado.</returns>
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
    }
}
