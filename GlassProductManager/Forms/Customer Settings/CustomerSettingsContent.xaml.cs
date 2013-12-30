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
    public partial class CustomerSettingsContent : UserControl
    {
        private ListCollectionView m_CustomerListForSearch;
        private bool isEdit = false;
        private bool isNew = false;

        public CustomerSettingsContent()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (m_CustomerListForSearch == null || txtSearch.Text == "Search")
                    return;
                if (txtSearch.Text != "")
                {
                    if (m_CustomerListForSearch.CanFilter)
                    {
                        m_CustomerListForSearch.Filter =
                                new Predicate<object>(ContainsIt);

                        FilterIt();
                    }
                    else
                    {
                        m_CustomerListForSearch.Filter = null;
                    }
                }
                else
                {
                    m_CustomerListForSearch.Filter = null;
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
                                    currentRow.Email.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Misc.ToString()
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
                                    currentRow.PONumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                        currentRow.InvoiceNumber.ToString()
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

                foreach (CustomerSmartDataEntity row in m_CustomerListForSearch)
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
            ObservableCollection<CustomerSmartDataEntity> result = BusinessLogic.GetAllCustomers();
            dgCustomerList.ItemsSource = result;
            m_CustomerListForSearch = new ListCollectionView(result);
        }

        private void btnNewCusotmer_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus(true);
            dgCustomerList.CanUserAddRows = true;
            isNew = true;
        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            int index = isNew ? 2 : 1;

            //CustomerSmartDataEntity newCustomer = dgCustomerList.Items[dgCustomerList.Items.Count - index] as CustomerSmartDataEntity;
            CustomerSmartDataEntity newCustomer = dgCustomerList.Items[dgCustomerList.SelectedIndex] as CustomerSmartDataEntity;
            if (newCustomer == null)
            {
                Helper.ShowErrorMessageBox("Error during saving new customer.");
                return;
            }
            CustomerDetails customer = new CustomerDetails();
            customer.FirstName = newCustomer.FirstName;
            customer.LastName = newCustomer.LastName;
            customer.Address = newCustomer.Address;
            customer.Phone = newCustomer.Phone;
            customer.Email = newCustomer.Email;
            customer.Fax = newCustomer.Fax;
            customer.Misc = newCustomer.Misc;
            customer.Fax = newCustomer.Fax;

            if (isNew == true)
            {
                BusinessLogic.CreateNewCustomer(customer);
            }
            else if (isEdit == true)
            {
                BusinessLogic.UpdateCustomer(customer, newCustomer.ID);
            }

            if (isNew)
            {
                Helper.ShowInformationMessageBox("New customer saved successfully!");
            }
            else if (isEdit)
            {
                Helper.ShowInformationMessageBox("Customer information updated successfully!");
            }

            FillCustomerDetails();
            dgCustomerList.CanUserAddRows = false;

            UpdateStatus(false);
            
            isEdit = false;
            isNew = false;
        }

        private void btnCancelEdit_Click(object sender, RoutedEventArgs e)
        {
            FillCustomerDetails();
            dgCustomerList.CanUserAddRows = false;
            UpdateStatus(false);

            isEdit = false;
            isNew = false;
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
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

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus(true);
            isEdit = true;
        }

        private void UpdateStatus(bool status) 
        {
            dgCustomerList.IsReadOnly = !status;
            dgCustomerList.CanUserAddRows = !status;
            btnSaveCustomer.IsEnabled = status;
            btnCancelEdit.IsEnabled = status;
            btnEditCustomer.IsEnabled = !status;
            btnNewCusotmer.IsEnabled = !status;
        }

        void ShowHideDetails(object sender, RoutedEventArgs e)
        {
            CustomerSmartDataEntity obj = ((FrameworkElement)sender).DataContext as CustomerSmartDataEntity;

            //for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
            //    if (vis is DataGridRow)
            //    {
            //        var row = (DataGridRow)vis;
            //        row.DetailsVisibility =
            //          row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            //        break;
            //    }
        }
    }
}
