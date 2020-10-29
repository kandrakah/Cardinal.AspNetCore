using System.IO;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="MemoryStream"/>.
    /// </summary>
    public static class MemoryStreamExtensions
    {
        /// <summary>
        /// Extensão para salvar um MemoryStream em arquivo.
        /// </summary>
        /// <param name="stream">Este Stream</param>
        /// <param name="filePath">Diretório onde o stream será salvo junto com o nome do arquivo</param>
        /// <param name="fileMode">Modo de abertura do arquivo. Veja <see cref="FileMode"/></param>
        /// <param name="fileAccess">Modo de acesso ao arquivo. Veja <see cref="FileAccess"/></param>
        public static void WriteToFile(this MemoryStream stream, string filePath, FileMode fileMode = FileMode.Open, FileAccess fileAccess = FileAccess.Read)
        {
            var data = stream.ToArray();
            using (var file = new FileStream(filePath, fileMode, fileAccess))
            {
                file.Write(data, 0, data.Length);
                file.Flush();
            }
        }
    }
}
