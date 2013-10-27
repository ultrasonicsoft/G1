using DBHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

        internal static DataTable GetAllGlassTypes()
        {
            DataTable result = null;
            try
            {
                result = SQLHelper.GetDataTable(SelectQueries.GET_ALL_GLASS_TYPES);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        internal static ObservableCollection<GlassRate> GetPriceListByGlassTypeID(string selectedValue)
        {
            ObservableCollection<GlassRate> rateStructure = new ObservableCollection<GlassRate>();

            try
            {
                var result = SQLHelper.GetDataTable(string.Format(SelectQueries.GET_GLASS_RATES_BY_ID,selectedValue));
                if (result == null)
                    return null;

                for (int rowIndex = 0; rowIndex < result.Rows.Count; rowIndex++)
                {
                    rateStructure.Add(new GlassRate()
                    {
                        ID = result.Rows[rowIndex][ColumnNames.GLASS_ID].ToString(),
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
    }
}
