﻿using Cardinal.AspNetCore.Utils.Localization;
using Cardinal.AspNetCore.Utils.Services;
using Cardinal.Extensions;
using Cardinal.Utils;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Cardinal.AspNetCore.Utils
{

    /// <summary>
    /// Classe base para inicialização simplificada do WebAPI.
    /// </summary>
    public abstract class BaseProgram
    {
        private readonly static CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        /// <summary>
        /// Instância do logger da aplicação.
        /// </summary>
        protected static ILogger Logger { get; set; }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="args">Argumentos de inicialização.</param>
        protected static void Initialize<TStartup>(string[] args) where TStartup : class
        {
            var ver = CardinalVersion.Set(1, 0);
            Initialize<TStartup>(ver, "API Service", args);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="version">Versão do serviço web.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        protected static void Initialize<TStartup>(string version, string[] args) where TStartup : class
        {
            var ver = CardinalVersion.Parse(version);
            Initialize<TStartup>(ver, "API Service", args);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="version">Versão do serviço web.</param>
        /// <param name="description">Descrição serviço web.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        protected static void Initialize<TStartup>(string version, string description, string[] args, params string[] additionalConfigurationFiles) where TStartup : class
        {
            var ver = CardinalVersion.Parse(version);
            Initialize<TStartup>(ver, description, args, additionalConfigurationFiles);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="version">Versão do serviço web. veja <see cref="CardinalVersion"/>.</param>
        /// <param name="description">Descrição serviço web.</param>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        protected static void Initialize<TStartup>(CardinalVersion version, string description, string[] args, params string[] additionalConfigurationFiles) where TStartup : class
        {
            Constants.Initialize(version, description);
            var process = Process.GetCurrentProcess();
            Console.Title = $"{Constants.BaseName} {Constants.BaseVersion} - PID: {process.Id}";

            Start<TStartup>(args, (services) =>
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddSingleton<IServerInfoService, ServerInfoService>();
                return;
            }, additionalConfigurationFiles);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="configureServices">Serviços configurados à serem adicionados ao serviço.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        private static void Start<TStartup>(string[] args, Action<IServiceCollection> configureServices = null, params string[] additionalConfigurationFiles) where TStartup : class
        {
            string contentRoot = Environment.GetEnvironmentVariable("ASPNETCORE_CONTENTROOT");

            if (!string.IsNullOrWhiteSpace(contentRoot) && !string.IsNullOrEmpty(contentRoot))
            {
                var attr = File.GetAttributes(contentRoot);

                if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    throw new ArgumentException(Resource.ERROR_CONTENT_ROOT_INVALID.SetParameters("CONTENT_ROOT", contentRoot));
                }
            }
            else
            {
                contentRoot = AppDomain.CurrentDomain.BaseDirectory;
            }

            Start<TStartup>(args, contentRoot, configureServices, additionalConfigurationFiles);
        }

        /// <summary>
        /// Método de inicialização do serviço web.
        /// </summary>
        /// <typeparam name="TStartup">Classe de inicialização do serviço.</typeparam>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="basePath">Diretório base da aplicação.</param>
        /// <param name="configureServices">Serviços configurados à serem adicionados ao serviço.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        private static void Start<TStartup>(string[] args, string basePath, Action<IServiceCollection> configureServices = null, params string[] additionalConfigurationFiles) where TStartup : class
        {
            try
            {
                Logger = new NullLogger<BaseProgram>();
                Console.Title = $"{Constants.ApplicationName} - {Constants.Environment}";

                var config = LoadConfiguration(args, basePath, additionalConfigurationFiles);
                var configHost = config.GetConfigurations<HostConfigurations>("Host");
                var urls = configHost.Hosts != null ? configHost.Hosts.ToArray() : new string[] { };

                var builder = WebHost.CreateDefaultBuilder(args);

                if (configHost.UseKestrel)
                {
                    builder.UseKestrel(o =>
                    {
                        if (configHost.Certificate != null && configHost.Certificate.UseCertificate)
                        {
                            if (string.IsNullOrEmpty(configHost.Certificate.Path) || !File.Exists(configHost.Certificate.Path))
                            {
                                throw new FileNotFoundException(Resource.ERROR_CERTIFICATE_NOT_FOUND);
                            }

                            var cert = new X509Certificate2(configHost.Certificate.Path, configHost.Certificate.Password);

                            o.ConfigureHttpsDefaults(x =>
                            {
                                x.ServerCertificate = cert;
                            });
                        }
                    });
                }

                if (urls.Any())
                {
                    builder.UseUrls(urls);
                }

                builder.SuppressStatusMessages(true)
                 .UseContentRoot(basePath)
                 .UseConfiguration(config)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.AddSerilog(config);
                 })
                 .UseStartup<TStartup>();

                if (configureServices != null)
                {
                    builder = builder.ConfigureServices(configureServices);
                }

                if (configHost.UseIISIntegration)
                {
                    builder = builder.UseIISIntegration();
                }

                var host = builder.Build();
                try
                {
                    Logger = host.Services.GetCardinalService<ILogger<BaseProgram>>();
                }
                catch
                {
                    Logger = new NullLogger<BaseProgram>();
                }

                Logger.LogInformation(Resource.INITIALIZE_BASEPATH, basePath);
                if (configHost.UseIISIntegration)
                {
                    Logger.LogInformation(Resource.INITIALIZE_IIS_INTEGRATION);
                }
                Logger.LogInformation(Resource.INITIALIZATION_COMPLETE);
                foreach (var url in urls)
                {
                    Logger.LogInformation(Resource.LISTENING_URL, url);
                }
                var task = host.RunAsync(CancellationTokenSource.Token);
                task.Wait();
            }
            catch (Exception ex)
            {
                if (Logger == null)
                {
                    Logger = new NullLogger<BaseProgram>();
                }
                Logger.LogCritical(ex, ex.Message);
                Shutdown();
            }
        }

        /// <summary>
        /// Método para o encerramento do serviço.
        /// </summary>
        /// <param name="exitCode">Código de saída da aplicação.</param>
        public static void Shutdown(int exitCode = 0)
        {
            if (Logger == null)
            {
                Logger = new NullLogger<BaseProgram>();
            }
            Logger.LogInformation($"Encerrando serviço...");
            CancellationTokenSource.Cancel();
            Environment.ExitCode = exitCode;
            Logger.LogInformation($"Serviço encerrado com código {exitCode}");
            Environment.Exit(exitCode);
        }

        /// <summary>
        /// Método que efetua a leitura das configurações do serviço.
        /// </summary>
        /// <param name="args">Argumentos de inicialização.</param>
        /// <param name="basePath">Diretório base da aplicação.</param>
        /// <param name="additionalConfigurationFiles">Arquivos de configuração adicionais.</param>
        /// <returns>Instância de <see cref="IConfigurationRoot"/> contendo as configurações do serviço.</returns>
        private static IConfigurationRoot LoadConfiguration(string[] args, string basePath, params string[] additionalConfigurationFiles)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                throw new ArgumentNullException(Resource.ERROR_CONTENT_ROOT_INVALID);
            }

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{Constants.Environment}.json", true, true);

            if (additionalConfigurationFiles != null)
            {
                foreach (var configFile in additionalConfigurationFiles)
                {
                    configBuilder.AddJsonFile(configFile, true);
                }
            }

            configBuilder.AddEnvironmentVariables();

            if (args != null)
            {
                configBuilder.AddCommandLine(args);
            }

            return configBuilder.Build();
        }
    }
}
