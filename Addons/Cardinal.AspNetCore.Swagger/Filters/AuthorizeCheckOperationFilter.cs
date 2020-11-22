using Cardinal.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        private readonly SwaggerConfigurations _configurations;
        public AuthorizeCheckOperationFilter(IConfiguration configuration)
        {
            this._configurations = configuration.GetConfigurations<SwaggerConfigurations>("Swagger");
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

            var scopes = this._configurations.SecurityDefinitions.Flows.Scopes();

            if (hasAuthorize)
            {
                //operation.Responses.Clear();
                //operation.Responses.Add("200", new OpenApiResponse { Description = HttpStatusCode.OK.GetDescription() });
                //operation.Responses.Add("204", new OpenApiResponse { Description = HttpStatusCode.NoContent.GetDescription() });
                //operation.Responses.Add("400", new OpenApiResponse { Description = HttpStatusCode.BadRequest.GetDescription() });
                //operation.Responses.Add("401", new OpenApiResponse { Description = HttpStatusCode.Unauthorized.GetDescription() });
                //operation.Responses.Add("403", new OpenApiResponse { Description = HttpStatusCode.Forbidden.GetDescription() });
                //operation.Responses.Add("404", new OpenApiResponse { Description = HttpStatusCode.NotFound.GetDescription() });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {              
                        [             
                            new OpenApiSecurityScheme {
                                Reference = new OpenApiReference   
                                {           
                                    Type = ReferenceType.SecurityScheme, 
                                    Id = "oauth2"       
                                }
                            }              
                            ] = scopes}
                };

            }
        }
    }
}
