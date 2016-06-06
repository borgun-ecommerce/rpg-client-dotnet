using BorgunPayment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.API
{
    public class TokenSingleAPI : ITokenSingleAPI
    {
        private HttpClient client;

        public TokenSingleAPI(HttpClient client)
        {
            this.client = client;
        }

        public async Task<TokenSingleResponse> CreateAsync(TokenSingleRequest req)
        {
            TokenSingleResponse tokenRes = new TokenSingleResponse();
            HttpResponseMessage httpRes = await this.client.PostAsJsonAsync("api/token/single", req);
            tokenRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                tokenRes.Token = await httpRes.Content.ReadAsAsync<TokenSingleInfo>();
                if (httpRes.Headers.Location != null)
                {
                    tokenRes.Uri = httpRes.Headers.Location.AbsoluteUri;
                }
            }
            else
            {
                tokenRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return tokenRes;
        }

        public async Task<TokenSingleResponse> GetAsync(string token)
        {
            TokenSingleResponse tokenRes = new TokenSingleResponse();
            HttpResponseMessage httpRes = await this.client.GetAsync("api/token/single/" + token);
            tokenRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                TokenSingleInfo info = await httpRes.Content.ReadAsAsync<TokenSingleInfo>();
                tokenRes.Token = info;
            }
            else
            {
                tokenRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return tokenRes;
        }

        public async Task<TokenSingleResponse> DisableAsync(string token)
        {
            TokenSingleResponse tokenRes = new TokenSingleResponse();
            HttpResponseMessage httpRes = await this.client.PutAsync("api/token/single/" + token + "/disable", null);
            tokenRes.StatusCode = (int)httpRes.StatusCode;

            return tokenRes;
        }
    }
}
