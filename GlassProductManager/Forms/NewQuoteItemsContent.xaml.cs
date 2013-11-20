using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public Style textBoxNormalStyle;
        public Style textBoxErrorStyle;


        public ObservableCollection<CutoutData> allCutoutData
        {
            get { return currentItem._allCutoutData; }
            set { currentItem._allCutoutData = value; }
        }



        public NewQuoteItemsContent()
        {
            InitializeComponent();

            currentItem = new NewQuoteItemEntity();

            FillGlassTypes();
            FillShapes();

            FillCutoutData();
            FillInsulationDetails();
            currentItem.GlassType1 = new InsulationDetails();
            currentItem.GlassType2 = new InsulationDetails();
            FrameworkElement frameworkElement;

            frameworkElement = new FrameworkElement();
            //textBoxNormalStyle = (Style)frameworkElement.TryFindResource("textBoxNormalStyle");
            textBoxErrorStyle = (Style)frameworkElement.TryFindResource("textBoxErrorStyle");
        }

        private void FillCutoutData()
        {
            //CutoutData cutout = GetNewCutoutObject();
            //allCutoutData.Add(GetNewCutoutObject());

            dgCutoutDetails.SelectionChanged += new SelectionChangedEventHandler(dgCutout_SelectionChanged);
        }

        private void FillInsulationDetails()
        {
            var result = BusinessLogic.GetAllGlassTypes();
            cmbGlassType1.DisplayMemberPath = ColumnNames.GLASS_TYPE;
            cmbGlassType1.SelectedValuePath = ColumnNames.ID;
            cmbGlassType1.ItemsSource = result.DefaultView;

            cmbGlassType2.DisplayMemberPath = ColumnNames.GLASS_TYPE;
            cmbGlassType2.SelectedValuePath = ColumnNames.ID;
            cmbGlassType2.ItemsSource = result.DefaultView;

            ObservableCollection<string> list = new ObservableCollection<string>();
            list.Add("Yes");
            list.Add("No");

            cmbTemp1.ItemsSource = list;
            cmbTemp2.ItemsSource = list;

        }

        private CutoutData GetNewCutoutObject()
        {
            CutoutData data = new CutoutData() { Height = 0, Price = 0, Quantity = 0, Width = 0 };
            return data;
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
            if (cmbGlassType.SelectedValue == null)
                return;
            string glassID = cmbGlassType.SelectedValue.ToString();

            var result = BusinessLogic.GetThicknessByGlassID(glassID);
            cmbThickness.DisplayMemberPath = ColumnNames.THICKNESS;
            cmbThickness.SelectedValuePath = ColumnNames.THICKNESSID;
            cmbThickness.ItemsSource = result.DefaultView;

            currentItem.GlassTypeID = int.Parse(glassID);
            currentItem.GlassType = cmbGlassType.SelectedItem.ToString();
            currentItem.GlassType = (cmbGlassType.SelectedItem as System.Data.DataRowView)[1].ToString();
        }

        private void cmbThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbThickness.SelectedValue == null)
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

        private bool IsNumberOnly(TextBox input)
        {
            bool result = false;
            if (Regex.IsMatch(input.Text, @"^\d+$"))
            {
                input.Style = null;
                result = true;
            }
            else
            {
                textBoxNormalStyle = input.Style;
                input.Style = textBoxErrorStyle;
            }
            return result;
        }

        private void txtTotalSqFt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumberOnly(txtTotalSqFt))
            {
                NewItemsChanged(txtTotalSqFt.Text, "TotalSqFT");
            }
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
            if (IsNumberOnly(txtStraightPolishLongSide))
            {
                NewItemsChanged(txtStraightPolishLongSide.Text, "StraightPolishLongSide");
            }
        }

        private void txtStraightPolishShortSide_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumberOnly(txtStraightPolishShortSide))
            {
                NewItemsChanged(txtStraightPolishShortSide.Text, "StraightPolishShortSide");
            }
        }

        private void txtStraightPolishShortSide_LostFocus(object sender, RoutedEventArgs e)
        {
            txtStraightPolishShortSide.Text = string.IsNullOrEmpty(txtStraightPolishShortSide.Text) ? "0" : txtStraightPolishShortSide.Text;
        }

        private void txtStraightPolishTotalInches_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumberOnly(txtStraightPolishTotalInches))
            {
                NewItemsChanged(txtStraightPolishTotalInches.Text, "StraightPolishTotalInches");
            }
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
            if (IsNumberOnly(txtCustomShapePolishSize))
            {
                NewItemsChanged(txtCustomShapePolishSize.Text, "CustomPolishTotalInches");
            }
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
            if (IsNumberOnly(txtMiterTotalInches))
            {
                NewItemsChanged(txtMiterTotalInches.Text, "MiterTotalInches");
            }
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
            if (IsNumberOnly(txtNotchesNumber))
            {
                NewItemsChanged(txtNotchesNumber.Text, "Notches");
            }
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
            if (IsNumberOnly(txtHingesNumber))
            {
                NewItemsChanged(txtHingesNumber.Text, "Hinges");
            }
        }

        private void cbPatches_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsPatches = false;
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
            if (IsNumberOnly(txtPatchesNumber))
            {
                NewItemsChanged(txtPatchesNumber.Text, "Patches");
            }
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
                case "Holes":
                    currentItem.Holes = tempValue;
                    break;
                default:
                    break;
            }

            UpdateCurrentTotal();
        }

        #region Insulation Methods

        private void DataGrid_GotFocus(object sender, RoutedEventArgs e)
        {
            // Lookup for the source to be DataGridCell
            if (e.OriginalSource.GetType() == typeof(DataGridCell))
            {
                // Starts the Edit on the row;
                DataGrid grd = (DataGrid)sender;
                grd.BeginEdit(e);
            }
        }
        #endregion



        void dgCutout_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CalculateCutoutTotalPrice();
        }

        private void btnAddNewCutout_Click(object sender, RoutedEventArgs e)
        {
            CutoutData gridData = GetNewCutoutObject();
            allCutoutData.Add(gridData);
        }

        private void btnDeleteCutout_Click(object sender, RoutedEventArgs e)
        {
            CutoutData selectedItem = dgCutoutDetails.SelectedItem as CutoutData;
            if (selectedItem == null)
                return;
            allCutoutData.Remove(selectedItem);
        }

        private void dgCutoutDetails_LostFocus(object sender, RoutedEventArgs e)
        {
            CalculateCutoutTotalPrice();
        }

        private void CalculateCutoutTotalPrice()
        {
            double totalPrice = 0;

            CutoutData item = null;
            for (int index = 0; index < dgCutoutDetails.Items.Count; index++)
            {
                item = dgCutoutDetails.Items[index] as CutoutData;
                if (item == null)
                    continue;
                totalPrice += item.Quantity * item.Price;

            }

            lblCutoutTotal.Content = "$ " + totalPrice.ToString("0.00");

            currentItem.CutoutTotal = totalPrice;
            UpdateCurrentTotal();
        }

        private void cmbGlassType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGlassType1.SelectedValue == null)
                return;

            string glassID = cmbGlassType1.SelectedValue.ToString();

            var result = BusinessLogic.GetThicknessByGlassID(glassID);
            cmbThickness1.DisplayMemberPath = ColumnNames.THICKNESS;
            cmbThickness1.SelectedValuePath = ColumnNames.THICKNESSID;
            cmbThickness1.ItemsSource = result.DefaultView;

            UpdateInsulationGlassTotal(cmbGlassType1, cmbThickness1, cmbTemp1, currentItem.GlassType1, txtSqFt1, txtGlassType1Total);
        }

        private void cmbGlassType2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGlassType2.SelectedValue == null)
                return;

            string glassID = cmbGlassType2.SelectedValue.ToString();

            var result = BusinessLogic.GetThicknessByGlassID(glassID);
            cmbThickness2.DisplayMemberPath = ColumnNames.THICKNESS;
            cmbThickness2.SelectedValuePath = ColumnNames.THICKNESSID;
            cmbThickness2.ItemsSource = result.DefaultView;

            UpdateInsulationGlassTotal(cmbGlassType2, cmbThickness2, cmbTemp2, currentItem.GlassType2, txtSqFt2, txtGlassType2Total);

        }

        private void UpdateInsulationGlassTotal(ComboBox glassType, ComboBox thickness, ComboBox isTempered, InsulationDetails currentGlassType, TextBox currentSQFT, TextBox currentGlassTotal)
        {
            if (glassType.SelectedValue == null || thickness.SelectedValue == null || string.IsNullOrEmpty(currentSQFT.Text))
                return;

            currentGlassType.GlassTypeID = int.Parse(glassType.SelectedValue.ToString());
            currentGlassType.GlassType = (glassType.SelectedItem as System.Data.DataRowView)[1].ToString();

            currentGlassType.ThicknessID = int.Parse(thickness.SelectedValue.ToString());
            currentGlassType.Thickness = (thickness.SelectedItem as System.Data.DataRowView)[1].ToString();

            var result = BusinessLogic.GetRatesByGlassTypeAndThickness(currentGlassType.GlassTypeID, currentGlassType.ThicknessID);
            if (result == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                return;

            double _cutsqftRate = double.Parse(result.Tables[0].Rows[0][ColumnNames.CUTSQFT].ToString());
            double _temperedSQFT = double.Parse(result.Tables[0].Rows[0][ColumnNames.TEMPEREDSQFT].ToString());

            currentGlassType.IsTempered = isTempered.SelectedValue.ToString() == "Yes";
            currentGlassType.SqFt = int.Parse(currentSQFT.Text);

            if (currentGlassType.IsTempered)
            {
                currentGlassType.Total = currentGlassType.SqFt == 0 ? 0 : currentGlassType.SqFt * _temperedSQFT;
            }
            else
            {
                currentGlassType.Total = currentGlassType.SqFt == 0 ? 0 : currentGlassType.SqFt * _cutsqftRate;
            }

            currentGlassTotal.Text = currentGlassType.Total.ToString();

            UpdateInsulationTotal();
        }

        private void UpdateInsulationTotal()
        {
            lblMaterialCost.Content = "$ " + (currentItem.GlassType1.Total + currentItem.GlassType2.Total).ToString("0.00");

            double insulationTierCost = BusinessLogic.GetInsulationTierCost(currentItem.GlassType1.SqFt);
            lblInsulationTier.Content = "$ " + insulationTierCost.ToString("0.00");

            double insulationTierTotal = insulationTierCost * currentItem.GlassType1.SqFt;
            lblInsulationTierTotal.Content = "$ " + insulationTierTotal.ToString("0.00");

            double insulationTotal = insulationTierTotal + currentItem.GlassType1.Total + currentItem.GlassType2.Total;
            lblInsulationTotal.Content = "$ " + insulationTotal.ToString("0.00");

            currentItem.InsulateTotalCost = insulationTotal;
            UpdateCurrentTotal();
        }

        private void cmbTemp1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInsulationGlassTotal(cmbGlassType1, cmbThickness1, cmbTemp1, currentItem.GlassType1, txtSqFt1, txtGlassType1Total);
        }

        private void cmbThickness1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInsulationGlassTotal(cmbGlassType1, cmbThickness1, cmbTemp1, currentItem.GlassType1, txtSqFt1, txtGlassType1Total);
        }

        private void txtSqFt1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumberOnly(txtSqFt1))
            {
                if (currentItem == null)
                    return;

                if (txtSqFt2 != null)
                    txtSqFt2.Text = txtSqFt1.Text;
                UpdateInsulationGlassTotal(cmbGlassType1, cmbThickness1, cmbTemp1, currentItem.GlassType1, txtSqFt1, txtGlassType1Total);
            }
        }

        private void cmbThickness2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInsulationGlassTotal(cmbGlassType2, cmbThickness2, cmbTemp2, currentItem.GlassType2, txtSqFt2, txtGlassType2Total);
        }

        private void cmbTemp2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateInsulationGlassTotal(cmbGlassType2, cmbThickness2, cmbTemp2, currentItem.GlassType2, txtSqFt2, txtGlassType2Total);
        }

        private void txtSqFt1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSqFt1.Text))
            {
                txtSqFt1.Text = "0";
            }
        }

        private void cbHoles_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsHoles = true;
            UpdateCurrentTotal();
        }

        private void cbHoles_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsHoles = false;
            txtHoleNumbers.Text = "0";
            UpdateCurrentTotal();
        }

        private void txtHoleNumbers_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumberOnly(txtHoleNumbers))
            {
                NewItemsChanged(txtHoleNumbers.Text, "Holes");
            }
        }

        private void txtHoleNumbers_LostFocus(object sender, RoutedEventArgs e)
        {
            txtHoleNumbers.Text = string.IsNullOrEmpty(txtHoleNumbers.Text) ? "0" : txtHoleNumbers.Text;
        }

        private void cbLogo_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsLogoRequired = false;
        }

        private void cbLogo_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsLogoRequired = true;
        }

        private void txtMiterLongSide_LostFocus(object sender, RoutedEventArgs e)
        {
            txtMiterLongSide.Text = string.IsNullOrEmpty(txtMiterLongSide.Text) ? "0" : txtMiterLongSide.Text;
            if (IsNumberOnly(txtMiterLongSide))
            {
                currentItem.MiterLongSide = int.Parse(txtMiterLongSide.Text);
            }
        }

        private void txtMiterShortSide_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentItem == null)
                return;

            if (IsNumberOnly(txtMiterShortSide))
            {
                txtMiterShortSide.Text = string.IsNullOrEmpty(txtMiterShortSide.Text) ? "0" : txtMiterShortSide.Text;
                currentItem.MiterShortSide = int.Parse(txtMiterShortSide.Text);
            }
        }

        private void btnAddToQuote_Click(object sender, RoutedEventArgs e)
        {
            string itemDescription = currentItem.GetDescriptionString();

            Dashboard parent = Window.GetWindow(this) as Dashboard;
            if (parent != null)
            {
                NewQuoteContent content = parent.ucMainContent.CurrentPage as NewQuoteContent;
                if (content != null)
                {
                    NewQuoteGridContent grid = content.ucNewQuoteGrid.CurrentPage as NewQuoteGridContent;
                    if (grid != null)
                    {
                        QuoteGridEntity newItem = new QuoteGridEntity();
                        newItem.LineID = grid.allQuoteData.Count + 1;
                        newItem.Quantity = 1;
                        newItem.Description = itemDescription;
                        newItem.Dimension = GetDimensionString();
                        newItem.TotalSqFt = currentItem.TotalSqFT.ToString();
                        newItem.UnitPrice = currentItem.CurrentTotal.ToString();
                        newItem.Total = currentItem.CurrentTotal;
                        grid.allQuoteData.Add(newItem);
                    }
                }
            }
        }

        private string GetDimensionString()
        {
            string defaultFraction = "x/y";

            if (string.Equals(currentItem.GlassWidthFraction, defaultFraction) && string.Equals(currentItem.GlassHeightFraction, defaultFraction))
                return string.Format(@"{0}"" x {1}""", currentItem.GlassWidth, currentItem.GlassHeight);

            else if (false == string.Equals(currentItem.GlassWidthFraction, defaultFraction) && string.Equals(currentItem.GlassHeightFraction, defaultFraction))
                return string.Format(@"{0} {1}"" x {2}""", currentItem.GlassWidth, currentItem.GlassWidthFraction, currentItem.GlassHeight);

            else if (string.Equals(currentItem.GlassWidthFraction, defaultFraction) && false == string.Equals(currentItem.GlassHeightFraction, defaultFraction))
                return string.Format(@"{0}"" x {1} {2}""", currentItem.GlassWidth, currentItem.GlassHeight, currentItem.GlassHeightFraction);
            else
                return string.Format(@"{0} {1}"" x  {2} {3}""", currentItem.GlassWidth, currentItem.GlassWidthFraction, currentItem.GlassHeight, currentItem.GlassHeightFraction);
        }

        private void txtGlassWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentItem == null)
                return;

            if (IsNumberOnly(txtGlassWidth))
            {
                txtGlassWidth.Text = string.IsNullOrEmpty(txtGlassWidth.Text) ? "0" : txtGlassWidth.Text;
                currentItem.GlassWidth = int.Parse(txtGlassWidth.Text);
            }
        }

        private void txtGlassHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (currentItem == null)
                return;

            if (IsNumberOnly(txtGlassHeight))
            {
                txtGlassHeight.Text = string.IsNullOrEmpty(txtGlassHeight.Text) ? "0" : txtGlassHeight.Text;
                currentItem.GlassHeight = int.Parse(txtGlassHeight.Text);
            }
        }

        private void btnNewItem_Click(object sender, RoutedEventArgs e)
        {
            if (currentItem != null)
            {
                var result = Helper.ShowQuestionMessageBox("Are you sure to discard current changes?");
                if (result == MessageBoxResult.Yes)
                {
                    currentItem = new NewQuoteItemEntity();
                    allCutoutData.Clear();
                    currentItem.GlassType1 = new InsulationDetails();
                    currentItem.GlassType2 = new InsulationDetails();

                    UpdateCurrentTotal();
                    ResetAllControls();
                }
            }
            else
            {
                currentItem = new NewQuoteItemEntity();
                allCutoutData.Clear();
                currentItem.GlassType1 = new InsulationDetails();
                currentItem.GlassType2 = new InsulationDetails();

                UpdateCurrentTotal();
                ResetAllControls();
            }

        }

        private void ResetAllControls()
        {
            //Glass Type and thickness
            cmbGlassType.SelectedIndex = -1;
            cmbThickness.SelectedIndex = -1;

            cbLogo.IsChecked = false;
            cbIsTempered.IsChecked = false;

            // Dimension
            txtTotalSqFt.Text = "0";
            txtGlassHeight.Text = "0";
            txtGlassWidth.Text = "0";
            txtGlassWidth.Text = "0";

            // Custom Shape
            cmbShape.SelectedIndex = -1;
            txtShapeHeight.Text = "0";
            txtShapeWidth.Text = "0";

            // Straight Polish
            cbIsStraightPolish.IsChecked = false;
            txtStraightPolishTotalInches.Text = "0";
            txtStraightPolishLongSide.Text = "0";
            txtStraightPolishShortSide.Text = "0";

            // Custom Polish
            cbCustomShapePolish.IsChecked = false;
            txtCustomShapePolishSize.Text = "0";

            // Miter
            cbIsMiter.IsChecked = false;
            txtMiterTotalInches.Text = "0";
            txtMiterLongSide.Text = "0";
            txtMiterShortSide.Text = "0";

            // Misc
            cbNotches.IsChecked = false;
            cbPatches.IsChecked = false;
            cbHoles.IsChecked = false;
            cbHinges.IsChecked = false;

            //Insulation
            ResetInsulation();

            // Total labels
            lblCutoutTotal.Content = "$ 0.00";

        }

        private void ResetInsulation()
        {
            cmbGlassType1.SelectedIndex = -1;
            cmbThickness1.SelectedIndex = -1;
            cmbTemp1.SelectedIndex = -1;
            txtSqFt1.Text = "0";
            txtGlassType1Total.Text = "0";

            cmbGlassType2.SelectedIndex = -1;
            cmbThickness2.SelectedIndex = -1;
            cmbTemp2.SelectedIndex = -1;
            txtSqFt2.Text = "0";
            txtGlassType2Total.Text = "0";

            lblMaterialCost.Content = "$ 0.00";
            lblInsulationTier.Content = "$ 0.00";
            lblInsulationTierTotal.Content = "$ 0.00";
            lblInsulationTotal.Content = "$ 0.00";
        }

        private void btnResetItem_Click(object sender, RoutedEventArgs e)
        {
            if (currentItem != null)
            {
                var result = Helper.ShowQuestionMessageBox("Are you sure to discard current changes?");
                if (result == MessageBoxResult.Yes)
                {
                    ResetAllControls();
                }
            }
            else
            {
                ResetAllControls();
            }
        }

        private void txtGlassHeightFraction_GotFocus(object sender, RoutedEventArgs e)
        {
            txtGlassHeightFraction.Text = string.Empty;
        }

        private void txtGlassHeightFraction_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentItem == null)
                return;

            txtGlassHeightFraction.Text = string.IsNullOrEmpty(txtGlassHeightFraction.Text) ? "x/y" : txtGlassHeightFraction.Text;
            currentItem.GlassHeightFraction = txtGlassHeightFraction.Text;
        }

        private void txtGlassWidthFraction_LostFocus(object sender, RoutedEventArgs e)
        {
            if (currentItem == null)
                return;
            txtGlassWidthFraction.Text = string.IsNullOrEmpty(txtGlassWidthFraction.Text) ? "x/y" : txtGlassWidthFraction.Text;
            currentItem.GlassWidthFraction = txtGlassWidthFraction.Text;
        }

        private void txtGlassWidthFraction_GotFocus(object sender, RoutedEventArgs e)
        {
            txtGlassWidthFraction.Text = string.Empty;
        }

        private void cbInsulation_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsInsulation = true;
            UpdateCurrentTotal();
        }

        private void cbInsulation_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsInsulation = false;
            ResetInsulation();
            UpdateCurrentTotal();
        }

        private void cbCutout_Unchecked(object sender, RoutedEventArgs e)
        {
            currentItem.IsCutout = false;
            allCutoutData.Clear();
            lblCutoutTotal.Content = "$ 0.00";
            UpdateCurrentTotal();
        }

        private void cbCutout_Checked(object sender, RoutedEventArgs e)
        {
            currentItem.IsCutout = true;
            UpdateCurrentTotal();
        }

        private void dgCutoutDetails_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //if (e.EditAction == DataGridEditAction.Commit)
            //{
            //    //
            //    // custom commit action:
            //    // tab specific behavior for editing workflow.
            //    // moves to the next row and opens the second cell for edit
            //    // if the next row is the NewItemPlaceholder
            //    //

            //    if (e.Row.Item == dgCutoutDetails.Items[dgCutoutDetails.Items.Count - 2])
            //    {
            //        // set the new cell to be the last row and the second column
            //        int colIndex = 0;
            //        var rowToSelect = dgCutoutDetails.Items[dgCutoutDetails.Items.Count - 1];
            //        var colToSelect = dgCutoutDetails.Columns[colIndex];
            //        int rowIndex = dgCutoutDetails.Items.IndexOf(rowToSelect);

            //        // select the new cell
            //        dgCutoutDetails.SelectedCells.Clear();
            //        dgCutoutDetails.SelectedCells.Add(
            //            new DataGridCellInfo(rowToSelect, colToSelect));

            //        this.Dispatcher.BeginInvoke(new System.Windows.Threading.DispatcherOperationCallback((param) =>
            //        {
            //            // get the new cell, set focus, then open for edit
            //            var cell = DataGridHelper.GetCell(dgCutoutDetails, rowIndex, colIndex);
            //            cell.Focus();

            //            dgCutoutDetails.BeginEdit();
            //            return null;
            //        }), System.Windows.Threading.DispatcherPriority.Background, new object[] { null });
            //    }
            //}
        }

        private void txtShapeHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumberOnly(txtShapeHeight))
            {
            }
        }

        private void txtShapeWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (IsNumberOnly(txtShapeWidth))
            {
            }
        }
    }
}
