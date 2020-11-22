using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Cardinal.AspNetCore
{
    /// <summary>
    /// Classe que representa as informações de um usuário providas pela autoridade.
    /// </summary>
    public class UserInfo : Dictionary<string, object>
    {
        /// <summary>
        /// Método que converte este objeto em uma enumeração de claims. Veja <see cref="Claim"/>
        /// </summary>
        /// <returns>Enumeração de claims do usuário.</returns>
        public IEnumerable<Claim> ToClaims()
        {
            var claims = new HashSet<Claim>();
            foreach (var item in this)
            {
                if (item.Value is string)
                {
                    claims.Add(new Claim(item.Key, item.Value.ToString()));
                }
                else if (item.Value is JArray)
                {
                    var array = item.Value as JArray;
                    foreach (var itm in array)
                    {
                        claims.Add(new Claim(item.Key, itm.ToString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(item.Key, item.Value.ToString()));
                }
            }
            return claims;
        }

        /// <summary>
        /// Método que traz uma cadeia de caracteres que representa o objeto atual.
        /// </summary>
        /// <returns>Cadeia de caracteres que representa o objeto atual.</returns>
        public override string ToString()
        {
            var keys = new List<string> { DefaultClaims.NAME, DefaultClaims.NICKNAME, DefaultClaims.PREFERED_USERNAME, DefaultClaims.SUB };
            var name = this.Where(x => keys.Contains(x.Key)).Select(x => x.Value).FirstOrDefault();
            return name?.ToString();
        }
    }
}
