using System;
using System.Collections.Generic;

namespace BorgunPayment.Model
{
    public class TransactionInfo
    {
        /// <summary>
        /// Unique Id of this transaction.
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Type of transaction, preauth or sale.
        /// </summary>
        public TransactionTypes TransactionType { get; set; }
        
        /// <summary>
        /// Transaction amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Transaction currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// RRN
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Authorization code of transaction
        /// </summary>
        public string AuthCode { get; set; }

        /// <summary>
        /// Action code of transaction.
        /// </summary>
        public string ActionCode { get; set; }

        /// <summary>
        /// Status of transaction.
        /// </summary>
        public TransactionStatus TransactionStatus { get; set; }

        /// <summary>
        /// Payment method of transaction
        /// </summary>
        public TransactionInfoMethod PaymentMethod { get; set; }

        public Metadata Metadata { get; set; }

        public List<string> ErrorMessages { get; set; }

        public string Message { get; set; }
    }
}