using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore
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
        /// Salva todas as alterações feitas nesse repositório no banco de dados.        
        /// </summary>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>
        int SaveChanges();

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.
        /// Várias operações ativas na mesma instância de contexto não são suportadas. Usar
        /// 'await' para garantir que quaisquer operações assíncronas tenham sido concluídas antes de chamar
        /// outro método nesse contexto.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.
        /// Várias operações ativas na mesma instância de contexto não são suportadas. Usar
        /// 'await' para garantir que quaisquer operações assíncronas tenham sido concluídas antes de chamar
        /// outro método nesse contexto.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        /// será chamado depois que as alterações foram enviadas com sucesso para o banco de dados.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>Número de alterações na base de dados.</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
