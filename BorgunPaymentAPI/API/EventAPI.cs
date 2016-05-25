using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.API
{
    public class EventAPI
    {
        private HttpClient client;

        public EventAPI(HttpClient client)
        {
            this.client = client;
        }

    }
}
