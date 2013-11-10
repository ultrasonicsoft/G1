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

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for WelcomeUser.xaml
    /// </summary>
    public partial class WelcomeUser : UserControl
    {
        public string UserName 
        { 
            get 
            { 
                return UserName; 
            } 
            set 
            {
                lblUserName.Content = "Welcome " + value + "!"; 
            } 
        }

        public WelcomeUser()
        {
            InitializeComponent();
        }
    }
}
