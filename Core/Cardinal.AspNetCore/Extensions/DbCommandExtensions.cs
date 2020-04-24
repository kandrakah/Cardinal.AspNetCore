using System.Data.Common;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="DbCommand"/>.
    /// </summary>
    public static class DbCommandExtensions
    {
        /// <summary>
        /// Extensão que adiciona um novo parâmetro ao DbCommand.
        /// </summary>
        /// <typeparam name="T">Tipo de valor do parâmetro.</typeparam>
        /// <param name="cmd">Este Objeto.</param>
        /// <param name="key">Chave do parâmetro</param>
        /// <param name="value">Valor do parâmetro</param>        
        public static void AddParameter<T>(this DbCommand cmd, string key, T value)
        {
            var dbParameter = cmd.CreateParameter();
            dbParameter.ParameterName = key;
            dbParameter.Value = value;
            cmd.Parameters.Add(dbParameter);
        }
    }
}
