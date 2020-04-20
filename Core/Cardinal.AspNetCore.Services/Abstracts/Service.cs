using System;

namespace Cardinal.AspNetCore.Services
{
    /// <summary>
    /// Classe base para todos os serviços da aplicação.
    /// </summary>
    public abstract class Service : IDisposable
    {
        /// <summary>
        /// Instância do provedor de serviços da aplicação.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de serviços da aplicação.</param>
        public Service(IServiceProvider provider)
        {
            this.ServiceProvider = provider;
        }
        
        /// <summary>
        /// Método para liberação de recursos usados pelo serviço.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Método para liberação de recursos usados pelo serviço.
        /// </summary>
        /// <param name="disposing">Define se propriedades filhas também devem ser liberadas.</param>
        protected virtual void Dispose(bool disposing)
        {

        }
    }
}
