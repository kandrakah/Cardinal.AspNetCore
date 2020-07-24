using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Cardinal.Settings
{
    public class SwaggerSecurityRequerimentSettings
    {
        public Dictionary<SwaggerSecuritySchemeSettings, IList<string>> Schemes = new Dictionary<SwaggerSecuritySchemeSettings, IList<string>>();

        internal OpenApiSecurityRequirement ToOpenApi()
        {
            var requeriment = new OpenApiSecurityRequirement();
            foreach (var item in this.Schemes)
            {
                requeriment.Add(item.Key.ToOpenApi(), item.Value);
            }

            return requeriment;
        }

        public override string ToString()
        {
            return string.Join("", Schemes);
        }
    }
}
