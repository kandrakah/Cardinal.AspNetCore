using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Cardinal.AspNetCore.Identity
{
    public interface IAuthorizationService
    {
        void Authorize(AuthorizationFilterContext context, PermissionsAuthorizationRequirement requiredPermission);

        bool HavePermission(ClaimsIdentity identity, PermissionsAuthorizationRequirement requiredPermission);

        bool IsRoot(ClaimsIdentity identity);
    }
}
