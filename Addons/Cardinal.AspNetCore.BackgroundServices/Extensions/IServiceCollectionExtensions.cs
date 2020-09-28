using Cardinal.AspNetCore.BackgroundServices;
using Cardinal.AspNetCore.BackgroundServices.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cardinal.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBackgroundService<T>(this IServiceCollection services) where T : AbstractBackgroundService
        {
            services.AddBackgroundService<T>(options =>
            {
                options.TimeZoneInfo = TimeZoneInfo.Local;
                options.CronExpression = @"* * * * *";
            });
            return services;
        }

        public static IServiceCollection AddBackgroundService<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : AbstractBackgroundService
        {
            if (options == null)
            {
                throw new ArgumentNullException(Resource.ERROR_CONFIGURATION_NULL);
            }

            var config = new ScheduleConfig<T>();
            options.Invoke(config);

            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(Resource.ERROR_CRON_NULL_EMPTY);
            }

            try
            {
                CronExpression.Parse(config.CronExpression);
            }
            catch
            {
                throw new InvalidCastException(Resource.ERROR_CRON_INVALID);
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}
