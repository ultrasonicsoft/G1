﻿using System;
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
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (BusinessLogic.IsValidUser(txtUserName.Text, txtPassword.Password))
                {

                    Dashboard dialog = new Dashboard();
                    dialog.UserName = txtUserName.Text;
                    dialog.Show();
                    this.Close();
                }
                else
                {
                    Helper.ShowErrorMessageBox("Invalid User name or Password");
                    txtUserName.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnConfigureDatabase_Click(object sender, RoutedEventArgs e)
        {
            ConfigureDatabase configure = new ConfigureDatabase();
            configure.ShowDialog();
        }
    }
}
