using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    public class PriceEntity
    {
        public string GlassType { get; set; }
        public string Thickness { get; set; }
        public string CutSQFT { get; set; }
        public string TemperedSQFT { get; set; }
        public string PolishStraight { get; set; }
        public string PolishShape { get; set; }
        public string MiterRate { get; set; }
    }

}
