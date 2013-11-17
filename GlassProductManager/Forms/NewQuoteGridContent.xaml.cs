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

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for NewQuoteGridContent.xaml
    /// </summary>
    public partial class NewQuoteGridContent : UserControl
    {
        private ObservableCollection<QuoteGridEntity> _allQuoteData = new ObservableCollection<QuoteGridEntity>();

        public ObservableCollection<QuoteGridEntity> allQuoteData
        {
            get { return _allQuoteData; }
            set { _allQuoteData = value; }
        }

        public NewQuoteGridContent()
        {
            InitializeComponent();

            FillShippingMethods();
            FillLeadTimeTypes();
            FillLeadTime();

            dtQuoteCreatedOn.SelectedDate = DateTime.Now;
            dtQuoteRequestedOn.SelectedDate = DateTime.Now;

            UpdateDefaultLeadTimeSettings();

            _allQuoteData.CollectionChanged += _allQuoteData_CollectionChanged;
            GetNewQuoteID();

            SetOperatorAccess();
            FillSmartSearchData();

            FillCustomerNames();
  
        }

        private void FillCustomerNames()
        {
            var result = BusinessLogic.GetAllCustomerNames();
            cmbCustomers.DisplayMemberPath = ColumnNames.Item;
            cmbCustomers.SelectedValuePath = ColumnNames.ID;
            cmbCustomers.ItemsSource = result.DefaultView;
        }

        private void FillSmartSearchData()
        {
            txtSmartSearch.ItemsSource = BusinessLogic.GetSmartSearchData();
        }

        private void SetOperatorAccess()
        {
            if (FirmSettings.IsAdmin)
            {
                cmbOperator.Visibility = System.Windows.Visibility.Visible;
                txtOperatorName.Visibility = System.Windows.Visibility.Hidden;

                var result = BusinessLogic.GetAllOperatorNames();
                cmbOperator.DisplayMemberPath = ColumnNames.UserName;
                cmbOperator.SelectedValuePath = ColumnNames.ID;
                cmbOperator.ItemsSource = result.DefaultView;

                cmbOperator.Text = FirmSettings.UserName;
            }
            else
            {
                cmbOperator.Visibility = System.Windows.Visibility.Hidden;
                txtOperatorName.Visibility = System.Windows.Visibility.Visible;
                txtOperatorName.Text = FirmSettings.UserName;
            }

        }

        void _allQuoteData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateQuoteTotal();
        }

        private void UpdateQuoteTotal()
        {
            double subTotal = 0;
            double grandTotal = 0;

            foreach (var item in allQuoteData)
            {
                subTotal += item.Total;
            }
            lblSubTotal.Content = "$ " + subTotal.ToString("0.00");

            double energySurcharge = double.Parse(txtEnergySurcharge.Text);

            if (energySurcharge >0 &&  cbDollar.IsChecked == true)
            {
                grandTotal = subTotal + energySurcharge; 
            }
            else if(energySurcharge >0 )
            {
                grandTotal = subTotal + (energySurcharge / 100) * subTotal;
            }
            else
            {
                grandTotal = subTotal;
            }
            double discount = double.Parse(txtDiscount.Text);
            if (discount > 0)
            {
                grandTotal = grandTotal - (discount / 100) * subTotal;                
            }
            double deliveryCharges = double.Parse(txtDelivery.Text);

            if (deliveryCharges > 0)
            {
                grandTotal = grandTotal + deliveryCharges;
            }

            double rushOrder = double.Parse(txtRushOrder.Text);

            if (rushOrder > 0 && cbRush.IsChecked == true)
            {
                grandTotal = grandTotal + rushOrder;
            }
            double tax = double.Parse(txtTax.Text);
            if (tax > 0 )
            {
                grandTotal = grandTotal + tax;
            }

            lblGrandTotal.Content = "$ " + grandTotal.ToString("0.00");
        }

        private void GetNewQuoteID()
        {
            string quoteID = BusinessLogic.GetNewQuoteID();
            txtQuoteNumber.Text = quoteID;
        }

        private void UpdateDefaultLeadTimeSettings()
        {
            var result = BusinessLogic.GetLeadTimeSettings();
            if (result == null)
                return;
            cmbLeadTime.SelectedIndex = int.Parse(result.Rows[0][ColumnNames.LeadTimeID].ToString());
            cmbLeadTimeType.SelectedIndex = int.Parse(result.Rows[0][ColumnNames.LeadTimeTypeID].ToString());
            cbUseAsDefault.IsChecked = bool.Parse(result.Rows[0][ColumnNames.IsDefaultLeadTime].ToString());
        }

        private void FillShippingMethods()
        {
            var result = BusinessLogic.GetAllShippingMethods();
            cmbShippingMethod.DisplayMemberPath = ColumnNames.Shipping;
            cmbShippingMethod.SelectedValuePath = ColumnNames.ID;
            cmbShippingMethod.ItemsSource = result.DefaultView;
            cmbShippingMethod.SelectedIndex = 0;
        }
        private void FillLeadTimeTypes()
        {
            var result = BusinessLogic.GetAllLeadTimeTypes();
            cmbLeadTimeType.DisplayMemberPath = ColumnNames.LeadTimeType;
            cmbLeadTimeType.SelectedValuePath = ColumnNames.ID;
            cmbLeadTimeType.ItemsSource = result.DefaultView;
        }

        private void FillLeadTime()
        {
            var result = BusinessLogic.GetAllLeadTime();
            cmbLeadTime.DisplayMemberPath = ColumnNames.LeadTime;
            cmbLeadTime.SelectedValuePath = ColumnNames.ID;
            cmbLeadTime.ItemsSource = result.DefaultView;
        }

        private void btnRegenerate_Click(object sender, RoutedEventArgs e)
        {
            allQuoteData.Clear();

            ClearSubTotalSection();
        }

        private void ClearSubTotalSection()
        {
            lblSubTotal.Content = "0.00";
            txtDiscount.Text = "0.00";
            txtDelivery.Text = "0.00";
            txtRushOrder.Text = "0.00";
            txtTax.Text = "0.00";
            lblGrandTotal.Content = "0.00";
        }

        private void cbUseAsDefault_Checked(object sender, RoutedEventArgs e)
        {
            if (cmbLeadTime.SelectedIndex == -1 || cmbLeadTimeType.SelectedIndex == -1)
            {
                Helper.ShowErrorMessageBox("Please select Lead Time first");
                cbUseAsDefault.IsChecked = false;
                return;
            }

            BusinessLogic.SetDefaultLeadTime(cmbLeadTime.SelectedIndex, cmbLeadTimeType.SelectedIndex);
        }

        private void cbUseAsDefault_Unchecked(object sender, RoutedEventArgs e)
        {
            BusinessLogic.ResetDefaultLeadTime();
            cmbLeadTime.SelectedIndex = -1;
            cmbLeadTimeType.SelectedIndex = -1;
        }

        private void txtQuoteNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            //TODO: add validation control n put logic over there
            bool isQuoteNumberPresent = BusinessLogic.IsQuoteNumberPresent(txtQuoteNumber.Text);
            if (isQuoteNumberPresent)
            {
                txtQuoteNumber.Text = string.Empty;
                Helper.ShowErrorMessageBox("Quote Number already used! Kindly provide new quote number.");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count == 0)
            {
                Helper.ShowErrorMessageBox("There is no item in the quote. Please add atleast one.");
                return;
            }
            SaveQuoteHeader();
            SaveQuoteItems();
            SaveQuoteFooter();
            Helper.ShowInformationMessageBox("Quote saved successfully!");
        }

        private void SaveQuoteHeader()
        {
            QuoteHeader header = new QuoteHeader();
            header.QuoteNumber = txtQuoteNumber.Text;
            header.QuoteCreatedOn = dtQuoteCreatedOn.SelectedDate.Value.ToShortDateString();
            header.QuoteRequestedOn = dtQuoteRequestedOn.SelectedDate.Value.ToShortDateString();
            header.CustomerPO = txtCustomerPO.Text;
            header.IsNewCustomer = cbIsNewClient.IsChecked == true;
            header.IsShipToOtherAddress = cbIsShipToSameAddress.IsChecked == true;
            header.ShippingMethodID = cmbShippingMethod.SelectedIndex;
            header.LeadTimeID = cmbLeadTime.SelectedIndex;
            header.LeadTimeTypeID = cmbLeadTimeType.SelectedIndex;
            
            if(FirmSettings.IsAdmin)
            {
                header.OperatorName = cmbOperator.Text;
            }
            else
            {
                header.OperatorName = txtOperatorName.Text;
            }
            if (cbIsNewClient.IsChecked.Value == false)
            {
                header.CustomerID = int.Parse(cmbCustomers.SelectedValue.ToString());
            }
            header.SoldTo = new CustomerDetails();
            header.SoldTo.FirstName = txtSoldToFirstName.Text;
            header.SoldTo.LastName = txtSoldToLastName.Text;
            header.SoldTo.Address = txtSoldToAddress.Text;
            header.SoldTo.Phone = txtSoldToPhone.Text;
            header.SoldTo.Email = txtSoldToEmail.Text;
            header.SoldTo.Fax = txtSoldToFax.Text;
            header.SoldTo.Misc = txtShipToMisc.Text;

            if (cbIsShipToSameAddress.IsChecked == true)
            {
                header.ShipTo = new CustomerDetails();
                header.ShipTo.FirstName = txtShiptoFirstName.Text;
                header.ShipTo.LastName = txtShiptoLastName.Text;
                header.ShipTo.Address = txtShipToAddress.Text;
                header.ShipTo.Phone = txtShipToPhone.Text;
                header.ShipTo.Email = txtShipToEmail.Text;
                header.ShipTo.Fax = txtShipToFax.Text;
                header.ShipTo.Misc = txtShipToMisc.Text;
            }

            BusinessLogic.SaveQuoteHeader(header);
        }

        private void SaveQuoteItems()
        {
            BusinessLogic.SaveQuoteItems(txtQuoteNumber.Text, allQuoteData);
        }

        private void SaveQuoteFooter()
        {
            QuoteFooter footer = new QuoteFooter();
            footer.SubTotal = double.Parse(lblSubTotal.Content.ToString().Replace("$ ", string.Empty));
            footer.IsDollar = cbDollar.IsChecked.Value == true;
            footer.EnergySurcharge = double.Parse(txtEnergySurcharge.Text);
            footer.Discount = double.Parse(txtDiscount.Text);
            footer.Delivery = double.Parse(txtDelivery.Text);
            footer.IsRushOrder = cbRush.IsChecked.Value == true;
            footer.RushOrder = double.Parse(txtRushOrder.Text);
            footer.Tax = double.Parse(txtTax.Text);
            footer.GrandTotal = double.Parse(lblGrandTotal.Content.ToString().Replace("$ ", string.Empty));

            BusinessLogic.SaveQuoteFooter(txtQuoteNumber.Text, footer);
        }

        private void cbIsNewClient_Checked(object sender, RoutedEventArgs e)
        {
            cmbCustomers.IsEnabled = false;
        }

        private void cbIsNewClient_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbCustomers.IsEnabled = true;
        }

        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                DataGrid grd = (DataGrid)sender;
                grd.BeginEdit(e);
            }
        }

        private void dgQuoteItems_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateQuoteTotal();
        }

        private void txtEnergySurcharge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtEnergySurcharge.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void txtEnergySurcharge_LostFocus(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0)
            {
                txtEnergySurcharge.Text = string.IsNullOrEmpty(txtEnergySurcharge.Text) ? "0.00" : txtEnergySurcharge.Text;
            }
        }

        private void cbDollar_Checked(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtEnergySurcharge.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void cbDollar_Unchecked(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtEnergySurcharge.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void txtDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtDiscount.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void txtDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0)
            {
                txtDiscount.Text = string.IsNullOrEmpty(txtDiscount.Text) ? "0.00" : txtDiscount.Text;
            }
        }

        private void txtDelivery_LostFocus(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0)
            {
                txtDelivery.Text = string.IsNullOrEmpty(txtDelivery.Text) ? "0.00" : txtDelivery.Text;
            }
        }

        private void txtDelivery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtDelivery.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void txtRushOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allQuoteData.Count > 0 && cbRush.IsChecked == true && string.IsNullOrEmpty(txtRushOrder.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void txtRushOrder_LostFocus(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0 && cbRush.IsChecked == true)
            {
                txtRushOrder.Text = string.IsNullOrEmpty(txtRushOrder.Text) ? "0.00" : txtRushOrder.Text;
            }
        }

        private void txtTax_LostFocus(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0)
            {
                txtTax.Text = string.IsNullOrEmpty(txtTax.Text) ? "0.00" : txtTax.Text;
            }
        }

        private void txtTax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtTax.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void cbRush_Unchecked(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtRushOrder.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void cbRush_Checked(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtRushOrder.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void btnUpdateSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            QuoteGridEntity selectedLineItem = dgQuoteItems.SelectedItem as QuoteGridEntity;
            if (selectedLineItem == null)
                return;
            selectedLineItem.Total = (double.Parse(txtAdditionalCostForItem.Text) + double.Parse(selectedLineItem.UnitPrice)) * selectedLineItem.Quantity;
            UpdateQuoteTotal();

        }

        private void btnUpdateAllItem_Click(object sender, RoutedEventArgs e)
        {
            if (allQuoteData.Count > 0)
            {
                foreach (QuoteGridEntity selectedLineItem in allQuoteData)
                {
                    if (selectedLineItem == null || string.IsNullOrEmpty(txtAdditionalCostForItem.Text) || string.IsNullOrEmpty(selectedLineItem.UnitPrice))
                        continue;
                    
                    selectedLineItem.Total = (double.Parse(txtAdditionalCostForItem.Text) + double.Parse(selectedLineItem.UnitPrice)) * selectedLineItem.Quantity;
                }
                UpdateQuoteTotal();
            }
        }

        private void btnOpenQuote_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSmartSearch.Text))
            {
                Helper.ShowErrorMessageBox("Please select Customer/Quote No. first");
                return;
            }
            string quoteNumber = string.Empty;
            foreach (string item in txtSmartSearch.Text.Split('-'))
            {
                if (item.Trim().StartsWith("Q") || item.Trim().StartsWith("q"))
                {
                    quoteNumber = item.Trim();
                    break;
                }
            }
            if (string.IsNullOrEmpty(quoteNumber))
            {
                Helper.ShowErrorMessageBox("Invalid Quote Number");
                return;
            }
            OpenSelectedQuote(quoteNumber);
        }

        private void OpenSelectedQuote(string quoteNumber)
        {
            QuoteEntity result = BusinessLogic.GetQuoteDetails(quoteNumber);
            if (result == null)
            {
                Helper.ShowInformationMessageBox("No data found for selected quote!");
                return;
            }
            #region Fill Header Information

            txtQuoteNumber.Text = result.Header.QuoteNumber;
            txtCustomerPO.Text = result.Header.CustomerPO;
            dtQuoteCreatedOn.SelectedDate = DateTime.Parse(result.Header.QuoteCreatedOn);
            dtQuoteRequestedOn.SelectedDate = DateTime.Parse(result.Header.QuoteRequestedOn);

            SetSoldToDetails(result.Header.SoldTo);

            if (FirmSettings.IsAdmin)
            {
                cmbOperator.Text = result.Header.OperatorName;
            }
            {
                txtOperatorName.Text = result.Header.OperatorName;
            }
            if (result.Header.IsShipToOtherAddress)
            {
                SetShipToDetails(result.Header.ShipTo);
            }
            cmbShippingMethod.SelectedIndex = result.Header.ShippingMethodID;
            cmbLeadTime.SelectedIndex = result.Header.LeadTimeID;
            cmbLeadTimeType.SelectedIndex = result.Header.LeadTimeTypeID;

            #endregion

            #region Fill Line Items

            allQuoteData = result.LineItems;
            dgQuoteItems.ItemsSource = allQuoteData;

            #endregion

            #region Fill Footer Information
            
            if (result.Footer == null)
                return;
            
            lblSubTotal.Content = result.Footer.SubTotal.ToString("0.00");
            cbDollar.IsChecked = result.Footer.IsDollar;
            txtEnergySurcharge.Text = result.Footer.EnergySurcharge.ToString("0.00");
            txtDiscount.Text = result.Footer.Discount.ToString("0.00");
            txtDelivery.Text = result.Footer.Delivery.ToString("0.00");
            cbRush.IsChecked = result.Footer.IsRushOrder;
            txtRushOrder.Text = result.Footer.RushOrder.ToString("0.00");
            txtTax.Text = result.Footer.Tax.ToString("0.00");
            lblGrandTotal.Content= result.Footer.GrandTotal.ToString("0.00");

            #endregion
        }

        private void SetShipToDetails(CustomerDetails shipTo)
        {
            txtShiptoFirstName.Text = shipTo.FirstName;
            txtShiptoLastName.Text = shipTo.LastName;
            txtShipToAddress.Text = shipTo.Address;
            txtShipToPhone.Text = shipTo.Phone;
            txtShipToFax.Text = shipTo.Fax;
            txtShipToEmail.Text = shipTo.Email;
            txtShipToMisc.Text = shipTo.Misc;
        }

        private void SetSoldToDetails(CustomerDetails soldTo)
        {
            txtSoldToFirstName.Text = soldTo.FirstName;
            txtSoldToLastName.Text = soldTo.LastName;
            txtSoldToAddress.Text = soldTo.Address;
            txtSoldToPhone.Text = soldTo.Phone;
            txtSoldToFax.Text = soldTo.Fax;
            txtSoldToEmail.Text = soldTo.Email;
            txtSoldToMisc.Text = soldTo.Misc;
        }

        private void cmbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCustomers.SelectedValue == null)
                return;

            CustomerDetails soldTo = null;
            CustomerDetails shipTo = null;
            
            BusinessLogic.GetCustomerDetails(out soldTo, out shipTo, cmbCustomers.SelectedValue.ToString());

            if (soldTo != null)
            {
                SetSoldToDetails(soldTo);
            }
            if (shipTo != null)
            {
                SetShipToDetails(shipTo);
            }
        }

        private void btnNewQuote_Click(object sender, RoutedEventArgs e)
        {
            var result = Helper.ShowQuestionMessageBox("Are you sure to create new quote?");
            if (result == MessageBoxResult.Yes)
            {
                ResetQuote();
                GetNewQuoteID();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = Helper.ShowQuestionMessageBox("Are you sure to delete this quote?");
            if (result == MessageBoxResult.Yes)
            {
                BusinessLogic.DeleteQuote(txtQuoteNumber.Text);
                Helper.ShowInformationMessageBox("Quote deleted successfully.");
                ResetQuote();
            }
        }

        private void ResetQuote()
        {
            ResetQuoteHeader();
            ResetShipTo();
            ResetSoldTo();
            ResetQuoteItems();
            ResetQuoteFooter();
        }
       
        private void ResetQuoteHeader()
        {
            txtQuoteNumber.Text = string.Empty;
            txtCustomerPO.Text = string.Empty;
            dtQuoteCreatedOn.SelectedDate = DateTime.Now;
            dtQuoteRequestedOn.SelectedDate = DateTime.Now;
            cbIsNewClient.IsChecked = true;
            cmbCustomers.SelectedIndex = -1;
            cbIsShipToSameAddress.IsChecked = false;
        }

        private void ResetQuoteItems()
        {
            allQuoteData.Clear();
            dgQuoteItems.ItemsSource = allQuoteData;
        }

        private void ResetShipTo()
        {
            txtShiptoFirstName.Text = string.Empty;
            txtShiptoLastName.Text = string.Empty;
            txtShipToAddress.Text = string.Empty;
            txtShipToPhone.Text = string.Empty;
            txtShipToFax.Text = string.Empty;
            txtShipToEmail.Text = string.Empty;
            txtShipToMisc.Text = string.Empty;
        }

        private void ResetSoldTo()
        {
            txtSoldToFirstName.Text = string.Empty;
            txtSoldToLastName.Text = string.Empty;
            txtSoldToAddress.Text = string.Empty;
            txtSoldToPhone.Text = string.Empty;
            txtSoldToFax.Text = string.Empty;
            txtSoldToEmail.Text = string.Empty;
            txtSoldToMisc.Text = string.Empty;
        }

        private void ResetQuoteFooter()
        {
            lblSubTotal.Content = "0.00";
            cbDollar.IsChecked = false;
            txtEnergySurcharge.Text  = "0.00";
            txtDiscount.Text  = "0.00";
            txtDelivery.Text  = "0.00";
            cbRush.IsChecked  = false;
            txtRushOrder.Text  = "0.00";
            txtTax.Text  = "0.00";
            lblGrandTotal.Content = "0.00";
        }
    }
}
