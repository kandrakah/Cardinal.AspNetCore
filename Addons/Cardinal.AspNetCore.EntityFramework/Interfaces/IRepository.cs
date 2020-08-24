using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.EntityFramework.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TContext, TEntity> : IRepository<TContext> where TContext : DbContext where TEntity : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> All();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AllAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> AsQueryable();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    }

    /// <summary>
    /// Interface base para repositórios.
    /// </summary>
    /// <typeparam name="TContext">Contexto utilizado no repositório.</typeparam>
    public interface IRepository<TContext> : IRepository where TContext : DbContext
    {
        /// <summary>
        /// Método para adicionar uma entidade ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entity">Entidade à ser adicionada.</param>
        EntityEntry<T> Add<T>([NotNull] T entity) where T : Entity;

        /// <summary>
        /// Método para adicionar uma entidade ao contexto de forma assíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entity">Entidade à ser adicionada.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>Uma tarefa que representa a operação de adição assíncrona. O resultado da tarefa contém
        /// Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry para a entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a entidade.</returns>
        ValueTask<EntityEntry<T>> AddAsync<T>([NotNull] T entity, CancellationToken cancellationToken = default) where T : Entity;

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        void AddRange<T>([NotNull] params T[] entities) where T : Entity;

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        void AddRange<T>([NotNull] IEnumerable<T> entities) where T : Entity;

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        Task AddRangeAsync<T>([NotNull] params T[] entities) where T : Entity;

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        Task AddRangeAsync<T>([NotNull] IEnumerable<T> entities, CancellationToken cancellationToken = default) where T : Entity;

        /// <summary>
        /// Começa a rastrear a entidade especificada e as entradas acessíveis a partir da entidade especificada
        /// usando o estado Microsoft.EntityFrameworkCore.EntityState.Unchanged por padrão.
        /// </summary>
        /// <typeparam name="T">Tipo da entidade à ser adicionada.</typeparam>
        /// <param name="entity">Entidade à ser rastreada.</param>
        /// <returns><see cref="EntityEntry"/>para a entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a
        /// entidade.</returns>
        EntityEntry<T> Attach<T>([NotNull] T entity) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void AttachRange<T>([NotNull] params T[] entities) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void AttachRange<T>([NotNull] IEnumerable<T> entities) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<T> Entry<T>([NotNull] T entity) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        T Find<T>(params object[] keyValues) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        ValueTask<T> FindAsync<T>(params object[] keyValues) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<T> Remove<T>([NotNull] T entity) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void RemoveRange<T>([NotNull] params T[] entities) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void RemoveRange<T>([NotNull] IEnumerable<T> entities) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<T> Update<T>([NotNull] T entity) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void UpdateRange<T>([NotNull] params T[] entities) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        void UpdateRange<T>([NotNull] IEnumerable<T> entities) where T : Entity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
                
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

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
        int SaveChanges(bool acceptAllChangesOnSuccess);
    }
}
