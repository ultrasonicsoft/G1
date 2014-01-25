using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
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
using System.ComponentModel;
using System.Data;
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for NewQuoteGridContent.xaml
    /// </summary>
    public partial class NewQuoteGridContent : UserControl
    {
        public ObservableCollection<QuoteGridEntity> _allQuoteData = new ObservableCollection<QuoteGridEntity>();

        public ObservableCollection<QuoteGridEntity> allQuoteData
        {
            get { return _allQuoteData; }
            set { _allQuoteData = value; }
        }
        internal ObservableCollection<NewQuoteItemEntity> allLineItemDetails = new ObservableCollection<NewQuoteItemEntity>();

        public NewQuoteGridContent()
        {
            InitializeComponent();

            ConfigureInitialSetup();
        }

        public NewQuoteGridContent(bool _isOpenQuoteRequested, string _quoteNumber)
        {
            InitializeComponent();

            ConfigureInitialSetup();

            if (_isOpenQuoteRequested == true)
            {
                OpenSelectedQuote(_quoteNumber);
            }
        }

        private void ConfigureInitialSetup()
        {
            try
            {
                FillShippingMethods();
                FillLeadTimeTypes();
                FillLeadTime();

                cbIsNewClient.IsChecked = true;

                dtQuoteCreatedOn.SelectedDate = DateTime.Now;
                dtQuoteRequestedOn.SelectedDate = DateTime.Now;

                UpdateDefaultLeadTimeSettings();

                _allQuoteData.CollectionChanged += _allQuoteData_CollectionChanged;
                GetNewQuoteID();

                SetOperatorAccess();
                FillSmartSearchData();

                FillCustomerNames();

                FillPaymentTypes();
                FillQuoteStatus();

                FillAllQuoteNumbers();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            
        }

        private void FillCustomerNames()
        {
            try
            {
                var result = BusinessLogic.GetAllCustomerNames();
                cmbCustomers.DisplayMemberPath = ColumnNames.Item;
                cmbCustomers.SelectedValuePath = ColumnNames.ID;
                cmbCustomers.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillSmartSearchData()
        {
            //txtSmartSearch.ItemsSource = BusinessLogic.GetSmartSearchData();
        }

        private void FillPaymentTypes()
        {
            try
            {
                var result = BusinessLogic.GetAllPaymentTypes();
                cmbPaymentType.DisplayMemberPath = ColumnNames.Type;
                cmbPaymentType.SelectedValuePath = ColumnNames.ID;
                cmbPaymentType.ItemsSource = result.DefaultView;
                cmbPaymentType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void FillQuoteStatus()
        {
            try
            {
                var result = BusinessLogic.GetAllQuoteStatus();
                cmbQuoteStatus.DisplayMemberPath = ColumnNames.Type;
                cmbQuoteStatus.SelectedValuePath = ColumnNames.ID;
                cmbQuoteStatus.ItemsSource = result.DefaultView;
                cmbQuoteStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
          
        }

        private void FillAllQuoteNumbers()
        {
            try
            {
                var result = BusinessLogic.GetAllQuoteNumbers();
                cmbQuoteNumbers.DisplayMemberPath = ColumnNames.Type;
                cmbQuoteNumbers.SelectedValuePath = ColumnNames.ID;
                cmbQuoteNumbers.ItemsSource = result.DefaultView;
                cmbQuoteNumbers.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void SetOperatorAccess()
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        void _allQuoteData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateQuoteTotal();
        }

        private void UpdateQuoteTotal()
        {
            try
            {
                double subTotal = 0;
                double grandTotal = 0;

                foreach (var item in allQuoteData)
                {
                    if (item == null || item.Total == null)
                        continue;

                    if (false == Helper.IsValidCurrency(item.Total))
                    {
                        continue;
                    }
                    subTotal += double.Parse(item.Total);
                }
                lblSubTotal.Content = "$ " + subTotal.ToString("0.00");

                double energySurcharge = double.Parse(txtEnergySurcharge.Text);

                if (energySurcharge > 0 && cbDollar.IsChecked == true)
                {
                    grandTotal = subTotal + energySurcharge;
                }
                else if (energySurcharge > 0)
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
                if (tax > 0)
                {
                    grandTotal = grandTotal + tax;
                }

                lblGrandTotal.Content = "$ " + grandTotal.ToString("0.00");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void GetNewQuoteID()
        {
            try
            {
                string quoteID = BusinessLogic.GetNewQuoteID();
                txtQuoteNumber.Text = quoteID;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void UpdateDefaultLeadTimeSettings()
        {
            try
            {
                var result = BusinessLogic.GetLeadTimeSettings();
                if (result == null)
                    return;
                object tempDBValue = result.Rows[0][ColumnNames.LeadTimeID];
                cmbLeadTime.SelectedIndex = int.Parse(tempDBValue == DBNull.Value ? "0" : tempDBValue.ToString());
                tempDBValue = result.Rows[0][ColumnNames.LeadTimeTypeID];
                cmbLeadTimeType.SelectedIndex = int.Parse(tempDBValue == DBNull.Value ? "0" : tempDBValue.ToString());
                tempDBValue = result.Rows[0][ColumnNames.IsDefaultLeadTime];
                cbUseAsDefault.IsChecked = bool.Parse(tempDBValue == DBNull.Value ? "false" : tempDBValue.ToString());
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillShippingMethods()
        {
            try
            {
                var result = BusinessLogic.GetAllShippingMethods();
                cmbShippingMethod.DisplayMemberPath = ColumnNames.Shipping;
                cmbShippingMethod.SelectedValuePath = ColumnNames.ID;
                cmbShippingMethod.ItemsSource = result.DefaultView;
                cmbShippingMethod.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillLeadTimeTypes()
        {
            try
            {
                var result = BusinessLogic.GetAllLeadTimeTypes();
                cmbLeadTimeType.DisplayMemberPath = ColumnNames.LeadTimeType;
                cmbLeadTimeType.SelectedValuePath = ColumnNames.ID;
                cmbLeadTimeType.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillLeadTime()
        {
            try
            {
                var result = BusinessLogic.GetAllLeadTime();
                cmbLeadTime.DisplayMemberPath = ColumnNames.LeadTime;
                cmbLeadTime.SelectedValuePath = ColumnNames.ID;
                cmbLeadTime.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
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
            try
            {
                if (cmbLeadTime.SelectedIndex == -1 || cmbLeadTimeType.SelectedIndex == -1)
                {
                    Helper.ShowErrorMessageBox("Please select Lead Time first");
                    cbUseAsDefault.IsChecked = false;
                    return;
                }

                BusinessLogic.SetDefaultLeadTime(cmbLeadTime.SelectedIndex, cmbLeadTimeType.SelectedIndex);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cbUseAsDefault_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                BusinessLogic.ResetDefaultLeadTime();
                cmbLeadTime.SelectedIndex = -1;
                cmbLeadTimeType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void txtQuoteNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Helper.IsNonEmpty(txtQuoteNumber))
                {
                    bool isQuoteNumberPresent = BusinessLogic.IsQuoteNumberPresent(txtQuoteNumber.Text);
                    if (isQuoteNumberPresent)
                    {
                        txtQuoteNumber.Text = string.Empty;
                        Helper.ShowErrorMessageBox("Quote Number already used! Kindly provide new quote number.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int customerID = 0;

                if (true == string.IsNullOrWhiteSpace(txtSoldToFirstName.Text))
                {
                    Helper.ShowErrorMessageBox("Sold to First Name can not be empty. Please provide First Name.");
                    txtSoldToFirstName.Focus();
                    return;
                }
                if (false == Helper.IsNonEmpty(txtQuoteNumber))
                {
                    Helper.ShowErrorMessageBox("Quote Number can not be empty. Please provide Quote Number.");
                    txtQuoteNumber.Focus();
                    return;
                }

                if (allQuoteData.Count == 0)
                {
                    Helper.ShowErrorMessageBox("There is no item in the quote. Please add atleast one.");
                    return;
                }

                bool isQuoteNumberPresent = BusinessLogic.IsQuoteNumberPresent(txtQuoteNumber.Text);
                if (isQuoteNumberPresent)
                {
                    var result = Helper.ShowQuestionMessageBox("Quote already present. Press Yes to update existing. Press No to Clone existing quote.");
                    if (result == MessageBoxResult.Yes)
                    {

                        QuoteHeader header = BuildQuoteHeader(txtQuoteNumber.Text);
                        customerID = BusinessLogic.GetCustomerID(txtQuoteNumber.Text);
                        header.CustomerID = customerID;
                        BusinessLogic.UpdateQuoteHeader(header, true);

                        BusinessLogic.DeleteQuoteItems(txtQuoteNumber.Text);
                        BusinessLogic.SaveQuoteItems(txtQuoteNumber.Text, allQuoteData, allLineItemDetails);

                        QuoteFooter footer = BuildQuoteFooter();
                        BusinessLogic.UpdateQuoteFooter(txtQuoteNumber.Text, footer);
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        CloneQuote();
                    }
                }
                else
                {
                    SaveNewQuote(txtQuoteNumber.Text);
                }

                FillSmartSearchData();

                FillCustomerNames();

                cbIsNewClient.IsChecked = false;
                customerID = BusinessLogic.GetCustomerID(txtQuoteNumber.Text);
                cmbCustomers.SelectedValue = customerID;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void SaveNewQuote(string quoteNumber)
        {
            try
            {
                QuoteHeader header = BuildQuoteHeader(quoteNumber);
                BusinessLogic.SaveQuoteHeader(header);

                BusinessLogic.SaveQuoteItems(quoteNumber, allQuoteData, allLineItemDetails);

                QuoteFooter footer = BuildQuoteFooter();
                BusinessLogic.SaveQuoteFooter(quoteNumber, footer);

                Helper.ShowInformationMessageBox("Quote saved successfully!");
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private QuoteHeader BuildQuoteHeader(string quoteNumber)
        {
            QuoteHeader header = new QuoteHeader();
            try
            {
                header.QuoteNumber = quoteNumber;
                header.QuoteCreatedOn = dtQuoteCreatedOn.SelectedDate.Value.ToShortDateString();
                header.QuoteRequestedOn = dtQuoteRequestedOn.SelectedDate.Value.ToShortDateString();
                header.CustomerPO = txtCustomerPO.Text;
                header.IsNewCustomer = cbIsNewClient.IsChecked == true;
                header.IsShipToOtherAddress = cbIsShipToSameAddress.IsChecked == true;
                header.ShippingMethodID = cmbShippingMethod.SelectedIndex;
                header.LeadTimeID = cmbLeadTime.SelectedIndex;
                header.LeadTimeTypeID = cmbLeadTimeType.SelectedIndex;

                header.PaymentModeID = int.Parse(cmbPaymentType.SelectedValue.ToString());
                header.QuoteStatusID = int.Parse(cmbQuoteStatus.SelectedValue.ToString());

                if (FirmSettings.IsAdmin)
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
                header.SoldTo.Misc = txtSoldToMisc.Text;

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
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return header;
        }

        private QuoteFooter BuildQuoteFooter()
        {
            QuoteFooter footer = new QuoteFooter();
            try
            {
                footer.SubTotal = double.Parse(lblSubTotal.Content.ToString().Replace("$ ", string.Empty));
                footer.IsDollar = cbDollar.IsChecked.Value == true;
                footer.EnergySurcharge = double.Parse(txtEnergySurcharge.Text);
                footer.Discount = double.Parse(txtDiscount.Text);
                footer.Delivery = double.Parse(txtDelivery.Text);
                footer.IsRushOrder = cbRush.IsChecked.Value == true;
                footer.RushOrder = double.Parse(txtRushOrder.Text);
                footer.Tax = double.Parse(txtTax.Text);
                footer.GrandTotal = double.Parse(lblGrandTotal.Content.ToString().Replace("$ ", string.Empty));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return footer;
        }

        private void cbIsNewClient_Checked(object sender, RoutedEventArgs e)
        {
            cmbCustomers.IsEnabled = false;
            ChangeCustomerDetailsStatus(false);
        }

        private void ChangeCustomerDetailsStatus(bool status)
        {
            txtSoldToFirstName.IsReadOnly = status;
            txtSoldToLastName.IsReadOnly = status;
            txtSoldToAddress.IsReadOnly = status;
            txtSoldToPhone.IsReadOnly = status;
            txtSoldToEmail.IsReadOnly = status;
            txtSoldToFax.IsReadOnly = status;
            txtSoldToMisc.IsReadOnly = status;

            txtShiptoFirstName.IsReadOnly = status;
            txtShiptoLastName.IsReadOnly = status;
            txtShipToAddress.IsReadOnly = status;
            txtShipToPhone.IsReadOnly = status;
            txtShipToEmail.IsReadOnly = status;
            txtShipToFax.IsReadOnly = status;
            txtShipToMisc.IsReadOnly = status;
        }

        private void cbIsNewClient_Unchecked(object sender, RoutedEventArgs e)
        {
            cmbCustomers.IsEnabled = true;
            ChangeCustomerDetailsStatus(true);
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
            try
            {
                if (Helper.IsValidCurrency(txtEnergySurcharge))
                {
                    if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtEnergySurcharge.Text) == false)
                    {
                        UpdateQuoteTotal();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void txtEnergySurcharge_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Helper.IsValidCurrency(txtEnergySurcharge))
                {
                    if (allQuoteData.Count > 0)
                    {
                        txtEnergySurcharge.Text = string.IsNullOrEmpty(txtEnergySurcharge.Text) ? "0.00" : txtEnergySurcharge.Text;
                    }
                }
                else
                {
                    txtEnergySurcharge.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cbDollar_Checked(object sender, RoutedEventArgs e)
        {
            lblEnergySurcharge.Content = "Energy Surcharge ($):";
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtEnergySurcharge.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void cbDollar_Unchecked(object sender, RoutedEventArgs e)
        {
            lblEnergySurcharge.Content = "Energy Surcharge (%):";
            if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtEnergySurcharge.Text) == false)
            {
                UpdateQuoteTotal();
            }
        }

        private void txtDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtDiscount))
            {
                if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtDiscount.Text) == false)
                {
                    UpdateQuoteTotal();
                }
            }
        }

        private void txtDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtDiscount))
            {
                if (allQuoteData.Count > 0)
                {
                    txtDiscount.Text = string.IsNullOrEmpty(txtDiscount.Text) ? "0.00" : txtDiscount.Text;
                }
            }
            else
            {
                txtDiscount.Text = "0.00";
            }
        }

        private void txtDelivery_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtDelivery))
            {
                if (allQuoteData.Count > 0)
                {
                    txtDelivery.Text = string.IsNullOrEmpty(txtDelivery.Text) ? "0.00" : txtDelivery.Text;
                }
            }
            else
            {
                txtDelivery.Text = "0.00";
            }
        }

        private void txtDelivery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtDelivery))
            {
                if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtDelivery.Text) == false)
                {
                    UpdateQuoteTotal();
                }
            }

        }

        private void txtRushOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtRushOrder))
            {
                if (allQuoteData.Count > 0 && cbRush.IsChecked == true && string.IsNullOrEmpty(txtRushOrder.Text) == false)
                {
                    UpdateQuoteTotal();
                }
            }
        }

        private void txtRushOrder_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtRushOrder))
            {
                if (allQuoteData.Count > 0 && cbRush.IsChecked == true)
                {
                    txtRushOrder.Text = string.IsNullOrEmpty(txtRushOrder.Text) ? "0.00" : txtRushOrder.Text;
                }
            }
            else
            {
                txtRushOrder.Text = "0.00";
            }
        }

        private void txtTax_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtTax))
            {
                if (allQuoteData.Count > 0)
                {
                    txtTax.Text = string.IsNullOrEmpty(txtTax.Text) ? "0.00" : txtTax.Text;
                }
            }
            else
            {
                txtTax.Text = "0.00";
            }
        }

        private void txtTax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Helper.IsValidCurrency(txtTax))
            {
                if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtTax.Text) == false)
                {
                    UpdateQuoteTotal();
                }
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
            try
            {
                QuoteGridEntity selectedLineItem = dgQuoteItems.SelectedItem as QuoteGridEntity;
                if (selectedLineItem == null)
                    return;
                double newUnitPrice = double.Parse(txtAdditionalCostForItem.Text) + double.Parse(selectedLineItem.UnitPrice);
                selectedLineItem.UnitPrice = newUnitPrice.ToString("0.00");
                selectedLineItem.Total = (newUnitPrice * selectedLineItem.Quantity).ToString("0.00");
                UpdateQuoteTotal();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           

        }

        private void btnUpdateAllItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (allQuoteData.Count > 0)
                {
                    foreach (QuoteGridEntity selectedLineItem in allQuoteData)
                    {
                        if (selectedLineItem == null || string.IsNullOrEmpty(txtAdditionalCostForItem.Text) || string.IsNullOrEmpty(selectedLineItem.UnitPrice))
                            continue;

                        double newUnitPrice = double.Parse(txtAdditionalCostForItem.Text) + double.Parse(selectedLineItem.UnitPrice);
                        selectedLineItem.UnitPrice = newUnitPrice.ToString("0.00");
                        selectedLineItem.Total = (newUnitPrice * selectedLineItem.Quantity).ToString("0.00");
                    }
                    UpdateQuoteTotal();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
          
        }

        private void btnOpenQuote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtSmartSearch.Text))
                //{
                //    Helper.ShowErrorMessageBox("Please select Customer/Quote No. first");
                //    return;
                //}
                if (cmbQuoteNumbers.SelectedIndex < 0 && cmbQuoteNumbers.SelectedItem == null)
                {
                    Helper.ShowErrorMessageBox("Please select Quote!");
                    return;
                }
                string quoteNumber = (cmbQuoteNumbers.SelectedItem as System.Data.DataRowView)[1].ToString();
                //foreach (string item in txtSmartSearch.Text.Split('-'))
                //{
                //    if (item.Trim().StartsWith("Q") || item.Trim().StartsWith("q"))
                //    {
                //        quoteNumber = item.Trim();
                //        break;
                //    }
                //}
                if (string.IsNullOrEmpty(quoteNumber))
                {
                    Helper.ShowErrorMessageBox("Invalid Quote Number");
                    return;
                }
                OpenSelectedQuote(quoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void OpenSelectedQuote(string quoteNumber)
        {
            try
            {
                QuoteEntity result = BusinessLogic.GetQuoteDetails(quoteNumber);
                if (result == null)
                {
                    Helper.ShowInformationMessageBox("No data found for selected quote!");
                    return;
                }

                cbIsNewClient.IsChecked = false;
                int customerID = BusinessLogic.GetCustomerID(result.Header.QuoteNumber);
                cmbCustomers.SelectedValue = customerID;

                #region Fill Header Information

                txtQuoteNumber.Text = result.Header.QuoteNumber;
                txtCustomerPO.Text = result.Header.CustomerPO;
                dtQuoteCreatedOn.SelectedDate = DateTime.Parse(result.Header.QuoteCreatedOn);
                dtQuoteRequestedOn.SelectedDate = DateTime.Parse(result.Header.QuoteRequestedOn);
                cmbPaymentType.SelectedValue = result.Header.PaymentModeID;
                cmbQuoteStatus.SelectedValue = result.Header.QuoteStatusID;

                if (result.Header.SoldTo != null)
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
                lblGrandTotal.Content = result.Footer.GrandTotal.ToString("0.00");

                #endregion
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
          
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
            try
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            
        }

        private void btnNewQuote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = Helper.ShowQuestionMessageBox("Are you sure to create new quote?");
                if (result == MessageBoxResult.Yes)
                {
                    ResetQuote();
                    GetNewQuoteID();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
         
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = Helper.ShowQuestionMessageBox("Are you sure to delete this quote?");
                if (result == MessageBoxResult.Yes)
                {
                    BusinessLogic.DeleteQuote(txtQuoteNumber.Text);
                    Helper.ShowInformationMessageBox("Quote deleted successfully.");
                    ResetQuote();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
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
            cmbQuoteStatus.SelectedIndex = 0;
        }

        private void ResetQuoteItems()
        {
            try
            {
                if (allQuoteData == null)
                    allQuoteData = new ObservableCollection<QuoteGridEntity>();

                allQuoteData.Clear();
                dgQuoteItems.ItemsSource = allQuoteData;

                allLineItemDetails = new ObservableCollection<NewQuoteItemEntity>();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
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
            txtEnergySurcharge.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtDelivery.Text = "0.00";
            cbRush.IsChecked = false;
            txtRushOrder.Text = "0.00";
            txtTax.Text = "0.00";
            lblGrandTotal.Content = "0.00";
        }

        private void txtCustomerPO_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtSoldToFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoldToFirstName.Text))
            {
                txtSoldToFirstName.Text = string.Empty;
            }
        }

        private void txtSoldToFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (txtSoldToFirstName.Text.Equals("First Name"))
            //{
            //    txtSoldToFirstName.Text = string.Empty;
            //}
        }

        private void txtSoldToLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoldToLastName.Text))
            {
                //txtSoldToLastName.Text = "Last Name";
                txtSoldToLastName.Text = string.Empty;
            }
        }

        private void txtSoldToLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (txtSoldToLastName.Text.Equals("Last Name"))
            //{
            //    txtSoldToLastName.Text = string.Empty;
            //}
        }

        private void txtShiptoFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (txtShiptoFirstName.Text.Equals("First Name"))
            //{
            //    txtShiptoFirstName.Text = string.Empty;
            //}
        }

        private void txtShiptoFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtShiptoFirstName.Text))
            {
                //txtShiptoFirstName.Text = "First Name";
                txtShiptoFirstName.Text = string.Empty;
            }
        }

        private void txtShiptoLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (txtShiptoLastName.Text.Equals("Last Name"))
            //{
            //    txtShiptoLastName.Text = string.Empty;
            //}
        }

        private void txtShiptoLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtShiptoLastName.Text))
            {
                //txtShiptoLastName.Text = "Last Name";
                txtShiptoLastName.Text = string.Empty;
            }
        }

        private void txtSoldToPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (false == Helper.IsValidPhone(txtSoldToPhone))
            {
                txtSoldToPhone.Text = string.Empty;
            }
        }

        private void txtSoldToEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (false == Helper.IsValidEmail(txtSoldToEmail))
            {
                txtSoldToEmail.Text = string.Empty;
            }
        }

        private void txtShipToEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (false == Helper.IsValidEmail(txtShipToEmail))
            {
                txtShipToEmail.Text = string.Empty;
            }
        }

        private void txtAdditionalCostForItem_LostFocus(object sender, RoutedEventArgs e)
        {
            if (false == Helper.IsValidCurrency(txtAdditionalCostForItem))
            {
                txtAdditionalCostForItem.Text = "0.00";
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintQuote();
        }

        private void PrintQuote()
        {
            try
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string clientName = string.Empty;

                int customerID = BusinessLogic.GetCustomerID(txtQuoteNumber.Text);

                if (string.IsNullOrWhiteSpace(txtSoldToFirstName.Text) == false && string.IsNullOrWhiteSpace(txtSoldToLastName.Text) == false)
                {
                    clientName = string.Format("{0} {1} {2}", txtSoldToFirstName.Text.Trim(), txtSoldToLastName.Text.Trim(), customerID.ToString());
                }
                else
                {
                    clientName = string.Format("{0} {1}", txtSoldToFirstName.Text.Trim(), customerID.ToString());
                }
                string relativePath = folderPath + Constants.FolderSeparator + Constants.RootDirectory + Constants.FolderSeparator + clientName + Constants.FolderSeparator + Constants.Quote + Constants.FolderSeparator;
                string filename = string.Format(Constants.QuoteFileName, txtQuoteNumber.Text);
                string completeFilePath = relativePath + Constants.FolderSeparator + filename;

                if (Directory.Exists(relativePath) == false)
                {
                    Directory.CreateDirectory(relativePath);
                }
                if (File.Exists(completeFilePath) == true)
                {
                    var result1 = Helper.ShowQuestionMessageBox("Quote with same number already exists. Do you want to overwrite it?");
                    if (result1 != MessageBoxResult.Yes)
                    {
                        return;
                    }
                }

                // Create a new PDF document
                PdfDocument document = new PdfDocument();

                // Create an empty page
                PdfPage page = document.AddPage();

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Print Company Logo
                PrintLogo(gfx);
                PrintQuoteHeader(gfx, PdfPrintingSetting.NormalFont);

                XPen pen = new XPen(XColors.Black, 1);
                gfx.DrawRoundedRectangle(pen, 80, 180, 1100, 200, 30, 20);

                PrintSoldToAddress(gfx, PdfPrintingSetting.NormalFont);
                PrintShipToAddress(gfx, PdfPrintingSetting.NormalFont);
                PrintShippingDetails(gfx, PdfPrintingSetting.NormalFont);
                PrintQuoteDetails(gfx, PdfPrintingSetting.NormalFont);
                PrintQuoteFooter(gfx, PdfPrintingSetting.NormalFont);

                //PrinterSettings settings = new PrinterSettings();
                //PrintDialog oDiaLog = new PrintDialog();
                //if (oDiaLog.ShowDialog().Value )
                //{

                //    PDFPrintSettings pdfPrintSettings = new PDFPrintSettings(oDiaLog.PrinterSettings);
                //    this.PdfDocument.Print(pdfPrintSettings); // Here PDFDocument is document of PDF sharp objects.

                //}


                // Save the document...
                document.Save(completeFilePath);
                // ...and start a viewer.
                var result = Helper.ShowQuestionMessageBox("File Saved. Do you want to open it now?");
                if (result == MessageBoxResult.Yes)
                {
                    Process.Start(completeFilePath);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private static void PrintLogo(XGraphics gfx)
        {
            try
            {
                XImage image = XImage.FromFile("Logo.jpg");
                const double dx = 350, dy = 140;
                //gfx.TranslateTransform(dx / 2, dy / 2);
                gfx.ScaleTransform(0.5);
                //gfx.TranslateTransform(-dx / 2, -dy / 2);
                double width = image.PixelWidth * 72 / image.HorizontalResolution;
                double height = image.PixelHeight * 72 / image.HorizontalResolution;
                gfx.DrawImage(image, 5, 5, dx, dy);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
         
        }

        private void PrintQuoteHeader(XGraphics gfx, XFont font)
        {
            try
            {
                int xBaseOffset = 800;
                int xIncrementalOffset = 940;
                int yBaseOffset = 15;
                int yIncrementalOffset = 25;
                int labelWidth = 100;
                int labelHeight = 200;

                // Print Quote Number
                gfx.DrawString(lblQuoteNo.Content.ToString(), font, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);
                gfx.DrawString(txtQuoteNumber.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                yBaseOffset += yIncrementalOffset;

                // Print Customer PO
                gfx.DrawString(lblCustomerPO.Content.ToString(), font, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                gfx.DrawString(txtCustomerPO.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                yBaseOffset += yIncrementalOffset;

                // Print Quote Created On
                gfx.DrawString(lblQuoteCreatedOn.Content.ToString(), font, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                gfx.DrawString(dtQuoteCreatedOn.SelectedDate.Value.ToShortDateString(), font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                yBaseOffset += yIncrementalOffset;

                // Print Quote Requested On
                gfx.DrawString(lblRequestedShipDate.Content.ToString(), font, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                gfx.DrawString(dtQuoteRequestedOn.SelectedDate.Value.ToShortDateString(), font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                yBaseOffset += yIncrementalOffset;

                // Print Payment Mode
                gfx.DrawString(lblPaymentType.Content.ToString(), font, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                gfx.DrawString(cmbPaymentType.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void PrintSoldToAddress(XGraphics gfx, XFont font)
        {
            try
            {
                int xBaseOffset = 100;
                int xIncrementalOffset = 170;
                int yBaseOffset = 200;
                int yIncrementalOffset = 25;
                int labelWidth = 100;
                int labelHeight = 200;

                // Print Sold To
                gfx.DrawString(lblSoldTo.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);

                // Print Name 
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblSoldToName.Content.ToString(), font, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                if (string.IsNullOrWhiteSpace(txtSoldToLastName.Text) == false)
                {
                    gfx.DrawString(string.Format("{0}, {1}", txtSoldToLastName.Text, txtSoldToFirstName.Text), font, XBrushes.Black,
               new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight), XStringFormat.TopLeft);
                }
                else
                {
                    gfx.DrawString(string.Format("{1}", txtSoldToLastName.Text, txtSoldToFirstName.Text), font, XBrushes.Black,
               new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight), XStringFormat.TopLeft);
                }


                // Print Phone
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblSoldToAddress.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtSoldToAddress.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Phone
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblSoldtoPhone.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtSoldToPhone.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Fax
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblSoldToFax.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtSoldToFax.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Email
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblSoldToEmail.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtSoldToEmail.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Misc
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblSoldToMisc.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtSoldToMisc.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }
        private void PrintShipToAddress(XGraphics gfx, XFont font)
        {
            try
            {
                int xBaseOffset = 500;
                int xIncrementalOffset = 560;
                int yBaseOffset = 200;
                int yIncrementalOffset = 25;
                int labelWidth = 100;
                int labelHeight = 100;

                // Print Ship To
                gfx.DrawString(lblShipTo.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);

                // Print Name 
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblShipToName.Content.ToString(), font, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                if (false == string.IsNullOrEmpty(txtShiptoFirstName.Text) && false == string.IsNullOrEmpty(txtShiptoLastName.Text))
                {
                    gfx.DrawString(string.Format("{0}, {1}", txtShiptoLastName.Text, txtShiptoFirstName.Text), font, XBrushes.Black,
                     new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                     XStringFormat.TopLeft);
                }
                else if(false == string.IsNullOrEmpty(txtShiptoFirstName.Text) && string.IsNullOrEmpty(txtShiptoLastName.Text)== true)
                {
                    gfx.DrawString(string.Format("{0}", txtShiptoFirstName.Text), font, XBrushes.Black,
                     new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                     XStringFormat.TopLeft);
                }

                // Print Phone
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblShipToAddress.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtShipToAddress.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Phone
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblSoldtoPhone.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtShipToPhone.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Fax
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblShipToFax.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtShipToFax.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Email
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblShipToEmail.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtShipToEmail.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Misc
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblShipToMisc.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(txtShipToMisc.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void PrintShippingDetails(XGraphics gfx, XFont font)
        {
            try
            {
                int xBaseOffset = 900;
                int xIncrementalOffset = 1050;
                int yBaseOffset = 200;
                int yIncrementalOffset = 25;
                int labelWidth = 100;
                int labelHeight = 100;

                XPen pen = new XPen(XColors.Black, 1);

                // Print Operator Name 
                gfx.DrawString(lblOperator.Content.ToString(), font, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                gfx.DrawString(cmbOperator.Text, font, XBrushes.Black,
               new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
               XStringFormat.TopLeft);

                // Print Shipping Method
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblShippingMethod.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(cmbShippingMethod.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Phone
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblLeadTime.Content.ToString(), font, XBrushes.Black,
             new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                gfx.DrawString(string.Format("{0} {1}", cmbLeadTime.Text, cmbLeadTimeType.Text), font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void PrintQuoteDetails(XGraphics gfx, XFont font)
        {
            try
            {
                int xStartDetailRect = 10;
                int yStartDetailRect = 400;
                int widthDetailRect = 1180;
                int heightDetailRect = 700;

                int heightHeaderRect = 50;

                int xLineColumn = 90;
                int xQuantityColumn = 150;
                int xDescriptionColumn = 790;
                int xDimensionColumn = 930;
                int xSqFtColumn = 990;
                int xUnitPriceColumn = 1080;
                int xTotalColumn = 1190;

                XPen pen = new XPen(XColors.Black, 1);
                XRect detailsRect = new XRect(xStartDetailRect, yStartDetailRect, widthDetailRect, heightDetailRect);
                gfx.DrawRectangle(pen, detailsRect);

                XStringFormat format = new XStringFormat();
                XRect headerRect = new XRect(xStartDetailRect, yStartDetailRect, widthDetailRect, heightHeaderRect);

                gfx.DrawRectangle(pen, XBrushes.Gray, headerRect);
                gfx.DrawLine(XPens.Black, xLineColumn, yStartDetailRect, xLineColumn, yStartDetailRect + detailsRect.Height);
                gfx.DrawLine(XPens.Black, xQuantityColumn, yStartDetailRect, xQuantityColumn, yStartDetailRect + detailsRect.Height);
                gfx.DrawLine(XPens.Black, xDescriptionColumn, yStartDetailRect, xDescriptionColumn, yStartDetailRect + detailsRect.Height);
                gfx.DrawLine(XPens.Black, xDimensionColumn, yStartDetailRect, xDimensionColumn, yStartDetailRect + detailsRect.Height);
                gfx.DrawLine(XPens.Black, xSqFtColumn, yStartDetailRect, xSqFtColumn, yStartDetailRect + detailsRect.Height);
                gfx.DrawLine(XPens.Black, xUnitPriceColumn, yStartDetailRect, xUnitPriceColumn, yStartDetailRect + detailsRect.Height);
                gfx.DrawLine(XPens.Black, xTotalColumn, yStartDetailRect, xTotalColumn, yStartDetailRect + detailsRect.Height);

                XBrush brush = XBrushes.White;
                gfx.DrawString("Line No.", font, brush, new XRect(xStartDetailRect + 15, yStartDetailRect + 15, xLineColumn, heightHeaderRect), format);
                gfx.DrawString("Qty", font, brush, new XRect(xLineColumn + 15, yStartDetailRect + 15, xQuantityColumn, heightHeaderRect), format);
                gfx.DrawString("Description", font, brush, new XRect(xQuantityColumn + 65, yStartDetailRect + 15, xDescriptionColumn, heightHeaderRect), format);
                gfx.DrawString("Dimension (in)", font, brush, new XRect(xDescriptionColumn + 15, yStartDetailRect + 15, xDimensionColumn, heightHeaderRect), format);
                gfx.DrawString("Sq.Ft.", font, brush, new XRect(xDimensionColumn + 15, yStartDetailRect + 15, xSqFtColumn, heightHeaderRect), format);
                gfx.DrawString("Price/Pc ($)", font, brush, new XRect(xSqFtColumn + 15, yStartDetailRect + 15, xUnitPriceColumn, heightHeaderRect), format);
                gfx.DrawString("Total ($)", font, brush, new XRect(xUnitPriceColumn + 15, yStartDetailRect + 15, xTotalColumn, heightHeaderRect), format);

                int yQuoteItemOffset = yStartDetailRect + 45;
                int yOffset = 20;
                brush = XBrushes.Black;

                XTextFormatter tf = new XTextFormatter(gfx);
                //gfx.DrawRectangle(XBrushes.SeaShell, rect);
                //tf.Alignment = ParagraphAlignment.Left;
                XRect rect;
                foreach (QuoteGridEntity selectedLineItem in allQuoteData)
                {
                    if (selectedLineItem == null || selectedLineItem.Description == null || selectedLineItem.Dimension == null)
                        continue;

                    XSize size = gfx.MeasureString(selectedLineItem.Description, font);

                    gfx.DrawString(selectedLineItem.LineID.ToString(), font, brush, new XRect(xStartDetailRect + 40, yQuoteItemOffset + yOffset, xLineColumn, heightHeaderRect), format);
                    gfx.DrawString(selectedLineItem.Quantity.ToString(), font, brush, new XRect(xLineColumn + 25, yQuoteItemOffset + yOffset, xQuantityColumn, heightHeaderRect), format);

                    rect = new XRect(xQuantityColumn + 15, yQuoteItemOffset + yOffset, xDescriptionColumn, heightHeaderRect + 100);
                    tf.DrawString(selectedLineItem.Description, font, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx.DrawString(selectedLineItem.Description, font, brush, new XRect(xQuantityColumn + 15, yQuoteItemOffset + yOffset, xDescriptionColumn, heightHeaderRect + size.Height), format);

                    gfx.DrawString(selectedLineItem.Dimension, font, brush, new XRect(xDescriptionColumn + 15, yQuoteItemOffset + yOffset, xDimensionColumn, heightHeaderRect), format);
                    gfx.DrawString(selectedLineItem.TotalSqFt, font, brush, new XRect(xDimensionColumn + 15, yQuoteItemOffset + yOffset, xSqFtColumn, heightHeaderRect), format);
                    gfx.DrawString(selectedLineItem.UnitPrice, font, brush, new XRect(xSqFtColumn + 15, yQuoteItemOffset + yOffset, xUnitPriceColumn, heightHeaderRect), format);
                    gfx.DrawString(double.Parse(selectedLineItem.Total).ToString("0.00"), font, brush, new XRect(xUnitPriceColumn + 15, yQuoteItemOffset + yOffset, xTotalColumn, heightHeaderRect), format);

                    yQuoteItemOffset += 50;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
       
        }

        private void PrintQuoteFooter(XGraphics gfx, XFont font)
        {
            try
            {
                int xBaseOffset = 900;
                int xIncrementalOffset = 1080;
                int yBaseOffset = 1150;
                int yIncrementalOffset = 25;
                int labelWidth = 100;
                int labelHeight = 100;

                // Print Quote Number
                gfx.DrawString(lblSubTotalCaption.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);

                gfx.DrawString(lblSubTotal.Content.ToString(), font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Quote Number
                yBaseOffset += yIncrementalOffset;
                string energy = cbDollar.IsChecked.Value ? "Energy Surcharge ($):" : "Energy Surcharge (%):";
                gfx.DrawString(energy, PdfPrintingSetting.BoldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);
                gfx.DrawString(txtEnergySurcharge.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Quote Number
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblDiscount.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);
                gfx.DrawString(txtDiscount.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Quote Number
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblDelivery.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);
                gfx.DrawString(txtDelivery.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);

                // Print Quote Number
                if (cbRush.IsChecked.Value)
                {
                    yBaseOffset += yIncrementalOffset;
                    gfx.DrawString(lblRushOrder.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                      new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                      XStringFormat.TopLeft);
                    gfx.DrawString(txtRushOrder.Text, font, XBrushes.Black,
                    new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                    XStringFormat.TopLeft);
                }

                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblTax.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);
                gfx.DrawString(txtTax.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);


                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblGrandTotalCaption.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);
                gfx.DrawString(lblGrandTotal.Content.ToString(), PdfPrintingSetting.BoldFont, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgQuoteItems.Items.Count == 0)
                {
                    Helper.ShowErrorMessageBox("There are no line items for this quote. Can not print empty quote!");
                    return;
                }
                PrintQuote();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void txtShipToPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            if (false == Helper.IsValidPhone(txtShipToPhone))
            {
                txtShipToPhone.Text = string.Empty;
            }
        }

        private void btnClone_Click(object sender, RoutedEventArgs e)
        {
            CloneQuote();
        }

        private void CloneQuote()
        {
            string quoteNumber = BusinessLogic.GetNewQuoteID();
            SaveNewQuote(quoteNumber);
        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            string mailToURL = @"mailto:balramchavan@gmail.com?Subject=SubjTxt&Body=Bod_Txt&Attachment=c:\\file.txt";
            try
            {
                Process.Start(mailToURL);

            }
            catch (Exception)
            {
                mailToURL = @"mailto:balramchavan@gmail.com?Subject=SubjTxt&Body=Bod_Txt&Attach=c:\\file.txt";
                Process.Start(mailToURL);

            }
        }

        private void btnSendToSO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Helper.IsNonEmpty(txtQuoteNumber))
                {
                    bool isQuoteNumberPresent = BusinessLogic.IsQuoteNumberPresent(txtQuoteNumber.Text);
                    if (isQuoteNumberPresent == false)
                    {
                        Helper.ShowErrorMessageBox("Given quote number not found in system.");
                        return;
                    }
                    else if (BusinessLogic.IsSalesOrderPresent(txtQuoteNumber.Text))
                    {
                        Helper.ShowErrorMessageBox("Sales Order already present for given quote.");
                        return;
                    }
                    else
                    {
                        BusinessLogic.GenerateSaleOrder(txtQuoteNumber.Text, DateTime.Now);
                        BusinessLogic.GenerateWorksheet(txtQuoteNumber.Text, DateTime.Now);
                        string worksheetNumber = BusinessLogic.GetWorksheetNumber(txtQuoteNumber.Text);

                        BusinessLogic.GenerateWorksheetItems(worksheetNumber, allQuoteData);
                        ShowSaleOrderForm();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void ShowSaleOrderForm()
        {
            try
            {
                Dashboard parent = Window.GetWindow(this) as Dashboard;
                if (parent != null)
                {
                    DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                    DashboardHelper.ChangeDashboardSelection(parent, sideMenu.btnSaleOrder);
                    SalesOrderContent soContent = new SalesOrderContent(true, txtQuoteNumber.Text);
                    parent.ucMainContent.ShowPage(soContent);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbQuoteStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbQuoteStatus.SelectedItem != null)
                {
                    btnSendToSO.IsEnabled = (cmbQuoteStatus.SelectedItem as System.Data.DataRowView)[1].Equals(ColumnNames.Confirmed);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void txtSoldToPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Helper.IsValidPhone(txtSoldToPhone);
        }

        private void txtShipToPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            Helper.IsValidPhone(txtShipToPhone);
        }

        private void txtSoldToEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Helper.IsValidEmail(txtSoldToEmail);
        }

        private void txtShipToEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Helper.IsValidEmail(txtShipToEmail);
        }

        internal void ClearTextBox(object sender, RoutedEventArgs e)
        {
            TextBox input = sender as TextBox;
            if (input.Text.Trim() == "0.00" || input.Text.Trim() == "0")
            {
                input.Text = string.Empty;
            }

        }

        private void txtAdditionalCostForItem_TextChanged(object sender, TextChangedEventArgs e)
        {
            Helper.IsValidCurrency(txtAdditionalCostForItem);
        }

        private void btnUpdteLineItemGrid_Click(object sender, RoutedEventArgs e)
        {
            dgQuoteItems.CanUserAddRows = false;
            dgQuoteItems.SelectedIndex = -1;
            dgQuoteItems.CanUserAddRows = true;

        }

        private void dgQuoteItems_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                QuoteGridEntity entity = e.Row.Item as QuoteGridEntity;
                if (entity == null)
                {
                    return;
                }
                switch (e.Column.DisplayIndex)
                {
                    case 1:
                        TextBox txtQuantity = e.EditingElement as TextBox;
                        if (txtQuantity != null)
                        {
                            int quantity = 0;
                            int.TryParse(txtQuantity.Text, out quantity);
                            entity.Quantity = quantity;
                        }
                        break;
                    case 5:
                        TextBox txtUnitPrice = e.EditingElement as TextBox;
                        if (txtUnitPrice != null)
                        {
                            double unitPrice = 0;
                            double.TryParse(txtUnitPrice.Text, out unitPrice);
                            entity.UnitPrice = unitPrice.ToString("0.00");
                        }
                        break;
                    default:
                        break;
                }
                if (e.Column.DisplayIndex == 1 || e.Column.DisplayIndex == 5)
                {
                    entity.Total = (double.Parse(entity.UnitPrice ?? "0") * entity.Quantity).ToString("0.00");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnAddNewLineItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuoteGridEntity entity = new QuoteGridEntity();
                entity.LineID = dgQuoteItems.Items.Count + 1;
                allQuoteData.Add(entity);

                ICollectionView dataView = CollectionViewSource.GetDefaultView(dgQuoteItems.ItemsSource);
                //clear the existing sort order
                dataView.SortDescriptions.Clear();
                //create a new sort order for the sorting that is done lastly
                dataView.SortDescriptions.Add(new SortDescription("LineID", ListSortDirection.Ascending));
                //refresh the view which in turn refresh the grid
                dataView.Refresh();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnDeleteLineItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuoteGridEntity selectedItem = dgQuoteItems.SelectedItem as QuoteGridEntity;
                if (selectedItem == null)
                    return;

                QuoteGridEntity tempItem = null;
                for (int index = dgQuoteItems.SelectedIndex + 1; index < dgQuoteItems.Items.Count; index++)
                {
                    tempItem = dgQuoteItems.Items[index] as QuoteGridEntity;
                    if (tempItem == null)
                        continue;
                    tempItem.LineID = tempItem.LineID - 1;
                }
                allQuoteData.Remove(selectedItem);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void cmbQuoteNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbQuoteNumbers.SelectedIndex < 0 && cmbQuoteNumbers.SelectedItem == null)
                {
                    Helper.ShowErrorMessageBox("Please select Quote!");
                    return;
                }
                string quoteNumber = (cmbQuoteNumbers.SelectedItem as System.Data.DataRowView)[1].ToString();
                //foreach (string item in txtSmartSearch.Text.Split('-'))
                //{
                //    if (item.Trim().StartsWith("Q") || item.Trim().StartsWith("q"))
                //    {
                //        quoteNumber = item.Trim();
                //        break;
                //    }
                //}
                if (string.IsNullOrEmpty(quoteNumber))
                {
                    Helper.ShowErrorMessageBox("Invalid Quote Number");
                    return;
                }
                OpenSelectedQuote(quoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cbIsShipToSameAddress_Checked(object sender, RoutedEventArgs e)
        {
            EnableShippingAddressControls(true);
        }

        private void EnableShippingAddressControls(bool status)
        {
            ClearShippintAddressControls();

            txtShipToAddress.IsReadOnly = !status;
            txtShipToEmail.IsReadOnly = !status;
            txtShipToFax.IsReadOnly = !status;
            txtShiptoFirstName.IsReadOnly = !status;
            txtShiptoLastName.IsReadOnly = !status;
            txtShipToMisc.IsReadOnly = !status;
            txtShipToPhone.IsReadOnly = !status;
            txtShiptoLastName.IsReadOnly = !status;
        }

        private void ClearShippintAddressControls()
        {
            txtShipToAddress.Text = string.Empty;
            txtShipToEmail.Text = string.Empty;
            txtShipToFax.Text = string.Empty;
            txtShiptoFirstName.Text = string.Empty;
            txtShiptoLastName.Text = string.Empty;
            txtShipToMisc.Text = string.Empty;
            txtShipToPhone.Text = string.Empty;
            txtShiptoLastName.Text = string.Empty;
        }

        private void cbIsShipToSameAddress_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableShippingAddressControls(false);
        }

        private void dgQuoteItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            QuoteGridEntity selectedItem = dgQuoteItems.SelectedItem as QuoteGridEntity;
            if(selectedItem == null)
            {
                return;
            }
            NewQuoteItemEntity lineItem = BusinessLogic.GetLineItemDetails(txtQuoteNumber.Text, selectedItem.LineID);
            if(lineItem == null)
            {
                return;
            }
             Dashboard parent = Window.GetWindow(this) as Dashboard;
             if (parent != null)
             {
                 NewQuoteContent content = parent.ucMainContent.CurrentPage as NewQuoteContent;
                 if (content != null)
                 {
                     NewQuoteItemsContent item = content.ucNewQuoteItems.CurrentPage as NewQuoteItemsContent;
                     if (item != null)
                     {
                         if (item.currentItem == null)
                         {
                             item.currentItem = new NewQuoteItemEntity();
                         }
                         item.currentItem = lineItem;
                         item.FillLineItemDetails();
                     }
                 }
             }
        }
    }
}
