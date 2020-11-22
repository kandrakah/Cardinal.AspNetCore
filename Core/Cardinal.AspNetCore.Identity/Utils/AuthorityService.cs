//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Cardinal.AspNetCore.Identity.Utils
//{
//    internal class AuthorityService
//    {
//        internal HttpClient Client { get; }

//        internal AuthorityService(string authority, string token)
//        {
//            this.Client = new HttpClient();
//            this.Client.BaseAddress = new Uri(authority);
//            this.Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="cancellationToken"></param>
//        /// <returns></returns>
//        internal async Task<IEnumerable<Claim>> GetUserClaimsAsync(CancellationToken cancellationToken = default)
//        {
//            try
//            {
//                var result = await this.Client.GetAsync("connect/userinfo", cancellationToken).ConfigureAwait(false);
//                if (result.IsSuccessStatusCode)
//                {
//                    var resultData = await result.Content.ReadAsStringAsync();
//                    var data = JsonConvert.DeserializeObject<IDictionary<string, string>>(resultData);
//                    var claims = data.Select(x => new Claim(x.Key, x.Value)).ToList();
//                    return claims;
//                }
//                else
//                {
//                    var content = await result.Content.ReadAsStringAsync();
//                    throw new IdentityException(content);
//                }
//            }
//            catch (IdentityException ex)
//            {
//                throw ex;
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                throw ex;
//            }
//        }
//    }
//}
