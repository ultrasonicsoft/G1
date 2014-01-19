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
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for NewQuoteGridContent.xaml
    /// </summary>
    public partial class BarcodePrinter : UserControl
    {
        private ObservableCollection<QuoteGridEntity> _allQuoteData = new ObservableCollection<QuoteGridEntity>();
        private ObservableCollection<BarcodeLabel> _allPrintQueueJobs = new ObservableCollection<BarcodeLabel>();

        public ObservableCollection<QuoteGridEntity> allQuoteData
        {
            get { return _allQuoteData; }
            set { _allQuoteData = value; }
        }

        public ObservableCollection<BarcodeLabel> allPrintQueueJobs
        {
            get { return _allPrintQueueJobs; }
            set { _allPrintQueueJobs = value; }
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

            FillPrintJobQueue();
        }

        private void FillPrintJobQueue()
        {
            try
            {
                var result = BusinessLogic.GetPrintJobQueue();
                dgPrintQueue.ItemsSource = result;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillAllWorksheetNumbers()
        {
            try
            {
                var result = BusinessLogic.GetAllWorksheetNumbers();
                cmbWorksheetNumbers.DisplayMemberPath = ColumnNames.Type;
                cmbWorksheetNumbers.SelectedValuePath = ColumnNames.ID;
                cmbWorksheetNumbers.ItemsSource = result.DefaultView;
                cmbWorksheetNumbers.SelectedIndex = -1;
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

        private void OpenSelectedWorksheet(string quoteNumber)
        {
            try
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

        private void btnOpenSO_Click(object sender, RoutedEventArgs e)
        {
            OpenWorksheet();
        }

        private void OpenWorksheet()
        {
            try
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open a label format specifying a different printer 
                if (cmbPrinterSelection.SelectedValue == null)
                {
                    Helper.ShowErrorMessageBox("Please select printer!");
                    return;
                }

                if (cbPrintQueue.IsChecked.Value)
                {
                    PrintAllLabelFromQueue();
                }
                else
                {
                    List<BarcodeEntity> barcode = BusinessLogic.GetBarcodeDetails(txtQuoteNumber.Text);

                    if (barcode == null)
                    {
                        Helper.ShowErrorMessageBox("No data found for printing barcode!");
                        return;
                    }
                    PrintBarcode(barcode);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void PrintAllLabelFromQueue()
        {
            // Print queue barcode
            foreach (BarcodeLabel item in dgPrintQueue.Items)
            {
                if (item == null)
                {
                    return;
                }
                PrintLineItemFromQueue(item.ID, item.WSNumber, item.LineID, item.ItemID - 1);
            }
            Helper.ShowInformationMessageBox("All labels have been printed successfully!");
        }

        private void PrintBarcode(List<BarcodeEntity> allBarcodeData)
        {
            // Initialize and start a new engine 
            try
            {
                using (Engine btEngine = new Engine())
                {
                    btEngine.Start();

                    //TODO: corret logic
                    Result result = Result.Failure;

                    string fileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + BarCodeConstants.BarcodeTemplateFileName;
                    foreach (BarcodeEntity barcode in allBarcodeData)
                    {
                        result = PrintLineItem(btEngine, fileName, barcode);
                    }

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
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private Result PrintLineItem(Engine btEngine, string fileName, BarcodeEntity barcode)
        {
            Result result = Result.Failure;
            try
            {

                int currentLineItemQuantity = int.Parse(barcode.Quantity);
                for (int index = 0; index < currentLineItemQuantity; index++)
                {
                    result = PrintIndividualLineItem(btEngine, fileName, barcode, result, index,txtWSNumber.Text);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        private Result PrintIndividualLineItem(Engine btEngine, string fileName, BarcodeEntity barcode, Result result, int itemID,string wsNumber)
        {
            // Open a label format specifying the default printer 
            LabelFormatDocument btFormat = btEngine.Documents.Open(fileName);

            if (btFormat.SubStrings[BarCodeConstants.CustomerName] != null)
            {
                btFormat.SubStrings[BarCodeConstants.CustomerName].Value = barcode.LastName + " " + barcode.FirstName;
            }
            if (btFormat.SubStrings[BarCodeConstants.CurrentQuantity] != null)
            {
                btFormat.SubStrings[BarCodeConstants.CurrentQuantity].Value = (itemID + 1).ToString();
            }
            if (btFormat.SubStrings[BarCodeConstants.TotalQuantity] != null)
            {
                btFormat.SubStrings[BarCodeConstants.TotalQuantity].Value = barcode.Quantity;
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
                btFormat.SubStrings[BarCodeConstants.Logo].Value = barcode.Logo;
            }
            if (btFormat.SubStrings[BarCodeConstants.Description] != null)
            {
                btFormat.SubStrings[BarCodeConstants.Description].Value = barcode.Description;
            }
            if (btFormat.SubStrings[BarCodeConstants.BarcodeItem] != null)
            {
                btFormat.SubStrings[BarCodeConstants.BarcodeItem].Value = wsNumber + "-" + barcode.Line + "-" + (itemID + 1).ToString();
            }
            if (btFormat.SubStrings[BarCodeConstants.DueDate] != null)
            {
                btFormat.SubStrings[BarCodeConstants.DueDate].Value = barcode.DueDate;
            }
            if (btFormat.SubStrings[BarCodeConstants.GlassShape] != null)
            {
                btFormat.SubStrings[BarCodeConstants.GlassShape].Value = barcode.Shape;
            }
            string printerName = cmbPrinterSelection.SelectedValue.ToString();
            btFormat = btEngine.Documents.Open(fileName, printerName);
            // Print the label 
            result = btFormat.Print();
            return result;
        }

        public  void PrintLineItemFromQueue(int id, string wsNumber, int lineID, int itemID)
        {
            // Initialize and start a new engine 
            try
            {
                BarcodeEntity barcode = BusinessLogic.GetSpecificBarcodeDetail(wsNumber, lineID);

                if (barcode == null)
                {
                    Helper.ShowErrorMessageBox("No data found for printing barcode!");
                    return;
                }

                using (Engine btEngine = new Engine())
                {
                    btEngine.Start();

                    //TODO: corret logic
                    Result result = Result.Failure;

                    string fileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + BarCodeConstants.BarcodeTemplateFileName;
                    result = PrintIndividualLineItem(btEngine, fileName, barcode, result, itemID,barcode.Worksheet);

                    if (result == Result.Success)
                    {
                        BusinessLogic.RemoveLabelFromPrintQueue(id);
                        FillPrintJobQueue();
                        //Helper.ShowInformationMessageBox("Barcoded printing successfully!");
                    }
                    else
                    {
                        Helper.ShowInformationMessageBox("Barcoded printing failed!");
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbWorksheetNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenWorksheet();
        }

        private void cmbPrinterSelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnPrintSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Open a label format specifying a different printer 
                if (cmbPrinterSelection.SelectedValue == null)
                {
                    Helper.ShowErrorMessageBox("Please select printer!");
                    return;
                }

                if (cbPrintQueue.IsChecked.Value)
                {
                    // Print queue barcode
                    BarcodeLabel selectedJob = dgPrintQueue.SelectedItem as BarcodeLabel;
                    if(selectedJob == null)
                    {
                        return;
                    }
                    PrintLineItemFromQueue(selectedJob.ID, selectedJob.WSNumber, selectedJob.LineID, selectedJob.ItemID-1);
                    Helper.ShowInformationMessageBox("Label has been printed successfully!");
                }
                else
                {
                    // Print selected worksheet barcodes
                    using (Engine btEngine = new Engine())
                    {
                        btEngine.Start();
                        string fileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + BarCodeConstants.BarcodeTemplateFileName;

                        List<BarcodeEntity> allBarcodeData = BusinessLogic.GetBarcodeDetails(txtQuoteNumber.Text);
                        if (allBarcodeData == null)
                        {
                            Helper.ShowErrorMessageBox("No data found for printing barcode!");
                            return;
                        }
                        foreach (QuoteGridEntity item in dgQuoteItems.SelectedItems)
                        {
                            if (item != null)
                            {
                                BarcodeEntity barcode = allBarcodeData.FirstOrDefault(x => int.Parse(x.Line) == item.LineID);
                                if (barcode == null)
                                {
                                    continue;
                                }
                                PrintLineItem(btEngine, fileName, barcode);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cbPrintQueue_Checked(object sender, RoutedEventArgs e)
        {
            dgPrintQueue.IsEnabled = true;
            dgQuoteItems.IsEnabled = false;
        }

        private void cbPrintQueue_Unchecked(object sender, RoutedEventArgs e)
        {
            dgQuoteItems.IsEnabled = false;
            dgPrintQueue.IsEnabled = true;
        }
    }
}
