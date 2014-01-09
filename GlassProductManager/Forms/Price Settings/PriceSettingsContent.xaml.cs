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
        enum UserAction
        {
            None,
            AddNew,
            Edit,
            Cancel
        }

        private UserAction _glassTypeAction = UserAction.Cancel;
        private UserAction _thicknessAction = UserAction.Cancel;
        private UserAction _holeAction = UserAction.Cancel;

        public PriceSettingsContent()
        {
            InitializeComponent();

            FillGlassTypes();

            FillInsulationCost();

            FillMiscRates();

            FillThicknesses();
            SetGlassDetailsControlsStatus(true);
        }

        private void FillThicknesses()
        {
            try
            {
                var result = BusinessLogic.GetThicknesses();
                cmbThicknessHoleRates.DisplayMemberPath = ColumnNames.THICKNESS;
                cmbThicknessHoleRates.SelectedValuePath = ColumnNames.THICKNESSID;
                cmbThicknessHoleRates.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillMiscRates()
        {
            try
            {
                var result = BusinessLogic.GetMiscRates();
                if (result == null)
                    return;

                txtNotchRate.Text = result.NotchRate.ToString();
                txtHingeRate.Text = result.HingeRate.ToString();
                txtPatchRate.Text = result.PatchRate.ToString();
                txtMinimumTotalSqft.Text = result.MinimumTotalSqft.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillInsulationCost()
        {
            try
            {
                var result = BusinessLogic.GetAllInsulationCost();
                if (result == null)
                    return;
                txtTier1.Text = result.TierSqFt1.ToString();
                txtTierCost1.Text = result.TierCost1.ToString();
                txtTier2.Text = result.TierSqFt2.ToString();
                txtTierCost2.Text = result.TierCost2.ToString();
                txtTierCostMax.Text = result.TierCost3.ToString();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void SetGlassDetailsControlsStatus(bool status)
        {
            txtCutoutSqFtRate.IsReadOnly = status;
            txtTemperedRate.IsReadOnly = status;
            txtPolishStraightRate.IsReadOnly = status;
            txtPolishShapeRate.IsReadOnly = status;
            txtMiterRate.IsReadOnly = status;
        }

        private void FillGlassTypes()
        {
            try
            {
                var result = BusinessLogic.GetAllGlassTypes();
                cmbGlassType.DisplayMemberPath = ColumnNames.GLASS_TYPE;
                cmbGlassType.SelectedValuePath = ColumnNames.ID;
                cmbGlassType.ItemsSource = result.DefaultView;

                cmbGlassTypeManageThickness.DisplayMemberPath = ColumnNames.GLASS_TYPE;
                cmbGlassTypeManageThickness.SelectedValuePath = ColumnNames.ID;
                cmbGlassTypeManageThickness.ItemsSource = result.DefaultView;

                cmbGlassTypeManageGlassType.DisplayMemberPath = ColumnNames.GLASS_TYPE;
                cmbGlassTypeManageGlassType.SelectedValuePath = ColumnNames.ID;
                cmbGlassTypeManageGlassType.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbGlassType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbGlassType.SelectedValue == null)
                    return;
                string glassID = cmbGlassType.SelectedValue.ToString();

                var result = BusinessLogic.GetThicknessByGlassID(glassID);
                cmbThickness.DisplayMemberPath = ColumnNames.THICKNESS;
                cmbThickness.SelectedValuePath = ColumnNames.THICKNESSID;
                cmbThickness.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbThickness.SelectedValue == null)
                    return;

                string thicknessID = cmbThickness.SelectedValue.ToString();
                if (string.IsNullOrEmpty(thicknessID))
                    return;

                if (cmbGlassType.SelectedValue == null)
                    return;

                int _glassTypeID = int.Parse(cmbGlassType.SelectedValue.ToString());

                int _thicknessID = int.Parse(thicknessID);

                var result = BusinessLogic.GetRatesByGlassTypeAndThickness(_glassTypeID, _thicknessID);
                if (result == null || result.Tables.Count == 0 || result.Tables[0].Rows.Count == 0)
                    return;


                txtCutoutSqFtRate.Text = result.Tables[0].Rows[0][ColumnNames.CUTSQFT].ToString();
                txtTemperedRate.Text = result.Tables[0].Rows[0][ColumnNames.TEMPEREDSQFT].ToString();
                txtPolishStraightRate.Text = result.Tables[0].Rows[0][ColumnNames.POLISHSTRAIGHT].ToString();
                txtPolishShapeRate.Text = result.Tables[0].Rows[0][ColumnNames.POLISHSHAPE].ToString();
                txtMiterRate.Text = result.Tables[0].Rows[0][ColumnNames.MITER_RATE].ToString();
                //_notchRate = result.Tables[0].Rows[0][ColumnNames.NOTCH_RATE].ToString();
                //_hingeRate = result.Tables[0].Rows[0][ColumnNames.HINGE_RATE].ToString();
                //_patchRate = result.Tables[0].Rows[0][ColumnNames.PATCH_RATE].ToString();
                //_holeRate = result.Tables[0].Rows[0][ColumnNames.HOLE_RATE].ToString();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnEditGlassDetails_Click(object sender, RoutedEventArgs e)
        {

            SetGlassDetailsControlsStatus(false);
            btnSaveGlassDetails.IsEnabled = true;
            btnCancelEditGlassDetails.IsEnabled = true;
            btnEditGlassDetails.IsEnabled = false;
        }

        private void btnCancelEditGlassDetails_Click(object sender, RoutedEventArgs e)
        {
            cmbGlassType.SelectedIndex = 0;
            cmbThickness.SelectedIndex = 0;
            SetGlassDetailsControlsStatus(true);

            btnSaveGlassDetails.IsEnabled = false;
            btnCancelEditGlassDetails.IsEnabled = false;
            btnEditGlassDetails.IsEnabled = true;

        }

        private void btnSaveGlassDetails_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbGlassType.SelectedValue == null)
                    return;
                if (cmbThickness.SelectedValue == null)
                    return;

                GlassRateEntity updatedRate = new GlassRateEntity();
                updatedRate.GlassID = int.Parse(cmbGlassType.SelectedValue.ToString());
                updatedRate.ThicknessID = int.Parse(cmbThickness.SelectedValue.ToString());
                updatedRate.CutoutSqFtRate = double.Parse(txtCutoutSqFtRate.Text);
                updatedRate.TemperedRate = double.Parse(txtTemperedRate.Text);
                updatedRate.PolishStraightRate = double.Parse(txtPolishStraightRate.Text);
                updatedRate.PolishShapeRate = double.Parse(txtPolishShapeRate.Text);
                updatedRate.MiterRate = double.Parse(txtMiterRate.Text);

                if (BusinessLogic.UpdateGlassRate(updatedRate))
                {
                    Helper.ShowInformationMessageBox("Rates are updated for selected items successfully!");
                }
                else
                {
                    Helper.ShowErrorMessageBox("Save operation failed. Please contact your vendor!");
                }

                btnSaveGlassDetails.IsEnabled = false;
                btnCancelEditGlassDetails.IsEnabled = false;
                btnEditGlassDetails.IsEnabled = true;

                txtCutoutSqFtRate.IsReadOnly = true;
                txtTemperedRate.IsReadOnly = true;
                txtPolishStraightRate.IsReadOnly = true;
                txtPolishShapeRate.IsReadOnly = true;
                txtMiterRate.IsReadOnly = true;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbGlassTypeManageThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbGlassTypeManageThickness.SelectedValue == null)
                    return;
                string glassID = cmbGlassTypeManageThickness.SelectedValue.ToString();

                var result = BusinessLogic.GetThicknessByGlassID(glassID);
                lbThickness.DisplayMemberPath = ColumnNames.THICKNESS;
                lbThickness.SelectedValuePath = ColumnNames.THICKNESSID;
                lbThickness.ItemsSource = result.DefaultView;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void lbThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lbThickness.SelectedItem == null)
                    return;
                txtThicknessManage.Text = (lbThickness.SelectedItem as System.Data.DataRowView)[1].ToString();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnAddNewGlassType_Click(object sender, RoutedEventArgs e)
        {
            _glassTypeAction = UserAction.AddNew;
            btnSaveNewGlassType.IsEnabled = true;
            txtGlassTypeName.IsReadOnly = false;

            btnAddNewGlassType.IsEnabled = false;
            btnEditGlassType.IsEnabled = false;
            btnSaveNewGlassType.IsEnabled = true;
            btnDeleteGlassType.IsEnabled = false;
            btnCancelGlassType.IsEnabled = true;
            txtGlassTypeName.Focus();
        }

        private void btnEditGlassType_Click(object sender, RoutedEventArgs e)
        {
            _glassTypeAction = UserAction.Edit;
            txtGlassTypeName.IsReadOnly = false;

            btnAddNewGlassType.IsEnabled = false;
            btnEditGlassType.IsEnabled = false;
            btnSaveNewGlassType.IsEnabled = true;
            btnDeleteGlassType.IsEnabled = false;
            btnCancelGlassType.IsEnabled = true;
            txtGlassTypeName.Focus();
        }

        private void btnDeleteGlassType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbGlassTypeManageGlassType.SelectedValue == null)
                {
                    Helper.ShowErrorMessageBox("Select Glass Type");
                    return;
                }
                var result = Helper.ShowQuestionMessageBox("Are you sure to delete glass type with all its data?");
                if (result == MessageBoxResult.Yes)
                {
                    int glassID = int.Parse(cmbGlassTypeManageGlassType.SelectedValue.ToString());
                    if (BusinessLogic.DeleteGlassType(glassID))
                    {
                        Helper.ShowInformationMessageBox("Glass type deleted successfully!");
                        FillGlassTypes();
                        cmbGlassTypeManageGlassType.SelectedIndex = 0;
                    }
                    else
                    {
                        Helper.ShowInformationMessageBox("Error occured during deleting glass type. Please contact your vendor.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbGlassTypeManageGlassType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                txtGlassTypeName.Text = (cmbGlassTypeManageGlassType.SelectedItem as System.Data.DataRowView)[1].ToString().ToLower();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnSaveNewGlassType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_glassTypeAction == UserAction.AddNew && false == IsExistingGlassType())
                {
                    if (BusinessLogic.CreateNewGlassType(txtGlassTypeName.Text))
                    {
                        Helper.ShowInformationMessageBox("New Glass Type saved successfully!");
                        FillGlassTypes();
                        cmbGlassTypeManageGlassType.SelectedIndex = -1;
                        txtGlassTypeName.Text = string.Empty;
                    }
                    else
                    {
                        Helper.ShowErrorMessageBox("Error occured during saving glass type. Please contact your vendor");
                    }
                }
                else if (_glassTypeAction == UserAction.Edit)
                {
                    int glassTypeID = int.Parse((cmbGlassTypeManageGlassType.SelectedItem as System.Data.DataRowView)[0].ToString().ToLower());
                    if (BusinessLogic.UpdateGlassType(txtGlassTypeName.Text, glassTypeID))
                    {
                        Helper.ShowInformationMessageBox("Glass Type updated successfully!");
                        FillGlassTypes();
                        cmbGlassTypeManageGlassType.SelectedIndex = -1;
                        txtGlassTypeName.Text = string.Empty;
                    }
                    else
                    {
                        Helper.ShowErrorMessageBox("Error occured during saving glass type. Please contact your vendor");
                    }
                }

                btnAddNewGlassType.IsEnabled = true;
                btnEditGlassType.IsEnabled = true;
                btnSaveNewGlassType.IsEnabled = false;
                btnDeleteGlassType.IsEnabled = true;
                txtGlassTypeName.IsReadOnly = true;
                btnCancelGlassType.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
        private void btnCancelGlassType_Click(object sender, RoutedEventArgs e)
        {
            _glassTypeAction = UserAction.None;
            btnAddNewGlassType.IsEnabled = true;
            btnEditGlassType.IsEnabled = true;
            btnSaveNewGlassType.IsEnabled = false;
            btnDeleteGlassType.IsEnabled = true;
            btnCancelGlassType.IsEnabled = false;

        }

        private bool IsExistingGlassType()
        {
            bool result = false;
            try
            {
                foreach (var item in cmbGlassTypeManageGlassType.Items)
                {
                    if (txtGlassTypeName.Text.ToLower().Equals((item as System.Data.DataRowView)[1].ToString().ToLower()))
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        private void btnAddNewThickness_Click(object sender, RoutedEventArgs e)
        {
            _thicknessAction = UserAction.AddNew;

            btnAddNewThickness.IsEnabled = false;
            btnSaveThickness.IsEnabled = true;
            btnEditThickness.IsEnabled = false;
            btnCancelEditThickness.IsEnabled = true;
            txtThicknessManage.IsReadOnly = false;
            txtThicknessManage.Focus();
        }

        private void btnEditThickness_Click(object sender, RoutedEventArgs e)
        {
            if (lbThickness.Items.Count == 0)
            {
                Helper.ShowErrorMessageBox("There are no thickness to edit. Please craete new!");
                return;
            }
            _thicknessAction = UserAction.Edit;
            btnSaveThickness.IsEnabled = true;

            btnAddNewThickness.IsEnabled = false;
            btnSaveThickness.IsEnabled = true;
            btnEditThickness.IsEnabled = false;
            btnCancelEditThickness.IsEnabled = true;
            txtThicknessManage.IsReadOnly = false;
            txtThicknessManage.Focus();
        }

        private void btnCancelEditThickness_Click(object sender, RoutedEventArgs e)
        {
            _thicknessAction = UserAction.Cancel;

            btnAddNewThickness.IsEnabled = true;
            btnSaveThickness.IsEnabled = false;
            btnEditThickness.IsEnabled = true;
            btnCancelEditThickness.IsEnabled = false;
            txtThicknessManage.IsReadOnly = true;
            txtThicknessManage.Text = string.Empty;
        }

        private void btnSaveThickness_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_thicknessAction == UserAction.AddNew)
                {
                    if (true == IsExistingThickness())
                    {
                        Helper.ShowErrorMessageBox("This glass thickness already present in the system.");
                        return;
                    }
                    else
                    {
                        int glassID = int.Parse(cmbGlassTypeManageThickness.SelectedValue.ToString());
                        string newThickness = txtThicknessManage.Text;
                        if (BusinessLogic.CreateNewThickness(glassID, newThickness))
                        {
                            Helper.ShowInformationMessageBox("New thickness saved successfully!");
                            cmbGlassTypeManageThickness_SelectionChanged(null, null);
                            txtThicknessManage.Text = string.Empty;
                        }
                        else
                        {
                            Helper.ShowErrorMessageBox("Error occured during saving new thickness. Please contact your vendor");
                        }
                    }
                }
                else if (_thicknessAction == UserAction.Edit)
                {
                    int thicknessID = int.Parse(lbThickness.SelectedValue.ToString());
                    string newThickness = txtThicknessManage.Text;
                    if (BusinessLogic.UpdateThickness(thicknessID, newThickness))
                    {
                        Helper.ShowInformationMessageBox("Upaated Thickness saved successfully!");
                        cmbGlassTypeManageThickness_SelectionChanged(null, null);
                        txtThicknessManage.Text = string.Empty;
                    }
                    else
                    {
                        Helper.ShowErrorMessageBox("Error occured during saving thickness. Please contact your vendor");
                    }
                }

                btnAddNewThickness.IsEnabled = true;
                btnSaveThickness.IsEnabled = false;
                btnEditThickness.IsEnabled = true;
                btnCancelEditThickness.IsEnabled = false;
                txtThicknessManage.IsReadOnly = true;
                txtThicknessManage.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private bool IsExistingThickness()
        {
            bool result = false;
            try
            {
                foreach (var item in lbThickness.Items)
                {
                    if (txtThicknessManage.Text.ToLower().Equals((item as System.Data.DataRowView)[1].ToString().ToLower()))
                    {
                        result = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return result;
        }

        private void btnSaveInsulation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                InsulationCostEntity insualtionRate = new InsulationCostEntity();
                insualtionRate.TierSqFt1 = int.Parse(txtTier1.Text);
                insualtionRate.TierCost1 = double.Parse(txtTierCost1.Text);
                insualtionRate.TierSqFt2 = int.Parse(txtTier2.Text);
                insualtionRate.TierCost2 = double.Parse(txtTierCost2.Text);
                insualtionRate.TierCost3 = double.Parse(txtTierCostMax.Text);

                if (BusinessLogic.UpdateInsulationCost(insualtionRate))
                {
                    Helper.ShowInformationMessageBox("Insulation cost updated successfully!");
                }
                else
                {
                    Helper.ShowErrorMessageBox("Error while saving insulation cost. Kindly contact your vendor.");
                }
                txtTier1.IsReadOnly = true;
                txtTierCost1.IsReadOnly = true;
                txtTier2.IsReadOnly = true;
                txtTierCost2.IsReadOnly = true;
                txtTierCostMax.IsReadOnly = true;

                btnEditInsulation.IsEnabled = true;
                btnSaveInsulation.IsEnabled = false;
                btnCancelInsulation.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnSaveMiscRate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MiscRateEntity miscRate = new MiscRateEntity();
                miscRate.NotchRate = double.Parse(txtNotchRate.Text);
                miscRate.HingeRate = double.Parse(txtHingeRate.Text);
                miscRate.PatchRate = double.Parse(txtPatchRate.Text);
                miscRate.MinimumTotalSqft = double.Parse(txtMinimumTotalSqft.Text);

                if (BusinessLogic.UpdateMiscRate(miscRate))
                {
                    Helper.ShowInformationMessageBox("Misc Rates updated successfully!");
                }
                else
                {
                    Helper.ShowErrorMessageBox("Error while saving Misc Rates. Kindly contact your vendor.");
                }
                txtNotchRate.IsReadOnly = true;
                txtHingeRate.IsReadOnly = true;
                txtPatchRate.IsReadOnly = true;
                txtMinimumTotalSqft.IsReadOnly = true;

                btnEditMiscRate.IsEnabled = true;
                btnSaveMiscRate.IsEnabled = false;
                btnCancelMiscRate.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void cmbThicknessHoleRates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string temp = (cmbThicknessHoleRates.SelectedItem as System.Data.DataRowView)[0].ToString();
                if (string.IsNullOrEmpty(temp))
                    return;
                int thicknessID = int.Parse(temp);
                txtHoleRate.Text = BusinessLogic.GetHoleRateByThicknessID(thicknessID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnEditHoleRate_Click(object sender, RoutedEventArgs e)
        {
            txtHoleRate.IsReadOnly = false;

            btnEditHoleRate.IsEnabled = false;
            btnSaveHoleRate.IsEnabled = true;
            btnCancelHoleRate.IsEnabled = true;
        }

        private void btnCancelHoleRate_Click(object sender, RoutedEventArgs e)
        {
            txtHoleRate.IsReadOnly = true;

            btnEditHoleRate.IsEnabled = true;
            btnSaveHoleRate.IsEnabled = false;
            btnCancelHoleRate.IsEnabled = false;

        }

        private void btnSaveHoleRate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string temp = (cmbThicknessHoleRates.SelectedItem as System.Data.DataRowView)[0].ToString();
                if (string.IsNullOrEmpty(temp))
                    return;
                int thicknessID = int.Parse(temp);

                if (string.IsNullOrEmpty(txtHoleRate.Text))
                {
                    Helper.ShowErrorMessageBox("Please enter Hole rates");
                    return;
                }
                double holeRate = double.Parse(txtHoleRate.Text);
                if (BusinessLogic.UpdateHoleRate(thicknessID, holeRate))
                {
                    Helper.ShowInformationMessageBox("Hole rates updated successfully!");
                }
                else
                {
                    Helper.ShowErrorMessageBox("Error occured during updating Hole rate. Kindly contact your vendor");
                }
                txtHoleRate.IsReadOnly = true;

                btnEditHoleRate.IsEnabled = true;
                btnSaveHoleRate.IsEnabled = false;
                btnCancelHoleRate.IsEnabled = false;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void txtCutoutSqFtRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtCutoutSqFtRate.IsReadOnly == false)
            {
                btnSaveGlassDetails.IsEnabled = Helper.IsValidCurrency(txtCutoutSqFtRate);
            }
        }

        private void txtTemperedRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTemperedRate.IsReadOnly == false)
            {
                btnSaveGlassDetails.IsEnabled = Helper.IsValidCurrency(txtTemperedRate);
            }
        }

        private void txtPolishStraightRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPolishStraightRate.IsReadOnly == false)
            {
                btnSaveGlassDetails.IsEnabled = Helper.IsValidCurrency(txtPolishStraightRate);
            }
        }

        private void txtPolishShapeRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtPolishShapeRate.IsReadOnly == false)
            {
                btnSaveGlassDetails.IsEnabled = Helper.IsValidCurrency(txtPolishShapeRate);
            }
        }

        private void txtMiterRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtMiterRate.IsReadOnly == false)
            {
                btnSaveGlassDetails.IsEnabled = Helper.IsValidCurrency(txtMiterRate);
            }
        }

        private void txtTier1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditInsulation.IsEnabled == false)
            {
                btnSaveInsulation.IsEnabled = Helper.IsNumberOnly(txtTier1);
            }
        }

        private void txtTier2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditInsulation.IsEnabled == false)
            {
                btnSaveInsulation.IsEnabled = Helper.IsNumberOnly(txtTier2);
            }
        }

        private void txtTierCostMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditInsulation.IsEnabled == false)
            {
                btnSaveInsulation.IsEnabled = Helper.IsValidCurrency(txtTierCostMax);
            }
        }

        private void txtTierCost1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditInsulation.IsEnabled == false)
            {
                btnSaveInsulation.IsEnabled = Helper.IsValidCurrency(txtTierCost1);
            }
        }

        private void txtTierCost2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditInsulation.IsEnabled == false)
            {
                btnSaveInsulation.IsEnabled = Helper.IsValidCurrency(txtTierCost2);
            }
        }

        private void txtNotchRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditMiscRate.IsEnabled == false)
            {
                btnSaveMiscRate.IsEnabled = Helper.IsValidCurrency(txtNotchRate);
            }
        }

        private void txtHingeRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditMiscRate.IsEnabled == false)
            {
                btnSaveMiscRate.IsEnabled = Helper.IsValidCurrency(txtHingeRate);
            }
        }

        private void txtPatchRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditMiscRate.IsEnabled == false)
            {
                btnSaveMiscRate.IsEnabled = Helper.IsValidCurrency(txtPatchRate);
            }
        }

        private void txtMinimumTotalSqft_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditMiscRate.IsEnabled == false)
            {
                btnSaveMiscRate.IsEnabled = Helper.IsValidCurrency(txtMinimumTotalSqft);
            }
        }

        private void txtHoleRate_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnEditMiscRate.IsEnabled == false)
            {
                btnSaveHoleRate.IsEnabled = Helper.IsValidCurrency(txtHoleRate);
            }
        }

        private void btnEditInsulation_Click(object sender, RoutedEventArgs e)
        {
            txtTier1.IsReadOnly = false;
            txtTierCost1.IsReadOnly = false;
            txtTier2.IsReadOnly = false;
            txtTierCost2.IsReadOnly = false;
            txtTierCostMax.IsReadOnly = false;

            btnEditInsulation.IsEnabled = false;
            btnSaveInsulation.IsEnabled = true;
            btnCancelInsulation.IsEnabled = true;
        }



        private void btnCancelInsulation_Click(object sender, RoutedEventArgs e)
        {
            txtTier1.IsReadOnly = true;
            txtTierCost1.IsReadOnly = true;
            txtTier2.IsReadOnly = true;
            txtTierCost2.IsReadOnly = true;
            txtTierCostMax.IsReadOnly = true;

            btnEditInsulation.IsEnabled = true;
            btnSaveInsulation.IsEnabled = false;
            btnCancelInsulation.IsEnabled = false;
        }

        private void btnEditMiscRate_Click(object sender, RoutedEventArgs e)
        {
            txtNotchRate.IsReadOnly = false;
            txtHingeRate.IsReadOnly = false;
            txtPatchRate.IsReadOnly = false;
            txtMinimumTotalSqft.IsReadOnly = false;

            btnEditMiscRate.IsEnabled = false;
            btnSaveMiscRate.IsEnabled = true;
            btnCancelMiscRate.IsEnabled = true;
        }

        private void btnCancelMiscRate_Click(object sender, RoutedEventArgs e)
        {
            txtNotchRate.IsReadOnly = true;
            txtHingeRate.IsReadOnly = true;
            txtPatchRate.IsReadOnly = true;
            txtMinimumTotalSqft.IsReadOnly = true;

            btnEditMiscRate.IsEnabled = true;
            btnSaveMiscRate.IsEnabled = false;
            btnCancelMiscRate.IsEnabled = false;

            FillMiscRates();
        }

    }
}
