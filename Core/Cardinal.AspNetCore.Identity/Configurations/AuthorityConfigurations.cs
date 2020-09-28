using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using System.Collections.Generic;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Classe de configurações Identity
    /// </summary>
    public class AuthorityConfigurations
    {
        /// <summary>
        /// Configuração que ativa o serviço de permissões padrão.
        /// </summary>
        public bool UsePermissionsService { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SaveToken { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public SupportedTokens SupportedTokens { get; set; } = SupportedTokens.Both;

        /// <summary>
        /// 
        /// </summary>
        public string RoleClaimType { get; set; } = JwtClaimTypes.Role;

        /// <summary>
        /// 
        /// </summary>
        public string NameClaimType { get; set; } = JwtClaimTypes.Name;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateIssuer { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateAudience { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool ValidateLifetime { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool RequireHttpsMetadata { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> Scopes { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public PermissionsValidationType PermissionsValidationType { get; set; } = PermissionsValidationType.Claim;
    }
}
