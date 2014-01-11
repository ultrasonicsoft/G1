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
using Ultrasonicsoft.Products.BackupManager;

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
        MakePayment,
        BarCodePrinter,
        ConfigureDatabase
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
        BarcodePrinter barcodePrinter;

        public DashboardMenu()
        {
            try
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
                barcodePrinter = new BarcodePrinter();

                availableOptions.Add(btnHome, home);
                availableOptions.Add(btnCreateNewQuote, newQuote);
                availableOptions.Add(btnPriceSettings, priceSettings);
                availableOptions.Add(btnCustomerSettings, customer);
                availableOptions.Add(btnSaleOrder, soSection);
                availableOptions.Add(btnWorksheet, worksheetSection);
                availableOptions.Add(btnInvoice, invoice);
                availableOptions.Add(btnMakePayment, makePayment);
                availableOptions.Add(btnCommanderSection, commanderSection);
                availableOptions.Add(btnBarcodePrinter, barcodePrinter);

                btnHome.IsChecked = true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
          
        }

        internal void ShowCurrentPage(System.Windows.Controls.Primitives.ToggleButton selectedOption, UserControl currentPage = null)
        {
            try
            {
                Dashboard parent = Window.GetWindow(this) as Dashboard;
                if (parent != null)
                {
                    NewQuoteContent newQuote = parent.ucMainContent.CurrentPage as NewQuoteContent;

                    if (newQuote != null && selectedOption != btnCreateNewQuote && IsIndirectCall == false)
                    {
                        var result = Helper.ShowQuestionMessageBox("All your current Quote change will be lost. Are you sure to leave Quote page?");
                        if (result != MessageBoxResult.Yes)
                        {
                            selectedOption.IsChecked = false;
                            return;
                        }
                    }
                }

                foreach (KeyValuePair<System.Windows.Controls.Primitives.ToggleButton, UserControl> item in availableOptions)
                {
                    if (item.Key == selectedOption)
                    {
                        item.Key.IsChecked = true;
                        if (IsIndirectCall == false)
                        {
                            if (parent != null)
                            {
                                parent.ucMainContent.ShowPage(currentPage);
                                //parent.ucMainContent.ShowPage(item.Value);
                            }
                        }
                    }
                    else
                    {
                        item.Key.IsChecked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            
        }

        private void btnHome_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnHome, new HomeContent());
        }

        private void btnCreateNewQuote_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnCreateNewQuote, new NewQuoteContent());
        }

        private void btnCustomerSettings_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnCustomerSettings, new CustomerSettingsContent());
        }

        private void btnPriceSettings_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnPriceSettings, new PriceSettingsContent());
        }

        private void btnSaleOrder_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnSaleOrder, new SalesOrderContent());
        }

        private void btnWorksheet_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnWorksheet, new WorksheetContent());
        }

        private void btnInvoice_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnInvoice, new InvoiceContent());
        }

        private void btnMakePayment_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnMakePayment, new MakeInvoicePayment());
        }

        private void btnCommanderSection_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnCommanderSection, new CommanderSectionContent());
        }

        private void btnBarcodePrinter_Checked(object sender, RoutedEventArgs e)
        {
            ShowCurrentPage(btnBarcodePrinter, new BarcodePrinter());
        }

        private void btnConfigureDatabase_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ConfigureDatabase configureDB = new ConfigureDatabase();
                configureDB.ShowDialog();
                btnConfigureDatabase.IsChecked = false;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void btnBackupDatabase_Checked(object sender, RoutedEventArgs e)
        {
            DBBackupManager manager = new DBBackupManager();
            string dbBackupFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) +
                                        System.IO.Path.DirectorySeparatorChar.ToString() + Constants.DBBackup;

            string backupFileName = dbBackupFolder + System.IO.Path.DirectorySeparatorChar.ToString() +
                                       Constants.DatabaseName + "-" + DateTime.Now.ToString("yyyyMMdd") + Constants.BackupExtension;

            manager.SetupBackupFolder(dbBackupFolder);
            manager.BackupDatabase(backupFileName, Constants.DatabaseServerName, Constants.DatabaseName);
            string message = "Database backup done! Do you want to open backup folder?";
            var result = Helper.ShowQuestionMessageBox(message, "Case Control System",MessageBoxButton.YesNo);
            
            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
            {
                System.Diagnostics.Process.Start(dbBackupFolder);
            }
        }
    }
}
