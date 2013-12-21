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
using Seagull.BarTender.Print;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for NewQuoteGridContent.xaml
    /// </summary>
    public partial class BarcodePrinter : UserControl
    {
        private ObservableCollection<QuoteGridEntity> _allQuoteData = new ObservableCollection<QuoteGridEntity>();

        public ObservableCollection<QuoteGridEntity> allQuoteData
        {
            get { return _allQuoteData; }
            set { _allQuoteData = value; }
        }

        public BarcodePrinter()
        {
            InitializeComponent();

            ConfigureInitialSetup();
        }

        public BarcodePrinter(bool _isOpenQuoteRequested, string _quoteNumber)
        {
            InitializeComponent();

            if (_isOpenQuoteRequested == true)
            {
                ConfigureInitialSetup();

                OpenSelectedWorksheet(_quoteNumber);
            }
        }

        private void ConfigureInitialSetup()
        {
            FillShippingMethods();
            FillLeadTimeTypes();
            FillLeadTime();
            FillPaymentTypes();
            FillSmartSearchData();

            FillAllWorksheetNumbers();
        }

        private void FillAllWorksheetNumbers()
        {
            var result = BusinessLogic.GetAllWorksheetNumbers();
            cmbWorksheetNumbers.DisplayMemberPath = ColumnNames.Type;
            cmbWorksheetNumbers.SelectedValuePath = ColumnNames.ID;
            cmbWorksheetNumbers.ItemsSource = result.DefaultView;
            cmbWorksheetNumbers.SelectedIndex = -1;
        }

        private void FillSmartSearchData()
        {
            //txtSmartSearch.ItemsSource = BusinessLogic.GetSmartSearchData();
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

        private void FillPaymentTypes()
        {
            var result = BusinessLogic.GetAllPaymentTypes();
            cmbPaymentType.DisplayMemberPath = ColumnNames.Type;
            cmbPaymentType.SelectedValuePath = ColumnNames.ID;
            cmbPaymentType.ItemsSource = result.DefaultView;
            cmbPaymentType.SelectedIndex = 0;
        }

        private void OpenSelectedWorksheet(string quoteNumber)
        {
            QuoteEntity result = BusinessLogic.GetQuoteDetails(quoteNumber);
            if (result == null)
            {
                Helper.ShowInformationMessageBox("No data found for selected quote!");
                return;
            }

            txtWSNumber.Text = result.Header.WorksheetNumber;

            #region Fill Header Information

            txtQuoteNumber.Text = result.Header.QuoteNumber;
            txtCustomerPO.Text = result.Header.CustomerPO;
            dtWSCreatedOn.SelectedDate = DateTime.Parse(result.Header.SaleOrderConfirmedOn);
            dtQuoteRequestedOn.SelectedDate = DateTime.Parse(result.Header.QuoteRequestedOn);
            cmbPaymentType.SelectedValue = result.Header.PaymentModeID;

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

        private void btnOpenSO_Click(object sender, RoutedEventArgs e)
        {
            OpenWorksheet();
        }

        private void OpenWorksheet()
        {
            if (cmbWorksheetNumbers.SelectedIndex < 0 && cmbWorksheetNumbers.SelectedItem == null)
            {
                Helper.ShowErrorMessageBox("Please select Worksheet number");
                return;
            }
            string quoteNumber = (cmbWorksheetNumbers.SelectedItem as System.Data.DataRowView)[0].ToString();
            if (string.IsNullOrEmpty(quoteNumber))
            {
                Helper.ShowErrorMessageBox("Invalid Quote Number associated with this Worksheet. No data found!");
                return;
            }
            OpenSelectedWorksheet(quoteNumber);
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            BarcodeEntity barcode = BusinessLogic.GetBarcodeDetails(txtQuoteNumber.Text);

            PrintBarcode(barcode);
        }

        private void PrintBarcode(BarcodeEntity barcode)
        {
            // Initialize and start a new engine 

            using (Engine btEngine = new Engine())
            {
                ;

                string fileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) +  @"\Glass_Barcode_Template.btw";
                btEngine.Start();
                // Open a label format specifying the default printer 
                LabelFormatDocument btFormat = btEngine.Documents.Open(fileName);

                if (btFormat.SubStrings[BarCodeConstants.CustomerName] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.CustomerName].Value = barcode.LastName + " " + barcode.FirstName;
                }
                if (btFormat.SubStrings[BarCodeConstants.SalesOrder] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.SalesOrder].Value = barcode.SalesOrder;
                }
                if (btFormat.SubStrings[BarCodeConstants.OrderDate] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.OrderDate].Value = barcode.OrderDate;
                }
                if (btFormat.SubStrings[BarCodeConstants.CustomerPO] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.CustomerPO].Value = barcode.CustomerPO;
                }
                if (btFormat.SubStrings[BarCodeConstants.Worksheet] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.Worksheet].Value = barcode.Worksheet;
                }
                if (btFormat.SubStrings[BarCodeConstants.Size] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.Size].Value = barcode.Size;
                }
                if (btFormat.SubStrings[BarCodeConstants.SQFT] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.SQFT].Value = barcode.SqFt;
                }
                if (btFormat.SubStrings[BarCodeConstants.Logo] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.Logo].Value = barcode.Logo =="1"? "Logo" : "No Logo";
                }
                if (btFormat.SubStrings[BarCodeConstants.Description] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.Description].Value = barcode.Description;
                }
                if (btFormat.SubStrings[BarCodeConstants.BarcodeItem] != null)
                {
                    btFormat.SubStrings[BarCodeConstants.BarcodeItem].Value = txtWSNumber.Text + "-" + barcode.Line;
                }
                // Open a label format specifying a different printer 
                btFormat = btEngine.Documents.Open(fileName, "Microsoft XPS Document Writer");
                // Print the label 
                Result result = btFormat.Print();

                if (result == Result.Success)
                {
                    Helper.ShowInformationMessageBox("Barcoded printing successfully!");
                }
                else
                {
                    Helper.ShowInformationMessageBox("Barcoded printing failed!");
                }
            } 
        }

        private void DeleteWorksheet()
        {
            if (Helper.IsNonEmpty(txtQuoteNumber) && Helper.IsNonEmpty(txtWSNumber))
            {
                bool isWorksheetPresent = BusinessLogic.IsWorksheetPresent(txtQuoteNumber.Text);
                if (isWorksheetPresent)
                {
                    BusinessLogic.DeleteWorksheet(txtQuoteNumber.Text);
                    Helper.ShowInformationMessageBox("Worksheet is delete successfully!");
                }
            }
            else
            {
                Helper.ShowErrorMessageBox("Please provide required details.");
            }
        }

        private void cmbWorksheetNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenWorksheet();
        }
    }
}
