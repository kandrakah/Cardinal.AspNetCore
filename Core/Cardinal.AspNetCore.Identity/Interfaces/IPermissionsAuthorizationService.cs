using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Interface do serviço de autorização
    /// </summary>
    public interface IPermissionsAuthorizationService
    {
        /// <summary>
        /// Método que efetua a validação da autorização de acesso.
        /// </summary>
        /// <param name="context">Contexto de autorização</param>
        /// <param name="requiredPermission">Requerimento de permissão</param>
        Task Authorize(AuthorizationFilterContext context, PermissionsAuthorizationRequirement requiredPermission);

        /// <summary>
        /// Método que efetua a verificação de permissão de uma identidade considerando o requerimento de permissão informado.
        /// </summary>
        /// <param name="requiredPermission">Requerimento de permissão</param>
        /// <param name="missingPermissions"></param>
        /// <returns>Verdadeiro caso o usuário atenda aos critérios requisitados e falso caso contrário</returns>
        bool ValidatePermissionByClaims(PermissionsAuthorizationRequirement requiredPermission, out List<string> missingPermissions);
    }
}
