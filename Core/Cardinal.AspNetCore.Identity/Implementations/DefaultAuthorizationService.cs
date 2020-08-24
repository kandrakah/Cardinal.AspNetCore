using Cardinal.AspNetCore.Identity.Extensions;
using Cardinal.AspNetCore.Identity.Localization;
using Cardinal.Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Implementação padrão do serviço de autorização.
    /// </summary>
    internal class DefaultAuthorizationService : IAuthorizationService, IService
    {
        /// <summary>
        /// Instância do provedor de configurações.
        /// </summary>
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Instância do serviço de log.
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Acessor do contexto http.
        /// </summary>
        private IHttpContextAccessor Accessor { get; }

        private ISystemUser SystemUser { get; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="logger">Instância do serviço de logs.</param>
        /// <param name="configuration">Instância do container de configurações.</param>
        /// <param name="systemUser">Instância do usuário do sistema.</param>
        /// <param name="accessor">Instância do accessor do contexto http.</param>
        public DefaultAuthorizationService(ILogger<DefaultAuthorizationService> logger, IConfiguration configuration, ISystemUser systemUser, IHttpContextAccessor accessor)
        {
            this.Configuration = configuration;
            this.Logger = logger;
            this.SystemUser = systemUser;
            this.Accessor = accessor;
        }

        /// <summary>
        /// Método que efetua a validação da autorização de acesso.
        /// </summary>
        /// <param name="context">Contexto de autorização</param>
        /// <param name="requiredPermission">Requerimento de permissão</param>
        public async Task Authorize(AuthorizationFilterContext context, PermissionsAuthorizationRequirement requiredPermission)
        {
            // Se permite anônimo, não é necessário validar nada.
            if (requiredPermission.ValidationType.Equals(PermissionValidationType.Annonymous))
            {
                this.Logger.LogInformation(Resource.AUTHORIZATION_ANNONYMOUS, this.Accessor.HttpContext.Request.Path, this.SystemUser.RemoteIpAddress);
                return;
            }

            this.Logger.LogInformation(Resource.AUTHORIZATION_PROTECTED, this.Accessor.HttpContext.Request.Path, this.SystemUser.RemoteIpAddress);
            // Obtendo configurações da autoridade.
            var settings = this.Configuration.GetAuthoritySettings();
            // Obtendo token de autorização.
            var token = context.GetAuthorizationToken();

            var tokenDecoder = new JwtSecurityTokenHandler();

            this.Logger.LogInformation(Resource.AUTHORIZATION_VALIDATIND_TOKEN);
            // Verificando se é possível ler o token.
            if (!tokenDecoder.CanReadToken(token))
            {
                this.Logger.LogWarning(Resource.AUTHORIZATION_ERROR_TOKEN_WRONG_FORMAT);
                context.Result = this.Unauthorized();
                return;
            }

            // Fazendo a leitura do token.
            var jwtToken = tokenDecoder.ReadJwtToken(token);

            // Efetuando a validação do token junto a autoridade.
            var result = await jwtToken.ValidateAsync(settings);

            // Se o resultado não é válido, retorna 401.
            if (!result.IsValid)
            {
                this.Logger.LogWarning(Resource.AUTHORIZATION_ERROR_INVALID_TOKEN, result.Status, result.Message);
                context.Result = this.Unauthorized();
                return;
            }

            // Se os dados do usuário são nulos, não possui identidade ou não está autenticado, retorna 401.
            if (result.Principal == null || !result.Principal.Identities.Any() || !result.Principal.Identity.IsAuthenticated)
            {
                this.Logger.LogWarning(Resource.AUTHORIZATION_USER_UNHAUTHORIZED);
                context.Result = this.Unauthorized();
                return;
            }

            this.Logger.LogInformation(Resource.AUTHORIZATION_ADDING_USER_DETAILS);

            // Atualizando dados do usuário.
            await this.UpdateSystemUser(settings, token);

            // Se é necessária apenas autenticação, não há porquê continuar.
            if (requiredPermission.ValidationType.Equals(PermissionValidationType.RequireAuthenticatedOnly))
            {
                this.Logger.LogInformation(Resource.AUTHORIZATION_USER_AUTHORIZED, this.SystemUser.DisplayName, this.Accessor.HttpContext.Request.Path);
                return;
            }

            // Se há uma permissão padrão, transforme-a em permissão de controller.
            if (requiredPermission.Contains(IdentityConstants.PERMISSION_DEFAULT_TAG))
            {
                var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var controllerType = actionDescriptor.ControllerTypeInfo.AsType();
                requiredPermission.ReplaceDefault(controllerType.GetControllerDefaultPermission());
            }


            var havePermission = false;
            var missingPermissions = new List<string>();
            switch (settings.PermissionsValidationType)
            {
                case PermissionsValidationType.Claim:
                    havePermission = ValidatePermissionByClaims(requiredPermission, out missingPermissions);
                    break;
                case PermissionsValidationType.Authority:
                    break;
            }

            // Se o usuário não possui permissão para acesso ao serviço, retorna 401.
            if (!havePermission)
            {
                this.LogPermissionErrorMessage(requiredPermission.ValidationType, missingPermissions);
                context.Result = this.Unauthorized();
                return;
            }

            this.Logger.LogInformation(Resource.AUTHORIZATION_USER_AUTHORIZED, this.SystemUser.DisplayName, this.Accessor.HttpContext.Request.Path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validationType"></param>
        /// <param name="missingPermissions"></param>
        private void LogPermissionErrorMessage(PermissionValidationType validationType, IEnumerable<string> missingPermissions)
        {
            var perms = string.Join(", ", missingPermissions);
            switch (validationType)
            {
                case PermissionValidationType.RequireOneOf:
                    this.Logger.LogInformation(Resource.ERROR_PERMISSION_MISSING_SINGLE_LOGGER, this.SystemUser.DisplayName, perms);
                    break;
                case PermissionValidationType.RequireAll:
                    this.Logger.LogInformation(Resource.ERROR_PERMISSION_MISSING_MULTI_LOGGER, this.SystemUser.DisplayName, perms);
                    break;
            }
        }

        /// <summary>
        /// Método que efetua a verificação de permissão de uma identidade considerando o requerimento de permissão informado.
        /// </summary>
        /// <param name="requiredPermission">Requerimento de permissão</param>
        /// <param name="missingPermissions"></param>
        /// <returns>Verdadeiro caso o usuário atenda aos critérios requisitados e falso caso contrário</returns>
        public virtual bool ValidatePermissionByClaims(PermissionsAuthorizationRequirement requiredPermission, out List<string> missingPermissions)
        {
            var result = false;
            if (this.SystemUser.IsRoot())
            {
                this.Logger.LogInformation(Resource.AUTHORIZATION_USER_ISROOT);
                missingPermissions = new List<string>();
                return true;
            }

            var usrPermissions = this.SystemUser.Permissions.Select(x => x.Value).ToList();

            var ownedPermissions = requiredPermission.Permissions.Where(x => usrPermissions.Contains(x)).ToList();
            missingPermissions = requiredPermission.Permissions.Where(x => !usrPermissions.Contains(x)).ToList();

            result = requiredPermission.ValidationType switch
            {
                PermissionValidationType.RequireOneOf => ownedPermissions.Any(),
                PermissionValidationType.RequireAll => ownedPermissions.Count() == requiredPermission.Permissions.Count(),
                _ => true,
            };
            return result;
        }

        /// <summary>
        /// Método que verifica se o usuário possui permissão máxima.
        /// </summary>
        /// <param name="token">Identidade do usuário</param>
        /// <returns>Verdadeiro caso o usuário possua a permissão máxima e falso caso contrário</returns>
        public virtual bool IsRoot(string token)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public IActionResult Result(HttpStatusCode statusCode)
        {
            return new StatusCodeResult((int)statusCode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Unauthorized()
        {
            return new UnauthorizedResult();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Forbid()
        {
            return new ForbidResult();
        }

        /// <summary>
        /// Método que faz a atualização dos dados do usuário com informações da autoridade.
        /// </summary>
        /// <param name="settings">Configurações da autoridade</param>
        /// <param name="token">Token de acesso</param>
        private async Task UpdateSystemUser(AuthoritySettings settings, string token)
        {
            try
            {
                var client = new HttpClient();
                var userInfo = await client.GetUserInfoAsync(settings.Authority, token);
                var claims = userInfo.ToClaims();
                var identity = new ClaimsIdentity(claims);
                this.Accessor.HttpContext.User.AddIdentity(identity);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(Resource.AUTHORIZATION_ERROR_UPDATE_SYSTEM_USER, ex.Message);
            }
        }

        /// <summary>
        /// Método para liberação de recursos usados pelo serviço.
        /// </summary>
        public void Dispose() { }
    }
}
