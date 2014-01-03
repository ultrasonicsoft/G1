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

            }
            catch (Exception ex)
            {
            }

            return item;
        }
    }
}