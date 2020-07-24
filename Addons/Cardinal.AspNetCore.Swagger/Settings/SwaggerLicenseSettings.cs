using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Cardinal.Settings
{
    public class SwaggerLicenseSettings
    {
        public string Name { get; set; } = "MIT";
        public string Url { get; set; } = "https://github.com/kandrakah/Cardinal.AspNetCore/blob/master/LICENSE";
        public IDictionary<string, IOpenApiExtension> Extensions { get; set; } = new Dictionary<string, IOpenApiExtension>();

        internal OpenApiLicense ToOpenApi()
        {
            var item = new OpenApiLicense()
            {
                Name = this.Name,
                Url = new Uri(this.Url),
                Extensions = this.Extensions
            };

            return item;
        }

        public override string ToString()
        {
            return $"[NAME:{this.Name}][URL:{this.Url}]";
        }
    }
}
