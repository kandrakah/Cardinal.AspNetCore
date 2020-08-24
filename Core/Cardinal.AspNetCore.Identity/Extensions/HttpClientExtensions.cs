using Cardinal.AspNetCore;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tiny.RestClient;

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
        /// <returns>Dados do usuário</returns>
        public static async Task<UserInfo> GetUserInfoAsync(this HttpClient client, string authorityUri, string token)
        {
            var tinyClient = new TinyRestClient(client, authorityUri);
            try
            {
                tinyClient.Settings.DefaultHeaders.AddBearer(token);
                var userInfo = await tinyClient.GetRequest("connect/userinfo").ExecuteAsync<UserInfo>();
                return userInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
