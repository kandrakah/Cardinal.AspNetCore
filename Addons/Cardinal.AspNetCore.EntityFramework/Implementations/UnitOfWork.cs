using Cardinal.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Implementação da unidade de trabalho.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Instância do contexto.
        /// </summary>
        private DbContext _context;

        /// <summary>
        /// Instância de uma transação.
        /// </summary>
        private IDbContextTransaction Transaction { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto.</param>
        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método que cria uma instância de um <see cref="DbSet{TEntity}"/> de uma entidade.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <returns>DbSet da entidade solicitada.</returns>
        public DbSet<T> Set<T>() where T : Entity
        {
            return this._context.Set<T>();
        }

        /// <summary>
        /// Método que inicia uma transação.
        /// </summary>
        /// <returns>Instância da transação.</returns>
        public IDbContextTransaction BeginTransaction()
        {
            if (Transaction != null)
            {
                throw new ServiceException("Uma transação já se encontra em andamento!");
            }

            this.Transaction = this._context.Database.BeginTransaction();
            return this.Transaction;
        }

        /// <summary>
        /// Método que inicia uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Instância da transação.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction != null)
            {
                throw new ServiceException("Uma transação já se encontra em andamento nesta instância!");
            }

            this.Transaction = await this._context.Database.BeginTransactionAsync(cancellationToken);
            return this.Transaction;
        }

        /// <summary>
        /// Método que faz a reversão de alterações não confirmadas de uma transação.
        /// </summary>
        public void Rollback()
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            this.Transaction.Rollback();
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que faz a reversão de alterações não confirmadas de uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Operação assíncrona.</returns>
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            await this.Transaction.RollbackAsync(cancellationToken);
            await this.Transaction.DisposeAsync();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que faz a confirmação de alterações de uma transação.
        /// </summary>
        public void Commit()
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            this.Transaction.Commit();
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que faz a confirmação de alterações de uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Operação assíncrona.</returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            await this.Transaction.CommitAsync(cancellationToken);
            await this.Transaction.DisposeAsync();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <returns>Contagem de registros alterados.</returns>
        public int SaveChanges()
        {
            if (this.Transaction != null)
            {
                throw new ServiceException("Não é possível salvar o contexto com uma transação em andamento! Confirme ou restaure a transação antes de salvar!");
            }

            var result = this._context.SaveChanges();
            return result;
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction != null)
            {
                throw new ServiceException("Não é possível salvar o contexto com uma transação em andamento! Confirme ou restaure a transação antes de salvar!");
            }

            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// Método que efetua a liberação de resursos de um objeto.
        /// </summary>
        public void Dispose()
        {
            this.Transaction?.Dispose();
        }
    }

    /// <summary>
    /// Implementação da unidade de trabalho.
    /// </summary>
    /// <typeparam name="TContext">Contexto à ser utilizado pela unidade de trabalho.</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        /// <summary>
        /// Instância do contexto.
        /// </summary>
        private DbContext _context;

        /// <summary>
        /// Instância de uma transação.
        /// </summary>
        private IDbContextTransaction Transaction { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto.</param>
        public UnitOfWork(TContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Método que cria uma instância de um <see cref="DbSet{TEntity}"/> de uma entidade.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade.</typeparam>
        /// <returns>DbSet da entidade solicitada.</returns>
        public DbSet<T> Set<T>() where T : Entity
        {
            return this._context.Set<T>();
        }

        /// <summary>
        /// Método que inicia uma transação.
        /// </summary>
        /// <returns>Instância da transação.</returns>
        public IDbContextTransaction BeginTransaction()
        {
            if (Transaction != null)
            {
                throw new ServiceException("Uma transação já se encontra em andamento!");
            }

            this.Transaction = this._context.Database.BeginTransaction();
            return this.Transaction;
        }

        /// <summary>
        /// Método que inicia uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Instância da transação.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (Transaction != null)
            {
                throw new ServiceException("Uma transação já se encontra em andamento nesta instância!");
            }

            this.Transaction = await this._context.Database.BeginTransactionAsync(cancellationToken);
            return this.Transaction;
        }

        /// <summary>
        /// Método que faz a reversão de alterações não confirmadas de uma transação.
        /// </summary>
        public void Rollback()
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            this.Transaction.Rollback();
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que faz a reversão de alterações não confirmadas de uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Operação assíncrona.</returns>
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            await this.Transaction.RollbackAsync(cancellationToken);
            await this.Transaction.DisposeAsync();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que faz a confirmação de alterações de uma transação.
        /// </summary>
        public void Commit()
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            this.Transaction.Commit();
            this.Transaction.Dispose();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que faz a confirmação de alterações de uma transação.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Operação assíncrona.</returns>
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new ServiceException("Não há nenhuma transação em andamento nesta instância!");
            }

            await this.Transaction.CommitAsync(cancellationToken);
            await this.Transaction.DisposeAsync();
            this.Transaction = null;
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <returns>Contagem de registros alterados.</returns>
        public int SaveChanges()
        {
            if (this.Transaction != null)
            {
                throw new ServiceException("Não é possível salvar o contexto com uma transação em andamento! Confirme ou restaure a transação antes de salvar!");
            }

            var result = this._context.SaveChanges();
            return result;
        }

        /// <summary>
        /// Método que efetua a confirmação de alterações do contexto.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Contagem de registros alterados.</returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction != null)
            {
                throw new ServiceException("Não é possível salvar o contexto com uma transação em andamento! Confirme ou restaure a transação antes de salvar!");
            }

            var result = await this._context.SaveChangesAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// Método que efetua a liberação de resursos de um objeto.
        /// </summary>
        public void Dispose()
        {
            this.Transaction?.Dispose();            
        }
    }
}
