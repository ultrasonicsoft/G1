using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    class Constants
    {
        internal const string FolderSeparator = "\\";
        internal const string RootDirectory = "Glass Control Clients";
        internal const string Quote = "Quotes";
        internal const string QuoteFileName = "Quote {0}.pdf";
        internal const string SaleOrderFileName = "Sale Order {0}.pdf";
        internal const string Worksheet = "Worksheet";
        internal const string WorksheetFileName = "Worksheet {0}.pdf";
        internal const string SaleOrder = "Sale Orders";
        internal const string Invoices = "Invoice";
        internal const string InvoiceFileName = "Invoice {0}.pdf";
        internal const string DBBackup = "DBBackup";
        internal const string DatabaseName = "GlassManagerDB";
        internal const string BackupExtension = ".bak";
        internal const string DatabaseServerName = @"localhost\SQLEXPRESS";
        internal static string BarCodeLabelTemplateFileName = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\" + BarCodeConstants.BarcodeTemplateFileName;
        //If maximum line items are up to 11 then we can print single page with footer
        internal const int MAXIMUM_LINE_ITEM_ON_FIRST_PAGE = 11;
        // If maximum line items are more than 11 then we will print 13 items on first page and rest on next pages
        internal const int LINE_ITEMS_ON_FIRST_PAGE_MULTIPLE_PAGES = 13;
        internal const float PDF_ZOOM_FACTOR = 0.5F;
        internal const int PDF_PAGE_ALL_MARGIN = 72;
        internal const int MAXIMUM_LINE_ITEM_ON_MIDDLE_PAGE = 18;
    }

    class BarCodeConstants
    {
        internal const string BarcodeTemplateFileName = "Glass_Barcode_Template.btw";
        internal const string CustomerName = "CustomerName";
        internal const string SalesOrder = "SalesOrder";
        internal const string OrderDate = "OrderDate";
        internal const string CustomerPO = "CustomerPO";
        internal const string Worksheet = "Worksheet";
        internal const string Size = "Size";
        internal const string TotalQuantity = "TotalQuantity";
        internal const string CurrentQuantity = "CurrentQuantity";
        internal const string Line = "Line";
        internal const string SQFT = "SQFT";
        internal const string Logo = "Logo";
        internal const string Description = "Description";
        internal const string BarcodeItem = "BarcodeItem";
        internal const string DueDate = "DueDate";
        internal const string GlassShape = "GlassShape";
    }
    
   enum WorksheetItemStatus
   {
       NotStarted,
       Cut,
       Polish,
       Drill,
       WaterJet,
       Temper,
       Insulate,
       Completed
   }

}
