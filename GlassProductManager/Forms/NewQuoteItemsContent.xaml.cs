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
            FillShapes();
        }

        private void FillGlassTypes()
        {
            var result = BusinessLogic.GetAllGlassTypes();
            cmbGlassType.DisplayMemberPath = ColumnNames.GLASS_TYPE;
            cmbGlassType.SelectedValuePath = ColumnNames.ID;
            cmbGlassType.ItemsSource = result.DefaultView;
        }
        
        private void FillShapes()
        {
            var result = BusinessLogic.GetAllShapes();
            cmbShape.DisplayMemberPath = ColumnNames.SHAPE;
            cmbShape.SelectedValuePath = ColumnNames.ID;
            cmbShape.ItemsSource = result.DefaultView;
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
            NewItemsChanged(txtTotalSqFt.Text, "TotalSqFT");

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
            NewItemsChanged(txtStraightPolishLongSide.Text, "StraightPolishLongSide");
        }

        private void txtStraightPolishShortSide_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewItemsChanged(txtStraightPolishShortSide.Text, "StraightPolishShortSide");
        }

        private void txtStraightPolishShortSide_LostFocus(object sender, RoutedEventArgs e)
        {
            txtStraightPolishShortSide.Text = string.IsNullOrEmpty(txtStraightPolishShortSide.Text) ? "0" : txtStraightPolishShortSide.Text;
        }

        private void txtStraightPolishTotalInches_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewItemsChanged(txtStraightPolishTotalInches.Text, "StraightPolishTotalInches");
        }

        private void txtStraightPolishTotalInches_LostFocus(object sender, RoutedEventArgs e)
        {
            txtStraightPolishTotalInches.Text = string.IsNullOrEmpty(txtStraightPolishTotalInches.Text) ? "0" : txtStraightPolishTotalInches.Text;
        }

        private void txtCustomShapePolishSize_LostFocus(object sender, RoutedEventArgs e)
        {
            txtCustomShapePolishSize.Text = string.IsNullOrEmpty(txtCustomShapePolishSize.Text) ? "0" : txtCustomShapePolishSize.Text;
        }

        private void txtCustomShapePolishSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewItemsChanged(txtCustomShapePolishSize.Text, "CustomPolishTotalInches");
        }

        private void cbIsStraightPolish_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsStraightPolish = false;
            txtStraightPolishLongSide.Text = "0";
            txtStraightPolishLongSide.Text = "0";
            txtStraightPolishTotalInches.Text = "0";
            UpdateCurrentTotal();
        }

        private void cbCustomShapePolish_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsCustomShapePolish = false;
            txtCustomShapePolishSize.Text = "0";
            UpdateCurrentTotal();
        }

        private void cbCustomShapePolish_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsCustomShapePolish = true;
            UpdateCurrentTotal();
        }

        private void cbIsMiter_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsMiter = false;
            txtMiterTotalInches.Text = "0";
            UpdateCurrentTotal();
        }

        private void cbIsMiter_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsMiter = true;
            UpdateCurrentTotal();
        }

        private void txtMiterTotalInches_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewItemsChanged(txtMiterTotalInches.Text, "MiterTotalInches");
        }

        private void txtMiterTotalInches_LostFocus(object sender, RoutedEventArgs e)
        {
            txtMiterTotalInches.Text = string.IsNullOrEmpty(txtMiterTotalInches.Text) ? "0" : txtMiterTotalInches.Text;
        }

        private void cbNotches_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsNotch = false;
            txtNotchesNumber.Text = "0";
            UpdateCurrentTotal();
        }

        private void cbNotches_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsNotch = true;
            UpdateCurrentTotal();
        }

        private void txtNotchesNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewItemsChanged(txtNotchesNumber.Text, "Notches");
        }

        private void txtNotchesNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            txtNotchesNumber.Text = string.IsNullOrEmpty(txtNotchesNumber.Text) ? "0" : txtNotchesNumber.Text;
        }

        private void cbHinges_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsHinges = false;
            txtHingesNumber.Text = "0";
            UpdateCurrentTotal();
        }

        private void cbHinges_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsHinges = true;
            UpdateCurrentTotal();
        }

        private void txtHingesNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            txtHingesNumber.Text = string.IsNullOrEmpty(txtHingesNumber.Text) ? "0" : txtHingesNumber.Text;
        }

        private void txtHingesNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewItemsChanged(txtHingesNumber.Text, "Hinges");
        }
     
        private void cbPatches_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsPatches= false;
            txtPatchesNumber.Text = "0";
            UpdateCurrentTotal();
        }

        private void cbPatches_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsPatches = true;
            UpdateCurrentTotal();
        }

        private void txtPatchesNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            NewItemsChanged(txtPatchesNumber.Text, "Patches");
        }

        private void txtPatchesNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            txtPatchesNumber.Text = string.IsNullOrEmpty(txtPatchesNumber.Text) ? "0" : txtPatchesNumber.Text;
        }

        private void NewItemsChanged(string newValue, string propertyChanged)
        {
            if (currentItem == null)
                return;

            int tempValue = 0;
            int.TryParse(newValue, out tempValue);

            switch (propertyChanged)
            {
                case "StraightPolishTotalInches":
                    currentItem.StraightPolishTotalInches = tempValue;
                    break;
                case "CustomPolishTotalInches":
                    currentItem.CustomPolishTotalInches = tempValue;
                    break;
                case "StraightPolishShortSide":
                    currentItem.StraightPolishShortSide = tempValue;
                    break;
                case "StraightPolishLongSide":
                    currentItem.StraightPolishLongSide = tempValue;
                    break;
                case "TotalSqFT":
                    currentItem.TotalSqFT = tempValue;
                    break;
                case "MiterTotalInches":
                    currentItem.MiterTotalInches = tempValue;
                    break;
                case "Notches":
                    currentItem.Notches = tempValue;
                    break;
                case "Hinges":
                    currentItem.Hinges = tempValue;
                    break;
                case "Patches":
                    currentItem.Patches = tempValue;
                    break;
                default:
                    break;
            }

            UpdateCurrentTotal();
        }
       
    }
}
