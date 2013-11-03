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
    /// Interaction logic for NewQuoteItemsContent.xaml
    /// </summary>
    public partial class NewQuoteItemsContent : UserControl
    {
        NewQuoteItemEntity currentItem = null;
        
        public NewQuoteItemsContent()
        {
            InitializeComponent();

            currentItem = new NewQuoteItemEntity();

            FillGlassTypes();
        }

        private void FillGlassTypes()
        {
            var result = BusinessLogic.GetAllGlassTypes();
            cmbGlassType.DisplayMemberPath = ColumnNames.GLASS_TYPE;
            cmbGlassType.SelectedValuePath = ColumnNames.ID;
            cmbGlassType.ItemsSource = result.DefaultView;
        }

        private void cmbGlassType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string glassID = cmbGlassType.SelectedValue.ToString();
            if (string.IsNullOrEmpty(glassID))
                return;

            var result = BusinessLogic.GetThicknessByGlassID(glassID);
            cmbThickness.DisplayMemberPath = ColumnNames.THICKNESS;
            cmbThickness.SelectedValuePath = ColumnNames.THICKNESSID;
            cmbThickness.ItemsSource = result.DefaultView;

            currentItem.GlassTypeID = int.Parse(glassID);
        }

        private void cmbThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbThickness.SelectedValue==null)
                return;

            string thicknessID = cmbThickness.SelectedValue.ToString();
            if (string.IsNullOrEmpty(thicknessID))
                return;

            currentItem.ThicknessID = int.Parse(thicknessID);
            UpdateCurrentTotal();
        }

        private void UpdateCurrentTotal()
        {
            lblTotalTillNow.Content = "$ " + currentItem.CurrentTotal.ToString("0.00");
        }

        private void cbIsTempered_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsTempered = true;
            UpdateCurrentTotal();
        }

        private void cbIsTempered_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsTempered = false;
            UpdateCurrentTotal();
        }

        private void txtTotalSqFt_LostFocus(object sender, RoutedEventArgs e)
        {
            txtTotalSqFt.Text = string.IsNullOrEmpty(txtTotalSqFt.Text) ? "0" : txtTotalSqFt.Text;
        }

        private void txtTotalSqFt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentItem == null)
                return;
            string totalsqft = txtTotalSqFt.Text;
            if (string.IsNullOrEmpty(totalsqft))
            {
                totalsqft = "0";
            }
            currentItem.TotalSqFT = double.Parse(totalsqft);
            UpdateCurrentTotal();
        }

        private void cbIsStraightPolish_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsStraightPolish = true;
            UpdateCurrentTotal();
        }

        private void txtStraightPolishLongSide_LostFocus(object sender, RoutedEventArgs e)
        {
            txtStraightPolishLongSide.Text = string.IsNullOrEmpty(txtStraightPolishLongSide.Text) ? "0" : txtStraightPolishLongSide.Text;
        }

        private void txtStraightPolishLongSide_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentItem == null)
                return;

            string straightPolishLongSide = txtStraightPolishLongSide.Text;
            if (string.IsNullOrEmpty(straightPolishLongSide))
            {
                straightPolishLongSide = "0";
            }
            currentItem.StraightPolishLongSide = double.Parse(straightPolishLongSide);
            UpdateCurrentTotal();
        }

        private void txtStraightPolishShortSide_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentItem == null)
                return;

            string straightPolishShortSide = txtStraightPolishShortSide.Text;
            if (string.IsNullOrEmpty(straightPolishShortSide))
            {
                straightPolishShortSide = "0";
            }
            currentItem.StraightPolishLongSide = double.Parse(straightPolishShortSide);
            UpdateCurrentTotal();
        }

        private void txtStraightPolishShortSide_LostFocus(object sender, RoutedEventArgs e)
        {
            txtStraightPolishShortSide.Text = string.IsNullOrEmpty(txtStraightPolishShortSide.Text) ? "0" : txtStraightPolishShortSide.Text;
        }

    }
}
