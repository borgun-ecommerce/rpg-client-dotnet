
namespace BorgunPayment.Model
{
    public class VerifyCardResult
    {
        /// <summary>
        /// Id of transaction that verified the card.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Action code received from the gateway during card verification.
        /// </summary>
        public string ActionCode { get; set; }
    }
}
