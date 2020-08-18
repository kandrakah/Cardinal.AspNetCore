using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Interfaces
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
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que efetua de forma assíncrona o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>Número de alterações na base de dados.</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
