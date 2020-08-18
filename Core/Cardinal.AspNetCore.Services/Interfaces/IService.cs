using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Services
{
    /// <summary>
    /// Interface base para todos os serviços da aplicação.
    /// </summary>
    public interface IService
    {
        
    }

    /// <summary>
    /// Interface base para todos os serviços da aplicação.
    /// </summary>
    /// <typeparam name="TEntity">Entidade gerenciada pelo serviço.</typeparam>
    public interface IService<TEntity> : IService where TEntity : Entity
    {
        /// <summary>
        /// Método que faz a adição de uma nova entidade à base de dados.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Método que faz a adição assíncrona de uma nova entidade à base de dados.
        /// </summary>
        /// <param name="entity">Entidade à ser adicionada.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Método que faz a localização de uma entidade na base de dados.
        /// </summary>
        /// <param name="keyValues">Valores usados na busca pela entidade.</param>
        /// <returns>Entidade associada aos valores informados ou null caso nenhuma entidade tenha sido localizada.</returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Método que faz a localização assíncrona de uma entidade na base de dados.
        /// </summary>
        /// <param name="keyValues">Valores usados na busca pela entidade.</param>
        /// <returns>Entidade associada aos valores informados ou null caso nenhuma entidade tenha sido localizada.</returns>
        Task<TEntity> FindAsync(params object[] keyValues);

        /// <summary>
        /// Método que traz todas as entidades atualmente registradas na base de dados.
        /// </summary>
        /// <returns>Coleção contendo todas.</returns>
        IEnumerable<TEntity> All();

        /// <summary>
        /// Método que traz de forma assíncrona todas as entidades atualmente registradas na base de dados.
        /// </summary>
        /// <returns>Coleção contendo todas.</returns>
        Task<IEnumerable<TEntity>> AllAsync();

        /// <summary>
        /// Método que atualiza apenas propriedades alteradas de uma entidade na base de dados.
        /// </summary>
        /// <param name="entity">Entidade à ser atualizada.</param>
        /// <returns>Informações de rastreamento da entidade à ser adicionada.</returns>
        void Attach(TEntity entity);

        /// <summary>
        /// Método que atualiza apenas propriedades alteradas das entidades na base de dados.
        /// </summary>
        /// <param name="entities">Entidades à serem atualizadas.</param>
        void AttachRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Método que faz a atualização de uma entidade na base de dados.
        /// </summary>
        /// <param name="entity">Entidade à ser atualizada.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Método que faz a atualização de várias entidades na base de dados.
        /// </summary>
        /// <param name="entities">Entidades à serem atualizadas.</param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Método que faz a remoção de uma entidade da base de dados.
        /// </summary>
        /// <param name="id">Id da entidade à ser removida.</param>
        void Remove(Guid id);

        /// <summary>
        /// Método que faz a remoção de várias entidades da base de dados.
        /// </summary>
        /// <param name="ids">Ids das entidades à serem removidas.</param>
        void RemoveRange(IEnumerable<Guid> ids);

        /// <summary>
        /// Método que faz a remoção de uma entidade da base de dados.
        /// </summary>
        /// <param name="entity">Entidade à ser removida.</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Método que faz a remoção de várias entidades da base de dados.
        /// </summary>
        /// <param name="entities">Entidades à serem removidas.</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Método que faz a busca de entidades com base nos termos informados.
        /// </summary>
        /// <param name="terms">Termos de busca.</param>
        /// <returns>Coleção de entidades encontradas na busca.</returns>
        IEnumerable<TEntity> Search(params string[] terms);

        /// <summary>
        /// Método que faz o filtro das entidades com base nos parâmetros informados.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Coleção de entidades encontradas na busca.</returns>
        IEnumerable<TEntity> Where(Func<TEntity, int, bool> predicate);

        /// <summary>
        /// Método que faz o filtro das entidades com base nos parâmetros informados.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Coleção de entidades encontradas na busca.</returns>
        IEnumerable<TEntity> Where(Func<TEntity, bool> predicate);

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        /// <param name="solationLevel">Nível de isolamento da transação.</param>
        void BeginTransaction(IsolationLevel solationLevel);

        /// <summary>
        /// Método que inicia uma nova transação.
        /// </summary>
        Task BeginTransactionAsync();

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
        /// Método que efetua o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <returns>Número de alterações na base de dados.</returns>
        int SaveChanges();

        /// <summary>
        /// Método que efetua de forma assíncrona o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <returns>Número de alterações na base de dados.</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Método que efetua o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns>Número de alterações na base de dados.</returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Método que efetua de forma assíncrona o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns>Número de alterações na base de dados.</returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess);
    }
}
