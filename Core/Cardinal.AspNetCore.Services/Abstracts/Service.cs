using Cardinal.AspNetCore.Extensions;
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
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <typeparam name="T">Interface do serviço solicitada.</typeparam>
        /// <returns>Instância do serviço solicitado.</returns>
        protected T GetService<T>()
        {
            var result = this.ServiceProvider.GetCardinalService<T>();
            return result;
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <param name="service">Tipo do serviço solicitado.</param>
        /// <returns>Instância do serviço solicitado.</returns>
        protected object GetService(Type service)
        {
            var result = this.ServiceProvider.GetCardinalService(service);
            return result;
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
