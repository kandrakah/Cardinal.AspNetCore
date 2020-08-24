using Cardinal.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IConfiguration"/>.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Extensão que traz as configurações tipadas da configuração.
        /// </summary>
        /// <param name="configuration">Instância de <see cref="IConfiguration"/></param>
        /// <param name="sectionName">Nome da sessão de configurações</param>
        /// <returns>Objeto contendo as configurações.</returns>
        public static AuthoritySettings GetAuthoritySettings(this IConfiguration configuration, string sectionName = "Authority") 
        {            
            var settings = configuration.GetSettings<AuthoritySettings>(sectionName);
            return settings;
        }
    }
}
