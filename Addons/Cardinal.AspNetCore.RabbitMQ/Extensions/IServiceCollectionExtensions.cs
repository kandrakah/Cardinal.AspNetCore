using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Core.DependencyInjection;

namespace Cardinal.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMQService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRabbitMqClient(configuration);
            return services;
        }
    }
}
