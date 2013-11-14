using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class StoredProcedures
    {
        internal const string GetAllGlassTypes = "GetAllGlassTypes";
        internal const string GetAllShapes = "[GetAllShapes]";
        internal const string GetThicknessByGlassID = "GetThicknessByGlassID";
        internal const string GetRates = "GetRates";
        internal const string GetInsulationCost = "[GetInsulationCost]";
        internal const string GetAllShippingMethods = "[GetAllShippingMethods]";
        internal const string GetAllLeadTimeTypes = "[GetAllLeadTimeTypes]";
        internal const string GetAllLeadTime = "[GetAllLeadTime]";
        internal const string IsDefaultLeadTimeSet = "[IsDefaultLeadTimeSet]";
        internal const string GetDefaultLeadTimeSet = "[GetDefaultLeadTimeSet]";
        internal const string SetDefaultLeadTime = "[SetDefaultLeadTime]";
        internal const string ResetDefaultLeadTime = "[ResetDefaultLeadTime]";
        internal const string GetNewQuoteID = "[GetNewQuoteID]";
        internal const string IsQuoteNumberPresent = "[IsQuoteNumberPresent]";
        internal const string AddQuoteHeader = "[AddQuoteHeader]";
        internal const string CreateNewCustomer = "[CreateNewCustomer]";
        internal const string InsertShippingDetails = "[InsertShippingDetails]";
    }
}
