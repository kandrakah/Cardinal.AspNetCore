using Cardinal.AspNetCore.Identity.Localization;
using Cardinal.Exceptions;
using Cardinal.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Classe de atributo que define a permissão necessária para acesso ao endpoint com ela decorado.
    /// </summary>
    public class PermissionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="method">Método de execução do endpoint. Veja <see cref="HttpMethod"/></param>
        public PermissionAttribute(Method method) : this(method, PermissionValidationType.RequireAuthenticatedOnly)
        {
        }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="method">Método de execução do endpoint</param>
        /// <param name="validationType">Tipo de validação de permissão. Veja <see cref="PermissionValidationType"/></param>
        /// <param name="permissions">Permissões requeridas para autorização</param>
        public PermissionAttribute(Method method, PermissionValidationType validationType, params string[] permissions) : base(typeof(PermissionAttributeImpl))
        {
            var permission = new PermissionsAuthorizationRequirement(method, validationType, permissions);
            Arguments = new[] { permission };
        }

        /// <summary>
        /// Implementação do atributo de permissão.
        /// </summary>
        private class PermissionAttributeImpl : Attribute, IAuthorizationFilter
        {
            private readonly ILogger _logger;
            private readonly PermissionsAuthorizationRequirement _requiredPermission;
            private readonly IServiceProvider _provider;
            private readonly IConfiguration _configuration;

            /// <summary>
            /// Método construtor
            /// </summary>
            /// <param name="logger">Instância do serviço de log</param>
            /// <param name="requiredPermissions">Permissões requeridas pelo atributo</param>
            /// <param name="provider">Instância do provedor de serviços</param>
            /// <param name="configuration">Instância do container de configurações</param>
            public PermissionAttributeImpl(ILogger<PermissionAttribute> logger, PermissionsAuthorizationRequirement requiredPermissions, IServiceProvider provider, IConfiguration configuration)
            {
                this._logger = logger;
                this._requiredPermission = requiredPermissions;
                this._provider = provider;
                this._configuration = configuration;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var settings = this._configuration.GetSettings<AuthoritySettings>("Authority");
                if (settings.UsePermissionsService)
                {
                    try
                    {
                        var authorizationService = this._provider.GetCardinalService<IAuthorizationService>();
                        authorizationService.Authorize(context, this._requiredPermission);
                    }
                    catch (ServiceNotFoundException)
                    {
                        _logger.LogError(Resource.ERROR_AUTHORIZATION_SERVICE_NOT_REGISTERED);
                        context.Result = new StatusCodeResult(500);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.GetBaseException().Message);
                        context.Result = new StatusCodeResult(500);
                    }
                }
            }
        }
    }
}
