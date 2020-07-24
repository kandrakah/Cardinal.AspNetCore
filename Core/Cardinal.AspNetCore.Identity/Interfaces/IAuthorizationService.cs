using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Cardinal.AspNetCore.Identity
{
    /// <summary>
    /// Interface do serviço de autorização
    /// </summary>
    public interface IAuthorizationService
    {
        /// <summary>
        /// Método que efetua a validação da autorização de acesso.
        /// </summary>
        /// <param name="context">Contexto de autorização</param>
        /// <param name="requiredPermission">Requerimento de permissão</param>
        void Authorize(AuthorizationFilterContext context, PermissionsAuthorizationRequirement requiredPermission);

        /// <summary>
        /// Método que efetua a verificação de permissão de uma identidade considerando o requerimento de permissão informado.
        /// </summary>
        /// <param name="identity">Identidade do usuário</param>
        /// <param name="requiredPermission">Requerimento de permissão</param>
        /// <returns>Verdadeiro caso o usuário atenda aos critérios requisitados e falso caso contrário</returns>
        bool HavePermission(ClaimsIdentity identity, PermissionsAuthorizationRequirement requiredPermission);

        /// <summary>
        /// Método que verifica se o usuário possui permissão máxima.
        /// </summary>
        /// <param name="identity">Identidade do usuário</param>
        /// <returns>Verdadeiro caso o usuário possua a permissão máxima e falso caso contrário</returns>
        bool IsRoot(ClaimsIdentity identity);
    }
}
