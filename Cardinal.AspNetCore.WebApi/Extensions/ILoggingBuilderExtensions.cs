using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cardinal.AspNetCore.WebApi.Extensions
{
    ///// <summary>
    ///// 
    ///// </summary>
    //public static partial class ILoggingBuilderExtensions
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="loggingBuilder"></param>
    //    /// <param name="config"></param>
    //    /// <param name="sectionName"></param>
    //    /// <returns></returns>
    //    public static ILoggingBuilder AddSerilog(this ILoggingBuilder loggingBuilder, IConfiguration config, string sectionName = ConfigurationLoggerConfigurationExtensions.DefaultSectionName)
    //    {
    //        var loggerConfig = new LoggerConfiguration();

    //        if (config.GetSection(sectionName).Exists())
    //        {
    //            loggerConfig.ReadFrom.Configuration(config, sectionName);
    //        }
    //        else
    //        {
    //            var settings = new Dictionary<string, string>()
    //            {
    //                { "write-to:Console", ""},
    //                { "using:File", "Serilog.Sinks.Console" },
    //            };
    //            loggerConfig.ReadFrom.KeyValuePairs(settings);
    //        }

    //        var logger = loggerConfig.CreateLogger();
    //        return loggingBuilder.AddSerilog(logger, true);
    //    }
    //}
}
