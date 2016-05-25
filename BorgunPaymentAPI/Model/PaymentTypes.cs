using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BorgunPayment.Model
{
    public enum PaymentTypes
    {
        Unknown = 1,
        Card = 2,
        TokenSingle = 3,
        TokenMulti = 4
    }
}