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
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for NewQuoteGridContent.xaml
    /// </summary>
    public partial class SalesOrderContent : UserControl
    {
        private ObservableCollection<QuoteGridEntity> _allQuoteData = new ObservableCollection<QuoteGridEntity>();

        public ObservableCollection<QuoteGridEntity> allQuoteData
        {
            get { return _allQuoteData; }
            set { _allQuoteData = value; }
        }

        public SalesOrderContent()
        {
            InitializeComponent();

            ConfigureInitialSetup();
        }

        public SalesOrderContent(bool _isOpenQuoteRequested, string _quoteNumber)
        {
            InitializeComponent();

            if (_isOpenQuoteRequested == true)
            {
                ConfigureInitialSetup();

                OpenSelectedSaleOrder(_quoteNumber);
            }
        }

        private void ConfigureInitialSetup()
        {
            FillShippingMethods();
            FillLeadTimeTypes();
            FillLeadTime();
            FillPaymentTypes();
            FillSmartSearchData();
            FillAllSalesOrderNumbers();
        }

        private void FillAllSalesOrderNumbers()
        {
            try
            {
                var result = BusinessLogic.GetAllSalesOrderNumbers();
                cmbSalesOrderNumbers.DisplayMemberPath = ColumnNames.Type;
                cmbSalesOrderNumbers.SelectedValuePath = ColumnNames.ID;
                cmbSalesOrderNumbers.ItemsSource = result.DefaultView;
                cmbSalesOrderNumbers.SelectedIndex = -1;
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

        private void OpenSelectedSaleOrder(string quoteNumber)
        {
            try
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
                dtQuoteCreatedOn.SelectedDate = DateTime.Parse(result.Header.SaleOrderConfirmedOn);
                dtQuoteRequestedOn.SelectedDate = DateTime.Parse(result.Header.QuoteRequestedOn);
                cmbPaymentType.SelectedValue = result.Header.PaymentModeID;
                txtSONumber.Text = result.Header.SalesOrderNumber;

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

        private void btnExportPdf_Click(object sender, RoutedEventArgs e)
        {
            PrintQuote();
        }

        private void PrintQuote()
        {
            try
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string clientName = string.Format("{0} {1}", txtSoldToFirstName.Text, txtSoldToLastName.Text);
                string relativePath = folderPath + Constants.FolderSeparator + Constants.RootDirectory + Constants.FolderSeparator + clientName + Constants.FolderSeparator + Constants.SaleOrder + Constants.FolderSeparator;
                string filename = string.Format(Constants.SaleOrderFileName, txtSONumber.Text);
                string completeFilePath = relativePath + "\\" + filename;

                if (Directory.Exists(relativePath) == false)
                {
                    Directory.CreateDirectory(relativePath);
                }
                if (File.Exists(completeFilePath) == true)
                {
                    var result1 = Helper.ShowQuestionMessageBox("Sale Order with same number already exists. Do you want to overwrite it?");
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
                int yHeaderOffset = 15;
                int yBaseOffset = 55;
                int yIncrementalOffset = 25;
                int labelWidth = 100;
                int labelHeight = 100;

                XFont headerFont = new XFont("Verdana", 22, XFontStyle.Bold);
                gfx.DrawString("Sales Order", headerFont, XBrushes.Black,
             new XRect(xBaseOffset, yHeaderOffset, labelWidth, labelHeight),
             XStringFormat.TopLeft);

                // Print Quote Number
                gfx.DrawString(lblSONumber.Content.ToString(), font, XBrushes.Black,
                  new XRect(xBaseOffset, yBaseOffset, labelWidth, labelHeight),
                  XStringFormat.TopLeft);
                gfx.DrawString(txtSONumber.Text, font, XBrushes.Black,
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
                gfx.DrawString(lblSOCreatedOn.Content.ToString(), font, XBrushes.Black,
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
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnOpenSO_Click(object sender, RoutedEventArgs e)
        {
            OpenSalesOrder();
        }

        private void OpenSalesOrder()
        {
            try
            {
                if (cmbSalesOrderNumbers.SelectedIndex < 0 && cmbSalesOrderNumbers.SelectedItem == null)
                {
                    Helper.ShowErrorMessageBox("Please select Sales Order Number");
                    return;
                }
                string quoteNumber = (cmbSalesOrderNumbers.SelectedItem as System.Data.DataRowView)[0].ToString();

                if (string.IsNullOrEmpty(quoteNumber))
                {
                    Helper.ShowErrorMessageBox("Invalid Quote Number associated with this Sales order. No data found!");
                    return;
                }
                OpenSelectedSaleOrder(quoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
     
        }

        private void btnOpenWorksheet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dashboard parent = Window.GetWindow(this) as Dashboard;
                if (parent != null)
                {
                    WorksheetContent wsContent = new WorksheetContent(true, txtQuoteNumber.Text);
                    parent.ucMainContent.ShowPage(wsContent);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
           
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteSalesOrder();
        }

        private void DeleteSalesOrder()
        {
            try
            {
                if (Helper.IsNonEmpty(txtQuoteNumber) && Helper.IsNonEmpty(txtSONumber))
                {
                    bool isSalesOrderPresent = BusinessLogic.IsSalesOrderPresent(txtQuoteNumber.Text);
                    if (isSalesOrderPresent)
                    {
                        BusinessLogic.DeleteSalesOrder(txtQuoteNumber.Text);
                        Helper.ShowInformationMessageBox("Sales Order is delete successfully!");

                        ResetControls();
                    }
                }
                else
                {
                    Helper.ShowErrorMessageBox("Please provide required details.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void ResetControls()
        {
            txtSONumber.Text = string.Empty;
            txtCustomerPO.Text = string.Empty;
            dtQuoteCreatedOn.SelectedDate = null;
            dtQuoteRequestedOn.SelectedDate = null;
            cmbPaymentType.SelectedIndex = -1;
            txtQuoteNumber.Text = string.Empty;

            FillAllSalesOrderNumbers();
            cmbSalesOrderNumbers.SelectedIndex= -1;
            
            cbIsShipToSameAddress.IsChecked = false;

            txtSoldToAddress.Text = string.Empty;
            txtSoldToEmail.Text = string.Empty;
            txtSoldToFax.Text = string.Empty;
            txtSoldToFirstName.Text = string.Empty;
            txtSoldToLastName.Text = string.Empty;
            txtSoldToMisc.Text = string.Empty;
            txtSoldToPhone.Text = string.Empty;

            txtShipToAddress.Text = string.Empty;
            txtShipToEmail.Text = string.Empty;
            txtShipToFax.Text = string.Empty;
            txtShiptoFirstName.Text = string.Empty;
            txtShiptoLastName.Text = string.Empty;
            txtShipToMisc.Text = string.Empty;
            txtShipToPhone.Text = string.Empty;

            allQuoteData = new ObservableCollection<QuoteGridEntity>();
            dgQuoteItems.ItemsSource = allQuoteData;

            lblSubTotal.Content = "$ 0.00";
            cbDollar.IsChecked = false;
            cbRush.IsChecked = false;

            txtEnergySurcharge.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtDelivery.Text = string.Empty;
            txtRushOrder.Text = string.Empty;
            txtTax.Text = string.Empty;

            lblGrandTotal.Content = "$ 0.00";

        }

        private void cmbSalesOrderNumbers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpenSalesOrder();
        }
    }
}
