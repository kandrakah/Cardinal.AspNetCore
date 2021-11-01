using Microsoft.Extensions.Logging;
using System;

namespace Cardinal.AspNetCore.Utils
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCategoryName"></typeparam>
    public class NullLogger<TCategoryName> : NullLogger, ILogger<TCategoryName>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ILogger<TCategoryName> Create()
        {
            return new NullLogger<TCategoryName>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NullLogger : ILogger, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel"></param>
        /// <param name="eventId"></param>
        /// <param name="state"></param>
        /// <param name="exception"></param>
        /// <param name="formatter"></param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            ConsoleColor originalColor = Console.ForegroundColor;
            Console.WriteLine($"[{eventId.Id,2}: {logLevel,-12}]");

            Console.ForegroundColor = originalColor;
            Console.WriteLine($"     [CONSOLE] - {formatter(state, exception)}");
        }
    }
}
