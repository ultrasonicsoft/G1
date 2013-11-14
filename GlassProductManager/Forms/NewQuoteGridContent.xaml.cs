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

            GetNewQuoteID();
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
            txtSubTotal.Text = "0.00";
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
            SaveQuoteHeader();

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

            header.SoldTo = new ShippingDetails();
            header.SoldTo.FirstName = txtSoldToFirstName.Text;
            header.SoldTo.LastName = txtSoldToLastName.Text;
            header.SoldTo.Address = txtSoldToAddress.Text;
            header.SoldTo.Phone = txtSoldToPhone.Text;
            header.SoldTo.Email = txtSoldToEmail.Text;
            header.SoldTo.Fax = txtSoldToFax.Text;
            header.SoldTo.Misc = txtShipToMisc.Text;

            if (cbIsShipToSameAddress.IsChecked == true)
            {
                header.ShipTo = new ShippingDetails();
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

        private void cbIsNewClient_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbIsNewClient_Unchecked(object sender, RoutedEventArgs e)
        {

        }
       
    }
}
