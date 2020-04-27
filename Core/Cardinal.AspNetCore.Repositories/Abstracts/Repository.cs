using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Abstracts.Repositories
{
    /// <summary>
    /// Classe base para repositórios.
    /// </summary>
    public abstract class Repository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        protected Repository()
        {
        }

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        public virtual void BeginTransaction()
        {

        }

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        /// <param name="cancelationtoken">Token de cancelamento.</param>
        /// <returns></returns>
        public virtual Task BeginTransactionAsync(CancellationToken cancelationtoken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Método para efetivar a transação.
        /// </summary>
        public virtual void CommitTransaction()
        {

        }

        /// <summary>
        /// Método para efetivar a transação de forma assíncrona.
        /// </summary>
        public virtual Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Método para reverter uma transação mal sucedida.
        /// </summary>
        public virtual void RollbackTransaction()
        {

        }

        /// <summary>
        /// Método para reverter uma transação mal sucedida de forma assíncrona.
        /// </summary>
        public virtual Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.        
        /// </summary>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>        
        /// simultaniedade ocorre quando um número inesperado de linhas é afetado durante o salvamento.        
        public abstract int SaveChanges();

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.        
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        /// será chamado depois que as alterações foram enviadas com sucesso para o banco de dados.</param>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>        
        /// simultaniedade ocorre quando um número inesperado de linhas é afetado durante o salvamento.     
        public abstract int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.
        /// Várias operações ativas na mesma instância de contexto não são suportadas. Usar
        /// 'await' para garantir que quaisquer operações assíncronas tenham sido concluídas antes de chamar
        /// outro método nesse contexto.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>        
        /// simultaniedade ocorre quando um número inesperado de linhas é afetado durante o salvamento.        
        public abstract Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

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
        public abstract Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
