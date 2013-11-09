using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class InsulationDetails 
    {
        public int GlassTypeID{ get; set; }
        public int ThicknessID { get; set; }
        public bool IsTempered { get; set; }
        public int SqFt { get; set; }
        public double Total { get; set; }
    }

  
}
