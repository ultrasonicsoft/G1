using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for HomeContent.xaml
    /// </summary>
    public partial class HomeContent : UserControl
    {
        public HomeContent()
        {
            InitializeComponent();

            GetPrintQueueNotificationCount();
        }

        private void GetPrintQueueNotificationCount()
        {
            int result = BusinessLogic.GetPrintQueueNotificationCount();
            lblNotification.Content = string.Format("Total label printing requests in queue: {0}", result);
        }

        private void btnOpenNotification_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dashboard parent = Window.GetWindow(this) as Dashboard;
                if (parent != null)
                {
                    DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                    DashboardHelper.ChangeDashboardSelection(parent, sideMenu.btnBarcodePrinter);
                    BarcodePrinter barcodePrinter = new BarcodePrinter();
                    parent.ucMainContent.ShowPage(barcodePrinter);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}
