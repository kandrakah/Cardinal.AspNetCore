using Cardinal.AspNetCore.Utils;
using Microsoft.Extensions.Logging;

namespace Cardinal.Extensions
{
    /// <summary>
    /// <see cref="ILogger"/> extension methods.
    /// </summary>
    public static partial class LoggerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="obj"></param>
        public static void LogCritical(this ILogger logger, object obj)
        {
            logger.LogCritical(LogSerializer.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="obj"></param>
        public static void LogDebug(this ILogger logger, object obj)
        {
            logger.LogDebug(LogSerializer.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="obj"></param>
        public static void LogError(this ILogger logger, object obj)
        {
            logger.LogError(LogSerializer.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="obj"></param>
        public static void LogInformation(this ILogger logger, object obj)
        {
            logger.LogInformation(LogSerializer.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="obj"></param>
        public static void LogTrace(this ILogger logger, object obj)
        {
            logger.LogTrace(LogSerializer.Serialize(obj));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="obj"></param>
        public static void LogWarning(this ILogger logger, object obj)
        {
            logger.LogWarning(LogSerializer.Serialize(obj));
        }
    }
}
