using Cardinal.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Identity.Extensions
{
    /// <summary>
    /// Extensões para <see cref="JwtSecurityToken"/>
    /// </summary>
    public static class JwtSecurityTokenExtensions
    {
        /// <summary>
        /// Extensão que faz a validação de um token.
        /// </summary>
        /// <param name="token">Objeto referenciado</param>
        /// <param name="settings">Configurações de autoridade</param>
        /// <returns>Resultado da validação do token</returns>
        public static async Task<TokenValidationResult> ValidateAsync(this JwtSecurityToken token, IdentityConfigurations settings)
        {
            TokenValidationResult result;
            try
            {
                var tokenDecoder = new JwtSecurityTokenHandler();

                var parameters = await settings.GetTokenParametesAsync(token);
                var principal = tokenDecoder.ValidateToken(token.RawData, parameters, out SecurityToken validatedToken);

                result = new TokenValidationResult(TokenValidation.Valid)
                {
                    Principal = principal,
                    SecurityToken = validatedToken
                };
            }
            catch (SecurityTokenExpiredException ex)
            {
                result = new TokenValidationResult(TokenValidation.Expired, ex.Message);
            }
            catch (SecurityTokenInvalidAudienceException ex)
            {
                result = new TokenValidationResult(TokenValidation.InvalidAudience, ex.Message);
            }
            catch (SecurityTokenInvalidLifetimeException ex)
            {
                result = new TokenValidationResult(TokenValidation.InvalidLifetime, ex.Message);
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                result = new TokenValidationResult(TokenValidation.InvalidSignature, ex.Message);
            }
            catch (SecurityTokenNoExpirationException ex)
            {
                result = new TokenValidationResult(TokenValidation.NoExpiration, ex.Message);
            }
            catch (SecurityTokenNotYetValidException ex)
            {
                result = new TokenValidationResult(TokenValidation.NotYetValid, ex.Message);
            }
            catch (SecurityTokenReplayAddFailedException ex)
            {
                result = new TokenValidationResult(TokenValidation.ReplayAdd, ex.Message);
            }
            catch (SecurityTokenReplayDetectedException ex)
            {
                result = new TokenValidationResult(TokenValidation.ReplayDetected, ex.Message);
            }
            catch (Exception ex)
            {
                result = new TokenValidationResult(TokenValidation.Error, ex.Message);
            }

            return result;
        }
    }
}
