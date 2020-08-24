using Cardinal.AspNetCore.EntityFramework.Localization;
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

namespace Cardinal.AspNetCore.EntityFramework.Repositories
{
    /// <summary>
    /// Classe base para repositórios.
    /// </summary>
    /// <typeparam name="TContext">Contexto utilizado no repositório.</typeparam>
    /// <typeparam name="TEntity">Entidade gerenciada pelo repositório.</typeparam>
    public abstract class Repository<TContext, TEntity> : Repository<TContext> where TContext : DbContext where TEntity : Entity
    {
        /// <summary>
        /// DbSet da entidade gerenciada.
        /// </summary>
        protected DbSet<TEntity> Entities { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Contexto utilizado no repositório.</param>
        public Repository(TContext context) : base(context)
        {
            this.Entities = context.Set<TEntity>();
        }

        /// <summary>
        /// Método que tráz todos os registros da entidade.
        /// </summary>
        /// <returns>Enumeração de entidades.</returns>
        public IEnumerable<TEntity> All()
        {
            return this.Entities.ToList();
        }

        /// <summary>
        /// Método que traz todos os registros da entidade.
        /// </summary>
        /// <returns>Enumeração de entidades.</returns>
        public async Task<IEnumerable<TEntity>> AllAsync()
        {
            return await this.Entities.ToListAsync();
        }

        /// <summary>
        /// Método que traz uma entidade relativa ao Id informado.
        /// </summary>
        /// <param name="id">Id da entidade desejada.</param>
        /// <returns>Entidade referente ao Id informado ou null caso nenhuma seja localizada.</returns>
        public TEntity Get(Guid id)
        {
            return this.Entities.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// Método que traz a lista buscável da entidade.
        /// </summary>
        /// <returns>Lista buscável da entidade.</returns>
        public IQueryable<TEntity> AsQueryable()
        {
            return this.Entities.AsQueryable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Entities.Where(predicate);
        }
    }

    /// <summary>
    /// Classe base para repositórios.
    /// </summary>
    /// <typeparam name="TContext">Contexto utilizado no repositório.</typeparam>
    public abstract class Repository<TContext> : Repository where TContext : DbContext
    {
        /// <summary>
        /// Contexto do repositório.
        /// </summary>
        protected TContext Context { get; set; }

        /// <summary>
        /// Instância da transação.
        /// </summary>
        protected IDbContextTransaction Transaction { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto conectado à base de dados.</param>
        public Repository(TContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Método para adicionar uma entidade ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entity">Entidade à ser adicionada.</param>
        public virtual EntityEntry<T> Add<T>([NotNull] T entity) where T : Entity
        {
            return this.Context.Add(entity);
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
        public virtual ValueTask<EntityEntry<T>> AddAsync<T>([NotNull] T entity, CancellationToken cancellationToken = default) where T : Entity
        {
            return this.Context.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual void AddRange<T>([NotNull] params T[] entities) where T : Entity
        {
            this.Context.AddRange(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual void AddRange<T>([NotNull] IEnumerable<T> entities) where T : Entity
        {
            this.Context.AddRange(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        public virtual async Task AddRangeAsync<T>([NotNull] params T[] entities) where T : Entity
        {
            await this.Context.AddRangeAsync(entities);
        }

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        public virtual async Task AddRangeAsync<T>([NotNull] IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : Entity
        {
            await this.Context.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// Começa a rastrear a entidade especificada e as entradas acessíveis a partir da entidade especificada
        /// usando o estado Microsoft.EntityFrameworkCore.EntityState.Unchanged por padrão.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entity">Entidade à ser rastreada.</param>
        /// <returns><see cref="EntityEntry"/>para a entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a
        /// entidade.</returns>
        public EntityEntry<T> Attach<T>([NotNull] T entity) where T : Entity
        {
            return this.Context.Attach(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public virtual void AttachRange<T>([NotNull] params T[] entities) where T : Entity
        {
            this.Context.AttachRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public virtual void AttachRange<T>([NotNull] IEnumerable<T> entities) where T : Entity
        {
            this.Context.AttachRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<T> Entry<T>([NotNull] T entity) where T : Entity
        {
            return this.Context.Entry(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual T Find<T>(params object[] keyValues) where T : Entity
        {
            return this.Context.Find<T>(keyValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public virtual ValueTask<T> FindAsync<T>(params object[] keyValues) where T : Entity
        {
            return this.Context.FindAsync<T>(keyValues);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<T> Remove<T>([NotNull] T entity) where T : Entity
        {
            return this.Context.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public virtual void RemoveRange<T>([NotNull] params T[] entities) where T : Entity
        {
            this.Context.RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public virtual void RemoveRange<T>([NotNull] IEnumerable<T> entities) where T : Entity
        {
            this.Context.RemoveRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual EntityEntry<T> Update<T>([NotNull] T entity) where T : Entity
        {
            return this.Context.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public virtual void UpdateRange<T>([NotNull] params T[] entities) where T : Entity
        {
            this.Context.UpdateRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public virtual void UpdateRange<T>([NotNull] IEnumerable<T> entities) where T : Entity
        {
            this.Context.UpdateRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void BeginTransaction()
        {
            if (this.Transaction != null)
            {
                throw new TransactionException(Resource.ERROR_RUNNING_TRANSACTION);
            }

            this.Transaction = this.Context.Database.BeginTransaction();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction != null)
            {
                throw new TransactionException(Resource.ERROR_RUNNING_TRANSACTION);
            }

            this.Transaction = await this.Context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CommitTransaction()
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(Resource.ERROR_NO_RUNNING_TRANSACTION);
            }

            this.Transaction?.Commit();
            this.Transaction.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(Resource.ERROR_NO_RUNNING_TRANSACTION);
            }

            await this.Transaction?.CommitAsync(cancellationToken);
            await this.Transaction.DisposeAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void RollbackTransaction()
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(Resource.ERROR_NO_RUNNING_TRANSACTION);
            }

            this.Transaction?.Rollback();
            this.Transaction.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (this.Transaction == null)
            {
                throw new TransactionException(Resource.ERROR_NO_RUNNING_TRANSACTION);
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
