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
    }
}
