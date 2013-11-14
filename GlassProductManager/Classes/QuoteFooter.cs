using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class QuoteFooter
    {
        internal string QuoteNumber { get; set; }
        internal double SubTotal { get; set; }
        internal bool IsDollar { get; set; }
        internal double EnergySurcharge { get; set; }
        internal double Discount { get; set; }
        internal double Delivery { get; set; }
        internal bool IsRushOrder { get; set; }
        internal double RushOrder { get; set; }
        internal double Tax { get; set; }
        internal double GrandTotal { get; set; }
    }
}
