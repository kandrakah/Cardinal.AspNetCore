using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Cardinal.AspNetCore
{
    public class SwaggerContactConfigurations
    {
        public string Name { get; set; } = "Cardinal";

        public string Url { get; set; } = "https://github.com/kandrakah/Cardinal.AspNetCore";

        public string Email { get; set; } = string.Empty;

        public IDictionary<string, IOpenApiExtension> Extensions { get; set; } = new Dictionary<string, IOpenApiExtension>();

        public OpenApiContact ToOpenApi()
        {
            var item = new OpenApiContact()
            {
                Name = this.Name,
                Url = new Uri(this.Url),
                Email = this.Email,
                Extensions = this.Extensions
            };

            return item;
        }

        public override string ToString()
        {
            return $"[NAME:{this.Name}][EMAIL:{this.Email}][URL:{this.Url}]";
        }
    }
}
