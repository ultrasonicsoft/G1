using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GlassProductManager
{
    internal class BusinessLogic
    {
        internal static bool IsValidUser(string userName, string password)
        {
            bool isValidUser = false;
            DataSet result = null;
            try
            {
                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "userName";
                pUserName.Value = userName;

                SqlParameter pPassword = new SqlParameter();
                pPassword.ParameterName = "password";
                pPassword.Value = password;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.IsValidUser, pUserName, pPassword);
                if (result == null || result.Tables == null || result.Tables[0].Rows.Count == 0)
                {
                    isValidUser = false;
                }
                else
                {
                    isValidUser = result.Tables[0].Rows[0][0].ToString() == "1";
                }
            }
            catch (Exception ex)
            {
                isValidUser = false;
            }
            return isValidUser;
        }

        internal static WorksheetItem GetWorksheetItemDetails(string worksheetNumber, string lineID, string itemID)
        {
            WorksheetItem item = null;
            try
            {
                SqlParameter pWSNumber = new SqlParameter();
                pWSNumber.ParameterName = "wsNumber";
                pWSNumber.Value = worksheetNumber;

                SqlParameter pLineID = new SqlParameter();
                pLineID.ParameterName = "lineID";
                pLineID.Value = lineID;

                SqlParameter pItemID = new SqlParameter();
                pItemID.ParameterName = "itemID";
                pItemID.Value = itemID;

                var result  = SQLHelper.ExecuteStoredProcedure(StoredProcedures.GetWorksheetItemDetails, pWSNumber, pItemID, pLineID);

                if(result == null || result.Tables == null || result.Tables.Count==0 ||result.Tables[0].Rows.Count==0)
                {
                    return item;
                }

                item = new WorksheetItem();
                item.ID = int.Parse(result.Tables[0].Rows[0][ColumnNames.ID].ToString());
                item.Description = result.Tables[0].Rows[0][ColumnNames.Description].ToString();
                item.Quantity = result.Tables[0].Rows[0][ColumnNames.Quantity].ToString();
                item.Quantity = itemID + "/" + item.Quantity;
                item.Status = result.Tables[0].Rows[0][ColumnNames.Status].ToString();
                
                item.IsPolish = bool.Parse(result.Tables[0].Rows[0][ColumnNames.IsPolish].ToString());
                item.IsDrill= bool.Parse(result.Tables[0].Rows[0][ColumnNames.IsDrills].ToString());
                item.IsWaterJet = bool.Parse(result.Tables[0].Rows[0][ColumnNames.IsWaterjet].ToString());
                item.IsTemper = bool.Parse(result.Tables[0].Rows[0][ColumnNames.IsTemper].ToString());
                item.IsInsulate = bool.Parse(result.Tables[0].Rows[0][ColumnNames.IsInsulate].ToString());

            }
            catch (Exception ex)
            {
            }

            return item;
        }

        internal static void UpdateGlassItemStatus(string glassItemID, string status)
        {
            DataSet result = null;
            try
            {
                SqlParameter pGlassItemID = new SqlParameter();
                pGlassItemID.ParameterName = "glassItemID";
                pGlassItemID.Value = int.Parse(glassItemID);

                SqlParameter pStatus = new SqlParameter();
                pStatus.ParameterName = "status";
                pStatus.Value = status;

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.UpdateGlassItemStatus, pGlassItemID, pStatus);
               
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static void AddJobToPrintQueue(BarcodeLabel item)
        {
            try
            {
                 SqlParameter pWSNumber = new SqlParameter();
                pWSNumber.ParameterName = "WSNumber";
                pWSNumber.Value = item.WSNumber;

                SqlParameter pUserName = new SqlParameter();
                pUserName.ParameterName = "userName";
                pUserName.Value = item.UserName;

                SqlParameter pLineID = new SqlParameter();
                pLineID.ParameterName = "lineID";
                pLineID.Value = item.LineID;

                SqlParameter pItemID = new SqlParameter();
                pItemID.ParameterName = "itemID";
                pItemID.Value = item.ItemID;

                SQLHelper.ExecuteStoredProcedure(StoredProcedures.AddJobToPrintQueue, pWSNumber, pLineID, pItemID, pUserName);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static bool IsValidWorksheetLineItem(BarcodeLabel item)
        {
            bool isValidWSItem = false;
            try
            {
                SqlParameter pWSNumber = new SqlParameter();
                pWSNumber.ParameterName = "WSNumber";
                pWSNumber.Value = item.WSNumber;

                SqlParameter pLineID = new SqlParameter();
                pLineID.ParameterName = "lineID";
                pLineID.Value = item.LineID;

                SqlParameter pItemID = new SqlParameter();
                pItemID.ParameterName = "itemID";
                pItemID.Value = item.ItemID;

                var result = SQLHelper.ExecuteStoredProcedure(StoredProcedures.IsValidWorksheetLineItem, pWSNumber, pLineID, pItemID);
                if (result == null || result.Tables == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                {
                    return isValidWSItem;
                }

                isValidWSItem = bool.Parse(result.Tables[0].Rows[0][ColumnNames.Status].ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
            return isValidWSItem;
        }
    }
}