using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                MainWindow main = null;
                if (string.IsNullOrEmpty(GlassProductManager.Properties.Settings.Default.ConnectionString) == false)
                {
                    SQLHelper.ConnectionString = GlassProductManager.Properties.Settings.Default.ConnectionString;
                }

                if (string.IsNullOrEmpty(SQLHelper.ConnectionString) == false)
                {
                    //main = new MainWindow();
                    //main.ShowDialog();
                    //main.Close();
                }
                else
                {
                    ConfigureDatabase configureDB = new ConfigureDatabase();
                    configureDB.ShowDialog();
                    if (configureDB.IsDatabaseReady)
                    {
                        //main = new MainWindow();
                        //main.ShowDialog();
                        //main.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}
