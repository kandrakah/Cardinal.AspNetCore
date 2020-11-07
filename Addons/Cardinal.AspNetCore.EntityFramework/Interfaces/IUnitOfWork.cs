using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Interface da unidade de trabalho.
    /// </summary>
    public interface IUnitOfWork : IUnitOfWork<DbContext>
    {

    }

    /// <summary>
    /// Interface da unidade de trabalho.
    /// </summary>
    /// <typeparam name="TContext">Contexto à ser utilizado pela unidade de trabalho.</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// Método que cria uma instância de um <see cref="DbSet{TEntity}"/> de uma entidade.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <returns>DbSet da entidade solicitada.</returns>
        DbSet<T> Set<T>() where T : Entity;

        /// <summary>
        /// Método que inicia uma transação.
        /// </summary>
        /// <returns>Instância da transação.</returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// Método que inicia uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Instância da transação.</returns>
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que faz a reversão de alterações não confirmadas de uma transação.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Método que faz a reversão de alterações não confirmadas de uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Operação assíncrona.</returns>
        Task RollbackAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que faz a confirmação de alterações de uma transação.
        /// </summary>
        void Commit();

        /// <summary>
        /// Método que faz a confirmação de alterações de uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Operação assíncrona.</returns>
        Task CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <returns>Contagem de registros alterados.</returns>
        int SaveChanges();

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
