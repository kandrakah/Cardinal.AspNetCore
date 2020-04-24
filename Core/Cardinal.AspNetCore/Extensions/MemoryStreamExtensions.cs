using System.IO;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para MemoryStream.
    /// </summary>
    public static class MemoryStreamExtensions
    {
        /// <summary>
        /// Extensão para salvar um MemoryStream em arquivo.
        /// </summary>
        /// <param name="stream">Este Stream.</param>
        /// <param name="filePath">Diretório onde o stream será salvo junto com o nome do arquivo.</param>
        public static void WriteToFile(this MemoryStream stream, string filePath)
        {
            var bytes = stream.ToArray();
            File.WriteAllBytes(filePath, bytes);
        }
    }
}
