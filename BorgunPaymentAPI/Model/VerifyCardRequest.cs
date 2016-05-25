namespace BorgunPayment.Model
{
    public class VerifyCardRequest
    {
        /// <summary>
        /// Amount of verify card request.
        /// </summary>
        public int CheckAmount { get; set; }

        /// <summary>
        /// Currency of verify card request.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Used if provided.
        /// </summary>
        public string CVC { get; set; }
    }
}
