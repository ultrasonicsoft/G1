using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    public class SQLHelper
    {
        private static string dbConnectionString = string.Empty;

        static SQLHelper()
        {
            //dbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["default"].ConnectionString;
        }

        public static string ConnectionString
        {
            get { return dbConnectionString; }
            set { dbConnectionString = value; }
        }

        public static bool ConfigureDatabase()
        {
            bool isConfigured = true;

            try
            {
                using (SqlConnection _con = new SqlConnection(dbConnectionString))
                {
                    string queryStatement = "SELECT * FROM Users";

                    using (SqlCommand _cmd = new SqlCommand(queryStatement, _con))
                    {
                        DataTable customerTable = new DataTable("Users");
                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(customerTable);
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isConfigured;
        }

        public static object GetScalarValue(string sqlQuery)
        {
            object scalarValue = null;

            try
            {
                using (SqlConnection _con = new SqlConnection(dbConnectionString))
                {
                    using (SqlCommand _cmd = new SqlCommand(sqlQuery, _con))
                    {
                        _con.Open();
                        scalarValue = _cmd.ExecuteScalar();
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return scalarValue;
        }

        public static DataTable GetDataTable(string sqlQuery)
        {
            DataTable result = null;
            try
            {
                using (SqlConnection _con = new SqlConnection(dbConnectionString))
                {
                    using (SqlCommand _cmd = new SqlCommand(sqlQuery, _con))
                    {
                        DataSet dsData = new DataSet();
                        SqlDataAdapter _dap = new SqlDataAdapter(_cmd);
                        _con.Open();
                        _dap.Fill(dsData);
                        result = dsData.Tables[0];
                        _con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //Logger;
            }
            return result;
        }

        public static DataSet ExecuteStoredProcedure(string procedureName, params object[] parameters)
        {
            DataSet result = new DataSet();
            try
            {
                using (SqlConnection _con = new SqlConnection(dbConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandText = procedureName;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = _con;
                        
                        if (parameters != null)
                        {
                            foreach (SqlParameter currentParameter in parameters)
                            {
                                command.Parameters.AddWithValue(currentParameter.ParameterName, currentParameter.Value);
                            }
                        }
                        DataSet dsData = new DataSet();
                        SqlDataAdapter _dap = new SqlDataAdapter(command);
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        da.Fill(result);
                        da.Dispose();
                    }
                }




            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public static bool TestConnection(string connectionString)
        {
            bool result = true;
            try
            {
                SqlConnection testConnection = new SqlConnection(connectionString);
                testConnection.Open();
                testConnection.Close();
            }
            catch (Exception ex)
            {
                result = false;
                //Helper.ShowErrorMessageBox("Database server is down! Please check your database server or contact your vendor!", "Case Control SysteM");
                //Environment.Exit(0);
            }
            return result;
        }
    }


}
