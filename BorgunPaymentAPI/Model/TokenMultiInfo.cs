
namespace BorgunPayment.Model
{
    public class TokenMultiInfo
    {
        /// <summary>
        /// Generated token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Masked pan
        /// </summary>
        public string PAN { get; set; }

        /// <summary>
        /// ExpYear in format yyyy
        /// </summary>
        public string ExpYear { get; set; }

        /// <summary>
        /// ExpMonth in format mm
        /// </summary>
        public string ExpMonth { get; set; }

        /// <summary>
        /// Token enabled
        /// </summary>
        public bool Enabled { get; set; }

        public VerifyCardResult VerifyCardResult { get; set; }

        public Metadata Metadata { get; set; }
    }
}
