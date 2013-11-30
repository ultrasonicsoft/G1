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
    public partial class InvoiceMasterContent : UserControl
    {
        private ListCollectionView m_InvoiceListForSearch;

        public InvoiceMasterContent()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (m_InvoiceListForSearch == null || txtSearch.Text == "Search")
                    return;
                if (txtSearch.Text != "")
                {
                    if (m_InvoiceListForSearch.CanFilter)
                    {
                        m_InvoiceListForSearch.Filter =
                                new Predicate<object>(ContainsIt);

                        FilterIt();
                    }
                    else
                    {
                        m_InvoiceListForSearch.Filter = null;
                    }
                }
                else
                {
                    m_InvoiceListForSearch.Filter = null;
                    FilterIt();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = string.Empty;
            }
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search";
            }
        }

        public bool ContainsIt(object value)
        {
            try
            {
                InvoiceEntity currentRow = value as InvoiceEntity;

                if (dgInvoiceDetails.Columns.Count > 1)
                {
                    if (currentRow != null)
                    {
                        if (IsSearchCriteriaMatched(currentRow)) return true;
                    }
                }
                else
                {
                    if (IsSearchCriteriaMatched(currentRow)) return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return false;
        }

        private bool IsSearchCriteriaMatched(InvoiceEntity currentRow)
        {
            return currentRow.InvoiceNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                        currentRow.QuoteNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.FullName.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||

                                    currentRow.Total.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.CompletedDate.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.BalanceDue.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.PaymentMode.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower());
        }

        public void FilterIt()
        {
            try
            {
                dgInvoiceDetails.ItemsSource = null;
                ObservableCollection<InvoiceEntity> fileList = new ObservableCollection<InvoiceEntity>();

                foreach (InvoiceEntity row in m_InvoiceListForSearch)
                {
                    fileList.Add(row);
                }
                dgInvoiceDetails.ItemsSource = fileList;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FillInvoiceDetails();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillInvoiceDetails()
        {
            var result = BusinessLogic.GetInvoiceMasterData();
            dgInvoiceDetails.ItemsSource = result;
            m_InvoiceListForSearch = new ListCollectionView(result);
        }

        private void btnOpenInvoice_Click(object sender, RoutedEventArgs e)
        {
            OpenInvoice();
        }

        private void dgInvoiceDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenInvoice();
        }

        private void OpenInvoice()
        {
            Dashboard parent = Window.GetWindow(this) as Dashboard;

            InvoiceEntity entity = dgInvoiceDetails.SelectedItem as InvoiceEntity;

            if (entity == null)
            {
                return;
            }
            if (parent != null)
            {
                DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                DashboardHelper.ChangeDashboardSelection(parent, sideMenu.btnInvoice);
                InvoiceContent invoice = new InvoiceContent(true, entity.QuoteNumber);
                parent.ucMainContent.ShowPage(invoice);
            }
        }

      

    }
}
