using Cardinal.Utils;

namespace Cardinal.AspNetCore.Utils.Services
{
    /// <summary>
    /// Interface do serviço de informação do servidor.
    /// </summary>
    public interface IServerInfoService
    {
        /// <summary>
        /// Nome da aplicação.
        /// </summary>
        public string ApplicationName { get; }

        /// <summary>
        /// Versão da aplicação.
        /// </summary>
        public CardinalVersion ApplicationVersion { get; }

        /// <summary>
        /// Ambiente de execução da aplicação.
        /// </summary>
        public string Environment { get; }

        /// <summary>
        /// Indica se a aplicação está em modo de produção.
        /// </summary>
        public bool IsProduction { get; }

        /// <summary>
        /// Indica se a aplicação está em modo de desenvolvimento.
        /// </summary>
        public bool IsDevelopment { get; }
    }
}
