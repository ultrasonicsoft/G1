using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    public class WorksheetLineItemEntity
    {
        public int ID { get; set; }
        public string LineID { get; set; }
        public string ItemID { get; set; }
        public string OperatorName { get; set; }
        public string ModifiedOn { get; set; }
        public string Status { get; set; }

    }
}
