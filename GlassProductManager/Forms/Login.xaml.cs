using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (BusinessLogic.IsValidUser(txtUserName.Text,txtPassword.Password))
            {
                MessageBox.Show("Jai Ganesh");
            }
            else
            {
                MessageBox.Show("no");
            }
        }
    }
}
