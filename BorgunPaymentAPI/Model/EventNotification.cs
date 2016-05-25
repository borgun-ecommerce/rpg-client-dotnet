using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorgunPayment.Model
{
    public class EventNotification
    {
        public string EventId { get; set; }

        public SubEventType EventType { get; set; }

        public DateTime Created { get; set; }

        public object Data { get; set; }
    }
}
