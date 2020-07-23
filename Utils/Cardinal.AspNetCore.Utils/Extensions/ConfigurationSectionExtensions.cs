using Microsoft.Extensions.Configuration;
using System;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="IConfigurationSection"/>.
    /// </summary>
    public static partial class ConfigurationSectionExtensions
    {
        /// <summary>
        /// Extensão que traz as configurações tipadas da sessão.
        /// </summary>
        /// <typeparam name="T">Objeto contendo as configurações da sessão.</typeparam>
        /// <param name="section">Este Objeto.</param>
        /// <returns></returns>
        public static T GetSettings<T>(this IConfigurationSection section) where T : class
        {
            var settings = section.Get<T>();
            if (settings == null)
            {
                settings = Activator.CreateInstance<T>();
            }
            return settings;
        }
    }
}
