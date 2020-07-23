using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Collections.Generic;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="ILoggingBuilder"/>.
    /// </summary>
    public static class LoggingBuilderExtensions
    {
        /// <summary>
        /// Extensão para adição do Serilog ao serviço.
        /// </summary>
        /// <param name="loggingBuilder">Instância de <see cref="ILoggingBuilder"/></param>
        /// <param name="config">Instância de <see cref="IConfiguration"/></param>
        /// <param name="sectionName">Nome da sessão de configurações</param>
        /// <returns>Instância de <see cref="ILoggingBuilder"/> informado via parâmetro <paramref name="loggingBuilder"/></returns>
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
