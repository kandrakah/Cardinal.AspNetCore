using IdentityServer4.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Cardinal.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStore(this IServiceCollection services)
        {
            services.AddScoped<IEventSink, DefaultEventSink>();
            return services;
        }

        public static IServiceCollection AddEventStore<TImplementation>(this IServiceCollection services) where TImplementation : class, IEventSink
        {
            services.AddScoped<IEventSink, TImplementation>();
            return services;
        }
    }
}
