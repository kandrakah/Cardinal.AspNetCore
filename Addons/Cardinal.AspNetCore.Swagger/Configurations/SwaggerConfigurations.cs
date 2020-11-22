using Cardinal.AspNetCore.Utils;
using Microsoft.OpenApi.Models;
using System;

namespace Cardinal.AspNetCore
{
    public class SwaggerConfigurations
    {
        public string Title { get; set; } = $"{Constants.ApplicationName} API";

        public string Version { get; set; } = Constants.BaseVersion.ToString();

        public string Description { get; set; } = $"{Constants.ApplicationName} API";

        public SwaggerContactConfigurations Contact { get; set; } = new SwaggerContactConfigurations();

        public SwaggerLicenseConfigurations License { get; set; } = new SwaggerLicenseConfigurations();

        public string EndpointUri { get; set; }

        public bool UseSecurity { get; set; } = false;

        public string AuthAppName { get; set; } = "Swagger";

        public string ClientId { get; set; } = "swagger";

        public string Secret { get; set; } = "swagger";

        public bool UsePkce { get; set; } = false;

        public string DocumentationPath = AppContext.BaseDirectory;

        public string DocumentationPattern = "*.xml";

        public bool IncludeControllerXmlComments = false;

        public SwaggerSecurityDefinitionConfigurations SecurityDefinitions { get; set; } = new SwaggerSecurityDefinitionConfigurations();

        public SwaggerSecurityRequerimentConfigurations SecurityRequeriment { get; set; } = new SwaggerSecurityRequerimentConfigurations();        

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
