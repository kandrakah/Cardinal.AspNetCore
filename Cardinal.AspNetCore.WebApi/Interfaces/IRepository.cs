using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.WebApi.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Método para adicionar uma entidade ao contexto.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        /// <returns></returns>
        EntityEntry<TEntity> Add([NotNull] TEntity entity);

        /// <summary>
        /// Método para adicionar uma entidade ao contexto de forma assíncrona.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        /// <returns>Uma tarefa que representa a operação de adição assíncrona. O resultado da tarefa contém
        /// Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry para a entidade.
        /// A entrada fornece acesso para alterar informações e operações de rastreamento para a entidade.</returns>
        ValueTask<EntityEntry<TEntity>> AddAsync([NotNull] TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        void AddRange([NotNull] params TEntity[] entities);

        /// <summary>
        /// Método para adicionar várias entidades ao contexto.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        void AddRange([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        Task AddRangeAsync([NotNull] params TEntity[] entities);

        /// <summary>
        /// Método para adicionar várias entidades ao contexto de forma assíncrona.
        /// </summary>
        /// <param name="entities">Entidades à serem adicionadas.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> para observar enquanto aguarda a conclusão da tarefa.</param>
        Task AddRangeAsync([NotNull] IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Attach(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void AttachRange([NotNull] params TEntity[] entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void AttachRange([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        ValueTask<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Remove([NotNull] TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange([NotNull] params TEntity[] entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Update([NotNull] TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange([NotNull] params TEntity[] entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange([NotNull] IEnumerable<TEntity> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<TEntity> Where(Expression<Func<TEntity, int, bool>> predicate);

        /// <summary>
        /// 
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Salva todas as alterações feitas nesse repositório no banco de dados.        
        /// </summary>
        /// <returns>O número de entradas de estado gravadas no banco de dados.</returns>
        /// <exception cref="DbUpdateException">Foi encontrado um erro ao salvar no banco de dados.</exception>
        /// <exception cref="DbUpdateConcurrencyException">Uma violação de simultaneidade é encontrada ao salvar no banco de dados. Uma violação
        /// simultaniedade ocorre quando um número inesperado de linhas é afetado durante o salvamento.
        /// Isso geralmente ocorre porque os dados no banco de dados foram modificados desde que foram
        /// carregado na memória.</exception>
        int SaveChanges();

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
