using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Cardinal.Settings
{
    public class SwaggerSecurityDefinitionFlowSettings
    {
        public SwaggerSecurityAuthFlowSettings Implicit { get; set; }

        public SwaggerSecurityAuthFlowSettings Password { get; set; }

        public SwaggerSecurityAuthFlowSettings ClientCredentials { get; set; }

        public SwaggerSecurityAuthFlowSettings AuthorizationCode { get; set; }

        public IDictionary<string, IOpenApiExtension> Extensions { get; set; } = new Dictionary<string, IOpenApiExtension>();

        internal OpenApiOAuthFlows ToOpenApi()
        {
            var item = new OpenApiOAuthFlows()
            {
                Implicit = this.Implicit?.ToOpenApi(),
                Password = this.Password?.ToOpenApi(),
                ClientCredentials = this.ClientCredentials?.ToOpenApi(),
                AuthorizationCode = this.AuthorizationCode?.ToOpenApi(),
                Extensions = this.Extensions
            };

            return item;
        }
    }
}
