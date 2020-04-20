using Cardinal.AspNetCore.Identity.Utils;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore.Identity
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 
        /// </summary>
        public Method Method { get; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<string> RequiredPermissions { get; }

        /// <summary>
        /// Enumerador para indicar o tipo de validação de múltiplas permissões.
        /// </summary>
        public PermissionValidationType ValidationType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="validationType"></param>
        /// <param name="requiredPermissions"></param>
        public PermissionsAuthorizationRequirement(Method method, PermissionValidationType validationType = PermissionValidationType.RequireOneOrMore, params string[] requiredPermissions)
        {
            this.RequiredPermissions = new HashSet<string>();
            if (requiredPermissions == null || !requiredPermissions.Any())
            {
                this.RequiredPermissions.Add(IdentityConstants.PERMISSION_DEFAULT_TAG);
            }
            else
            {
                this.RequiredPermissions = requiredPermissions;
            }

            this.Method = method;
            this.ValidationType = PermissionValidationType.RequireOneOrMore;
        }

        internal bool Contains(string permission)
        {
            return this.RequiredPermissions.Contains(permission);
        }

        internal void ReplaceDefault(string permission)
        {
            var p = this.RequiredPermissions.Where(x => x == IdentityConstants.PERMISSION_DEFAULT_TAG).FirstOrDefault();
            if (!string.IsNullOrEmpty(p))
            {
                this.RequiredPermissions.Remove(p);
                this.RequiredPermissions.Add(permission);
            }
        }
    }
}
