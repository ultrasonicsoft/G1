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
using System.Windows.Shapes;
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        #region Private Members

        WelcomeUser welcomeUser = new WelcomeUser();
        HomeContent homeContent = new HomeContent();
        DashboardMenu dashboardMenu = new DashboardMenu();

        private string _userName = string.Empty;

        #endregion

        #region Properties

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                welcomeUser.UserName = _userName;
            }
        }

        #endregion

        #region Constructor

        public Dashboard()
        {
            InitializeComponent();

            SetDefaultContent();
            
        }

        private void SetDefaultContent()
        {
            try
            {
                if (ucMainContent.CurrentPage == null)
                {
                    ucMainContent.ShowPage(homeContent);
                }
                if (ucWelcomeUser.CurrentPage == null)
                {
                    ucWelcomeUser.ShowPage(welcomeUser);
                }
                if (ucDashboardMenu.CurrentPage == null)
                {
                    ucDashboardMenu.ShowPage(dashboardMenu);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            
        }

        #endregion

    }
}
