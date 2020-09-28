using Microsoft.Extensions.Configuration;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IConfiguration"/>.
    /// </summary>
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// Extensão que traz as configurações tipadas da configuração.
        /// </summary>
        /// <typeparam name="T">Objeto contendo as configurações da sessão</typeparam>
        /// <param name="configuration">Instância de <see cref="IConfiguration"/></param>
        /// <param name="sectionName">Nome da sessão de configurações</param>
        /// <returns>Objeto contendo as configurações.</returns>
        public static T GetConfigurations<T>(this IConfiguration configuration, string sectionName) where T : class
        {
            var section = configuration.GetSection(sectionName);
            var settings = section?.GetConfigurations<T>();
            return settings;
        }
    }
}
