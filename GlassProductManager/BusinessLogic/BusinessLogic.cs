using DBHelper;
using System;
using System.Collections.Generic;
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
                var result = SQLHelper.GetScalarValue(string.Format(SelectQueries.USER_LOGIN_QUERY,userName,password));
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
    }
}
