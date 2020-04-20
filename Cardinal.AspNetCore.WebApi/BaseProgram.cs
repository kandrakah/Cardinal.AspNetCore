using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;

namespace Cardinal.AspNetCore.WebApi
{
    /// <summary>
    /// Classe base para inicialização simplificada do WebAPI.
    /// </summary>
    public abstract class BaseProgram
    {
        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="args">Argumentos de inicialização.</param>
        protected static void Initialize<TStartup>(string[] args) where TStartup : class
        {
            var ver = new CardinalVersion();
            Initialize<TStartup>(ver, "Cardinal Web API Service", args);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="version">Versão do serviço web.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        protected static void Initialize<TStartup>(string version, string[] args) where TStartup : class
        {
            var ver = new CardinalVersion(version);
            Initialize<TStartup>(ver, "Cardinal Web API Service", args);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="version">Versão do serviço web.</param>
        /// <param name="description">Descrição serviço web.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        protected static void Initialize<TStartup>(string version, string description, string[] args) where TStartup : class
        {
            var ver = new CardinalVersion(version);
            Initialize<TStartup>(ver, description, args);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="version">Versão do serviço web. veja <see cref="ICardinalVersion"/>.</param>
        /// <param name="description">Descrição serviço web.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        protected static void Initialize<TStartup>(ICardinalVersion version, string description, string[] args) where TStartup : class
        {
            CardinalConstants.Initialize(version, description);
            var process = Process.GetCurrentProcess();            
            Console.Title = $"{CardinalConstants.BaseName} {CardinalConstants.BaseVersion} - PID: {process.Id}";

            WebHostWrapper.Start<TStartup>(args, (services) =>
            {
                services.AddSingleton<ICardinalVersion, CardinalVersion>();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                return;
            });
        }
    }
}
