using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class QuoteEntity
    {
        internal QuoteHeader Header { get; set; }
        internal ObservableCollection<QuoteGridEntity> LineItems { get; set; }
        internal QuoteFooter Footer { get; set; }
    }
}
