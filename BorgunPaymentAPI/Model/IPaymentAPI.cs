using System.Threading.Tasks;

namespace BorgunPayment.Model
{
    public interface IPaymentAPI
    {
        Task<PaymentCancelResponse> CancelAsync(string transactionId);

        Task<PaymentCaptureResponse> CaptureAsync(string transactionId);

        Task<PaymentTransactionResponse> CreateAsync(PaymentRequest req);

        Task<PaymentTransactionResponse> GetTransactionAsync(string transactionId);

        Task<PaymentRefundResponse> RefundAsync(string transactionId);
    }
}