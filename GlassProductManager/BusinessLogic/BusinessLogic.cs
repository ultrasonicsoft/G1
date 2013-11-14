﻿using DBHelper;
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
                return result.ToString() == "1";
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

        internal static void SaveQuoteHeader(QuoteHeader header)
        {
            try
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

                SqlParameter pCustomerID = new SqlParameter();
                pCustomerID.ParameterName = "CustomerID";
                pCustomerID.Value = header.CustomerID;

                if (header.IsNewCustomer)
                {
                   string customerID = CreateNewCustomer(header.SoldTo);
                   
                    if (header.IsShipToOtherAddress == true && string.IsNullOrEmpty(customerID) == false)
                   {
                       InsertShippingDetails(customerID, header.QuoteNumber, header.ShipTo);
                   }
                }

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.AddQuoteHeader, pQuoteCreatedOn, pRequestedShipDate, pCustomerPO, pLeadTimeID, pLeadTimeTypeID, pShipToOtherAddress, pQuoteNumber, pCustomerID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private static string CreateNewCustomer(ShippingDetails soldTo)
        {
            string customerID = string.Empty;
            try
            {
                SqlParameter pAddress = new SqlParameter();
                pAddress.ParameterName = "Address";
                pAddress.Value = soldTo.Address;

                SqlParameter pFirstName = new SqlParameter();
                pFirstName.ParameterName = "FirstName";
                pFirstName.Value = soldTo.FirstName;

                SqlParameter pLastName = new SqlParameter();
                pLastName.ParameterName = "LastName";
                pLastName.Value = soldTo.LastName;

                SqlParameter pPhone = new SqlParameter();
                pPhone.ParameterName = "Phone";
                pPhone.Value = soldTo.Phone;

                SqlParameter pFax = new SqlParameter();
                pFax.ParameterName = "Fax";
                pFax.Value = soldTo.Fax;

                SqlParameter pEmail = new SqlParameter();
                pEmail.ParameterName = "Email";
                pEmail.Value = soldTo.Email;

                SqlParameter pMisc = new SqlParameter();
                pMisc.ParameterName = "Misc";
                pMisc.Value = soldTo.Misc;

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

        private static void InsertShippingDetails(string customerID, string quoteNumber, ShippingDetails shipTo)
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

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.InsertShippingDetails, pQuoteNumber,pCustomerID, pAddress, pFirstName, pLastName, pPhone, pFax, pEmail, pMisc);

            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}
