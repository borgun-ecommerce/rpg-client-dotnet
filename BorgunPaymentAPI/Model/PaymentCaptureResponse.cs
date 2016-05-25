using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.Model
{
    public class PaymentCaptureResponse
    {
        /// <summary>
        /// Http status code of response.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// CaptureAuthorizationResponse object received by the server.
        /// </summary>
        public CaptureAuthorizationResponse Result { get; set; }

        /// <summary>
        /// Message provided by response in case of errors / warnings.
        /// </summary>
        public string Message { get; set; }
    }
}
