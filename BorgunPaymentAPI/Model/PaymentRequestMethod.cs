
namespace BorgunPayment.Model
{
    public class PaymentRequestMethod
    {
        /// <summary>
        /// Type of payment method.
        /// </summary>
        public PaymentTypes PaymentType { get; set; }

        /// <summary>
        /// Required if payment method type is card.
        /// </summary>
        public string PAN { get; set; }

        /// <summary>
        /// Format: yyyy
        /// Required if payment method type is card.
        /// </summary>
        public string ExpYear { get; set; }

        /// <summary>
        /// Format: mm
        /// Required if payment method type is card.
        /// </summary>
        public string ExpMonth { get; set; }

        /// <summary>
        /// Optional, can be provided if payment method type is card.
        /// </summary>
        public string CVC2 { get; set; }

        /// <summary>
        /// Required if payment method type is TokenSingle or TokenMulti.
        /// </summary>
        public string Token { get; set; }
    }
}