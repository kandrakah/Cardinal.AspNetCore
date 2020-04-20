using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Classe de configurações Identity
    /// </summary>
    public class AuthoritySettings
    {
        /// <summary>
        /// Configuração que desativa o serviço de permissões padrão.
        /// </summary>
        public bool DeactivePermissionService { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Flow { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Secret { get; set; }

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
        public bool SaveToken { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string CookieName { get; set; } = "cardinal.cookie";

        /// <summary>
        /// 
        /// </summary>
        public CertificateStorage CertificateStorage { get; set; } = CertificateStorage.File;

        /// <summary>
        /// 
        /// </summary>
        public string CertificatePath { get; set; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        public string CertificatePass { get; set; } = "cardinal";

        /// <summary>
        /// 
        /// </summary>
        public string Thumbprint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ValidOnly { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public StoreName StoreName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StoreLocation StoreLocation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> Scopes { get; set; }        
    }
}
