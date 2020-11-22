using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore
{
    public class SwaggerSecurityDefinitionFlowConfigurations
    {
        public SwaggerSecurityAuthFlowConfigurations Implicit { get; set; }

        public SwaggerSecurityAuthFlowConfigurations Password { get; set; }

        public SwaggerSecurityAuthFlowConfigurations ClientCredentials { get; set; }

        public SwaggerSecurityAuthFlowConfigurations AuthorizationCode { get; set; }

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

        public string[] Scopes()
        {
            if (this.Implicit != null)
            {
                return this.Implicit.Scopes.Select(x => x.Value).ToArray();
            }
            else if (this.Password != null)
            {
                return this.Password.Scopes.Select(x => x.Value).ToArray();
            }
            else if (this.ClientCredentials != null)
            {
                return this.ClientCredentials.Scopes.Select(x => x.Value).ToArray();
            }
            else if (this.AuthorizationCode != null)
            {
                return this.AuthorizationCode.Scopes.Select(x => x.Value).ToArray();
            }
            else
            {
                return new string[0] { };
            }
        }
    }
}
