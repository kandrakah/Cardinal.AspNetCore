using Cardinal.AspNetCore.Exceptions;
using Cardinal.AspNetCore.HealthChecks.SqlServer.Localization;
using Cardinal.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;

namespace Cardinal.AspNetCore.HealthChecks.SqlServer.Extensions
{
    public static class IHealthChecksBuilderExtensions
    {
        private const string QUERY = "SELECT 1;";
        private const string NAME = "SqlServer";

        public static IHealthChecksBuilder AddSqlServer(this IHealthChecksBuilder builder, IConfiguration configuration, string section = HealthCheckConstants.DEFAULT_CONFIG_SECTION)
        {
            var config = configuration.GetConfigurations<SqlServerHealthCheckConfig>(section);
            builder.AddSqlServer(config);
            return builder;
        }

        public static IHealthChecksBuilder AddSqlServer(this IHealthChecksBuilder builder, SqlServerHealthCheckConfig configuration)
        {
            builder.AddSqlServer(_ => configuration.ConnectionString, configuration.Query, configuration.Name, configuration.FailureStatus, configuration.Tags, configuration.Timeout);
            return builder;
        }

        public static IHealthChecksBuilder AddSqlServer(this IHealthChecksBuilder builder, string connectionString, string query = default, string name = default, HealthStatus? failureStatus = default, IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            return builder.AddSqlServer(_ => connectionString, query, name, failureStatus, tags, timeout);
        }

        public static IHealthChecksBuilder AddSqlServer(this IHealthChecksBuilder builder, Func<IServiceProvider, string> connectionStringFactory, string query = default, string name = default, HealthStatus? failureStatus = default, IEnumerable<string> tags = default, TimeSpan? timeout = default)
        {
            if (connectionStringFactory == null)
            {
                throw new HealthCheckException(Resource.ERROR_CONNECTION_STRING_FACTORY_NULL);
            }

            name = string.IsNullOrEmpty(name) ? NAME : name;
            query = string.IsNullOrEmpty(query) ? QUERY : query;

            var registration = new HealthCheckRegistration(name, sp => new SqlServerHealthCheck(sp, connectionStringFactory(sp), query), failureStatus, tags, timeout);
            return builder.Add(registration);
        }
    }
}
