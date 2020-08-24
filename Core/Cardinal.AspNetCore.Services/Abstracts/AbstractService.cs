using Cardinal.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe base para todos os serviços da aplicação.
    /// </summary>
    public abstract class AbstractService : IDisposable
    {
        /// <summary>
        /// Instância do serviço de logs.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Instância do provedor de serviços da aplicação.
        /// </summary>
        protected IServiceProvider Provider { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de serviços da aplicação.</param>
        public AbstractService(IServiceProvider provider)
        {
            this.Provider = provider;

            var loggerFactory = this.Provider.GetCardinalService<ILoggerFactory>();
            this.Logger = loggerFactory.CreateLogger(this.GetType().Name);
        }
                
        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <typeparam name="T">Interface do serviço solicitada.</typeparam>
        /// <returns>Instância do serviço solicitado.</returns>
        protected T GetService<T>()
        {
            var result = this.Provider.GetCardinalService<T>();
            return result;
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <param name="service">Tipo do serviço solicitado.</param>
        /// <returns>Instância do serviço solicitado.</returns>
        protected object GetService(Type service)
        {
            var result = this.Provider.GetCardinalService(service);
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
