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

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for DashboardMenu.xaml
    /// </summary>
    public enum UserSelection
    {
        Home,
        NewQuote,
        RateSettings,
        CustomerSettings
    }

    public partial class DashboardMenu : UserControl
    {
        public DashboardMenu()
        {
            InitializeComponent();

            btnHome.IsChecked = true;
        }

        private void btnHome_Checked(object sender, RoutedEventArgs e)
        {
            UpdateToggleButtonStatus(UserSelection.Home);

            Dashboard parent = Window.GetWindow(this) as Dashboard;
            if (parent != null)
            {
                HomeContent homeContent = new HomeContent();
                parent.ucMainContent.ShowPage(homeContent);
            }
        }

        private void btnCreateNewQuote_Checked(object sender, RoutedEventArgs e)
        {
            UpdateToggleButtonStatus(UserSelection.NewQuote);

            Dashboard parent = Window.GetWindow(this) as Dashboard;
            NewQuoteContent newQuote = new NewQuoteContent();
            parent.ucMainContent.ShowPage(newQuote);

        }

        private void btnCustomerSettings_Checked(object sender, RoutedEventArgs e)
        {
            UpdateToggleButtonStatus(UserSelection.CustomerSettings);
            Dashboard parent = Window.GetWindow(this) as Dashboard;
            CustomerSettingsContent newQuote = new CustomerSettingsContent();
            parent.ucMainContent.ShowPage(newQuote);
        }

        private void btnPriceSettings_Checked(object sender, RoutedEventArgs e)
        {
            UpdateToggleButtonStatus(UserSelection.RateSettings);

            Dashboard parent = Window.GetWindow(this) as Dashboard;
            PriceSettingsContent priceSettings = new PriceSettingsContent();
            parent.ucMainContent.ShowPage(priceSettings);
        }

        private void UpdateToggleButtonStatus(UserSelection selection)
        {
            switch (selection)
            {
                case UserSelection.Home:
                    btnCreateNewQuote.IsChecked = false;
                    btnPriceSettings.IsChecked = false;
                    btnCustomerSettings.IsChecked = false;
                    break;
                case UserSelection.NewQuote:
                    btnHome.IsChecked = false;
                    btnPriceSettings.IsChecked = false;
                    btnCustomerSettings.IsChecked = false;
                    break;
                case UserSelection.RateSettings:
                    btnHome.IsChecked = false;
                    btnCreateNewQuote.IsChecked = false;
                    btnCustomerSettings.IsChecked = false;
                    break;
                case UserSelection.CustomerSettings:
                    btnHome.IsChecked = false;
                    btnCreateNewQuote.IsChecked = false;
                    btnPriceSettings.IsChecked = false;
                    break;
                default:
                    break;
            }
        }

       
    }
}
