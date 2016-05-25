using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;

namespace BorgunPayment.UnitTests.Mock
{
    public class HttpMessageHandlerMock : HttpMessageHandler
    {
        private string tokenSingleInfoJson = "{'Token':'TestTokenSingle', 'PAN':'424242******4242', 'ExpYear':'2018', 'ExpMonth':'09', 'Enabled':true, 'Used':false, 'ValidUntil':'2016-03-13T13:05:01Z', 'VerifyCardResult':{ 'TransactionId':'tr_Hk4mlnlYxDyGBMpMB6_KiFy5Rj9TtxQs', 'ActionCode':'000' }, 'Metadata':{ 'Payload':'TEST' }}";

        private string tokenMultiInfoJson = "{'Token':'TestTokenMulti','PAN':'424242******4242','ExpYear':'2018','ExpMonth':'09','Enabled':true,'Metadata':{'Payload':'TEST'}}";

        private string transactionInfoJson = "{'TransactionId':'TestTransaction','TransactionType':'Sale','Amount':100,'Currency':'352','TransactionDate':'2015-10-10T11:00:00','OrderId':'INTEGR317682','AuthCode':'913574','ActionCode':'000','TransactionStatus':'Accepted','PaymentMethod':{'PaymentType':'Card','PAN':'424242******4242','ExpYear':'2018','ExpMonth':'09','CardType':'MasterCard'},'Metadata':{'Payload':'TEST'}}";

        private string cancelAuthorizationJson = "{'TransactionId':'TestTransaction','ActionCode':'000'}";

        private string refundAuthorizationJson = "{'TransactionId':'TestTransaction','ActionCode':'000'}";

        private string captureAuthorizationJson = "{'TransactionId':'TestTransaction','ActionCode':'000'}";

        public HttpRequestMessage RequestMessage { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RequestMessage = request;
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            // Token Single API
            if (request.RequestUri.AbsolutePath.StartsWith("/api/token/single"))
            {
                if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Post)
                {
                    response.Content = new StringContent(this.tokenSingleInfoJson);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    if (request.Method == HttpMethod.Post)
                    {
                        response.StatusCode = HttpStatusCode.Created;
                        response.Headers.Location = new Uri("http://borgun/token");
                    }
                    
                }
                else if (request.Method == HttpMethod.Delete)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
            }

            // Token Multi API
            if (request.RequestUri.AbsolutePath.StartsWith("/api/token/multi"))
            {
                if (request.Method == HttpMethod.Get || request.Method == HttpMethod.Post)
                {
                    response.Content = new StringContent(this.tokenMultiInfoJson);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    if (request.Method == HttpMethod.Post)
                    {
                        response.StatusCode = HttpStatusCode.Created;
                        response.Headers.Location = new Uri("http://borgun/token");
                    }

                }
                else if (request.Method == HttpMethod.Delete)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
            }

            // Payment API
            if (request.RequestUri.AbsolutePath.StartsWith("/api/payment"))
            {
                if (request.Method == HttpMethod.Post)
                {
                    response.StatusCode = HttpStatusCode.Created;
                    response.Content = new StringContent(this.transactionInfoJson);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response.Headers.Location = new Uri("http://borgun/transaction");
                }
                else if (request.Method == HttpMethod.Get)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StringContent(this.transactionInfoJson);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
                else if (request.Method == HttpMethod.Put)
                {
                    if (request.RequestUri.AbsolutePath.EndsWith("cancel"))
                    {
                        response.Content = new StringContent(this.cancelAuthorizationJson);
                    }
                    else if (request.RequestUri.AbsolutePath.EndsWith("refund"))
                    {
                        response.Content = new StringContent(this.refundAuthorizationJson);
                    }
                    else if(request.RequestUri.AbsolutePath.EndsWith("capture"))
                    {
                        response.Content = new StringContent(this.captureAuthorizationJson);
                    }

                    response.StatusCode = HttpStatusCode.OK;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
            }

            return Task.FromResult(response);
        }
    }
}
