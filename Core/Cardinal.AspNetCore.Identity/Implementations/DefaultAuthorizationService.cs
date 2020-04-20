using Cardinal.AspNetCore.Extensions;
using Cardinal.AspNetCore.Identity.Extensions;
using Cardinal.AspNetCore.Identity.Utils;
using Cardinal.AspNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Implementação padrão do serviço de autorização.
    /// </summary>
    internal class DefaultAuthorizationService : IAuthorizationService, IService
    {
        /// <summary>
        /// Instância do provedor de serviços.
        /// </summary>
        private IServiceProvider Provider { get; }

        /// <summary>
        /// Instância do provedor de configurações.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Instância do serviço de log.
        /// </summary>
        private readonly ILogger Logger;

        public DefaultAuthorizationService(ILogger<DefaultAuthorizationService> logger, IServiceProvider provider, IConfiguration configuration)
        {
            this.Provider = provider;
            this.Configuration = configuration;
            this.Logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requiredPermission"></param>
        public void Authorize(AuthorizationFilterContext context, PermissionsAuthorizationRequirement requiredPermission)
        {
            // Se permite anônimo, não é necessário validar nada.
            if (requiredPermission.ValidationType.Equals(PermissionValidationType.Annonymous))
            {
                return;
            }
            
            // Obtém-se o token de acesso.
            var token = context.GetAuthorizationToken();
            
            var principal = this.GetPrincipalFromToken(token, out SecurityToken validatedToken);

            if (principal == null || !principal.Identities.Any() || !principal.Identity.IsAuthenticated)
            {
                context.Result = this.Unauthorized();
                return;
            }

            if (requiredPermission.Contains(IdentityConstants.PERMISSION_DEFAULT_TAG))
            {
                var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
                var controllerType = actionDescriptor.ControllerTypeInfo.AsType();
                requiredPermission.ReplaceDefault(controllerType.GetControllerDefaultPermission());
            }

            if (!HavePermission((ClaimsIdentity)principal.Identity, requiredPermission))
            {
                context.Result = this.Unauthorized();
                return;
            }
        }

        public virtual bool HavePermission(ClaimsIdentity identity, PermissionsAuthorizationRequirement requiredPermission)
        {
            return IsRoot(identity);
        }

        public virtual bool IsRoot(ClaimsIdentity identity)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public IActionResult Result(HttpStatusCode statusCode)
        {
            Logger.LogWarning($"{(int)statusCode}: {statusCode.GetDescription()}");
            return new StatusCodeResult((int)statusCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IActionResult Unauthorized(string message = "")
        {
            Logger.LogWarning($"401: {message}");
            return new UnauthorizedResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public IActionResult Forbid(string message = "")
        {
            Logger.LogWarning($"403: {message}");
            return new ForbidResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="validatedToken"></param>
        /// <returns></returns>
        private ClaimsPrincipal GetPrincipalFromToken(string token, out SecurityToken validatedToken)
        {
            var tokenDecoder = new JwtSecurityTokenHandler();

            if (tokenDecoder.CanReadToken(token))
            {
                var jwtSecurityToken = (JwtSecurityToken)tokenDecoder.ReadToken(token);
                var settings = this.Configuration.GetSection("Authority").GetSettings<AuthoritySettings>();
                var parameters = SecurityUtils.GetTokenParametes(settings);
                var principal = tokenDecoder.ValidateToken(jwtSecurityToken.RawData, parameters, out validatedToken);

                return principal;
            }
            else
            {
                validatedToken = null;
                return null;
            }
        }

        /// <summary>
        /// Método para liberação de recursos usados pelo serviço.
        /// </summary>
        public void Dispose() { }
    }
}
