using Cardinal.AspNetCore.Utils;
using Microsoft.OpenApi.Models;

namespace Cardinal.Settings
{
    public class SwaggerSettings
    {
        public string Title { get; set; } = $"{Constants.ApplicationName} API";

        public string Version { get; set; } = Constants.BaseVersion.ToString();

        public string Description { get; set; } = $"{Constants.ApplicationName} API";

        public SwaggerContactSettings Contact { get; set; } = new SwaggerContactSettings();

        public SwaggerLicenseSettings License { get; set; } = new SwaggerLicenseSettings();

        public string EndpointUri { get; set; }

        public bool UseSecurity { get; set; } = false;

        public string AuthAppName { get; set; } = "Swagger";

        public string ClientId { get; set; } = "swagger";

        public string Secret { get; set; } = "swagger";

        public bool UsePkce { get; set; } = false;

        public SwaggerSecurityDefinitionSettings SecurityDefinitions { get; set; } = new SwaggerSecurityDefinitionSettings();

        public SwaggerSecurityRequerimentSettings SecurityRequeriment { get; set; } = new SwaggerSecurityRequerimentSettings();        

        internal OpenApiInfo ToOpenApi()
        {
            var item = new OpenApiInfo()
            {
                Title = this.Title,
                Version = this.Version,
                Description = this.Description,
                Contact = this.Contact.ToOpenApi(),
                License = this.License.ToOpenApi()
            };

            return item;
        }


        internal string GetEndpointUri()
        {
            return string.IsNullOrEmpty(this.EndpointUri) ? $"/swagger/{this.Version}/swagger.json" : this.EndpointUri;
        }

        public override string ToString()
        {
            return $"[TITLE:{this.Title}][VERSION:{this.Version}][DESCRIPTION:{this.Description}]";
        }
    }
}
