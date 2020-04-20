using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;

namespace Cardinal.AspNetCore.Extensions
{
    public static partial class ILoggingBuilderExtensions
    {
        /// <summary>
        /// Adds Serilog logger
        /// </summary>
        /// <param name="loggingBuilder">Instance of
        /// <see cref="ILoggingBuilder"/></param>
        /// <param name="config">Instance of <see cref="IConfiguration"/>
        /// </param>
        /// <returns>Instance of
        /// <see cref="ILoggingBuilder"/> provided via
        /// <paramref name="loggingBuilder"/> argument</returns>
        public static ILoggingBuilder AddSerilog(this ILoggingBuilder loggingBuilder, IConfiguration config, string sectionName = ConfigurationLoggerConfigurationExtensions.DefaultSectionName)
        {
            var loggerConfig = new LoggerConfiguration();
            if (config.GetSection(sectionName).Exists())
            {
                loggerConfig.ReadFrom.Configuration(config, sectionName);
            }
            else
            {
                var settings = new Dictionary<string, string>()
                {
                    { "write-to:Console", ""},
                    { "using:File", "Serilog.Sinks.Console" },
                };
                loggerConfig.ReadFrom.KeyValuePairs(settings);
            }

            var logger = loggerConfig.CreateLogger();
            return loggingBuilder.AddSerilog(logger, true);
        }
    }
}
