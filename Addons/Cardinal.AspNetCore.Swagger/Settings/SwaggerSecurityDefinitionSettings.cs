using Microsoft.OpenApi.Models;

namespace Cardinal.Settings
{
    public class SwaggerSecurityDefinitionSettings
    {
        public string Scheme { get; set; } = "Bearer";

        public string Name { get; set; } = "Authorization";

        public string Description { get; set; } = "Autenticação para Swagger";

        public ParameterLocation In { get; set; } = ParameterLocation.Header;

        public SecuritySchemeType Type { get; set; } = SecuritySchemeType.ApiKey;

        internal OpenApiSecurityScheme ToOpenApi()
        {
            var scheme = new OpenApiSecurityScheme()
            {
                Name = this.Name,
                Description = this.Description,
                In = this.In,
                Type = this.Type,
                Scheme = this.Scheme
            };

            return scheme;
        }

        public override string ToString()
        {
            return $"[SCHEME:{this.Scheme}][NAME:{this.Name}][DESCRIPTION:{this.Description}][IN:{this.In}][TYPE:{this.Type}]";
        }
    }
}
