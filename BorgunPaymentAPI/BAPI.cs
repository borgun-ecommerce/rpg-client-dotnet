using BorgunPayment.API;
using BorgunPayment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace BorgunPayment
{
    public class RPGClient : IRPGClient
    {
        private HttpClient client;

        private ITokenSingleAPI tokenSingle;

        private ITokenMultiAPI tokenMulti;

        private IPaymentAPI payment;

        public ITokenSingleAPI TokenSingle { get { return tokenSingle; } }

        public ITokenMultiAPI TokenMulti { get { return tokenMulti; } }

        public IPaymentAPI Payment { get { return payment; } }

        public RPGClient(string merchantKey, string serviceUri, HttpMessageHandler httpMessageHandler)
        {
            this.client = new HttpClient(httpMessageHandler);

            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client.BaseAddress = new Uri(serviceUri);
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(merchantKey + ":")));

            this.tokenSingle = new TokenSingleAPI(this.client);
            this.tokenMulti = new TokenMultiAPI(this.client);
            this.payment = new PaymentAPI(this.client);
        }

        public RPGClient(string merchantKey, string serviceUri) : this(merchantKey, serviceUri, new HttpClientHandler())
        {

        }

        public RPGClient(IPaymentAPI paymentApi, ITokenSingleAPI tokenSingleApi, ITokenMultiAPI tokenMultiApi)
        {
            this.payment = paymentApi;
            this.tokenSingle = tokenSingleApi;
            this.tokenMulti = tokenMultiApi;
        }
    }
}
