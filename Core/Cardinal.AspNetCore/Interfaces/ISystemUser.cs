using System.Collections.Generic;
using System.Security.Claims;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Interface que representa um usuário do sistema.
    /// </summary>
    public interface ISystemUser
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        string Sub { get; }

        /// <summary>
        /// Atributo que representa o nome de usuário.
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Atributo que representa o nome apresentável do usuário.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Identificador único do cliente utilizado pelo usuário.
        /// </summary>
        string ClientId { get; }

        /// <summary>
        /// Atributo que representa o Ip local do usuário.
        /// </summary>
        string LocalIpAddress { get; }

        /// <summary>
        /// Atributo que representa o Ip remoto do usuário.
        /// </summary>
        string RemoteIpAddress { get; }

        /// <summary>
        /// Atributo que indica se o usuário está autenticado.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Atributo com a lista de permissões do usuário.
        /// </summary>
        IEnumerable<Claim> Permissions { get; }
        
        /// <summary>
        /// Método que obtém uma claim do usuário de um tipo declarado. 
        /// Caso hajam várias claims do mesmo tipo, a primeira será retornada.
        /// </summary>
        /// <param name="type">Tipo da claim desejada.</param>
        /// <returns>Claim referente ao tipo solicitado ou null caso a mesma não seja encontrada.</returns>
        Claim GetClaim(string type);

        /// <summary>
        /// Método que obtém uma enumeração de claims do usuário de um tipo declarado.
        /// </summary>
        /// <param name="type">tipo das claims desejadas</param>
        /// <returns>Enumeração de claims do tipo solicitado.</returns>
        IEnumerable<Claim> GetClaims(string type);

        /// <summary>
        /// Método que verifica se o usuário tem determinada permissão.
        /// </summary>
        /// <param name="permission">Permissão requerida.</param>
        /// <returns>Verdadeiro caso o usuário possua a permissão requerida e falso caso contrário.</returns>
        bool HavePermission(string permission);

        /// <summary>
        /// Método que verifica se o usuário possui a permissão mestre.
        /// </summary>
        /// <returns>Verdadeiro caso o usuário possua a permissão mestre e falso caso contrário.</returns>
        bool IsRoot();
    }
}
