using Cardinal.Utils;

namespace Cardinal.AspNetCore.Utils.Services
{
    /// <summary>
    /// Serviço de informação do servidor.
    /// </summary>
    public class ServerInfoService : IServerInfoService
    {
        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public string ApplicationName => Constants.BaseName;

        /// <summary>
        /// Versão da aplicação.
        /// </summary>
        public CardinalVersion ApplicationVersion => Constants.BaseVersion;

        /// <summary>
        /// Ambiente de execução da aplicação.
        /// </summary>
        public string Environment => Constants.Environment;

        /// <summary>
        /// Indica se a aplicação está em modo de produção.
        /// </summary>
        public bool IsProduction => Constants.IsProduction;

        /// <summary>
        /// Indica se a aplicação está em modo de desenvolvimento.
        /// </summary>
        public bool IsDevelopment => Constants.IsDevelopment;
    }
}
