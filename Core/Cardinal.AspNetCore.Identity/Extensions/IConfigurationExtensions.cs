﻿using Cardinal.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IConfiguration"/>.
    /// </summary>
    public static class IConfigurationExtensions
    {
        /// <summary>
        /// Extensão que traz as configurações de identidade.
        /// </summary>
        /// <param name="configuration">Instância de <see cref="IConfiguration"/></param>
        /// <param name="sectionName">Nome da sessão de configurações</param>
        /// <returns>Objeto contendo as configurações.</returns>
        public static IdentityConfigurations GetIdentityConfigurations(this IConfiguration configuration, string sectionName = "Identity") 
        {            
            var settings = configuration.GetConfigurations<IdentityConfigurations>(sectionName);
            return settings;
        }
    }
}
