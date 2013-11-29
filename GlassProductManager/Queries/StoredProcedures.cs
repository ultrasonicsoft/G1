﻿using System;
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
        internal const string InsertQuoteLineItem = "[InsertQuoteLineItem]";
        internal const string InsertQuoteFooter = "[InsertQuoteFooter]";
        internal const string GetAllOperatorNames = "[GetAllOperatorNames]";
        internal const string GetSmartSearchData = "[GetSmartSearchData]";
        internal const string GetQuoteDetails = "[GetQuoteDetails]";
        internal const string GetAllCustomerNames = "[GetAllCustomerNames]";
        internal const string GetCustomerDetails = "[GetCustomerDetails]";
        internal const string DeleteQuote = "[DeleteQuote]";
        internal const string GetAllCustomerDetails = "[GetAllCustomerDetails]";
        internal const string DeleteCustomer = "[DeleteCustomer]";
        internal const string UpdateGlassRates = "[UpdateGlassRates]";
        internal const string AddNewGlassType = "[AddNewGlassType]";
        internal const string DeleteGlassType = "[DeleteGlassType]";
        internal const string CreateNewThickness = "[CreateNewThickness]";
        internal const string GetAllInsulationCost = "[GetAllInsulationCost]";
        internal const string UpdateInsulationCost = "[UpdateInsulationCost]";
        internal const string GetMiscRates = "[GetMiscRates]";
        internal const string UpdateMiscRate = "[UpdateMiscRate]";
        internal const string GetThicknesses = "[GetThicknesses]";
        internal const string GetHoleRateByThicknessID = "[GetHoleRateByThicknessID]";
        internal const string UpdateHoleRate = "[UpdateHoleRate]";
        internal const string UpdateGlassType = "[UpdateGlassType]";
        internal const string UpdateThickness = "[UpdateThickness]";
        internal const string GetAllPaymentTypes = "[GetAllPaymentTypes]";
        internal const string GetAllQuoteStatus = "[GetAllQuoteStatus]";
        internal const string GetQuoteMasterData = "[GetQuoteMasterData]";
        internal const string GetSaleOrderMasterData = "[GetSaleOrderMasterData]";
        internal const string GetWorksheetMasterData = "[GetWorksheetMasterData]";
        internal const string DeleteQuoteItems = "[DeleteQuoteItems]";
        internal const string UpdateQuoteHeader = "[UpdateQuoteHeader]";
        internal const string UpdateQuoteFooter = "[UpdateQuoteFooter]";
        internal const string GenerateSaleOrder = "[GenerateSaleOrder]";
        internal const string GenerateWorksheet = "[GenerateWorksheet]";
        internal const string GetSaleOrderNumber = "[GetSaleOrderNumber]";
        internal const string IsSalesOrderPresent = "[IsSalesOrderPresent]";
        internal const string IsWorksheetPresent = "[IsWorksheetPresent]";
        internal const string DeleteSalesOrder = "[DeleteSalesOrder]";
        internal const string DeleteWorksheet = "[DeleteWorksheet]";
    }
}
