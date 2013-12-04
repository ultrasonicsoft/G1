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

        private void FillPaymentTypes()
        {
            var result = BusinessLogic.GetAllPaymentTypes();
            cmbPaymentType.DisplayMemberPath = ColumnNames.Type;
            cmbPaymentType.SelectedValuePath = ColumnNames.ID;
            cmbPaymentType.ItemsSource = result.DefaultView;
            cmbPaymentType.SelectedIndex = 0;
        }

        private void FillQuoteStatus()
        {
            var result = BusinessLogic.GetAllQuoteStatus();
            cmbQuoteStatus.DisplayMemberPath = ColumnNames.Type;
            cmbQuoteStatus.SelectedValuePath = ColumnNames.ID;
            cmbQuoteStatus.ItemsSource = result.DefaultView;
            cmbQuoteStatus.SelectedIndex = 0;
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
                if (item == null || item.Total == null)
                    continue;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
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
                    BusinessLogic.UpdateQuoteHeader(header);

                    BusinessLogic.DeleteQuoteItems(txtQuoteNumber.Text);
                    BusinessLogic.SaveQuoteItems(txtQuoteNumber.Text, allQuoteData);

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

        }

        private void SaveNewQuote(string quoteNumber)
        {
            QuoteHeader header = BuildQuoteHeader(quoteNumber);
            BusinessLogic.SaveQuoteHeader(header);

            BusinessLogic.SaveQuoteItems(quoteNumber, allQuoteData);

            QuoteFooter footer = BuildQuoteFooter();
            BusinessLogic.SaveQuoteFooter(quoteNumber, footer);

            Helper.ShowInformationMessageBox("Quote saved successfully!");
        }

        private QuoteHeader BuildQuoteHeader(string quoteNumber)
        {
            QuoteHeader header = new QuoteHeader();
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
            return header;
        }

        private QuoteFooter BuildQuoteFooter()
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
            if (Helper.IsValidCurrency(txtEnergySurcharge))
            {
                if (allQuoteData.Count > 0 && string.IsNullOrEmpty(txtEnergySurcharge.Text) == false)
                {
                    UpdateQuoteTotal();
                }
            }
        }

        private void txtEnergySurcharge_LostFocus(object sender, RoutedEventArgs e)
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
            QuoteGridEntity selectedLineItem = dgQuoteItems.SelectedItem as QuoteGridEntity;
            if (selectedLineItem == null)
                return;
            selectedLineItem.Total = ((double.Parse(txtAdditionalCostForItem.Text) + double.Parse(selectedLineItem.UnitPrice)) * selectedLineItem.Quantity).ToString("0.00");
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

                    selectedLineItem.Total = ((double.Parse(txtAdditionalCostForItem.Text) + double.Parse(selectedLineItem.UnitPrice)) * selectedLineItem.Quantity).ToString("0.00");
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
                txtSoldToFirstName.Text = "First Name";
            }
        }

        private void txtSoldToFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSoldToFirstName.Text.Equals("First Name"))
            {
                txtSoldToFirstName.Text = string.Empty;
            }
        }

        private void txtSoldToLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSoldToLastName.Text))
            {
                txtSoldToLastName.Text = "Last Name";
            }
        }

        private void txtSoldToLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSoldToLastName.Text.Equals("Last Name"))
            {
                txtSoldToLastName.Text = string.Empty;
            }
        }

        private void txtShiptoFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtShiptoFirstName.Text.Equals("First Name"))
            {
                txtShiptoFirstName.Text = string.Empty;
            }
        }

        private void txtShiptoFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtShiptoFirstName.Text))
            {
                txtShiptoFirstName.Text = "First Name";
            }
        }

        private void txtShiptoLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtShiptoLastName.Text.Equals("Last Name"))
            {
                txtShiptoLastName.Text = string.Empty;
            }
        }

        private void txtShiptoLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtShiptoLastName.Text))
            {
                txtShiptoLastName.Text = "Last Name";
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
                string clientName = string.Format("{0} {1}", txtSoldToFirstName.Text, txtSoldToLastName.Text);
                string relativePath = folderPath + Constants.FolderSeparator + Constants.RootDirectory + Constants.FolderSeparator+  clientName + Constants.FolderSeparator + Constants.Quote + Constants.FolderSeparator;
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

                // Create a font
                XFont font = new XFont("Verdana", 12, XFontStyle.Regular);

                // Print Company Logo
                PrintLogo(gfx);
                PrintQuoteHeader(gfx, font);

                XPen pen = new XPen(XColors.Black, 1);
                gfx.DrawRoundedRectangle(pen, 80, 180, 1100, 200, 30, 20);

                PrintSoldToAddress(gfx, font);
                PrintShipToAddress(gfx, font);
                PrintShippingDetails(gfx, font);
                PrintQuoteDetails(gfx, font);
                PrintQuoteFooter(gfx, font);

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
                MessageBox.Show(ex.Message);
            }
        }

        private static void PrintLogo(XGraphics gfx)
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

        private void PrintQuoteHeader(XGraphics gfx, XFont font)
        {
            int xBaseOffset = 800;
            int xIncrementalOffset = 940;
            int yBaseOffset = 15;
            int yIncrementalOffset = 25;
            int labelWidth = 100;
            int labelHeight = 100;

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

        private void PrintShipToAddress(XGraphics gfx, XFont font)
        {
            int xBaseOffset = 500;
            int xIncrementalOffset = 560;
            int yBaseOffset = 200;
            int yIncrementalOffset = 25;
            int labelWidth = 100;
            int labelHeight = 100;

            XFont boldFont = new XFont("Verdana", 12, XFontStyle.Bold);

            // Print Ship To
            gfx.DrawString(lblShipTo.Content.ToString(), boldFont, XBrushes.Black,
              new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
              XStringFormat.TopLeft);

            // Print Name 
            yBaseOffset += yIncrementalOffset;
            gfx.DrawString(lblShipToName.Content.ToString(), font, XBrushes.Black,
            new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);

            gfx.DrawString(string.Format("{0}, {1}", txtShiptoLastName.Text, txtShiptoFirstName.Text), font, XBrushes.Black,
           new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
           XStringFormat.TopLeft);

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

        private void PrintSoldToAddress(XGraphics gfx, XFont font)
        {
            int xBaseOffset = 100;
            int xIncrementalOffset = 160;
            int yBaseOffset = 200;
            int yIncrementalOffset = 25;
            int labelWidth = 100;
            int labelHeight = 100;




            XFont boldFont = new XFont("Verdana", 12, XFontStyle.Bold);
            // Print Sold To
            gfx.DrawString(lblSoldTo.Content.ToString(), boldFont, XBrushes.Black,
              new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
              XStringFormat.TopLeft);

            // Print Name 
            yBaseOffset += yIncrementalOffset;
            gfx.DrawString(lblSoldToName.Content.ToString(), font, XBrushes.Black,
            new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);

            gfx.DrawString(string.Format("{0}, {1}", txtSoldToLastName.Text, txtSoldToFirstName.Text), font, XBrushes.Black,
           new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
           XStringFormat.TopLeft);

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

        private void PrintShippingDetails(XGraphics gfx, XFont font)
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

        private void PrintQuoteDetails(XGraphics gfx, XFont font)
        {
            int xStartDetailRect = 80;
            int yStartDetailRect = 400;
            int widthDetailRect = 1100;
            int heightDetailRect = 700;

            int heightHeaderRect = 50;

            int xLineColumn = 150;
            int xQuantityColumn = 200;
            int xDescriptionColumn = 790;
            int xDimensionColumn = 920;
            int xSqFtColumn = 980;
            int xUnitPriceColumn = 1080;
            int xTotalColumn = 1180;

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

        private void PrintQuoteFooter(XGraphics gfx, XFont font)
        {
            int xBaseOffset = 900;
            int xIncrementalOffset = 1060;
            int yBaseOffset = 1150;
            int yIncrementalOffset = 25;
            int labelWidth = 100;
            int labelHeight = 100;

            XFont boldFont = new XFont("Verdana", 12, XFontStyle.Bold);

            // Print Quote Number
            gfx.DrawString(lblSubTotalCaption.Content.ToString(), boldFont, XBrushes.Black,
              new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
              XStringFormat.TopLeft);

            gfx.DrawString(lblSubTotal.Content.ToString(), font, XBrushes.Black,
            new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);

            // Print Quote Number
            yBaseOffset += yIncrementalOffset;
            string energy = cbDollar.IsChecked.Value ? "Energy Surcharge ($):" : "Energy Surcharge (%):";
            gfx.DrawString(energy, boldFont, XBrushes.Black,
              new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
              XStringFormat.TopLeft);
            gfx.DrawString(txtEnergySurcharge.Text, font, XBrushes.Black,
            new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);

            // Print Quote Number
            yBaseOffset += yIncrementalOffset;
            gfx.DrawString(lblDiscount.Content.ToString(), boldFont, XBrushes.Black,
              new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
              XStringFormat.TopLeft);
            gfx.DrawString(txtDiscount.Text, font, XBrushes.Black,
            new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);

            // Print Quote Number
            yBaseOffset += yIncrementalOffset;
            gfx.DrawString(lblDelivery.Content.ToString(), boldFont, XBrushes.Black,
              new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
              XStringFormat.TopLeft);
            gfx.DrawString(txtDelivery.Text, font, XBrushes.Black,
            new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);

            // Print Quote Number
            if (cbRush.IsChecked.Value)
            {
                yBaseOffset += yIncrementalOffset;
                gfx.DrawString(lblRushOrder.Content.ToString(), boldFont, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);
                gfx.DrawString(txtRushOrder.Text, font, XBrushes.Black,
                new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
                XStringFormat.TopLeft);
            }

            yBaseOffset += yIncrementalOffset;
            gfx.DrawString(lblTax.Content.ToString(), boldFont, XBrushes.Black,
              new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
              XStringFormat.TopLeft);
            gfx.DrawString(txtTax.Text, font, XBrushes.Black,
            new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);


            yBaseOffset += yIncrementalOffset;
            gfx.DrawString(lblGrandTotalCaption.Content.ToString(), boldFont, XBrushes.Black,
            new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);
            gfx.DrawString(lblGrandTotal.Content.ToString(), boldFont, XBrushes.Black,
            new XRect(xIncrementalOffset, yBaseOffset, labelWidth, labelHeight),
            XStringFormat.TopLeft);
        }

        private void btnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            PrintQuote();
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
                    ShowSaleOrderForm();
                }
            }
        }

        private void ShowSaleOrderForm()
        {
            Dashboard parent = Window.GetWindow(this) as Dashboard;
            if (parent != null)
            {
                DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                DashboardHelper.ChangeDashboardSelection(parent, sideMenu.btnSaleOrder);
                SalesOrderContent soContent = new SalesOrderContent(true,txtQuoteNumber.Text);
                parent.ucMainContent.ShowPage(soContent);
            }
        }

        private void cmbQuoteStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbQuoteStatus.SelectedItem != null)
            {
                btnSendToSO.IsEnabled = (cmbQuoteStatus.SelectedItem as System.Data.DataRowView)[1].Equals(ColumnNames.Confirmed);
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
    }
}
