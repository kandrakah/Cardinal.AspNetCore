using Cardinal.AspNetCore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Cardinal.Extensions
{
    /// <summary>
    /// Extensões para <see cref="HttpClient"/>
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Extensão para busca dos dados do usuário junto à autoridade.
        /// </summary>
        /// <param name="client">Objeto referenciado</param>
        /// <param name="authorityUri">Endereço da autoridade.</param>
        /// <param name="token">Token de acesso do usuário.</param>
        /// <param name="cancellationToken">Token de cancelamento de operação assíncrona.</param>
        /// <returns>Dados do usuário</returns>
        public static async Task<UserInfo> GetUserInfoAsync(this HttpClient client, string authorityUri, string token, CancellationToken cancellationToken = default)
        {
            try
            {
                client.BaseAddress = new Uri(authorityUri);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = await client.GetAsync("connect/userinfo", cancellationToken);
                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();
                    var userInfo = JsonConvert.DeserializeObject<UserInfo>(json);
                    return userInfo;
                }
                else
                {
                    var content = await result.Content.ReadAsStringAsync();
                    throw new IdentityException(result.StatusCode, content);
                }
            }
            catch (IdentityException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
