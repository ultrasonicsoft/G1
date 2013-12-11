using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SettingsContent.xaml
    /// </summary>
    public partial class CommanderSectionContent : UserControl
    {
        public CommanderSectionContent()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FillWorkItemTypes();
                LoadSelectedWorkItem();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillWorkItemTypes()
        {
            List<string> workItems = new List<string>();
            workItems.Add("Quotes");
            workItems.Add("Sale Orders");
            workItems.Add("Worksheets");
            workItems.Add("Invoices");
            cmbWorkItemTypes.ItemsSource = workItems;
            cmbWorkItemTypes.SelectedIndex = 0;
        }

        private void btnOpenWorkItem_Click(object sender, RoutedEventArgs e)
        {
            LoadSelectedWorkItem();
        }

        private void LoadSelectedWorkItem()
        {
            switch (cmbWorkItemTypes.SelectedIndex)
            {
                case 0:
                    QuoteMasterContent quoteMaster = new QuoteMasterContent();
                    ucWorkItem.ShowPage(quoteMaster);
                    break;
                case 1:
                    SaleOrderMasterContent saleOrderMaster = new SaleOrderMasterContent();
                    ucWorkItem.ShowPage(saleOrderMaster);
                    break;
                case 2:
                    WorksheetMasterContent worksheetMaster = new WorksheetMasterContent();
                    ucWorkItem.ShowPage(worksheetMaster);
                    break;
                case 3:
                    InvoiceMasterContent invoiceMaster = new InvoiceMasterContent();
                    ucWorkItem.ShowPage(invoiceMaster);
                    break;
                default:
                    break;
            }
        }

        private void cmbWorkItemTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadSelectedWorkItem();
        }
    }
}
