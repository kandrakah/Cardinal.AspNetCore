using Cardinal.AspNetCore.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cardinal.AspNetCore.Extensions
{
    /// <summary>
    /// Classe de extensões para ServiceProvider.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Extensão para verificar se um serviço está registrado no pool de serviços.
        /// </summary>
        /// <param name="provider">Este provedor de serviços.</param>
        /// <param name="service">Tipo do serviço buscado.</param>
        /// <returns>Verdadeiro caso o serviço exista e falso caso contrário.</returns>
        public static bool ServiceExists(this IServiceProvider provider, Type service)
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
        public static bool ServiceExists<T>(this IServiceProvider provider)
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
    }
}
