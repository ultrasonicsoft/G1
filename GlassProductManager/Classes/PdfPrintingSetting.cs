using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Drawing;

namespace GlassProductManager
{
    internal class PdfPrintingSetting
    {
        // Create a normal font
        internal static XFont NormalFont = new XFont("Arial", 16, XFontStyle.Regular);

        // Create a Bold font
        internal static XFont BoldFont = new XFont("Arial", 16, XFontStyle.Bold);

       
    }
}
