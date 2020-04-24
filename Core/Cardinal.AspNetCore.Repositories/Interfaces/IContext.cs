using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Repositories
{
    /// <summary>
    /// Interface padrão para contextos.
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Salva todos os dados alterados no contexto na base de dados.
        /// </summary>
        /// <returns>Número de entradas escritas na base de dados.</returns>
        int SaveChanges();

        /// <summary>
        /// Salva todos os dados alterados no contexto na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether >
        /// is called after the changes have been sent successfully to the database.</param>
        /// <returns>Número de entradas escritas na base de dados.</returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Salva todos os dados alterados no contexto na base de dados de forma assíncrona.
        /// </summary>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to
        /// complete.</param>
        /// <returns>Número de entradas escritas na base de dados.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Salva todos os dados alterados no contexto na base de dados de forma assíncrona.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether />
        /// is called after the changes have been sent successfully to the database.</param>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to
        /// complete.</param>
        /// <returns>Número de entradas escritas na base de dados.</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que executa um comando de consulta SQL diretamente na base de dados.
        /// </summary>
        /// <param name="sql">Comando de consulta SQL.</param>
        /// <returns>Resultado da consulta à base de dados. Veja <see cref="DbDataReader"/>.</returns>
        DbDataReader Execute(string sql);

        /// <summary>
        /// Método que executa um comando SQL de alteração diretamente na base de dados.
        /// </summary>
        /// <param name="sql">Comando SQL de alteração.</param>
        /// <returns>Quantidade de registros alterados.</returns>
        int ExecuteUpdate(string sql);
    }
}
