using Cardinal.AspNetCore.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Service<TEntity> : Service where TEntity : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        protected IRepository<TEntity> Repository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public Service(IServiceProvider provider) : base(provider)
        {
            this.Repository = this.GetService<IRepository<TEntity>>();
        }

        /// <summary>
        /// Método para adicionar uma entidade ao contexto.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        public virtual TEntity Add([NotNull] TEntity entity)
        {
            var result = this.Repository.Add(entity);
            return result.Entity;
        }

        /// <summary>
        /// Método para adicionar uma entidade ao contexto de forma assíncrona.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>Uma tarefa que representa a operação de adição assíncrona. O resultado da tarefa contém
        /// Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry para a entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a entidade.</returns>
        public virtual async Task<TEntity> AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await this.Repository.AddAsync(entity, cancellationToken);
            return result.Entity;
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual void AddRange([NotNull] params TEntity[] entities)
        {
            this.Repository.AddRange(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual void AddRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Repository.AddRange(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual async Task AddRangeAsync([NotNull] params TEntity[] entities)
        {
            await this.Repository.AddRangeAsync(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        public virtual async Task AddRangeAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await this.Repository.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Começa a rastrear a entidade especificada e as entradas acessíveis a partir da entidade especificada
        /// usando o estado Microsoft.EntityFrameworkCore.EntityState.Unchanged por padrão.
        /// </summary>
        /// <param name="entity">Entidade à ser rastreada.</param>
        /// <returns>entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a
        /// entidade.</returns>
        public virtual TEntity Attach([NotNull] TEntity entity)
        {
            var result = this.Repository.Attach(entity);
            return result.Entity;
        }

        public virtual void AttachRange([NotNull] params TEntity[] entities)
        {
            this.Repository.AddRange(entities);
        }

        public virtual void AttachRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Repository.AddRange(entities);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            var result = this.Repository.Find(keyValues);
            return result;
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var result = await this.Repository.FindAsync(keyValues);
            return result;
        }

        public TEntity Remove([NotNull] TEntity entity)
        {
            var result = this.Repository.Remove(entity);
            return result.Entity;
        }

        public void RemoveRange([NotNull] params TEntity[] entities)
        {
            this.Repository.RemoveRange(entities);           
        }

       public void RemoveRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Repository.RemoveRange(entities);
        }

        public TEntity Update([NotNull] TEntity entity)
        {
            var result = this.Repository.Update(entity);
            return result.Entity;
        }

        public void UpdateRange([NotNull] params TEntity[] entities)
        {
            this.Repository.UpdateRange(entities);
        }

        public void UpdateRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Repository.UpdateRange(entities);
        }

        public void BeginTransaction()
        {
            this.Repository.BeginTransaction();
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            this.Repository.BeginTransactionAsync(cancellationToken);
        }

        public async void CommitTransaction()
        {
            this.Repository.CommitTransaction();
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await this.Repository.CommitTransactionAsync(cancellationToken);
        }

        public void RollbackTransaction()
        {
            this.Repository.RollbackTransaction();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await this.Repository.RollbackTransactionAsync(cancellationToken);
        }

        public int SaveChanges()
        {
            return this.Repository.SaveChanges();
        }

        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return this.Repository.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.Repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await this.Repository.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
