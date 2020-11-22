using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Cardinal.AspNetCore
{
    public class SwaggerSecurityAuthFlowConfigurations
    {
        public string AuthorizationUrl { get; set; }

        public string TokenUrl { get; set; }

        public string RefreshUrl { get; set; }

        public IDictionary<string, string> Scopes { get; set; } = new Dictionary<string, string>();

        public IDictionary<string, IOpenApiExtension> Extensions { get; set; } = new Dictionary<string, IOpenApiExtension>();

        internal OpenApiOAuthFlow ToOpenApi()
        {
            var item = new OpenApiOAuthFlow()
            {
                AuthorizationUrl = string.IsNullOrEmpty(this.AuthorizationUrl) ? null : new Uri(this.AuthorizationUrl),
                TokenUrl = string.IsNullOrEmpty(this.TokenUrl) ? null : new Uri(this.TokenUrl),
                RefreshUrl = string.IsNullOrEmpty(this.RefreshUrl) ? null : new Uri(this.RefreshUrl),
                Scopes = this.Scopes,
                Extensions = this.Extensions
            };

            return item;
        }

        public override string ToString()
        {
            var scopes = string.Empty;

            foreach (var ext in this.Scopes)
            {
                scopes += $"[{ext.Key}:{ext.Value}]";
            }

            return $"[AUTHORIZATION:{this.AuthorizationUrl}][TOKEN:{this.TokenUrl}][REFRESH:{this.RefreshUrl}][SCOPES:{scopes}]";
        }
    }
}
