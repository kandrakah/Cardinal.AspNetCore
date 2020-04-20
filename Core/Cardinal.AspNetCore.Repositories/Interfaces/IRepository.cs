using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Repositories
{
    public interface IRepository
    {
        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        /// <param name="solationLevel">Nível de isolamento da transação.</param>
        void BeginTransaction(IsolationLevel solationLevel);

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        /// <param name="cancelationtoken">Token de cancelamento.</param>
        /// <returns></returns>
        Task BeginTransactionAsync(CancellationToken cancelationtoken = default);

        /// <summary>
        /// Método para efetivar a transação.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Método para reverter uma transação mal sucedida.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Método que efetua o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <returns>Número de alterações na base de dados.</returns>
        int SaveChanges();

        /// <summary>
        /// Método que efetua de forma assíncrona o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <returns>Número de alterações na base de dados.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Método que efetua o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns>Número de alterações na base de dados.</returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Método que efetua de forma assíncrona o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns>Número de alterações na base de dados.</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);

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

        /// <summary>
        /// Método para liberação de recursos usados pelo repositório.
        /// </summary>
        void Dispose();
    }
}
