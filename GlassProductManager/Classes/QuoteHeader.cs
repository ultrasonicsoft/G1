using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class QuoteHeader
    {
        public string QuoteNumber { get; set; }
        public string QuoteCreatedOn { get; set; }
        public string QuoteRequestedOn { get; set; }
        public bool IsNewCustomer { get; set; }
        public string CustomerPO { get; set; }
        public int ShippingMethodID { get; set; }
        public CustomerDetails SoldTo { get; set; }
        public bool IsShipToOtherAddress { get; set; }
        public CustomerDetails ShipTo { get; set; }
        public int LeadTimeID { get; set; }
        public int LeadTimeTypeID { get; set; }
        public int CustomerID { get; set; }
        public string OperatorName { get; set; }
        public int PaymentModeID { get; set; }
        public int QuoteStatusID { get; set; }
        public string SalesOrderNumber { get; set; }
        public string SaleOrderConfirmedOn { get; set; }
        public string WorksheetNumber { get; set; }
    }

    public class CustomerDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Misc { get; set; }

    }
}
