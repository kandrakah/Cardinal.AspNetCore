using System.Text.Json;
using System.Text.Json.Serialization;

namespace Cardinal.AspNetCore.Serilog
{
    /// <summary>
    /// Helper to JSON serialize object data for logging.
    /// </summary>
    public static class LogSerializer
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            WriteIndented = true,           
            
        };

        /// <summary>
        /// Construtor estático.
        /// </summary>
        static LogSerializer()
        {
            options.Converters.Add(new JsonStringEnumConverter());
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, options);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object Desserialize(string json)
        {
            return JsonSerializer.Deserialize(json, typeof(object), options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T Desserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
