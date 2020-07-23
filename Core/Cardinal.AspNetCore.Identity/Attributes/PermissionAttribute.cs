using Cardinal.AspNetCore.Identity.Localization;
using Cardinal.Exceptions;
using Cardinal.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Classe de atributos que define a permissão necessária para acesso ao endpoint com ela decorado.
    /// </summary>
    public class PermissionAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="validationType"></param>
        /// <param name="permissions"></param>
        public PermissionAttribute(Method method, PermissionValidationType validationType, params string[] permissions) : base(typeof(PermissionAttributeImpl))
        {
            var permission = new PermissionsAuthorizationRequirement(method, validationType, permissions);
            Arguments = new[] { permission };
        }

        /// <summary>
        /// 
        /// </summary>
        private class PermissionAttributeImpl : Attribute, IAuthorizationFilter
        {
            private readonly ILogger Logger;
            private readonly PermissionsAuthorizationRequirement RequiredPermission;
            private readonly IServiceProvider Provider;
            private readonly IConfiguration Configuration;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="logger"></param>
            /// <param name="requiredPermissions"></param>
            /// <param name="provider"></param>
            public PermissionAttributeImpl(ILogger<PermissionAttribute> logger, PermissionsAuthorizationRequirement requiredPermissions, IServiceProvider provider)
            {
                this.Logger = logger;
                this.RequiredPermission = requiredPermissions;
                this.Provider = provider;
                this.Configuration = provider.GetCardinalService<IConfiguration>();
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="context"></param>
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                var settings = this.Configuration.GetSettings<AuthoritySettings>("Authority");
                if (settings.UsePermissionsService)
                {
                    try
                    {
                        var authorizationService = this.Provider.GetCardinalService<IAuthorizationService>();
                        authorizationService.Authorize(context, this.RequiredPermission);
                    }
                    catch (ServiceNotFoundException)
                    {
                        Logger.LogError(Resource.ERROR_AUTHORIZATION_SERVICE_NOT_REGISTERED);
                        context.Result = new StatusCodeResult(500);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex, ex.GetBaseException().Message);
                        context.Result = new StatusCodeResult(500);
                    }
                }
            }
        }
    }
}
