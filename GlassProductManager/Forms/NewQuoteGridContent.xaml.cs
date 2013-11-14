using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for NewQuoteGridContent.xaml
    /// </summary>
    public partial class NewQuoteGridContent : UserControl
    {
        private ObservableCollection<QuoteGridEntity> _allQuoteData = new ObservableCollection<QuoteGridEntity>();

        public ObservableCollection<QuoteGridEntity> allQuoteData
        {
            get { return _allQuoteData; }
            set { _allQuoteData = value; }
        }

        public NewQuoteGridContent()
        {
            InitializeComponent();

            FillShippingMethods();
            FillLeadTimeTypes();
            FillLeadTime();

            dtQuoteCreatedOn.SelectedDate = DateTime.Now;
            dtQuoteRequestedOn.SelectedDate = DateTime.Now;

            UpdateDefaultLeadTimeSettings();
        }

        private void UpdateDefaultLeadTimeSettings()
        {
            var result = BusinessLogic.GetLeadTimeSettings();
            if (result == null)
                return;
            cmbLeadTime.SelectedIndex = int.Parse(result.Rows[0][ColumnNames.LeadTimeID].ToString());
            cmbLeadTimeType.SelectedIndex = int.Parse(result.Rows[0][ColumnNames.LeadTimeTypeID].ToString());
            cbUseAsDefault.IsChecked = bool.Parse(result.Rows[0][ColumnNames.IsDefaultLeadTime].ToString());
        }

        private void FillShippingMethods()
        {
            var result = BusinessLogic.GetAllShippingMethods();
            cmbShippingMethod.DisplayMemberPath = ColumnNames.Shipping;
            cmbShippingMethod.SelectedValuePath = ColumnNames.ID;
            cmbShippingMethod.ItemsSource = result.DefaultView;
        }
        private void FillLeadTimeTypes()
        {
            var result = BusinessLogic.GetAllLeadTimeTypes();
            cmbLeadTimeType.DisplayMemberPath = ColumnNames.LeadTimeType;
            cmbLeadTimeType.SelectedValuePath = ColumnNames.ID;
            cmbLeadTimeType.ItemsSource = result.DefaultView;
        }

        private void FillLeadTime()
        {
            var result = BusinessLogic.GetAllLeadTime();
            cmbLeadTime.DisplayMemberPath = ColumnNames.LeadTime;
            cmbLeadTime.SelectedValuePath = ColumnNames.ID;
            cmbLeadTime.ItemsSource = result.DefaultView;
        }

        private void btnRegenerate_Click(object sender, RoutedEventArgs e)
        {
            allQuoteData.Clear();

            ClearSubTotalSection();
        }

        private void ClearSubTotalSection()
        {
            lblSubTotal.Content = "0.00";
            txtSubTotal.Text = "0.00";
            txtDiscount.Text = "0.00";
            txtDelivery.Text = "0.00";
            txtRushOrder.Text = "0.00";
            txtTax.Text = "0.00";
            lblGrandTotal.Content = "0.00";
        }

        private void cbUseAsDefault_Checked(object sender, RoutedEventArgs e)
        {
            if (cmbLeadTime.SelectedIndex == -1 || cmbLeadTimeType.SelectedIndex == -1)
            {
                Helper.ShowErrorMessageBox("Please select Lead Time first");
                cbUseAsDefault.IsChecked = false;
                return;
            }

            BusinessLogic.SetDefaultLeadTime(cmbLeadTime.SelectedIndex, cmbLeadTimeType.SelectedIndex);
        }

        private void cbUseAsDefault_Unchecked(object sender, RoutedEventArgs e)
        {
            BusinessLogic.ResetDefaultLeadTime();
            cmbLeadTime.SelectedIndex = -1;
            cmbLeadTimeType.SelectedIndex = -1;
        }

       
    }
}
