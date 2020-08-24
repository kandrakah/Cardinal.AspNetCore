﻿using Cardinal.AspNetCore.Identity;
using Cardinal.Extensions;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe que representa um usuário ativo no sistema.
    /// </summary>
    public class IdentityUser : ISystemUser
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
        /// Provedor de serviços.
        /// </summary>
        private IServiceProvider Provider { get; }

        /// <summary>
        /// Identidade principal do usuário.
        /// </summary>
        private ClaimsPrincipal Principal { get; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="provider">Instância do provedor de serviços</param>
        public IdentityUser(IServiceProvider provider)
        {
            this.Provider = provider;
            this.Principal = this.GetPrincipal();
        }

        private ClaimsPrincipal GetPrincipal()
        {
            var accessor = this.Provider.GetCardinalService<IHttpContextAccessor>();
            return accessor.HttpContext.User;
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
            return this.GetClaim(JwtClaimTypes.Subject)?.Value;
        }

        /// <summary>
        /// Método que obtém o nome de usuário.
        /// </summary>
        /// <returns>Nome de usuário.</returns>
        private string GetUsername()
        {
            var preferedUsername = this.GetClaim(JwtClaimTypes.PreferredUserName)?.Value;
            if (preferedUsername.IsPresent())
            {
                return preferedUsername;
            }

            var username = this.GetClaim("username")?.Value;
            if (username.IsPresent())
            {
                return username;
            }

            var nickName = this.GetClaim(JwtClaimTypes.NickName)?.Value;
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
            var givenName = this.GetClaim(JwtClaimTypes.GivenName)?.Value;
            var familyName = this.GetClaim(JwtClaimTypes.FamilyName)?.Value;
            if (givenName.IsPresent())
            {
                return $"{givenName} {familyName}".Trim();
            }

            var preferedUsername = this.GetClaim(JwtClaimTypes.PreferredUserName)?.Value;
            if (preferedUsername.IsPresent())
            {
                return preferedUsername;
            }

            return this.GetUsername();
        }

        /// <summary>
        /// Método que obtém o Id único do cliente usado pelo usuário.
        /// </summary>
        /// <returns>Id único do cliente do usuário.</returns>
        private string GetClientId()
        {
            return this.GetClaim(JwtClaimTypes.ClientId)?.Value;
        }

        /// <summary>
        /// Método que traz se o usuário está autenticado.
        /// </summary>
        /// <returns></returns>
        private bool Authenticated()
        {
            return this.Principal != null && this.Principal.Identity.IsAuthenticated;
        }

        /// <summary>
        /// Método que obtém o Ip local da conexão.
        /// </summary>
        /// <returns>Ip local da conexão.</returns>
        private string GetLocalIpAddress()
        {
            var accessor = this.Provider.GetCardinalService<IHttpContextAccessor>();
            return accessor.HttpContext.Connection.LocalIpAddress.ToString();
        }

        /// <summary>
        /// Método que obtém o Ip remoto da conexão.
        /// </summary>
        /// <returns>Ip remoto da conexão.</returns>
        private string GetRemoteIpAddress()
        {
            var accessor = this.Provider.GetCardinalService<IHttpContextAccessor>();
            return accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        /// <summary>
        /// Método que obtém as permissões do usuário.
        /// </summary>
        /// <returns>Enumerador de permissões do usuário.</returns>
        private IEnumerable<Claim> GetPermissions()
        {
            return this.GetClaims(CardinalJwtClaimTypes.Permission)?.Select(x => new Claim(x.Type, x.Value)).ToList();
        }

        /// <summary>
        /// Método que verifica se o usuário tem determinada permissão.
        /// </summary>
        /// <param name="permission">Permissão requerida.</param>
        /// <returns>Verdadeiro caso o usuário possua a permissão requerida e falso caso contrário.</returns>
        public bool HavePermission(string permission)
        {
            return this.Permissions.Where(x => x.Value.Equals( permission, StringComparison.OrdinalIgnoreCase)).Any();
        }

        /// <summary>
        /// Método que verifica se o usuário possui a permissão mestre.
        /// </summary>
        /// <returns>Verdadeiro caso o usuário possua a permissão mestre e falso caso contrário.</returns>
        public bool IsRoot()
        {
            return this.HavePermission(IdentityConstants.PERMISSION_ROOT_TAG);
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