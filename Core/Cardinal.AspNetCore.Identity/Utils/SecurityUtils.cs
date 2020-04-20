using Cardinal.AspNetCore.Exceptions;
using Cardinal.AspNetCore.Identity.Localization;
using Cardinal.AspNetCore.Utils;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Cardinal.AspNetCore.Identity.Utils
{
    /// <summary>
    /// Classe interna de utilidades de segurança.
    /// </summary>
    internal static class SecurityUtils
    {
        /// <summary>
        /// Método interno que retorna os parâmetros de validação de token OAuth.
        /// </summary>
        /// <param name="settings">Configurações de autoridade usadas nos parâmetros. Veja <see cref="AuthoritySettings"/>.</param>
        /// <returns>Parâmetros de validação do Token. Veja <see cref="TokenValidationParameters"/>.</returns>
        internal static TokenValidationParameters GetTokenParametes(AuthoritySettings settings)
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
        /// <param name="settings">Configurações de autoridade contendo os dados de acesso ao certificado.</param>
        /// <returns>Certificado de validação. Veja <see cref="X509Certificate2"/>.</returns>
        internal static X509Certificate2 GetCertificate(AuthoritySettings settings)
        {
            switch (settings.CertificateStorage)
            {
                case CertificateStorage.File:
                    return GetFromFile(settings.CertificatePath, settings.CertificatePass);
                case CertificateStorage.Storage:
                    return GetFromStorage(settings.Thumbprint, settings.ValidOnly, settings.StoreName, settings.StoreLocation);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Método que obtém o certificado provido de um arquivo.
        /// </summary>
        /// <param name="path">Localização do certificado no sistema de arquivos.</param>
        /// <param name="password">Senha de acesso ao certificado.</param>
        /// <returns>Certificado de validação. Veja <see cref="X509Certificate2"/>.</returns>
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
                    throw new FileNotFoundException(ResourceUtils.Translate(Resource.ERROR_CERTIFICATE_NOT_FOUND));
                }
            }
            catch (CryptographicException ex)
            {
                throw new CardinalException(ResourceUtils.Translate(Resource.ERROR_CERTIFICATE_FAIL), ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thumbprint"></param>
        /// <param name="validOnly"></param>
        /// <param name="storeName"></param>
        /// <param name="storeLocation"></param>
        /// <returns>Certificado de validação. Veja <see cref="X509Certificate2"/>.</returns>
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
