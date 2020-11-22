using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Cardinal.AspNetCore
{
    public class SwaggerSecurityRequerimentConfigurations
    {
        public Dictionary<SwaggerSecuritySchemeConfigurations, IList<string>> Schemes = new Dictionary<SwaggerSecuritySchemeConfigurations, IList<string>>();

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
