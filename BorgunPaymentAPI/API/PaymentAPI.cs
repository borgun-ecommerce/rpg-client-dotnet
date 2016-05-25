using BorgunPayment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.API
{
    public class PaymentAPI : IPaymentAPI
    {
        private HttpClient client;

        public PaymentAPI(HttpClient client)
        {
            this.client = client;
        }

        public async Task<PaymentTransactionResponse> CreateAsync(PaymentRequest req)
        {
            PaymentTransactionResponse paymentRes = new PaymentTransactionResponse();
            HttpResponseMessage httpRes = await this.client.PostAsJsonAsync("api/payment", req);
            paymentRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                paymentRes.Transaction = await httpRes.Content.ReadAsAsync<TransactionInfo>();
                if (httpRes.Headers.Location != null)
                {
                    paymentRes.Uri = httpRes.Headers.Location.AbsoluteUri;
                }
            }
            else
            {
                paymentRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return paymentRes;
        }

        public async Task<PaymentTransactionResponse> GetTransactionAsync(string transactionId)
        {
            PaymentTransactionResponse paymentRes = new PaymentTransactionResponse();
            HttpResponseMessage httpRes = await this.client.GetAsync("api/payment/" + transactionId);
            paymentRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                paymentRes.Transaction = await httpRes.Content.ReadAsAsync<TransactionInfo>();
            }
            else
            {
                paymentRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return paymentRes;
        }

        public async Task<PaymentCancelResponse> CancelAsync(string transactionId)
        {
            PaymentCancelResponse paymentRes = new PaymentCancelResponse();
            HttpResponseMessage httpRes = await this.client.PutAsync("api/payment/" + transactionId + "/cancel", null);
            paymentRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                paymentRes.Result = await httpRes.Content.ReadAsAsync<CancelAuthorizationResponse>();
            }
            else
            {
                paymentRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return paymentRes;
        }

        public async Task<PaymentCaptureResponse> CaptureAsync(string transactionId)
        {
            PaymentCaptureResponse paymentRes = new PaymentCaptureResponse();
            HttpResponseMessage httpRes = await this.client.PutAsync("api/payment/" + transactionId + "/capture", null);
            paymentRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                paymentRes.Result = await httpRes.Content.ReadAsAsync<CaptureAuthorizationResponse>();
            }
            else
            {
                paymentRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return paymentRes;
        }

        public async Task<PaymentRefundResponse> RefundAsync(string transactionId)
        {
            PaymentRefundResponse paymentRes = new PaymentRefundResponse();
            HttpResponseMessage httpRes = await this.client.PutAsync("api/payment/" + transactionId + "/refund", null);
            paymentRes.StatusCode = (int)httpRes.StatusCode;

            if (httpRes.IsSuccessStatusCode)
            {
                paymentRes.Result = await httpRes.Content.ReadAsAsync<RefundAuthorizationResponse>();
            }
            else
            {
                paymentRes.Message = await httpRes.Content.ReadAsStringAsync();
            }

            return paymentRes;
        }
    }
}
