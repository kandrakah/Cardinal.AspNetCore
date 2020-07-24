using Cardinal.Utils;
using System;

namespace Cardinal.AspNetCore.Utils
{
    /// <summary>
    /// Classe estática com constantes de sistema.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Nome base do serviço da aplicação.
        /// </summary>
        public static string BaseName { get; private set; }

        /// <summary>
        /// Versão base do serviço da aplicação.
        /// </summary>
        public static CardinalVersion BaseVersion { get; private set; }

        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public static string ApplicationName { get; private set; }

        /// <summary>
        /// Identificação se as constantes do serviço foram inicializadas.
        /// </summary>
        internal static bool Initialized { get; private set; }

        /// <summary>
        /// Atributo que determina o ambient atual.
        /// </summary>
        /// <returns>Ambiente atual da aplicação.</returns>
        internal static string Environment { get { return System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); } }

        /// <summary>
        /// atributo que indica se o ambiente atual é o de desenvolvimento.
        /// </summary>
        /// <returns>Verdadeiro caso o ambiente atual seja o de desenvolvimento e falso caso contrário.</returns>
        internal static bool IsDevelopment { get { return "Development".Equals(Environment, StringComparison.OrdinalIgnoreCase); } }

        /// <summary>
        /// Método que diz se o ambiente atual é o de produção.
        /// </summary>
        /// <returns>Verdadeiro caso o ambiente atual seja o de produção e falso caso contrário.</returns>
        internal static bool IsProduction { get { return "Production".Equals(Environment, StringComparison.OrdinalIgnoreCase); } }

        /// <summary>
        /// Método de inicialização do serviço da aplicação.
        /// </summary>
        /// <param name="version">Versão base do serviço da aplicação.</param>
        /// <param name="baseName">Nome da aplicação.</param>
        internal static void Initialize(string version, string baseName = "Cardinal Web API Service")
        {
            Initialize(CardinalVersion.Parse(version), baseName);
        }

        /// <summary>
        /// Método de inicialização do serviço da aplicação.
        /// </summary>
        /// <param name="version">Versão base do serviço da aplicação.</param>
        /// <param name="baseName">Nome da aplicação.</param>
        internal static void Initialize(CardinalVersion version, string baseName = "Cardinal Web API Service")
        {
            BaseName = baseName;
            BaseVersion = version;
            ApplicationName = $"{BaseName} {BaseVersion}";
            Initialized = true;
        }
    }
}
