using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Cardinal.Extensions;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe que representa um usuário ativo no sistema.
    /// </summary>
    public class SystemUser : ISystemUser
    {
        /// <summary>
        /// Identificador único do usuário.
        /// </summary>
        public string Sub => this.GetSub();

        /// <summary>
        /// Atributo que representa o nome de usuário.
        /// </summary>
        public string Username => this.GetUsername();

        /// <summary>
        /// Atributo que representa o nome apresentável do usuário.
        /// </summary>
        public string DisplayName => this.GetDisplayName();

        /// <summary>
        /// Identificador único do cliente utilizado pelo usuário.
        /// </summary>
        public string ClientId => this.GetClientId();

        /// <summary>
        /// Atributo que indica se o usuário está autenticado.
        /// </summary>
        public bool IsAuthenticated => this.Authenticated();

        /// <summary>
        /// Atributo que representa o Ip local do usuário.
        /// </summary>
        public string LocalIpAddress => this.GetLocalIpAddress();

        /// <summary>
        /// Atributo que representa o Ip remoto do usuário.
        /// </summary>
        public string RemoteIpAddress => this.GetRemoteIpAddress();

        /// <summary>
        /// Atributo com a lista de permissões do usuário.
        /// </summary>
        public IEnumerable<Claim> Permissions => GetPermissions();

        /// <summary>
        /// Accessor
        /// </summary>
        private IHttpContextAccessor Accessor { get; }

        /// <summary>
        /// Identidade principal do usuário.
        /// </summary>
        private ClaimsPrincipal Principal { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="accessor">Instância do acessor</param>
        public SystemUser(IHttpContextAccessor accessor)
        {
            this.Accessor = accessor;
            this.Principal = this.Accessor.HttpContext.User;
        }

        /// <summary>
        /// Método que obtém uma claim do usuário de um tipo declarado. 
        /// Caso hajam várias claims do mesmo tipo, a primeira será retornada.
        /// </summary>
        /// <param name="type">Tipo da claim desejada.</param>
        /// <returns>Claim referente ao tipo solicitado ou null caso a mesma não seja encontrada.</returns>
        public Claim GetClaim(string type)
        {
            return this.Principal.FindFirst(type);
        }

        /// <summary>
        /// Método que obtém uma enumeração de claims do usuário de um tipo declarado.
        /// </summary>
        /// <param name="type">tipo das claims desejadas</param>
        /// <returns>Enumeração de claims do tipo solicitado.</returns>
        public IEnumerable<Claim> GetClaims(string type)
        {
            return this.Principal.FindAll(type);
        }

        /// <summary>
        /// Método que obtém o id único do usuário.
        /// </summary>
        /// <returns></returns>
        private string GetSub()
        {
            return this.GetClaim("sub")?.Value;
        }

        /// <summary>
        /// Método que obtém o nome de usuário.
        /// </summary>
        /// <returns>Nome de usuário.</returns>
        private string GetUsername()
        {
            var username = this.GetClaim("username")?.Value;
            if (username.IsPresent())
            {
                return username;
            }

            var nickName = this.GetClaim("nickname")?.Value;
            if (nickName.IsPresent())
            {
                return nickName;
            }

            var name = this.Principal.Identity.Name;
            if (name.IsPresent())
            {
                return name;
            }

            var sub = this.GetSub();
            if (sub.IsPresent())
            {
                return sub;
            }

            return string.Empty;
        }

        /// <summary>
        /// Método que obtém o nome apresentável do usuário.
        /// </summary>
        /// <returns>Nome apresentável do usuário.</returns>
        private string GetDisplayName()
        {
            var preferedUsername = this.GetClaim("preferred_username")?.Value;
            if (preferedUsername.IsPresent())
            {
                return preferedUsername;
            }

            var givenName = this.GetClaim("given_name")?.Value;
            var familyName = this.GetClaim("family_name")?.Value;
            if (givenName.IsPresent())
            {
                return $"{givenName} {familyName}".Trim();
            }

            return this.GetUsername();
        }

        /// <summary>
        /// Método que obtém o Id único do cliente usado pelo usuário.
        /// </summary>
        /// <returns>Id único do cliente do usuário.</returns>
        private string GetClientId()
        {
            return this.GetClaim("client_id")?.Value;
        }

        /// <summary>
        /// Método que traz se o usuário está autenticado.
        /// </summary>
        /// <returns></returns>
        private bool Authenticated()
        {
            return this.Principal != null ? this.Principal.Identity.IsAuthenticated : false;
        }

        /// <summary>
        /// Método que obtém o Ip local da conexão.
        /// </summary>
        /// <returns>Ip local da conexão.</returns>
        private string GetLocalIpAddress()
        {
            return this.Accessor.HttpContext.Connection.LocalIpAddress.ToString();
        }

        /// <summary>
        /// Método que obtém o Ip remoto da conexão.
        /// </summary>
        /// <returns>Ip remoto da conexão.</returns>
        private string GetRemoteIpAddress()
        {
            return this.Accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        /// Método que obtém as permissões do usuário.
        /// </summary>
        /// <returns>Enumerador de permissões do usuário.</returns>
        private IEnumerable<Claim> GetPermissions()
        {
            return this.GetClaims("permission")?.Select(x => new Claim(x.Type, x.Value)).ToList();
        }

        /// <summary>
        /// Método que verifica se o usuário tem determinada permissão.
        /// </summary>
        /// <param name="permission">Permissão requerida.</param>
        /// <returns>Verdadeiro caso o usuário possua a permissão requerida e falso caso contrário.</returns>
        public bool HavePermission(string permission)
        {
            return this.GetClaims("permission").Where(x => x.Value == permission).Any();
        }

        /// <summary>
        /// Método que verifica se o usuário possui a permissão mestre.
        /// </summary>
        /// <returns>Verdadeiro caso o usuário possua a permissão mestre e falso caso contrário.</returns>
        public bool IsRoot()
        {
            return this.HavePermission("ROOT");
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            return $"[SUB:{this.Sub}][NAME:{this.DisplayName}][CLIENT:{this.ClientId}]";
        }
    }
}
