using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class WorksheetItemEntity
    {
        public string WSNumber { get; set; }
        public int ItemID { get; set; }
        public int LineID { get; set; }
        public string Status { get; set; }
        public string ModifiedByOperator { get; set; }
    }
}
