using Cardinal.AspNetCore.Identity;
using Cardinal.AspNetCore.Identity.Localization;
using Cardinal.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Classe de extensões para <see cref="AuthoritySettings"/>.
    /// </summary>
    public static class AuthoritySettingsExtensions
    {
        /// <summary>
        /// Extensão que retorna os parâmetros de validação de token OAuth.
        /// </summary>
        /// <param name="settings">Objeto referenciado</param>
        /// <returns>Parâmetros de validação do Token. Veja <see cref="TokenValidationParameters"/></returns>
        internal static TokenValidationParameters GetTokenParametes(this AuthoritySettings settings)
        {
            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = settings.ValidateIssuerSigningKey,
                    IssuerSigningKey = settings.ValidateIssuerSigningKey ? new X509SecurityKey(GetCertificate(settings)) : null,
                    ValidateIssuer = settings.ValidateIssuer,
                    ValidateAudience = settings.ValidateAudience,
                    ValidateLifetime = settings.ValidateLifetime,
                    ClockSkew = TimeSpan.Zero
                };
                return tokenValidationParameters;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Classe interna para a obtenção do certificado de validação de Token OAuth.
        /// </summary>
        /// <param name="settings">Objeto referenciado</param>
        /// <returns>Certificado de validação. Veja <see cref="X509Certificate2"/></returns>
        internal static X509Certificate2 GetCertificate(this AuthoritySettings settings)
        {
            return settings.CertificateStorage switch
            {
                CertificateStorage.File => GetFromFile(settings.CertificatePath, settings.CertificatePass),
                CertificateStorage.Storage => GetFromStorage(settings.Thumbprint, settings.ValidOnly, settings.StoreName, settings.StoreLocation),
                _ => null,
            };
        }

        /// <summary>
        /// Método que obtém o certificado provido de um arquivo.
        /// </summary>
        /// <param name="path">Localização do certificado no sistema de arquivos</param>
        /// <param name="password">Senha de acesso ao certificado</param>
        /// <returns>Certificado de validação. Veja <see cref="X509Certificate2"/></returns>
        private static X509Certificate2 GetFromFile(string path, string password)
        {
            try
            {
                if (File.Exists(path))
                {
                    return new X509Certificate2(path, password);
                }
                else
                {
                    throw new FileNotFoundException(Resource.ERROR_CERTIFICATE_NOT_FOUND);
                }
            }
            catch (CryptographicException ex)
            {
                throw new CardinalException(Resource.ERROR_CERTIFICATE_FAIL, ex);
            }
        }

        /// <summary>
        /// Método que obtém o certificado provido do repositório.
        /// </summary>
        /// <param name="thumbprint">Impressão digital do certificado</param>
        /// <param name="validOnly">Indica que somente certificados válidos</param>
        /// <param name="storeName">Nome do repositório. Veja <see cref="StoreName"/></param>
        /// <param name="storeLocation">Localização do repositório. Veja <see cref="StoreLocation"/></param>
        /// <returns>Certificado de validação. Veja <see cref="X509Certificate2"/></returns>
        private static X509Certificate2 GetFromStorage(string thumbprint, bool validOnly, StoreName storeName = StoreName.My, StoreLocation storeLocation = StoreLocation.CurrentUser)
        {
            var certificate = default(X509Certificate2);
            using (var store = new X509Store(storeName, storeLocation))
            {
                store.Open(OpenFlags.ReadOnly);
                var items = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly);
                certificate = items[0];
            }
            return certificate;
        }
    }
}
