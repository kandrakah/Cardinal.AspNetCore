using Cardinal.AspNetCore.Utils;
using Cardinal.AspNetCore.Utils.Services;
using Cardinal.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para ServiceProvider.
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// Extensão para verificar se um serviço está registrado no pool de serviços.
        /// </summary>
        /// <param name="provider">Este provedor de serviços.</param>
        /// <param name="service">Tipo do serviço buscado.</param>
        /// <returns>Verdadeiro caso o serviço exista e falso caso contrário.</returns>
        public static bool Exists(this IServiceProvider provider, Type service)
        {
            try
            {
                var result = provider.GetService(service);
                return result != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extensão para verificar se um serviço está registrado no pool de serviços.
        /// </summary>
        /// <typeparam name="T">Tipo do serviço buscado.</typeparam>
        /// <param name="provider">Este provedor de serviços.</param>
        /// <returns>Verdadeiro caso o serviço exista e falso caso contrário.</returns>
        public static bool Exists<T>(this IServiceProvider provider)
        {
            try
            {
                var result = provider.GetService<T>();
                return result != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extensão que obtém um serviço do pool de serviços.
        /// </summary>
        /// <param name="provider">Este provedor de serviços.</param>
        /// <param name="service">Tipo do serviço buscado.</param>
        /// <returns>Instância do serviço buscado.</returns>
        public static object GetCardinalService(this IServiceProvider provider, Type service)
        {
            var result = provider.GetService(service);
            if (result == null)
            {
                throw new ServiceNotFoundException(service);
            }
            return result;
        }

        /// <summary>
        /// Extensão que obtém um serviço do pool de serviços.
        /// </summary>
        /// <typeparam name="T">Tipo do serviço buscado.</typeparam>
        /// <param name="provider">Este provedor de serviços.</param>
        /// <returns>Instância do serviço buscado.</returns>
        public static T GetCardinalService<T>(this IServiceProvider provider)
        {
            var result = provider.GetService<T>();

            if (result == null)
            {
                throw new ServiceNotFoundException(typeof(T));
            }
            return result;
        }

        /// <summary>
        /// Extensão que obtém o serviço de informações do servidor.
        /// </summary>
        /// <param name="provider">Este provedor de serviços.</param>
        /// <returns>Instância do serviço <see cref="IServerInfoService"/></returns>
        public static IServerInfoService GetServerInfoService(this IServiceProvider provider)
        {
            return provider.GetCardinalService<IServerInfoService>();
        }

        /// <summary>
        /// Extensão que obtém o serviço de log.
        /// </summary>
        /// <typeparam name="T">Tipo da classe invocadora do logger.</typeparam>
        /// <param name="provider">Este provedor de serviços.</param>
        /// <returns>Instância do serviço <see cref="ILogger"/> referente à classe solicitante.</returns>
        public static ILogger<T> GetLoggerService<T>(this IServiceProvider provider) where T : class
        {
            if (provider.Exists<ILogger<T>>())
            {
                return provider.GetCardinalService<ILogger<T>>();
            }
            else
            {
                return new NullLogger<T>();
            }
        }
    }
}
