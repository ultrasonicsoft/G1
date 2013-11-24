using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class InvoiceEntity
    {
        public string InvoiceNumber { get; set; }
        public string QuoteNumber { get; set; }
        public string FullName { get; set; }
        public string Total { get; set; }
        public string CompletedDate { get; set; }
    }

}
