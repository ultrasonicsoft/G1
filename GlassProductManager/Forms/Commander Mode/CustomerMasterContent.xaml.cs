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
    public partial class CustomerMasterContent : UserControl
    {
        private ListCollectionView _CustomerListForSearch;

        public CustomerMasterContent()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (_CustomerListForSearch == null || txtSearch.Text == "Search")
                    return;
                if (txtSearch.Text != "")
                {
                    if (_CustomerListForSearch.CanFilter)
                    {
                        _CustomerListForSearch.Filter =
                                new Predicate<object>(ContainsIt);

                        FilterIt();
                    }
                    else
                    {
                        _CustomerListForSearch.Filter = null;
                    }
                }
                else
                {
                    _CustomerListForSearch.Filter = null;
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
                CustomerSmartDataEntity currentRow = value as CustomerSmartDataEntity;

                if (dgCustomerList.Columns.Count > 1)
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

        private bool IsSearchCriteriaMatched(CustomerSmartDataEntity currentRow)
        {
            return currentRow.FirstName.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                        currentRow.LastName.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Address.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||

                                    currentRow.Phone.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Fax.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.QuoteNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.SONumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.WorksheetNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.InvoiceNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.PONumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Email.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Misc.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower());
        }

        public void FilterIt()
        {
            try
            {
                dgCustomerList.ItemsSource = null;
                ObservableCollection<CustomerSmartDataEntity> fileList = new ObservableCollection<CustomerSmartDataEntity>();

                foreach (CustomerSmartDataEntity row in _CustomerListForSearch)
                {
                    fileList.Add(row);
                }
                dgCustomerList.ItemsSource = fileList;
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
                FillCustomerDetails();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillCustomerDetails()
        {
            try
            {
                var result = BusinessLogic.GetAllCustomerDetails();
                //dgCustomerList.ItemsSource = result;
                _CustomerListForSearch = new ListCollectionView(result);

                ListCollectionView collection = new ListCollectionView(result);
                collection.GroupDescriptions.Add(new PropertyGroupDescription("FullName"));
                dgCustomerList.ItemsSource = collection;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void dgCustomerList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //OpenWorksheet();
        }

        private void OpenWorksheet()
        {
            try
            {
                Dashboard parent = Window.GetWindow(this) as Dashboard;
                WorksheetEntity entity = dgCustomerList.SelectedItem as WorksheetEntity;

                if (entity == null)
                {
                    return;
                }
                if (parent != null)
                {
                    DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                    DashboardHelper.ChangeDashboardSelection(parent, sideMenu.btnWorksheet);
                    WorksheetContent newQuote = new WorksheetContent(true, entity.QuoteNumber);
                    parent.ucMainContent.ShowPage(newQuote);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnOpenWorksheet_Click(object sender, RoutedEventArgs e)
        {
            OpenWorksheet();
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            DeleteCustomer();
        }

        private void DeleteCustomer()
        {
            try
            {
                if (FirmSettings.IsAdmin == false)
                {
                    Helper.ShowErrorMessageBox("Only Administrator can delete customer");
                    return;
                }
                CustomerSmartDataEntity selectedCustomer = dgCustomerList.SelectedItem as CustomerSmartDataEntity;
                if (selectedCustomer == null)
                {
                    Helper.ShowErrorMessageBox("Please select customer to delete.");
                    return;
                }

                var result = Helper.ShowQuestionMessageBox("All information of customer will be deleted. Are you sure to delete?");
                if (result == MessageBoxResult.Yes)
                {
                    BusinessLogic.DeleteCustomer(selectedCustomer.ID);
                    Helper.ShowInformationMessageBox("Customer deleted successfully!");
                    FillCustomerDetails();
                    dgCustomerList.CanUserAddRows = false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

    }
}
