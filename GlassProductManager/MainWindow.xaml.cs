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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (pageTransitionControl.CurrentPage == null)
            {
                Login loginPage = new Login();

                pageTransitionControl.ShowPage(loginPage);
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            //if (pageTransitionControl.CurrentPage == null)
            //{
            //    Login loginPage = new Login();

            //    pageTransitionControl.ShowPage(loginPage);
            //}
            //else
            //{
            //    if (pageTransitionControl.CurrentPage is NewPage)
            //    {
            //        NewUserCntrl newPage = new NewUserCntrl();

            //        pageTransitionControl.ShowPage(newPage);
            //    }
            //    else if (pageTransitionControl.CurrentPage is NewUserCntrl)
            //    {
            //        NewPage newPage = new NewPage();

            //        pageTransitionControl.ShowPage(newPage);
            //    }
            //}
        }
    }
}
