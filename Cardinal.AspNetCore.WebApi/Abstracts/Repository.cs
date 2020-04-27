using Cardinal.AspNetCore.Abstracts.Repositories;
using Cardinal.AspNetCore.Utils;
using Cardinal.AspNetCore.WebApi.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Cardinal.AspNetCore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : Repository<TEntity, DbContext> where TEntity : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context) : base(context)
        {
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    public class Repository<TEntity, TContext> : Repository where TEntity : Entity where TContext : DbContext
    {
        /// <summary>
        /// Contexto do repositório.
        /// </summary>
        protected TContext Context { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected DbSet<TEntity> Entity { get; set; }

        /// <summary>
        /// Instância da transação.
        /// </summary>
        protected IDbContextTransaction Transaction { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Repository(TContext context)
        {
            this.Context = context;
            this.Entity = this.Context.Set<TEntity>();
        }

        /// <summary>
        /// Método para adicionar uma entidade ao contexto.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        public virtual EntityEntry<TEntity> Add([NotNull] TEntity entity)
        {
            return this.Entity.Add(entity);
        }

        /// <summary>
        /// Método para adicionar uma entidade ao contexto de forma assíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entity">Entidade à ser adicionada.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>Uma tarefa que representa a operação de adição assíncrona. O resultado da tarefa contém
        /// Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry para a entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a entidade.</returns>
        public virtual ValueTask<EntityEntry<TEntity>> AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default)
        {
            return this.Entity.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual void AddRange([NotNull] params TEntity[] entities)
        {
            this.Entity.AddRange(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual void AddRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Entity.AddRange(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual async Task AddRangeAsync([NotNull] params TEntity[] entities)
        {
            await this.Entity.AddRangeAsync(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        public virtual async Task AddRangeAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await this.Entity.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Começa a rastrear a entidade especificada e as entradas acessíveis a partir da entidade especificada
        /// usando o estado Microsoft.EntityFrameworkCore.EntityState.Unchanged por padrão.
        /// </summary>
        /// <param name="entity">Entidade à ser rastreada.</param>
        /// <returns><see cref="EntityEntry"/>para a entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a
        /// entidade.</returns>
        public EntityEntry<TEntity> Attach([NotNull] TEntity entity)
        {
            return this.Entity.Attach(entity);
        }

        public virtual void AttachRange([NotNull] params TEntity[] entities)
        {
            this.Entity.AttachRange(entities);
        }

        public virtual void AttachRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Entity.AttachRange(entities);
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return this.Entity.Find(keyValues);
        }

        public virtual ValueTask<TEntity> FindAsync(params object[] keyValues)
        {
            return this.Entity.FindAsync(keyValues);
        }

        public virtual EntityEntry<TEntity> Remove([NotNull] TEntity entity)
        {
            return this.Entity.Remove(entity);
        }

        public virtual void RemoveRange([NotNull] params TEntity[] entities)
        {
            this.Entity.RemoveRange(entities);
        }

        public virtual void RemoveRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Entity.RemoveRange(entities);
        }

        public virtual EntityEntry<TEntity> Update([NotNull] TEntity entity)
        {
            return this.Entity.Update(entity);
        }

        public virtual void UpdateRange([NotNull] params TEntity[] entities)
        {
            this.Entity.UpdateRange(entities);
        }

        public virtual void UpdateRange([NotNull] IEnumerable<TEntity> entities)
        {
            this.Entity.UpdateRange(entities);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Entity.Where(predicate);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate)
        {
            return this.Entity.Where(predicate);
        }

        public override void BeginTransaction()
        {
            if (this.Transaction != null)
            {
                throw new TransactionException(ResourceUtils.Translate(Resource.ERROR_RUNNING_TRANSACTION));
            }

            this.Transaction = this.Context.Database.BeginTransaction();
        }

        public override async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction != null)
            {
                throw new TransactionException(ResourceUtils.Translate(Resource.ERROR_RUNNING_TRANSACTION));
            }

            this.Transaction = await this.Context.Database.BeginTransactionAsync(cancellationToken);
        }

        public override void CommitTransaction()
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(ResourceUtils.Translate(Resource.ERROR_NO_RUNNING_TRANSACTION));
            }

            this.Transaction?.Commit();
            this.Transaction.Dispose();
        }

        public override async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(ResourceUtils.Translate(Resource.ERROR_NO_RUNNING_TRANSACTION));
            }

            await this.Transaction?.CommitAsync(cancellationToken);
            await this.Transaction.DisposeAsync();
        }

        public override void RollbackTransaction()
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(ResourceUtils.Translate(Resource.ERROR_NO_RUNNING_TRANSACTION));
            }

            this.Transaction?.Rollback();
            this.Transaction.Dispose();
        }

        public override async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(ResourceUtils.Translate(Resource.ERROR_NO_RUNNING_TRANSACTION));
            }

            await this.Transaction?.RollbackAsync(cancellationToken);
            await this.Transaction.DisposeAsync();
        }

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.        
        /// </summary>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>
        /// <exception cref="DbUpdateException">Foi encontrado um erro ao salvar no banco de dados.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Uma violação de simultaneidade é encontrada ao salvar no banco de dados. Uma violação
        /// simultaniedade ocorre quando um número inesperado de linhas é afetado durante o salvamento.
        /// Isso geralmente ocorre porque os dados no banco de dados foram modificados desde que foram
        /// carregado na memória.</exception>
        public override int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.        
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indica se Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
        /// será chamado depois que as alterações foram enviadas com sucesso para o banco de dados.</param>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>
        /// <exception cref="DbUpdateException">Foi encontrado um erro ao salvar no banco de dados.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Uma violação de simultaneidade é encontrada ao salvar no banco de dados. Uma violação
        /// simultaniedade ocorre quando um número inesperado de linhas é afetado durante o salvamento.
        /// Isso geralmente ocorre porque os dados no banco de dados foram modificados desde que foram
        /// carregado na memória.</exception>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return this.Context.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.
        /// Várias operações ativas na mesma instância de contexto não são suportadas. Usar
        /// 'await' para garantir que quaisquer operações assíncronas tenham sido concluídas antes de chamar
        /// outro método nesse contexto.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>
        /// <exception cref="DbUpdateException">Foi encontrado um erro ao salvar no banco de dados.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Uma violação de simultaneidade é encontrada ao salvar no banco de dados. Uma violação
        /// simultaniedade ocorre quando um número inesperado de linhas é afetado durante o salvamento.
        /// Isso geralmente ocorre porque os dados no banco de dados foram modificados desde que foram
        /// carregado na memória.</exception>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.Context.SaveChangesAsync(cancellationToken);
        }

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
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            return await this.Context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
