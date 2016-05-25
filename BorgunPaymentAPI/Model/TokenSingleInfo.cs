using System;

namespace BorgunPayment.Model
{
    public class TokenSingleInfo
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Masked pan
        /// </summary>
        public string PAN { get; set; }

        /// <summary>
        /// Expiration year in format yyyy
        /// </summary>
        public string ExpYear { get; set; }

        /// <summary>
        /// Expiration month in format MM
        /// </summary>
        public string ExpMonth { get; set; }

        /// <summary>
        /// Is the token disabled manually ?
        /// NOTE: A token can still be enabled even if it has been used or expired.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Has the token been used.
        /// </summary>
        public bool Used 
        {
            get
            {
                return this.UsedTime.HasValue;
            }
        }

        /// <summary>
        /// Id of transaction that the token was used in.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Expiration date of token.
        /// </summary>
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// Time the token was used.
        /// </summary>
        public DateTime? UsedTime { get; set; }

        /// <summary>
        /// Info on verify card transaction.
        /// </summary>
        public VerifyCardResult VerifyCardResult { get; set; }

        /// <summary>
        /// Metadata.
        /// </summary>
        public Metadata Metadata { get; set; }
    }
}
