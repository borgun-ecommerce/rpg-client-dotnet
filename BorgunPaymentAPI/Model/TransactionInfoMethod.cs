
namespace BorgunPayment.Model
{
    public class TransactionInfoMethod
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
        /// Required if payment method type is TokenSingle or TokenMulti.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Information on CardType.
        /// </summary>
        public string CardType { get; set; }

        /// <summary>
        /// Information if the card is debit card or credit card.
        /// </summary>
        public string IsDebit { get; set; }
    }
}
