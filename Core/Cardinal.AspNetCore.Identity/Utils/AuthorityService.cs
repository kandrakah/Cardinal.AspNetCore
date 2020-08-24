using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiny.RestClient;

namespace Cardinal.AspNetCore.Identity.Utils
{
    internal class AuthorityService
    {
        internal TinyRestClient Client { get; }

        internal AuthorityService(string authority, string token)
        {
            this.Client = new TinyRestClient(new HttpClient(), authority);
            this.Client.Settings.DefaultHeaders.AddBearer(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        internal async Task<IEnumerable<Claim>> GetUserClaimsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await this.Client.GetRequest("connect/userinfo").ExecuteAsync<IDictionary<string, string>>(cancellationToken);
                var claims = result.Select(x => new Claim(x.Key, x.Value)).ToList();                
                return claims;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
