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
        Worksheet,
        Invoice,
        MakePayment
    }

    public partial class DashboardMenu : UserControl
    {
        public bool IsIndirectCall { get; set; }

        Dictionary<System.Windows.Controls.Primitives.ToggleButton, UserControl> availableOptions = null;
        HomeContent home;
        NewQuoteContent newQuote;
        CustomerSettingsContent customer;
        PriceSettingsContent priceSettings;
        SalesOrderContent soSection;
        WorksheetContent worksheetSection;
        CommanderSectionContent commanderSection;
        InvoiceContent invoice;
        MakeInvoicePayment makePayment;

        public DashboardMenu()
        {
            InitializeComponent();

            availableOptions = new Dictionary<System.Windows.Controls.Primitives.ToggleButton, UserControl>();

            home = new HomeContent();
            newQuote = new NewQuoteContent();
            customer = new CustomerSettingsContent();
            priceSettings = new PriceSettingsContent();
            soSection = new SalesOrderContent();
            worksheetSection = new WorksheetContent();
            invoice = new InvoiceContent();
            makePayment = new MakeInvoicePayment();
            commanderSection = new CommanderSectionContent();

            availableOptions.Add(btnHome, home);
            availableOptions.Add(btnCreateNewQuote, newQuote);
            availableOptions.Add(btnPriceSettings, priceSettings);
            availableOptions.Add(btnCustomerSettings, customer);
            availableOptions.Add(btnSaleOrder, soSection);
            availableOptions.Add(btnWorksheet, worksheetSection);
            availableOptions.Add(btnInvoice, invoice);
            availableOptions.Add(btnMakePayment, makePayment);
            availableOptions.Add(btnCommanderSection, commanderSection);

            btnHome.IsChecked = true;
        }

        internal void ShowCurrentPage(System.Windows.Controls.Primitives.ToggleButton selectedOption)
        {
            foreach (KeyValuePair<System.Windows.Controls.Primitives.ToggleButton, UserControl> item in availableOptions)
            {
                if (item.Key == selectedOption)
                {
                    item.Key.IsChecked = true;
                    if (IsIndirectCall == false)
                    {
                        Dashboard parent = Window.GetWindow(this) as Dashboard;
                        if (parent != null)
                        {
                            parent.ucMainContent.ShowPage(item.Value);
                        }
                    }
                }
                else
                {
                    item.Key.IsChecked = false;
                }
            }
        }

        private void btnHome_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnHome);
        }

        private void btnCreateNewQuote_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnCreateNewQuote);
        }

        private void btnCustomerSettings_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnCustomerSettings);
        }

        private void btnPriceSettings_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnPriceSettings);
        }

        private void btnSaleOrder_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnSaleOrder);
        }

        private void btnWorksheet_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnWorksheet);
        }

        private void btnInvoice_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnInvoice);
        }

        private void btnMakePayment_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnMakePayment);
        }
        
        private void btnCommanderSection_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnCommanderSection);
        }
    }
}
