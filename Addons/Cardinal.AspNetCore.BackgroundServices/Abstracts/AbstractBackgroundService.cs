using Quartz;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore
{
    public abstract class AbstractBackgroundService : BackgroundService, IHostedService, IDisposable
    {
        /// <summary>
     /// Instância do serviço de logs.
     /// </summary>
        protected readonly ILogger _logger;

        /// <summary>
        /// Instância do provedor de serviços.
        /// </summary>
        protected readonly IServiceProvider _provider;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de serviços.</param>
        public AbstractBackgroundService(IServiceProvider provider)
        {
            this._provider = provider;
            var factory = this._provider.GetCardinalService<ILoggerFactory>();
            this._logger = factory.CreateLogger(this.GetType());
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <typeparam name="T">Interface do serviço solicitada.</typeparam>
        /// <returns>Instância do serviço solicitado.</returns>
        protected internal T GetService<T>()
        {
            var result = this._provider.GetCardinalService<T>();
            return result;
        }

        /// <summary>
        /// Método que obtém a instância de um serviço.
        /// </summary>
        /// <param name="service">Tipo do serviço solicitado.</param>
        /// <returns>Instância do serviço solicitado.</returns>
        protected internal object GetService(Type service)
        {
            var result = this._provider.GetCardinalService(service);
            return result;
        }
    }
}
