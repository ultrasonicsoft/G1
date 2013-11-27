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
        CustomerSettings,
        CommanderSection,
        SaleOrder,
        Worksheet
    }

    public partial class DashboardMenu : UserControl
    {
        private bool _isIndirectCall = false;
        public bool IsIndirectCall { get; set; }

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

            if (IsIndirectCall == false)
            {
                Dashboard parent = Window.GetWindow(this) as Dashboard;
                NewQuoteContent newQuote = new NewQuoteContent();
                parent.ucMainContent.ShowPage(newQuote);
            }
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

        private void btnCommanderSection_Checked(object sender, RoutedEventArgs e)
        {
            UpdateToggleButtonStatus(UserSelection.CommanderSection);

            Dashboard parent = Window.GetWindow(this) as Dashboard;
            CommanderSectionContent commanderSection = new CommanderSectionContent();
            parent.ucMainContent.ShowPage(commanderSection);
        }

        private void btnSaleOrder_Checked(object sender, RoutedEventArgs e)
        {
            UpdateToggleButtonStatus(UserSelection.SaleOrder);

            Dashboard parent = Window.GetWindow(this) as Dashboard;
            SalesOrderContent soSection = new SalesOrderContent();
            parent.ucMainContent.ShowPage(soSection);
        }

        private void btnWorksheet_Checked(object sender, RoutedEventArgs e)
        {
            UpdateToggleButtonStatus(UserSelection.Worksheet);

            Dashboard parent = Window.GetWindow(this) as Dashboard;
            WorksheetContent worksheetSection = new WorksheetContent();
            parent.ucMainContent.ShowPage(worksheetSection);
        }

        private void UpdateToggleButtonStatus(UserSelection selection)
        {
            switch (selection)
            {
                case UserSelection.Home:
                    SetHomeAsCurrentPage();
                    break;
                case UserSelection.NewQuote:
                    SetNewQuoteAsCurrentPage();
                    break;
                case UserSelection.RateSettings:
                    SetPriceSettingAsCurrentPage();
                    break;
                case UserSelection.CustomerSettings:
                    SetCustomerSettingsAsCurrentPage();
                    break;
                case UserSelection.CommanderSection:
                    SetCommanderSectionAsCurrentPage();
                    break;
                case UserSelection.SaleOrder:
                    SetSaleOrdersSectionAsCurrentPage();
                    break;
                case UserSelection.Worksheet:
                    SetWorksheetSectionAsCurrentPage();
                    break;
                default:
                    break;
            }
        }

        private void SetWorksheetSectionAsCurrentPage()
        {
            btnHome.IsChecked = false;
            btnPriceSettings.IsChecked = false;
            btnCustomerSettings.IsChecked = false;
            btnCommanderSection.IsChecked = false;
            btnCreateNewQuote.IsChecked = false;
            btnSaleOrder.IsChecked = false;
        }

        private void SetSaleOrdersSectionAsCurrentPage()
        {
            btnHome.IsChecked = false;
            btnPriceSettings.IsChecked = false;
            btnCustomerSettings.IsChecked = false;
            btnCommanderSection.IsChecked = false;
            btnCreateNewQuote.IsChecked = false;
            btnWorksheet.IsChecked = false;
        }

        internal void SetCommanderSectionAsCurrentPage()
        {
            btnHome.IsChecked = false;
            btnCreateNewQuote.IsChecked = false;
            btnPriceSettings.IsChecked = false;
            btnCustomerSettings.IsChecked = false;
        }

        internal void SetCustomerSettingsAsCurrentPage()
        {
            btnHome.IsChecked = false;
            btnCreateNewQuote.IsChecked = false;
            btnPriceSettings.IsChecked = false;
            btnCommanderSection.IsChecked = false;
        }

        internal void SetPriceSettingAsCurrentPage()
        {
            btnHome.IsChecked = false;
            btnCreateNewQuote.IsChecked = false;
            btnCustomerSettings.IsChecked = false;
            btnCommanderSection.IsChecked = false;
        }

        internal void SetNewQuoteAsCurrentPage()
        {
            btnHome.IsChecked = false;
            btnPriceSettings.IsChecked = false;
            btnCustomerSettings.IsChecked = false;
            btnCommanderSection.IsChecked = false;
        }

        internal void SetHomeAsCurrentPage()
        {
            btnCreateNewQuote.IsChecked = false;
            btnPriceSettings.IsChecked = false;
            btnCustomerSettings.IsChecked = false;
            btnCommanderSection.IsChecked = false;
        }

       
    }
}
