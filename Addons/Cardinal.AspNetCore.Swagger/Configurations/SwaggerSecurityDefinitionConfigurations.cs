using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Cardinal.AspNetCore
{
    public class SwaggerSecurityDefinitionConfigurations
    {
        public string Scheme { get; set; } = "Bearer";

        public string Name { get; set; } = "Authorization";

        public string Description { get; set; } = "Autenticação para Swagger";

        public ParameterLocation In { get; set; } = ParameterLocation.Header;

        public SecuritySchemeType Type { get; set; } = SecuritySchemeType.ApiKey;

        public SwaggerSecurityDefinitionFlowConfigurations Flows { get; set; }

        public IDictionary<string, IOpenApiExtension> Extensions { get; set; } = new Dictionary<string, IOpenApiExtension>();

        internal OpenApiSecurityScheme ToOpenApi()
        {
            var scheme = new OpenApiSecurityScheme()
            {
                Name = this.Name,
                Description = this.Description,
                In = this.In,
                Type = this.Type,
                Scheme = this.Scheme,
                Flows = this.Flows?.ToOpenApi(),
                Extensions = this.Extensions
            };

            return scheme;
        }

        public override string ToString()
        {
            return $"[SCHEME:{this.Scheme}][NAME:{this.Name}][DESCRIPTION:{this.Description}][IN:{this.In}][TYPE:{this.Type}]";
        }
    }
}
