using DBHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    internal class BusinessLogic
    {
        internal static bool IsValidUser(string userName, string password)
        {
            bool isValid = false;
            try
            {
                var result = SQLHelper.GetScalarValue(string.Format(SelectQueries.USER_LOGIN_QUERY, userName, password));
                if (result == null)
                    return false;
                isValid = result.ToString() == "1";

                if (isValid)
                {
                    result = SQLHelper.GetScalarValue(string.Format(SelectQueries.IS_ADMIN_QUERY, userName, password));
                    if (result == null)
                        FirmSettings.IsAdmin = false;
                    FirmSettings.IsAdmin = bool.Parse(result.ToString());
                    FirmSettings.UserName = userName;
                }

                return isValid;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return isValid;
        }

        internal static DataTable GetAllGlassTypes()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllGlassTypes, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static DataTable GetAllShapes()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllShapes, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static ObservableCollection<GlassRate> GetPriceListByGlassTypeID(string selectedValue)
        {
            ObservableCollection<GlassRate> rateStructure = new ObservableCollection<GlassRate>();

            try
            {
                //TODO: change query to SP. 
                var result = SQLHelper.GetDataTable(string.Format(SelectQueries.GET_GLASS_RATES_BY_ID, selectedValue));
                if (result == null)
                    return null;

                for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
                {
                    rateStructure.Add(new GlassRate()
                    {
                        ID = result.Rows[rowIndex][ColumnNames.ID].ToString(),
                        Thickness = result.Rows[rowIndex][ColumnNames.THICKNESS].ToString(),
                        CutSQFT = result.Rows[rowIndex][ColumnNames.CUTSQFT].ToString(),
                        TemperedSQFT = result.Rows[rowIndex][ColumnNames.TEMPEREDSQFT].ToString(),
                        PolishStraight = result.Rows[rowIndex][ColumnNames.POLISHSTRAIGHT].ToString(),
                        PolishShape = result.Rows[rowIndex][ColumnNames.POLISHSHAPE].ToString(),
                    });

                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return rateStructure;
        }

        internal static DataTable GetThicknessByGlassID(string glassID)
        {
            DataSet result = null;
            try
            {
                SqlParameter paramGlassID = new SqlParameter();
                paramGlassID.ParameterName = "GlassID";
                paramGlassID.Value = glassID;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetThicknessByGlassID, paramGlassID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static DataSet GetRatesByGlassTypeAndThickness(int _glassTypeID, int _thicknessID)
        {
            DataSet result = null;
            try
            {
                SqlParameter paramGlassID = new SqlParameter();
                paramGlassID.ParameterName = "GlassID";
                paramGlassID.Value = _glassTypeID;

                SqlParameter paramThicknessID = new SqlParameter();
                paramThicknessID.ParameterName = "ThicknessID";
                paramThicknessID.Value = _thicknessID;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetRates, paramGlassID, paramThicknessID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        internal static double GetInsulationTierCost(int sqft)
        {
            double insulationCost = 0;
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetInsulationCost);
                if (result != null && result.Tables.Count > 0 && result.Tables[0].Rows.Count == 0)
                {
                    Logger.LogMessage("Error: Failed to read Insulation cost from database. GetInsulationTierCost()");
                    return insulationCost;
                }

                int ceiling1 = int.Parse(result.Tables[0].Rows[0][ColumnNames.Ceiling1].ToString());
                int ceiling2 = int.Parse(result.Tables[0].Rows[0][ColumnNames.Ceiling2].ToString());

                if (sqft > ceiling2)
                {
                    insulationCost = double.Parse(result.Tables[0].Rows[0][ColumnNames.Cost3].ToString());
                }
                else if (sqft >= ceiling1 && sqft <= ceiling2)
                {
                    insulationCost = double.Parse(result.Tables[0].Rows[0][ColumnNames.Cost2].ToString());
                }
                else
                {
                    insulationCost = double.Parse(result.Tables[0].Rows[0][ColumnNames.Cost1].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return insulationCost;
        }

        internal static DataTable GetAllShippingMethods()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllShippingMethods, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static DataTable GetAllLeadTimeTypes()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllLeadTimeTypes, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static DataTable GetAllLeadTime()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllLeadTime, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static DataTable GetLeadTimeSettings()
        {
            DataSet result = null;
            try
            {
                var isDefaultLeadSet = SQLHelper.ExecuteStoredProcedure(StoredProcedures.IsDefaultLeadTimeSet);

                if (isDefaultLeadSet.Tables == null || isDefaultLeadSet.Tables.Count == 0 || isDefaultLeadSet.Tables[0].Rows == null || isDefaultLeadSet.Tables[0].Rows.Count == 0)
                    return null;
                bool setting = bool.Parse(isDefaultLeadSet.Tables[0].Rows[0][0].ToString());
                if (setting)
                    result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetDefaultLeadTimeSet);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static void SetDefaultLeadTime(int leadTimeID, int leadTimeTypeID)
        {
            try
            {
                SqlParameter paramGlassID = new SqlParameter();
                paramGlassID.ParameterName = "leadTimeID";
                paramGlassID.Value = leadTimeID;

                SqlParameter paramThicknessID = new SqlParameter();
                paramThicknessID.ParameterName = "leadTimeTypeID";
                paramThicknessID.Value = leadTimeTypeID;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.SetDefaultLeadTime, paramGlassID, paramThicknessID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void ResetDefaultLeadTime()
        {
            try
            {
                SQLHelper.ExecuteStoredProcedure(StoredProcedures.ResetDefaultLeadTime);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static string GetNewQuoteID()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetNewQuoteID, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? string.Empty : result.Tables[0].Rows[0][0].ToString();
        }

        internal static bool IsQuoteNumberPresent(string quoteNumber)
        {
            DataSet result = null;
            try
            {
                SqlParameter paramQuoteNumber = new SqlParameter();
                paramQuoteNumber.ParameterName = "quoteNumber";
                paramQuoteNumber.Value = quoteNumber;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.IsQuoteNumberPresent, paramQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? false : result.Tables[0].Rows[0][0].ToString() == "1";
        }

        internal static bool IsSalesOrderPresent(string quoteNumber)
        {
            DataSet result = null;
            try
            {
                SqlParameter paramQuoteNumber = new SqlParameter();
                paramQuoteNumber.ParameterName = "quoteNumber";
                paramQuoteNumber.Value = quoteNumber;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.IsSalesOrderPresent, paramQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? false : result.Tables[0].Rows[0][0].ToString() == "1";
        }

        internal static bool IsWorksheetPresent(string quoteNumber)
        {
            DataSet result = null;
            try
            {
                SqlParameter paramQuoteNumber = new SqlParameter();
                paramQuoteNumber.ParameterName = "quoteNumber";
                paramQuoteNumber.Value = quoteNumber;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.IsWorksheetPresent, paramQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? false : result.Tables[0].Rows[0][0].ToString() == "1";
        }

        internal static void SaveQuoteHeader(QuoteHeader header)
        {
            try
            {
                ProcessQuoteHeader(header, StoredProcedures.AddQuoteHeader);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private static void ProcessQuoteHeader(QuoteHeader header, string spName)
        {
            SqlParameter pQuoteCreatedOn = new SqlParameter();
            pQuoteCreatedOn.ParameterName = "CreatedOn";
            pQuoteCreatedOn.Value = header.QuoteCreatedOn;

            SqlParameter pRequestedShipDate = new SqlParameter();
            pRequestedShipDate.ParameterName = "RequestedShipDate";
            pRequestedShipDate.Value = header.QuoteRequestedOn;

            SqlParameter pCustomerPO = new SqlParameter();
            pCustomerPO.ParameterName = "CustomerPO";
            pCustomerPO.Value = header.CustomerPO;

            SqlParameter pLeadTimeID = new SqlParameter();
            pLeadTimeID.ParameterName = "LeadTimeID";
            pLeadTimeID.Value = header.LeadTimeID;

            SqlParameter pLeadTimeTypeID = new SqlParameter();
            pLeadTimeTypeID.ParameterName = "LeadTimeTypeID";
            pLeadTimeTypeID.Value = header.LeadTimeTypeID;

            SqlParameter pShipToOtherAddress = new SqlParameter();
            pShipToOtherAddress.ParameterName = "ShipToOtherAddress";
            pShipToOtherAddress.Value = header.IsShipToOtherAddress;

            SqlParameter pQuoteNumber = new SqlParameter();
            pQuoteNumber.ParameterName = "QuoteNumber";
            pQuoteNumber.Value = header.QuoteNumber;

            SqlParameter pShippingMethodID = new SqlParameter();
            pShippingMethodID.ParameterName = "ShippingMethodID";
            pShippingMethodID.Value = header.ShippingMethodID;

            SqlParameter pCustomerID = new SqlParameter();
            pCustomerID.ParameterName = "CustomerID";
            pCustomerID.Value = header.CustomerID;

            SqlParameter pOperatorName = new SqlParameter();
            pOperatorName.ParameterName = "OperatorName";
            pOperatorName.Value = header.OperatorName;

            SqlParameter pPaymentModeID = new SqlParameter();
            pPaymentModeID.ParameterName = "PaymentModeID";
            pPaymentModeID.Value = header.PaymentModeID;

            SqlParameter pQuoteStatusID = new SqlParameter();
            pQuoteStatusID.ParameterName = "QuoteStatusID";
            pQuoteStatusID.Value = header.QuoteStatusID;

            if (header.IsNewCustomer)
            {
                string customerID = CreateNewCustomer(header.SoldTo);

                if (header.IsShipToOtherAddress == true && string.IsNullOrEmpty(customerID) == false)
                {
                    InsertShippingDetails(customerID, header.QuoteNumber, header.ShipTo);
                }
                pCustomerID.Value = int.Parse(customerID);
            }

            SQLHelper.ExecuteStoredProcedure(spName, pQuoteCreatedOn, pRequestedShipDate, pCustomerPO, pLeadTimeID, pLeadTimeTypeID,
                    pShipToOtherAddress, pQuoteNumber, pCustomerID, pShippingMethodID, pOperatorName, pPaymentModeID, pQuoteStatusID);
        }

        internal static string CreateNewCustomer(CustomerDetails soldTo)
        {
            string customerID = string.Empty;
            try
            {
                SqlParameter pAddress = new SqlParameter();
                pAddress.ParameterName = "Address";
                pAddress.Value = soldTo.Address??string.Empty;

                SqlParameter pFirstName = new SqlParameter();
                pFirstName.ParameterName = "FirstName";
                pFirstName.Value = soldTo.FirstName;

                SqlParameter pLastName = new SqlParameter();
                pLastName.ParameterName = "LastName";
                pLastName.Value = soldTo.LastName;

                SqlParameter pPhone = new SqlParameter();
                pPhone.ParameterName = "Phone";
                pPhone.Value = soldTo.Phone ?? string.Empty;

                SqlParameter pFax = new SqlParameter();
                pFax.ParameterName = "Fax";
                pFax.Value = soldTo.Fax ?? string.Empty;

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "Email";
                pEmail.Value = soldTo.Email ?? string.Empty;

                SqlParameter pMisc = new SqlParameter();
                pMisc.ParameterName = "Misc";
                pMisc.Value = soldTo.Misc ?? string.Empty;

                var result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.CreateNewCustomer, pAddress, pFirstName, pLastName, pPhone, pFax, pEmail, pMisc);
                if (result == null || result.Tables == null || result.Tables.Count == 0)
                {
                    return customerID;
                }
                customerID = result.Tables[0].Rows[0][0].ToString();

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return customerID;
        }

        private static void InsertShippingDetails(string customerID, string quoteNumber, CustomerDetails shipTo)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pCustomerID = new SqlParameter();
                pCustomerID.ParameterName = "CustomerID";
                pCustomerID.Value = customerID;

                SqlParameter pAddress = new SqlParameter();
                pAddress.ParameterName = "Address";
                pAddress.Value = shipTo.Address;

                SqlParameter pFirstName = new SqlParameter();
                pFirstName.ParameterName = "FirstName";
                pFirstName.Value = shipTo.FirstName;

                SqlParameter pLastName = new SqlParameter();
                pLastName.ParameterName = "LastName";
                pLastName.Value = shipTo.LastName;

                SqlParameter pPhone = new SqlParameter();
                pPhone.ParameterName = "Phone";
                pPhone.Value = shipTo.Phone;

                SqlParameter pFax = new SqlParameter();
                pFax.ParameterName = "Fax";
                pFax.Value = shipTo.Fax;

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "Email";
                pEmail.Value = shipTo.Email;

                SqlParameter pMisc = new SqlParameter();
                pMisc.ParameterName = "Misc";
                pMisc.Value = shipTo.Misc;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.InsertQuoteLineItem, pQuoteNumber, pCustomerID, pAddress, pFirstName, pLastName, pPhone, pFax, pEmail, pMisc);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void SaveQuoteItems(string quoteNumber, ObservableCollection<QuoteGridEntity> allQuoteData)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pLineID = null;
                SqlParameter pQuantity = null;
                SqlParameter pDescription = null;
                SqlParameter pDimension = null;
                SqlParameter pSqFt = null;
                SqlParameter pPricePerUnit = null;
                SqlParameter pTotal = null;

                foreach (QuoteGridEntity item in allQuoteData)
                {
                    pQuantity = new SqlParameter();
                    pQuantity.ParameterName = "Quantity";
                    pQuantity.Value = item.Quantity;

                    pLineID = new SqlParameter();
                    pLineID.ParameterName = "LineID";
                    pLineID.Value = item.LineID;


                    pDescription = new SqlParameter();
                    pDescription.ParameterName = "Description";
                    pDescription.Value = item.Description;

                    pDimension = new SqlParameter();
                    pDimension.ParameterName = "Dimension";
                    pDimension.Value = item.Dimension;

                    pSqFt = new SqlParameter();
                    pSqFt.ParameterName = "SqFt";
                    pSqFt.Value = int.Parse(item.TotalSqFt);

                    pPricePerUnit = new SqlParameter();
                    pPricePerUnit.ParameterName = "PricePerUnit";
                    pPricePerUnit.Value = double.Parse(item.UnitPrice);

                    pTotal = new SqlParameter();
                    pTotal.ParameterName = "Total";
                    pTotal.Value = item.Total;

                    SQLHelper.ExecuteStoredProcedure(StoredProcedures.InsertQuoteLineItem, pLineID, pQuoteNumber, pQuantity, pDescription, pDimension, pSqFt, pPricePerUnit, pTotal);

                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void SaveQuoteFooter(string quoteNumber, QuoteFooter footer)
        {
            try
            {
                ProcessQuoteFooter(quoteNumber, footer, StoredProcedures.InsertQuoteFooter);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private static void ProcessQuoteFooter(string quoteNumber, QuoteFooter footer, string spName)
        {
            SqlParameter pQuoteNumber = new SqlParameter();
            pQuoteNumber.ParameterName = "QuoteNumber";
            pQuoteNumber.Value = quoteNumber;

            SqlParameter pSubTotal = new SqlParameter();
            pSubTotal.ParameterName = "SubTotal";
            pSubTotal.Value = footer.SubTotal;

            SqlParameter pIsDollar = new SqlParameter();
            pIsDollar.ParameterName = "IsDollar";
            pIsDollar.Value = footer.IsDollar;

            SqlParameter pEnergySurcharge = new SqlParameter();
            pEnergySurcharge.ParameterName = "EnergySurcharge";
            pEnergySurcharge.Value = footer.EnergySurcharge;

            SqlParameter pDiscount = new SqlParameter();
            pDiscount.ParameterName = "Discount";
            pDiscount.Value = footer.Discount;

            SqlParameter pDelivery = new SqlParameter();
            pDelivery.ParameterName = "Delivery";
            pDelivery.Value = footer.Delivery;

            SqlParameter pIsRushOrder = new SqlParameter();
            pIsRushOrder.ParameterName = "IsRush";
            pIsRushOrder.Value = footer.IsRushOrder;

            SqlParameter pRushOrder = new SqlParameter();
            pRushOrder.ParameterName = "RushOrder";
            pRushOrder.Value = footer.RushOrder;

            SqlParameter pTax = new SqlParameter();
            pTax.ParameterName = "Tax";
            pTax.Value = footer.Tax;

            SqlParameter pGrandTotal = new SqlParameter();
            pGrandTotal.ParameterName = "GrandTotal";
            pGrandTotal.Value = footer.GrandTotal;

            SQLHelper.ExecuteStoredProcedure(spName, pQuoteNumber, pSubTotal, pIsDollar, pEnergySurcharge, pDiscount, pDelivery, pIsRushOrder, pRushOrder, pTax, pGrandTotal);
        }

        internal static DataTable GetAllOperatorNames()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllOperatorNames, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static List<string> GetSmartSearchData()
        {
            List<string> data = new List<string>();

            try
            {
                DataSet result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetSmartSearchData, null);
                if (result == null || result.Tables == null || result.Tables.Count == 0)
                    return data;
                foreach (DataRow item in result.Tables[0].Rows)
                {
                    data.Add(item[0].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return data;
        }

        internal static QuoteEntity GetQuoteDetails(string quoteNumber)
        {
            QuoteEntity quoteDetails = null;

            DataSet result = null;
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetQuoteDetails, pQuoteNumber);
                if (result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                {
                    return quoteDetails;
                }
                quoteDetails = new QuoteEntity();
                quoteDetails.Header = new QuoteHeader();
                quoteDetails.Header.CustomerPO = result.Tables[0].Rows[0][ColumnNames.CustomerPO].ToString();
                quoteDetails.Header.IsShipToOtherAddress = bool.Parse(result.Tables[0].Rows[0][ColumnNames.ShipToOtherAddress].ToString());
                quoteDetails.Header.LeadTimeID = int.Parse(result.Tables[0].Rows[0][ColumnNames.LeadTimeID].ToString());
                quoteDetails.Header.LeadTimeTypeID = int.Parse(result.Tables[0].Rows[0][ColumnNames.LeadTimeTypeID].ToString());
                quoteDetails.Header.QuoteCreatedOn = result.Tables[0].Rows[0][ColumnNames.CreatedOn].ToString();
                quoteDetails.Header.QuoteNumber = result.Tables[0].Rows[0][ColumnNames.QuoteNumber].ToString();
                quoteDetails.Header.QuoteRequestedOn = result.Tables[0].Rows[0][ColumnNames.RequestedShipDate].ToString();
                quoteDetails.Header.ShippingMethodID = int.Parse(result.Tables[0].Rows[0][ColumnNames.ShippingMethodID].ToString());
                quoteDetails.Header.OperatorName = result.Tables[0].Rows[0][ColumnNames.OperatorName].ToString();
                quoteDetails.Header.PaymentModeID = int.Parse(result.Tables[0].Rows[0][ColumnNames.PaymentModeID].ToString());
                quoteDetails.Header.QuoteStatusID = int.Parse(result.Tables[0].Rows[0][ColumnNames.QuoteStatusID].ToString());
                quoteDetails.Header.SalesOrderNumber = result.Tables[0].Rows[0][ColumnNames.SONumber].ToString();
                quoteDetails.Header.SaleOrderConfirmedOn = result.Tables[0].Rows[0][ColumnNames.ConfirmedDate].ToString();
                quoteDetails.Header.WorksheetNumber = result.Tables[0].Rows[0][ColumnNames.WSNumber].ToString();
                quoteDetails.Header.InvoiceCompletedOn = result.Tables[0].Rows[0][ColumnNames.CompletedDate].ToString();

                quoteDetails.Header.BalanceDue = double.Parse(double.Parse(result.Tables[0].Rows[0][ColumnNames.BalanceDue].ToString()).ToString("0.00"));

                quoteDetails.Header.SoldTo = new CustomerDetails();
                quoteDetails.Header.SoldTo.FirstName = result.Tables[0].Rows[0][ColumnNames.SoldTo_FirstName].ToString();
                quoteDetails.Header.SoldTo.LastName = result.Tables[0].Rows[0][ColumnNames.SoldTo_LastName].ToString();
                quoteDetails.Header.SoldTo.Address = result.Tables[0].Rows[0][ColumnNames.SoldTo_Address].ToString();
                quoteDetails.Header.SoldTo.Email = result.Tables[0].Rows[0][ColumnNames.SoldTo_Email].ToString();
                quoteDetails.Header.SoldTo.Fax = result.Tables[0].Rows[0][ColumnNames.SoldTo_Fax].ToString();
                quoteDetails.Header.SoldTo.Phone = result.Tables[0].Rows[0][ColumnNames.SoldTo_Phone].ToString();
                quoteDetails.Header.SoldTo.Misc = result.Tables[0].Rows[0][ColumnNames.SoldTo_Misc].ToString();

                if (quoteDetails.Header.IsShipToOtherAddress)
                {
                    quoteDetails.Header.ShipTo = new CustomerDetails();
                    quoteDetails.Header.ShipTo.FirstName = result.Tables[0].Rows[0][ColumnNames.ShipTo_FirstName].ToString();
                    quoteDetails.Header.ShipTo.LastName = result.Tables[0].Rows[0][ColumnNames.ShipTo_LastName].ToString();
                    quoteDetails.Header.ShipTo.Address = result.Tables[0].Rows[0][ColumnNames.ShipTo_Address].ToString();
                    quoteDetails.Header.ShipTo.Email = result.Tables[0].Rows[0][ColumnNames.ShipTo_Email].ToString();
                    quoteDetails.Header.ShipTo.Fax = result.Tables[0].Rows[0][ColumnNames.ShipTo_Fax].ToString();
                    quoteDetails.Header.ShipTo.Phone = result.Tables[0].Rows[0][ColumnNames.ShipTo_Phone].ToString();
                    quoteDetails.Header.ShipTo.Misc = result.Tables[0].Rows[0][ColumnNames.ShipTo_Misc].ToString();
                }

                if (result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[1].Rows.Count == 0)
                {
                    return quoteDetails;
                }

                quoteDetails.LineItems = new ObservableCollection<QuoteGridEntity>();
                QuoteGridEntity lineItem = null;
                foreach (DataRow currentRow in result.Tables[1].Rows)
                {
                    lineItem = new QuoteGridEntity();
                    lineItem.LineID = int.Parse(currentRow[ColumnNames.LineID].ToString());
                    lineItem.Quantity = int.Parse(currentRow[ColumnNames.Quantity].ToString());
                    lineItem.Description = currentRow[ColumnNames.Description].ToString();
                    lineItem.Dimension = currentRow[ColumnNames.Dimension].ToString();
                    lineItem.TotalSqFt = currentRow[ColumnNames.SqFt].ToString();
                    lineItem.UnitPrice = double.Parse(currentRow[ColumnNames.PricePerUnit].ToString()).ToString("0.00");
                    lineItem.Total = double.Parse(currentRow[ColumnNames.Total].ToString()).ToString("0.00");

                    quoteDetails.LineItems.Add(lineItem);
                }

                if (result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[2].Rows.Count == 0)
                {
                    return quoteDetails;
                }
                quoteDetails.Footer = new QuoteFooter();
                quoteDetails.Footer.SubTotal = double.Parse(result.Tables[2].Rows[0][ColumnNames.SubTotal].ToString());
                quoteDetails.Footer.IsDollar = bool.Parse(result.Tables[2].Rows[0][ColumnNames.IsDollar].ToString());
                quoteDetails.Footer.EnergySurcharge = double.Parse(result.Tables[2].Rows[0][ColumnNames.EnergySurcharge].ToString());
                quoteDetails.Footer.Discount = double.Parse(result.Tables[2].Rows[0][ColumnNames.Discount].ToString());
                quoteDetails.Footer.Delivery = double.Parse(result.Tables[2].Rows[0][ColumnNames.Delivery].ToString());
                quoteDetails.Footer.IsRushOrder = bool.Parse(result.Tables[2].Rows[0][ColumnNames.IsRush].ToString());
                quoteDetails.Footer.RushOrder = double.Parse(result.Tables[2].Rows[0][ColumnNames.RushOrder].ToString());
                quoteDetails.Footer.Tax = double.Parse(result.Tables[2].Rows[0][ColumnNames.Tax].ToString());
                quoteDetails.Footer.GrandTotal = double.Parse(result.Tables[2].Rows[0][ColumnNames.GrandTotal].ToString());
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return quoteDetails;
        }

        internal static ObservableCollection<InvoicePaymentEntity> GetInvoicePaymentDetails(string quoteNumber)
        {
            ObservableCollection<InvoicePaymentEntity> paymentDetails = new ObservableCollection<InvoicePaymentEntity>();
            DataSet result = null;
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetInvoicePaymentDetails, pQuoteNumber);
                if (result == null || result.Tables == null || result.Tables.Count == 0)
                    return paymentDetails;

                InvoicePaymentEntity payment = null;
                for (int rowIndex = 0; rowIndex < result.Tables[0].Rows.Count; rowIndex++)
                {
                    payment = new InvoicePaymentEntity();
                    payment.ID = int.Parse(result.Tables[0].Rows[rowIndex][ColumnNames.ID].ToString());
                    payment.PaymentDate = result.Tables[0].Rows[rowIndex][ColumnNames.PaymentDate].ToString();
                    payment.Amount = double.Parse(result.Tables[0].Rows[rowIndex][ColumnNames.Amount].ToString()).ToString("0.00");
                    payment.Description = result.Tables[0].Rows[rowIndex][ColumnNames.Description].ToString();

                    paymentDetails.Add(payment);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return paymentDetails;
        }
        internal static DataTable GetAllCustomerNames()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllCustomerNames, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static void GetCustomerDetails(out CustomerDetails soldTo, out CustomerDetails shipTo, string customerID)
        {
            DataSet result = null;
            soldTo = null;
            shipTo = null;
            try
            {
                SqlParameter pCustomerID = new SqlParameter();
                pCustomerID.ParameterName = "CustomerID";
                pCustomerID.Value = customerID;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetCustomerDetails, pCustomerID);

                if (result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                {
                    return;
                }
                soldTo = new CustomerDetails();
                soldTo.FirstName = result.Tables[0].Rows[0][ColumnNames.SoldTo_FirstName].ToString();
                soldTo.LastName = result.Tables[0].Rows[0][ColumnNames.SoldTo_LastName].ToString();
                soldTo.Address = result.Tables[0].Rows[0][ColumnNames.SoldTo_Address].ToString();
                soldTo.Email = result.Tables[0].Rows[0][ColumnNames.SoldTo_Email].ToString();
                soldTo.Fax = result.Tables[0].Rows[0][ColumnNames.SoldTo_Fax].ToString();
                soldTo.Phone = result.Tables[0].Rows[0][ColumnNames.SoldTo_Phone].ToString();
                soldTo.Misc = result.Tables[0].Rows[0][ColumnNames.SoldTo_Misc].ToString();

                shipTo = new CustomerDetails();
                shipTo.FirstName = result.Tables[0].Rows[0][ColumnNames.ShipTo_FirstName].ToString();
                shipTo.LastName = result.Tables[0].Rows[0][ColumnNames.ShipTo_LastName].ToString();
                shipTo.Address = result.Tables[0].Rows[0][ColumnNames.ShipTo_Address].ToString();
                shipTo.Email = result.Tables[0].Rows[0][ColumnNames.ShipTo_Email].ToString();
                shipTo.Fax = result.Tables[0].Rows[0][ColumnNames.ShipTo_Fax].ToString();
                shipTo.Phone = result.Tables[0].Rows[0][ColumnNames.ShipTo_Phone].ToString();
                shipTo.Misc = result.Tables[0].Rows[0][ColumnNames.ShipTo_Misc].ToString();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void DeleteQuote(string quoteNumber)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.DeleteQuote, pQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static ObservableCollection<CustomerSmartDataEntity> GetAllCustomerDetails()
        {
            ObservableCollection<CustomerSmartDataEntity> customerList = new ObservableCollection<CustomerSmartDataEntity>();
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllCustomerDetails, null);
                if (result == null || result.Tables == null || result.Tables.Count == 0)
                    return customerList;

                CustomerSmartDataEntity newCustomer = null;
                for (int rowIndex = 0; rowIndex < result.Tables[0].Rows.Count; rowIndex++)
                {
                    newCustomer = new CustomerSmartDataEntity();
                    newCustomer.ID = result.Tables[0].Rows[rowIndex][ColumnNames.ID].ToString();
                    newCustomer.FirstName = result.Tables[0].Rows[rowIndex][ColumnNames.FirstName].ToString();
                    newCustomer.LastName = result.Tables[0].Rows[rowIndex][ColumnNames.LastName].ToString();
                    newCustomer.Address = result.Tables[0].Rows[rowIndex][ColumnNames.Address].ToString();
                    newCustomer.Phone = result.Tables[0].Rows[rowIndex][ColumnNames.Phone].ToString();
                    newCustomer.Fax = result.Tables[0].Rows[rowIndex][ColumnNames.Fax].ToString();
                    newCustomer.Email = result.Tables[0].Rows[rowIndex][ColumnNames.Email].ToString();
                    newCustomer.Misc = result.Tables[0].Rows[rowIndex][ColumnNames.Misc].ToString();
                    newCustomer.SONumber = result.Tables[0].Rows[rowIndex][ColumnNames.SONumber].ToString();
                    newCustomer.WorksheetNumber = result.Tables[0].Rows[rowIndex][ColumnNames.WorksheetNumber].ToString();
                    newCustomer.PONumber = result.Tables[0].Rows[rowIndex][ColumnNames.PONumber].ToString();
                    newCustomer.InvoiceNumber = result.Tables[0].Rows[rowIndex][ColumnNames.InvoiceNumber].ToString();

                    newCustomer.QuoteNumber = result.Tables[0].Rows[rowIndex][ColumnNames.QuoteNumber].ToString();

                    customerList.Add(newCustomer);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return customerList;
        }

        internal static void DeleteCustomer(string customerID)
        {
            try
            {
                SqlParameter pCustomerID = new SqlParameter();
                pCustomerID.ParameterName = "CustomerID";
                pCustomerID.Value = customerID;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.DeleteCustomer, pCustomerID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static bool UpdateGlassRate(GlassRateEntity updatedRate)
        {
            bool result = true;
            try
            {
                SqlParameter pGlassID = new SqlParameter();
                pGlassID.ParameterName = "GlassID";
                pGlassID.Value = updatedRate.GlassID;

                SqlParameter pThicknessID = new SqlParameter();
                pThicknessID.ParameterName = "ThicknessID";
                pThicknessID.Value = updatedRate.ThicknessID;

                SqlParameter pCutoutSqFtRate = new SqlParameter();
                pCutoutSqFtRate.ParameterName = "cutSqft";
                pCutoutSqFtRate.Value = updatedRate.CutoutSqFtRate;

                SqlParameter pTemperedRate = new SqlParameter();
                pTemperedRate.ParameterName = "temperedSqft";
                pTemperedRate.Value = updatedRate.TemperedRate;

                SqlParameter pPolishStraightRate = new SqlParameter();
                pPolishStraightRate.ParameterName = "polishStraight";
                pPolishStraightRate.Value = updatedRate.PolishStraightRate;

                SqlParameter pPolishShapeRate = new SqlParameter();
                pPolishShapeRate.ParameterName = "polishShape";
                pPolishShapeRate.Value = updatedRate.PolishShapeRate;

                SqlParameter pMiterRate = new SqlParameter();
                pMiterRate.ParameterName = "miterRate";
                pMiterRate.Value = updatedRate.MiterRate;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateGlassRates, pGlassID, pThicknessID, pCutoutSqFtRate, pTemperedRate, pPolishShapeRate, pPolishStraightRate, pMiterRate);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static bool CreateNewGlassType(string glassType)
        {
            bool result = true;
            try
            {
                SqlParameter pGlassType = new SqlParameter();
                pGlassType.ParameterName = "glassType";
                pGlassType.Value = glassType;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.AddNewGlassType, pGlassType);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static bool UpdateGlassType(string updatedGlassType, int glassTypeID)
        {
            bool result = true;
            try
            {
                SqlParameter pUpdatedGlassType = new SqlParameter();
                pUpdatedGlassType.ParameterName = "updatedGlassType";
                pUpdatedGlassType.Value = updatedGlassType;

                SqlParameter pGlasstypeID = new SqlParameter();
                pGlasstypeID.ParameterName = "glasstypeID";
                pGlasstypeID.Value = glassTypeID;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateGlassType, pUpdatedGlassType, pGlasstypeID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static bool DeleteGlassType(int glassID)
        {
            bool result = true;
            try
            {
                SqlParameter pGlassID = new SqlParameter();
                pGlassID.ParameterName = "glassID";
                pGlassID.Value = glassID;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.DeleteGlassType, pGlassID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static bool CreateNewThickness(int glassID, string thickness)
        {
            bool result = true;
            try
            {
                SqlParameter pGlassID = new SqlParameter();
                pGlassID.ParameterName = "glassID";
                pGlassID.Value = glassID;

                SqlParameter pThickness = new SqlParameter();
                pThickness.ParameterName = "thickness";
                pThickness.Value = thickness;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.CreateNewThickness, pGlassID, pThickness);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static bool UpdateThickness(int glassID, string thickness)
        {
            bool result = true;
            try
            {
                SqlParameter pGlassID = new SqlParameter();
                pGlassID.ParameterName = "glassID";
                pGlassID.Value = glassID;

                SqlParameter pThickness = new SqlParameter();
                pThickness.ParameterName = "thickness";
                pThickness.Value = thickness;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateThickness, pGlassID, pThickness);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static InsulationCostEntity GetAllInsulationCost()
        {
            InsulationCostEntity rates = null;
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllInsulationCost, null);

                if (result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                {
                    return rates;
                }
                rates = new InsulationCostEntity();
                rates.TierCost1 = double.Parse(result.Tables[0].Rows[0][ColumnNames.Cost1].ToString());
                rates.TierSqFt1 = int.Parse(result.Tables[0].Rows[0][ColumnNames.Ceiling1].ToString());
                rates.TierCost2 = double.Parse(result.Tables[0].Rows[0][ColumnNames.Cost2].ToString());
                rates.TierSqFt2 = int.Parse(result.Tables[0].Rows[0][ColumnNames.Ceiling2].ToString());
                rates.TierCost3 = double.Parse(result.Tables[0].Rows[0][ColumnNames.Cost3].ToString());

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return rates;
        }

        internal static bool UpdateInsulationCost(InsulationCostEntity insualtionRate)
        {
            bool result = true;
            try
            {
                SqlParameter pCeiling1 = new SqlParameter();
                pCeiling1.ParameterName = "Ceiling1";
                pCeiling1.Value = insualtionRate.TierSqFt1;

                SqlParameter pCost1 = new SqlParameter();
                pCost1.ParameterName = "Cost1";
                pCost1.Value = insualtionRate.TierCost1;

                SqlParameter pCeiling2 = new SqlParameter();
                pCeiling2.ParameterName = "Ceiling2";
                pCeiling2.Value = insualtionRate.TierSqFt2;

                SqlParameter pCost2 = new SqlParameter();
                pCost2.ParameterName = "Cost2";
                pCost2.Value = insualtionRate.TierCost2;

                SqlParameter pCost3 = new SqlParameter();
                pCost3.ParameterName = "Cost3";
                pCost3.Value = insualtionRate.TierCost3;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateInsulationCost, pCeiling1, pCost1, pCeiling2, pCost2, pCost3);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static MiscRateEntity GetMiscRates()
        {
            MiscRateEntity rates = null;
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetMiscRates, null);

                if (result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                {
                    return rates;
                }
                rates = new MiscRateEntity();
                rates.NotchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.NOTCH_RATE].ToString());
                rates.HingeRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.HINGE_RATE].ToString());
                rates.PatchRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.PATCH_RATE].ToString());

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return rates;
        }

        internal static bool UpdateMiscRate(MiscRateEntity miscRate)
        {
            bool result = true;
            try
            {
                SqlParameter pNotchRate = new SqlParameter();
                pNotchRate.ParameterName = "NotchRate";
                pNotchRate.Value = miscRate.NotchRate;

                SqlParameter pHingeRate = new SqlParameter();
                pHingeRate.ParameterName = "HingeRate";
                pHingeRate.Value = miscRate.HingeRate;

                SqlParameter pPatchRate = new SqlParameter();
                pPatchRate.ParameterName = "PatchRate";
                pPatchRate.Value = miscRate.PatchRate;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateMiscRate, pNotchRate, pHingeRate, pPatchRate);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static DataTable GetThicknesses()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetThicknesses, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static string GetHoleRateByThicknessID(int thicknessID)
        {
            DataSet result = null;
            try
            {
                SqlParameter pThicknessID = new SqlParameter();
                pThicknessID.ParameterName = "ThicknessID";
                pThicknessID.Value = thicknessID;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetHoleRateByThicknessID, pThicknessID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0 ? string.Empty : result.Tables[0].Rows[0][0].ToString();
        }

        internal static bool UpdateHoleRate(int thicknessID, double holeRate)
        {
            bool result = true;
            try
            {
                SqlParameter pThicknessID = new SqlParameter();
                pThicknessID.ParameterName = "thicknessID";
                pThicknessID.Value = thicknessID;

                SqlParameter pHoleRate = new SqlParameter();
                pHoleRate.ParameterName = "holeRate";
                pHoleRate.Value = holeRate;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateHoleRate, pThicknessID, pHoleRate);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                result = false;
            }
            return result;
        }

        internal static DataTable GetAllPaymentTypes()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllPaymentTypes, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static DataTable GetAllQuoteStatus()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllQuoteStatus, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static ObservableCollection<QuoteMasterEntity> GetQuoteMasterData()
        {
            ObservableCollection<QuoteMasterEntity> quoteMasterData = null;
            try
            {
                var result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetQuoteMasterData, null);

                if (result == null || result.Tables == null || result.Tables.Count == 0)
                { return quoteMasterData; }

                quoteMasterData = new ObservableCollection<QuoteMasterEntity>();
                QuoteMasterEntity temp = null;
                for (int rowIndex = 0; rowIndex < result.Tables[0].Rows.Count; rowIndex++)
                {
                    temp = new QuoteMasterEntity();
                    temp.QuoteStatus = result.Tables[0].Rows[rowIndex][ColumnNames.Status] == DBNull.Value ? string.Empty : result.Tables[0].Rows[rowIndex][ColumnNames.Status].ToString();
                    temp.QuoteNumber = result.Tables[0].Rows[rowIndex][ColumnNames.QuoteNumber] == DBNull.Value ? string.Empty : result.Tables[0].Rows[rowIndex][ColumnNames.QuoteNumber].ToString();
                    temp.FullName = result.Tables[0].Rows[rowIndex][ColumnNames.FullName] == DBNull.Value ? string.Empty : result.Tables[0].Rows[rowIndex][ColumnNames.FullName].ToString();
                    temp.CreatedOn = result.Tables[0].Rows[rowIndex][ColumnNames.CreatedOn] == DBNull.Value ? string.Empty : result.Tables[0].Rows[rowIndex][ColumnNames.CreatedOn].ToString();
                    temp.Total = result.Tables[0].Rows[rowIndex][ColumnNames.Total] == DBNull.Value ? string.Empty : double.Parse(result.Tables[0].Rows[rowIndex][ColumnNames.Total].ToString()).ToString("0.00");
                    temp.EstimatedShipDate = result.Tables[0].Rows[rowIndex][ColumnNames.EstimatedShipDate] == DBNull.Value ? string.Empty : result.Tables[0].Rows[rowIndex][ColumnNames.EstimatedShipDate].ToString();
                    temp.PaymentType = result.Tables[0].Rows[rowIndex][ColumnNames.PaymentType] == DBNull.Value ? string.Empty : result.Tables[0].Rows[rowIndex][ColumnNames.PaymentType].ToString();
                    temp.CustomerPONumber = result.Tables[0].Rows[rowIndex][ColumnNames.CustomerPONumber] == DBNull.Value ? string.Empty : result.Tables[0].Rows[rowIndex][ColumnNames.CustomerPONumber].ToString();

                    quoteMasterData.Add(temp);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return quoteMasterData;
        }

        internal static ObservableCollection<SaleOrderEntity> GetSaleOrderMasterData()
        {
            ObservableCollection<SaleOrderEntity> quoteMasterData = null;
            try
            {
                var result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetSaleOrderMasterData, null);

                if (result == null || result.Tables == null || result.Tables.Count == 0)
                { return quoteMasterData; }

                quoteMasterData = new ObservableCollection<SaleOrderEntity>();
                SaleOrderEntity temp = null;
                object dbValue = null;
                for (int rowIndex = 0; rowIndex < result.Tables[0].Rows.Count; rowIndex++)
                {
                    temp = new SaleOrderEntity();
                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.SONumber];
                    temp.SaleOrderNumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.QuoteNumber];
                    temp.QuoteNumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.FullName];
                    temp.FullName = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.ConfirmedDate];
                    temp.RecordedDate = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.GrandTotal];
                    temp.Total = dbValue == DBNull.Value ? string.Empty : double.Parse(dbValue.ToString()).ToString("0.00");

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.PaymentType];
                    temp.PaymentType = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.WSNumber];
                    temp.WorksheetNumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.CustomerPO];
                    temp.CustomerPONumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    quoteMasterData.Add(temp);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return quoteMasterData;
        }

        internal static ObservableCollection<WorksheetEntity> GetWorksheetMasterData()
        {
            ObservableCollection<WorksheetEntity> quoteMasterData = null;
            try
            {
                var result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetWorksheetMasterData, null);

                if (result == null || result.Tables == null || result.Tables.Count == 0)
                { return quoteMasterData; }

                quoteMasterData = new ObservableCollection<WorksheetEntity>();
                WorksheetEntity temp = null;
                object dbValue = null;
                for (int rowIndex = 0; rowIndex < result.Tables[0].Rows.Count; rowIndex++)
                {
                    temp = new WorksheetEntity();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.WSNumber];
                    temp.WorksheetNumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.ConfirmedDate];
                    temp.CreatedOn = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.QuoteNumber];
                    temp.QuoteNumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.RequestedShipDate];
                    temp.DeliveryDate = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.FullName];
                    temp.FullName = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.Progress];
                    temp.Progress = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.TotalQuantity];
                    temp.TotalQuantity = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    quoteMasterData.Add(temp);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return quoteMasterData;
        }

        internal static ObservableCollection<InvoiceEntity> GetInvoiceMasterData()
        {
            ObservableCollection<InvoiceEntity> invoiceMasterData = null;
            try
            {
                var result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetInvoiceMasterData, null);

                if (result == null || result.Tables == null || result.Tables.Count == 0)
                { return invoiceMasterData; }

                invoiceMasterData = new ObservableCollection<InvoiceEntity>();
                InvoiceEntity invoice = null;
                object dbValue = null;

                for (int rowIndex = 0; rowIndex < result.Tables[0].Rows.Count; rowIndex++)
                {
                    invoice = new InvoiceEntity();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.InvoiceNumber];
                    invoice.InvoiceNumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.QuoteNumber];
                    invoice.QuoteNumber = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.FullName];
                    invoice.FullName = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.Total];
                    invoice.Total = dbValue == DBNull.Value ? string.Empty : double.Parse(dbValue.ToString()).ToString("0.00");

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.CompletedDate];
                    invoice.CompletedDate = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.BalanceDue];
                    invoice.BalanceDue = dbValue == DBNull.Value ? string.Empty : double.Parse(dbValue.ToString()).ToString("0.00");

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.PaymentType];
                    invoice.PaymentMode = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.CustomerPO];
                    invoice.CustomerPO = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    dbValue = result.Tables[0].Rows[rowIndex][ColumnNames.InvoiceStatus];
                    invoice.InvoiceStatus = dbValue == DBNull.Value ? string.Empty : dbValue.ToString();

                    invoiceMasterData.Add(invoice);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return invoiceMasterData;
        }

        internal static void UpdateQuoteHeader(QuoteHeader header)
        {
            ProcessQuoteHeader(header, StoredProcedures.UpdateQuoteHeader);
        }

        internal static void UpdateQuoteFooter(string quoteNumber, QuoteFooter footer)
        {
            ProcessQuoteFooter(quoteNumber, footer, StoredProcedures.UpdateQuoteFooter);
        }

        internal static void DeleteQuoteItems(string quoteNumber)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.DeleteQuoteItems, pQuoteNumber);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void GenerateSaleOrder(string quoteNumber, DateTime confirmedDate)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pConfirmedDate = new SqlParameter();
                pConfirmedDate.ParameterName = "ConfirmedDate";
                pConfirmedDate.Value = confirmedDate;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.GenerateSaleOrder, pQuoteNumber, pConfirmedDate);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void GenerateWorksheet(string quoteNumber, DateTime confirmedDate)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pConfirmedDate = new SqlParameter();
                pConfirmedDate.ParameterName = "ConfirmedDate";
                pConfirmedDate.Value = confirmedDate;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.GenerateWorksheet, pQuoteNumber, pConfirmedDate);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void GenerateInvoice(string quoteNumber, DateTime completedDate)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pCompletedDate = new SqlParameter();
                pCompletedDate.ParameterName = "CompletedDate";
                pCompletedDate.Value = completedDate;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.GenerateInvoice, pQuoteNumber, pCompletedDate);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static string GetSaleOrderNumber(string quoteNumber)
        {
            string saleOrderNumber = string.Empty;
            try
            {
                var result = SQLHelper.GetScalarValue(string.Format(SelectQueries.GetSaleOrderNumber, quoteNumber));
                if (result == null)
                    return saleOrderNumber;
                saleOrderNumber = result.ToString();

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return saleOrderNumber;
        }

        internal static void DeleteSalesOrder(string quoteNumber)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.DeleteSalesOrder, pQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void DeleteWorksheet(string quoteNumber)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.DeleteWorksheet, pQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void MakeNewPayment(InvoicePaymentEntity payment, string quoteNumber)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pPaymentDate = new SqlParameter();
                pPaymentDate.ParameterName = "PaymentDate";
                pPaymentDate.Value = payment.PaymentDate;

                SqlParameter pAmount = new SqlParameter();
                pAmount.ParameterName = "Amount";
                pAmount.Value = payment.Amount;

                SqlParameter pDescription = new SqlParameter();
                pDescription.ParameterName = "Description";
                pDescription.Value = payment.Description;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.MakePayment, pQuoteNumber, pPaymentDate, pAmount, pDescription);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void UpdateInvoicePayment(InvoicePaymentEntity payment, string quoteNumber)
        {
            try
            {
                SqlParameter pID = new SqlParameter();
                pID.ParameterName = "ID";
                pID.Value = payment.ID;

                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pPaymentDate = new SqlParameter();
                pPaymentDate.ParameterName = "PaymentDate";
                pPaymentDate.Value = payment.PaymentDate;

                SqlParameter pAmount = new SqlParameter();
                pAmount.ParameterName = "Amount";
                pAmount.Value = payment.Amount;

                SqlParameter pDescription = new SqlParameter();
                pDescription.ParameterName = "Description";
                pDescription.Value = payment.Description;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateInvoicePayment, pID, pQuoteNumber, pPaymentDate, pAmount, pDescription);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        internal static void DeleteInvoicePayment(int paymentID, string quoteNumber)
        {
            try
            {
                SqlParameter pID = new SqlParameter();
                pID.ParameterName = "ID";
                pID.Value = paymentID;

                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.DeleteInvoicePayment, pID, pQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }


        internal static DataTable GetAllInvoiceTypes()
        {
            DataSet result = null;
            try
            {
                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetAllInvoiceTypes, null);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result == null || result.Tables == null || result.Tables.Count == 0 ? null : result.Tables[0];
        }

        internal static void UpdateInvoiceStatusID(string quoteNumber, int invoiceStatusID)
        {
            try
            {
                SqlParameter pQuoteNumber = new SqlParameter();
                pQuoteNumber.ParameterName = "QuoteNumber";
                pQuoteNumber.Value = quoteNumber;

                SqlParameter pInvoiceStatusID = new SqlParameter();
                pInvoiceStatusID.ParameterName = "InvoiceStatusID";
                pInvoiceStatusID.Value = invoiceStatusID;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateInvoiceStatusID, pInvoiceStatusID, pQuoteNumber);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}
