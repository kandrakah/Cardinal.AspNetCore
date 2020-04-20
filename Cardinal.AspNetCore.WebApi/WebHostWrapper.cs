using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cardinal.AspNetCore.Extensions;
using Microsoft.Extensions.Logging;
using Cardinal.AspNetCore.Utils;
using Cardinal.AspNetCore.WebApi.Localization;

namespace Cardinal.AspNetCore.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class WebHostWrapper
    {
        private static CancellationTokenSource CancellationTokenSource = new CancellationTokenSource();

        private static ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <param name="configureServices"></param>
        public static void Start<TStartup>(string[] args, Action<IServiceCollection> configureServices = null) where TStartup : class
        {
            string contentRoot = Environment.GetEnvironmentVariable("ASPNETCORE_CONTENTROOT");

            if (!string.IsNullOrWhiteSpace(contentRoot))
            {
                var attr = File.GetAttributes(contentRoot);

                if ((attr & FileAttributes.Directory) != FileAttributes.Directory)
                {
                    throw new ArgumentException($"Given Content root \"{contentRoot}\"is not a valid directory");
                }
            }
            else
            {
                contentRoot = Directory.GetCurrentDirectory();
            }

            Start<TStartup>(args, contentRoot, configureServices);
        }

        /// <summary>
        /// Use this if you start identitybase from custom project 
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <param name="basePath"></param>
        /// <param name="configureServices"></param>
        public static void Start<TStartup>(string[] args, string basePath, Action<IServiceCollection> configureServices = null) where TStartup : class
        {
            try
            {
                Console.Title = $"{CardinalConstants.ApplicationName} - {GetEnvironment()}";
                var config = LoadConfig<TStartup>(args, basePath);
                var configHost = config.GetSection("Host");
                var urls = configHost.GetValue<string[]>("Urls") ?? new string[] { "http://localhost:5000", "https://localhost:5001" };
                var hostBuilder = new WebHostBuilder()
                    .UseKestrel()
                    .UseUrls(urls)
                    .SuppressStatusMessages(true)
                    .UseContentRoot(basePath)
                    .UseConfiguration(config)
                    .ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.AddSerilog(config);
                    })
                    .UseStartup<TStartup>();

                if (configureServices != null)
                {
                    hostBuilder = hostBuilder.ConfigureServices(configureServices);
                }

                if (configHost.GetValue<bool>("UseIISIntegration"))
                {
                    hostBuilder = hostBuilder.UseIISIntegration();
                }

                var host = hostBuilder.Build();
                Logger = host.Services.GetService<ILogger<BaseProgram>>();
                Logger.LogInformation(ResourceUtils.Translate(Resource.INITIALIZATION_COMPLETE));
                foreach (var url in urls)
                {
                    Logger.LogInformation(ResourceUtils.Translate(Resource.LISTENING_URL, ResourceUtils.Set("LISTENING_URL", url)));
                }
                var task = host.RunAsync(CancellationTokenSource.Token);
                task.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Shutdown();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static void Restart()
        {
            Shutdown(2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exitCode"></param>
        public static void Shutdown(int exitCode = 0)
        {
            CancellationTokenSource.Cancel();
            Environment.ExitCode = exitCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <param name="basePath"></param>
        /// <returns></returns>
        private static IConfigurationRoot LoadConfig<TStartup>(string[] args, string basePath) where TStartup : class
        {
            var environment = GetEnvironment();

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{environment}.json", true, true);

            //if (IsDevelopment())
            //{
            //    configBuilder.AddUserSecrets<TStartup>();
            //}

            configBuilder.AddEnvironmentVariables();

            if (args != null)
            {
                configBuilder.AddCommandLine(args);
            }

            return configBuilder.Build();
        }

        /// <summary>
        /// Método que recupera o ambiente atual.
        /// </summary>
        /// <returns>Ambiente atual da aplicação.</returns>
        public static string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        }

        /// <summary>
        /// Método que diz se o ambiente atual é o de desenvolvimento.
        /// </summary>
        /// <returns>Verdadeiro caso o ambiente atual seja o de desenvolvimento e falso caso contrário.</returns>
        public static bool IsDevelopment()
        {
            return "Development".Equals(GetEnvironment(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Método que diz se o ambiente atual é o de produção.
        /// </summary>
        /// <returns>Verdadeiro caso o ambiente atual seja o de produção e falso caso contrário.</returns>
        public static bool IsProduction()
        {
            return "Production".Equals(GetEnvironment(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
