using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using System.Collections.Generic;
using System.Linq;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Implementação do requerimento de autorização.
    /// </summary>
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
        /// Método construtor
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
            this.ValidationType = validationType;
        }

        /// <summary>
        /// Método que verifica se a permissão infomada existe no requerimento.
        /// </summary>
        /// <param name="permission">Nome da permissão à ser verificada</param>
        /// <returns>Verdadeiro caso a permissão exista no requerimento e falso caso contrário</returns>
        internal bool Contains(string permission)
        {
            return this.RequiredPermissions.Contains(permission);
        }

        /// <summary>
        /// Método que faz a substituição da permissão padrão pelo seu nome correto
        /// </summary>
        /// <param name="permission">Nome da permissão à ser adicionada</param>
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
