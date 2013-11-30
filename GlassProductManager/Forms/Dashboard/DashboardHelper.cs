using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class DashboardHelper
    {
        internal static void ChangeDashboardSelection(Dashboard parent, System.Windows.Controls.Primitives.ToggleButton selectedOption)
        {
            if (parent != null)
            {
                DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                if (sideMenu != null)
                {
                    sideMenu.IsIndirectCall = true;
                    sideMenu.ShowCurrentPage(selectedOption);
                    sideMenu.IsIndirectCall = false;
                }
            }
        }
    }
}
