using Cardinal.AspNetCore.HealthChecks.Core.Exceptions;
using Cardinal.AspNetCore.HealthChecks.SqlServer.Localization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Cardinal.Extensions;

namespace Cardinal.AspNetCore.HealthChecks.SqlServer
{
    public class SqlServerHealthCheck : IHealthCheck
    {
        private ILogger Logger { get; }

        private string ConnectionString { get; }

        private string Query { get; }

        public SqlServerHealthCheck(IServiceProvider sp, string connectionString, string query)
        {            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new HealthCheckException(Resource.ERROR_CONNECTION_STRING_NULL);
            }

            if (string.IsNullOrEmpty(query))
            {
                throw new HealthCheckException(Resource.ERROR_QUERY_NULL);
            }

            this.Logger = sp.GetLoggerService<SqlServerHealthCheck>();
            this.ConnectionString = connectionString;
            this.Query = query;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    await connection.OpenAsync(cancellationToken);

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = this.Query;
                        await command.ExecuteScalarAsync(cancellationToken);
                    }
                    Logger.LogInformation($"SqlServer: {HealthStatus.Healthy}");
                    return HealthCheckResult.Healthy();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "SqlServer: {status} - {msg}", context.Registration.FailureStatus, ex.Message);
                return new HealthCheckResult(context.Registration.FailureStatus, exception: ex);
            }
        }
    }
}
