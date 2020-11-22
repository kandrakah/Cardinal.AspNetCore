using Microsoft.OpenApi.Models;

namespace Cardinal.AspNetCore
{
    public class SwaggerSecuritySchemeConfigurations
    {
        public OpenApiReference Reference { get; set; } = new OpenApiReference() { Id = "Bearer", Type = ReferenceType.SecurityScheme };

        public string Scheme { get; set; } = "oauth2";

        public string Name { get; set; } = "Bearer";

        public ParameterLocation In { get; set; } = ParameterLocation.Header;

        public OpenApiSecurityScheme ToOpenApi()
        {
            var scheme = new OpenApiSecurityScheme()
            {
                Reference = this.Reference,
                Scheme = this.Scheme,
                Name = this.Name,
                In = this.In
            };

            return scheme;
        }
    }
}
