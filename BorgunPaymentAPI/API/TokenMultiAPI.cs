using BorgunPayment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.API
{
    public class TokenMultiAPI : ITokenMultiAPI
    {
        private HttpClient client;

        public TokenMultiAPI(HttpClient client)
        {
            this.client = client;
        }

        public async Task<TokenMultiResponse> CreateAsync(TokenMultiRequest req)
        {
            TokenMultiResponse tokenRes = new TokenMultiResponse();
            HttpResponseMessage httpRes = await this.client.PostAsJsonAsync("api/token/multi", req);
            tokenRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                tokenRes.Token = await httpRes.Content.ReadAsAsync<TokenMultiInfo>();
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

        public async Task<TokenMultiResponse> GetAsync(string token)
        {
            TokenMultiResponse tokenRes = new TokenMultiResponse();
            HttpResponseMessage httpRes = await this.client.GetAsync("api/token/multi/" + token);
            tokenRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                TokenMultiInfo info = await httpRes.Content.ReadAsAsync<TokenMultiInfo>();
                tokenRes.Token = info;
            }
            else
            {
                tokenRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return tokenRes;
        }

        public async Task<TokenMultiResponse> DeleteAsync(string token)
        {
            TokenMultiResponse tokenRes = new TokenMultiResponse();
            HttpResponseMessage httpRes = await this.client.DeleteAsync("api/token/multi/" + token);
            tokenRes.StatusCode = (int)httpRes.StatusCode;
            return tokenRes;
        }
    }
}
