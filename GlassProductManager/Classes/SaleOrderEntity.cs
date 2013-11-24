using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class SaleOrderEntity
    {
        public string SaleOrderNumber { get; set; }
        public string QuoteNumber { get; set; }
        public string FullName { get; set; }
        public string RecordedDate { get; set; }
        public string Total { get; set; }
        public string Balance { get; set; }
        public string PaymentType { get; set; }
        public string WorksheetNumber { get; set; }
        public string CustomerPONumber { get; set; }
    }

}
