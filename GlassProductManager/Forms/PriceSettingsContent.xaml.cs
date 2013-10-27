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
    /// Interaction logic for PriceSettingsContent.xaml
    /// </summary>
    public partial class PriceSettingsContent : UserControl
    {
        public PriceSettingsContent()
        {
            InitializeComponent();

            FillGlassTypes();
        }

        private void FillGlassTypes()
        {
            try
            {
                var result = BusinessLogic.GetAllGlassTypes();
                cmbGlassType.DisplayMemberPath = ColumnNames.GLASS_TYPE;
                cmbGlassType.SelectedValuePath = ColumnNames.GLASS_ID;
                cmbGlassType.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbGlassType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdatePriceList();
        }

        private void UpdatePriceList()
        {
            string selectedValue = cmbGlassType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(selectedValue))
                return;

            var result = BusinessLogic.GetPriceListByGlassTypeID(selectedValue);
            dgPriceList.ItemsSource = result;
        }

    }
}
