using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ConfigureDatabase : Window
    {
        public bool IsDatabaseReady { get; set; }

        public ConfigureDatabase()
        {
            InitializeComponent();

            DisplayCurrentConnectionString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtServerName.Text) || string.IsNullOrEmpty(txtDatabaseName.Text) || string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Password))
                {
                    Helper.ShowErrorMessageBox("Please provide required details!");
                    return;
                }

                Properties.Settings.Default.ConnectionString = string.Format(System.Configuration.ConfigurationSettings.AppSettings["defaultConStr"],
                                txtServerName.Text, txtDatabaseName.Text, txtUserName.Text, txtPassword.Password);
                SQLHelper.ConnectionString = Properties.Settings.Default.ConnectionString;
                Properties.Settings.Default.Save();

                Helper.ShowInformationMessageBox("Database server configured successfully!");
                IsDatabaseReady = true;
                this.Close();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                IsDatabaseReady = false;
            }
        }

        private void DisplayCurrentConnectionString()
        {
            try
            {
                if (false == string.IsNullOrEmpty(Properties.Settings.Default.ConnectionString))
                {
                    string[] parts = Properties.Settings.Default.ConnectionString.Split(';');

                    string[] temp = parts[0].Split('=');
                    txtServerName.Text = temp[1];

                    temp = parts[1].Split('=');
                    txtDatabaseName.Text = temp[1];

                    string database = temp[1];

                    temp = parts[2].Split('=');
                    txtUserName.Text = temp[1];

                    temp = parts[3].Split('=');
                    txtPassword.Password = temp[1];
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = string.Format(System.Configuration.ConfigurationSettings.AppSettings["defaultConStr"],
                                txtServerName.Text, txtDatabaseName.Text, txtUserName.Text, txtPassword.Password);
                if (SQLHelper.TestConnection(connectionString))
                {
                    Helper.ShowInformationMessageBox("Connection successful!");
                }
                else
                {
                    Helper.ShowErrorMessageBox("Connection failed! Please contact your vendor.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            
        }
    }
}
