using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.Model
{
    public class PaymentTransactionResponse
    {
        /// <summary>
        /// Http status code of response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Transaction object received by the response.
        /// </summary>
        public TransactionInfo Transaction { get; set; }

        /// <summary>
        /// ContentLocation header if provided by response.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// Message provided by response in case of errors / warnings.
        /// </summary>
        public string Message { get; set; }
    }
}
