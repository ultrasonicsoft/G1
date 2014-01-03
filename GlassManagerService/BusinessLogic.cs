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

                result = SQLHelper.ExecuteStoredProcedure(StoredProcedure.IsValidUser, pUserName, pPassword);
                if(result == null || result.Tables == null || result.Tables[0].Rows.Count ==0)
                {
                    isValidUser = false ;
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
    }
}