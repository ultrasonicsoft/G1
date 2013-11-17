using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class GlassRateEntity
    {
        public int GlassID { get; set; }
        public int ThicknessID { get; set; }
        public double CutoutSqFtRate { get; set; }
        public double TemperedRate { get; set; }
        public double PolishStraightRate { get; set; }
        public double PolishShapeRate { get; set; }
        public double MiterRate { get; set; }
    }
}
