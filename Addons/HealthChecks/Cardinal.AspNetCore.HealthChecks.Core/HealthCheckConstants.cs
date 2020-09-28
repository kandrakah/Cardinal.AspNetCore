using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cardinal.AspNetCore.HealthChecks
{
    public static class HealthCheckConstants
    {
        public const string DEFAULT_CONFIG_SECTION = "HealthCheck";

        public const string DEFAULT_CONTENT_TYPE = "application/json";

        public static byte[] emptyResponse = new byte[] { (byte)'{', (byte)'}' };

        public static Lazy<JsonSerializerOptions> options = new Lazy<JsonSerializerOptions>(() => CreateJsonOptions());

        public static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = true,
            };

            options.Converters.Add(new JsonStringEnumConverter());
            options.Converters.Add(new TimeSpanConverter());

            return options;
        }
    }
}
