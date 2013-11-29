using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class WorksheetEntity
    {
        public string WorksheetNumber { get; set; }
        public string QuoteNumber { get; set; }
        public string FullName { get; set; }
        public string CreatedOn { get; set; }
        public string DeliveryDate { get; set; }
        public string Progress { get; set; }
        public string TotalQuantity { get; set; }
    }

}
