namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe estática com constantes de sistema.
    /// </summary>
    public static class CardinalConstants
    {
        /// <summary>
        /// Nome base do serviço da aplicação.
        /// </summary>
        public static string BaseName { get; private set; }

        /// <summary>
        /// Versão base do serviço da aplicação.
        /// </summary>
        public static ICardinalVersion BaseVersion { get; private set; }

        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public static string ApplicationName { get; private set; }
        
        /// <summary>
        /// Identificação se as constantes do serviço foram inicializadas.
        /// </summary>
        public static bool Initialized { get; private set; }

        /// <summary>
        /// Método de inicialização do serviço da aplicação.
        /// </summary>
        /// <param name="version">Versão base do serviço da aplicação.</param>
        /// <param name="baseName">Nome da aplicação.</param>
        public static void Initialize(string version, string baseName = "Cardinal Web API Service")
        {
            Initialize(new CardinalVersion(version), baseName);
        }

        /// <summary>
        /// Método de inicialização do serviço da aplicação.
        /// </summary>
        /// <param name="version">Versão base do serviço da aplicação.</param>
        /// <param name="baseName">Nome da aplicação.</param>
        public static void Initialize(ICardinalVersion version, string baseName = "Cardinal Web API Service")
        {
            BaseName = baseName;
            BaseVersion = version;
            ApplicationName = $"{BaseName} {BaseVersion.ToString()}";
            Initialized = true;
        }
    }
}
