using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    internal class SelectQueries
    {
        internal const string USER_LOGIN_QUERY = "SELECT COUNT(1) FROM Users WHERE UserName='{0}' AND Password = '{1}'";
        internal const string GET_ALL_GLASS_TYPES = "SELECT [ID],[GlassType] FROM [LuGlass]";
        internal const string GET_GLASS_RATES_BY_ID = "SELECT * FROM GlassRates WHERE ID='{0}'";
    }
}
