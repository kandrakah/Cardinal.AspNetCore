using Microsoft.Extensions.Configuration;
using System;

namespace Cardinal.AspNetCore.Extensions
{
    public static partial class ConfigurationSectionExtensions
    {
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
