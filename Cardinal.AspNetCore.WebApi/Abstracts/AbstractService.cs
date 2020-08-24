using Cardinal.AspNetCore.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class AbstractService<TEntity> : AbstractService where TEntity : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        protected IRepository<TEntity> Repository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public AbstractService(IServiceProvider provider) : base(provider)
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

        /// <summary>
        /// Começa a rastrear um grupo de entidades e as entradas acessíveis a partir das entidades informadas
        /// usando o estado Microsoft.EntityFrameworkCore.EntityState.Unchanged por padrão para cada entidade.
        /// </summary>
        /// <param name="entities">Uma ou mais entidades à serem rastreadas.</param>
        public virtual void AttachRange([NotNull] params TEntity[] entities)
        {
            this.Repository.AddRange(entities);
        }

        /// <summary>
        /// Começa a rastrear uma enumeração de entidades e as entradas acessíveis a partir das entidades informadas
        /// usando o estado Microsoft.EntityFrameworkCore.EntityState.Unchanged por padrão para cada entidade.
        /// </summary>
        /// <param name="entities">Enumeração de entidades à serem rastreadas.</param>
        public virtual void AttachRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Repository.AddRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual TEntity Find(params object[] keyValues)
        {
            var result = this.Repository.Find(keyValues);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            var result = await this.Repository.FindAsync(keyValues);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Remove([NotNull] TEntity entity)
        {
            var result = this.Repository.Remove(entity);
            return result.Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange([NotNull] params TEntity[] entities)
        {
            this.Repository.RemoveRange(entities);           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
       public void RemoveRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Repository.RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update([NotNull] TEntity entity)
        {
            var result = this.Repository.Update(entity);
            return result.Entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateRange([NotNull] params TEntity[] entities)
        {
            this.Repository.UpdateRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Repository.UpdateRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        public void BeginTransaction()
        {
            this.Repository.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await this.Repository.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CommitTransaction()
        {
            this.Repository.CommitTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            await this.Repository.CommitTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RollbackTransaction()
        {
            this.Repository.RollbackTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            await this.Repository.RollbackTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return this.Repository.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return this.Repository.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.Repository.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await this.Repository.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
