using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Repositories
{
    /// <summary>
    /// Interface padrão para repositórios.
    /// </summary>
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
    }
}
