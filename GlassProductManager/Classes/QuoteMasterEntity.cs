using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class QuoteMasterEntity
    {
        public string QuoteStatus { get; set; }
        public string QuoteNumber { get; set; }
        public string FullName { get; set; }
        public string CreatedOn { get; set; }
        public string Total { get; set; }
        public string EstimatedShipDate { get; set; }
        public string PaymentType { get; set; }
        public string CustomerPONumber { get; set; }
    }

}
