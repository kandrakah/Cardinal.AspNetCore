using Cardinal.AspNetCore.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cardinal.AspNetCore.Extensions
{
    public static class ServiceProviderExtensions
    {
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

        public static object GetCardinalService(this IServiceProvider provider, Type service)
        {
            var result = provider.GetService(service);
            if (result == null)
            {
                throw new ServiceNotFoundException(service);
            }
            return result;
        }

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
