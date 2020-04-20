using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Repositories
{
    public abstract class Repository<TEntity, TContext> where TEntity : Entity where TContext : IContext
    {
        protected TContext Context { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context">Instância do contexto conectado à base de dados.</param>
        protected Repository(TContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Método que efetua o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <returns>Número de alterações na base de dados.</returns>
        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        /// <summary>
        /// Método que efetua de forma assíncrona o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <returns>Número de alterações na base de dados.</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.Context.SaveChangesAsync();
        }

        /// <summary>
        /// Método que efetua o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns>Número de alterações na base de dados.</returns>
        public int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return this.Context.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Método que efetua de forma assíncrona o salvamento das alterações feitas na base de dados.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns>Número de alterações na base de dados.</returns>
        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        {
            return await this.Context.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Método para liberação de recursos usados pelo contexto.
        /// </summary>
        public virtual void Dispose()
        {
            
        }
    }
}
