
namespace BorgunPayment.Model
{
    public class TokenMultiRequest
    {
        /// <summary>
        /// Cardnumber
        /// </summary>
        public string PAN { get; set; }

        /// <summary>
        /// Expiration month in format MM.
        /// </summary>
        public string ExpMonth { get; set; }

        /// <summary>
        /// Expiration year in format yyyy.
        /// </summary>
        public string ExpYear { get; set; }

        /// <summary>
        /// If provided, the card will be verified by authorizing the card.
        /// </summary>
        public VerifyCardRequest VerifyCard { get; set; }

        /// <summary>
        /// Merchant Metadata connected to the token.
        /// </summary>
        public Metadata Metadata { get; set; }
    }
}
