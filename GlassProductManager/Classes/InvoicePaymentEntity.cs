using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    public class InvoicePaymentEntity
    {
        public int ID { get; set; }
        public string PaymentDate { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
    }
}
