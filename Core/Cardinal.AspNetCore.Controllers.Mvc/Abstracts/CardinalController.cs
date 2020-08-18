using Cardinal.AspNetCore.Interfaces;
using Cardinal.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
namespace Cardinal.AspNetCore.Controllers.Mvc
{
    /// <summary>
    /// Classe base para todos os controllers do sistema.
    /// </summary>
    public class CardinalController : Controller, ICardinalController
    {
        /// <summary>
        /// Instância do serviço de logs.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Instância do provedor de serviços. Veja <see cref="IServiceProvider"/> para mais detalhes.
        /// </summary>
        protected IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="loggerFactory">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public CardinalController(ILoggerFactory loggerFactory, IServiceProvider provider)
        {
            this.Logger = loggerFactory.CreateLogger(this.GetType().Name);
            this.ServiceProvider = provider;
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="logger">Instância do serviço de logs.</param>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public CardinalController(ILogger logger, IServiceProvider provider)
        {
            this.Logger = logger;
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
    }
}
