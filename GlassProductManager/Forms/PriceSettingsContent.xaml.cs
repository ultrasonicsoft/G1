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
            var result = BusinessLogic.GetThicknesses();
            cmbThicknessHoleRates.DisplayMemberPath = ColumnNames.THICKNESS;
            cmbThicknessHoleRates.SelectedValuePath = ColumnNames.THICKNESSID;
            cmbThicknessHoleRates.ItemsSource = result.DefaultView;
        }

        private void FillMiscRates()
        {
            var result = BusinessLogic.GetMiscRates();
            if (result == null)
                return;

            txtNotchRate.Text = result.NotchRate.ToString();
            txtHingeRate.Text = result.HingeRate.ToString();
            txtPatchRate.Text = result.PatchRate.ToString();

        }

        private void FillInsulationCost()
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
            if (cmbGlassType.SelectedValue == null)
                return;
            string glassID = cmbGlassType.SelectedValue.ToString();

            var result = BusinessLogic.GetThicknessByGlassID(glassID);
            cmbThickness.DisplayMemberPath = ColumnNames.THICKNESS;
            cmbThickness.SelectedValuePath = ColumnNames.THICKNESSID;
            cmbThickness.ItemsSource = result.DefaultView;

        }

        private void cmbThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void btnEditGlassDetails_Click(object sender, RoutedEventArgs e)
        {
            SetGlassDetailsControlsStatus(false);
        }

        private void btnCancelEditGlassDetails_Click(object sender, RoutedEventArgs e)
        {
            cmbGlassType.SelectedIndex = 0;
            cmbThickness.SelectedIndex = 0;
            SetGlassDetailsControlsStatus(true);
        }

        private void btnSaveGlassDetails_Click(object sender, RoutedEventArgs e)
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
        }

        private void cmbGlassTypeManageThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbGlassTypeManageThickness.SelectedValue == null)
                return;
            string glassID = cmbGlassTypeManageThickness.SelectedValue.ToString();

            var result = BusinessLogic.GetThicknessByGlassID(glassID);
            lbThickness.DisplayMemberPath = ColumnNames.THICKNESS;
            lbThickness.SelectedValuePath = ColumnNames.THICKNESSID;
            lbThickness.ItemsSource = result.DefaultView;
        }

        private void lbThickness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtThicknessManage.Text = (lbThickness.SelectedItem as System.Data.DataRowView)[1].ToString();
        }

        private void btnAddNewGlassType_Click(object sender, RoutedEventArgs e)
        {
            _glassTypeAction = UserAction.AddNew;
            btnSaveNewGlassType.IsEnabled = true;
        }

        private void btnEditGlassType_Click(object sender, RoutedEventArgs e)
        {
            _glassTypeAction = UserAction.Edit ;
            btnSaveNewGlassType.IsEnabled = true;
        }

        private void btnDeleteGlassType_Click(object sender, RoutedEventArgs e)
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

        private void cmbGlassTypeManageGlassType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txtGlassTypeName.Text = (cmbGlassTypeManageGlassType.SelectedItem as  System.Data.DataRowView)[1].ToString().ToLower();
        }

        private void btnSaveNewGlassType_Click(object sender, RoutedEventArgs e)
        {
            if (_glassTypeAction== UserAction.AddNew && false == IsExistingGlassType())
            {
                if (BusinessLogic.CreateNewGlassType(txtGlassTypeName.Text))
                {
                    Helper.ShowInformationMessageBox("New Glass Type saved successfully!");
                    FillGlassTypes();
                    txtGlassTypeName.Text = string.Empty;
                }
                else
                {
                    Helper.ShowErrorMessageBox("Error occured during saving glass type. Please contact your vendor");
                }
            }
            else
            {
                Helper.ShowErrorMessageBox("This Glass type already present in the system.");
            }
        }

        private bool IsExistingGlassType()
        {
            bool result = false;
            foreach (var item in cmbGlassTypeManageGlassType.Items)
            {
                if (txtGlassTypeName.Text.ToLower().Equals((item as System.Data.DataRowView)[1].ToString().ToLower()))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void btnAddNewThickness_Click(object sender, RoutedEventArgs e)
        {
            _thicknessAction = UserAction.AddNew;
            btnSaveThickness.IsEnabled = true;
        }

        private void btnEditThickness_Click(object sender, RoutedEventArgs e)
        {
            _thicknessAction = UserAction.Edit;
            btnSaveThickness.IsEnabled = true;
        }

        private void btnCancelEditThickness_Click(object sender, RoutedEventArgs e)
        {
            _thicknessAction = UserAction.Cancel;
            btnSaveThickness.IsEnabled = false;
        }

        private void btnSaveThickness_Click(object sender, RoutedEventArgs e)
        {
            if (_thicknessAction == UserAction.AddNew && false == IsExistingThickness())
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
            else
            {
                Helper.ShowErrorMessageBox("This Glass type already present in the system.");
            }
        }

        private bool IsExistingThickness()
        {
            bool result = false;
            foreach (var item in lbThickness.Items)
            {
                if (txtThicknessManage.Text.ToLower().Equals((item as System.Data.DataRowView)[1].ToString().ToLower()))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private void btnSaveInsulation_Click(object sender, RoutedEventArgs e)
        {
            InsulationCostEntity insualtionRate = new InsulationCostEntity();
            insualtionRate.TierSqFt1 = int.Parse(txtTier1.Text);
            insualtionRate.TierCost1 = double.Parse(txtTierCost1.Text );
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
        }

        private void btnSaveMiscRate_Click(object sender, RoutedEventArgs e)
        {
            MiscRateEntity miscRate = new MiscRateEntity();
            miscRate.NotchRate = double.Parse(txtNotchRate.Text);
            miscRate.HingeRate= double.Parse(txtHingeRate.Text);
            miscRate.PatchRate = double.Parse(txtPatchRate.Text);

            if (BusinessLogic.UpdateMiscRate(miscRate))
            {
                Helper.ShowInformationMessageBox("Misc Rates updated successfully!");
            }
            else
            {
                Helper.ShowErrorMessageBox("Error while saving Misc Rates. Kindly contact your vendor.");
            }
        }

        private void cmbThicknessHoleRates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string temp = (cmbThicknessHoleRates.SelectedItem as System.Data.DataRowView)[0].ToString();
            if (string.IsNullOrEmpty(temp))
                return;
            int thicknessID = int.Parse(temp);
            txtHoleRate.Text = BusinessLogic.GetHoleRateByThicknessID(thicknessID);
        }

        private void btnEditHoleRate_Click(object sender, RoutedEventArgs e)
        {
            btnSaveHoleRate.IsEnabled = true;
        }

        private void btnCancelHoleRate_Click(object sender, RoutedEventArgs e)
        {
            btnSaveHoleRate.IsEnabled = false;

        }

        private void btnSaveHoleRate_Click(object sender, RoutedEventArgs e)
        {
            string temp = (cmbThicknessHoleRates.SelectedItem as System.Data.DataRowView)[0].ToString();
            if (string.IsNullOrEmpty(temp))
                return;
            int thicknessID = int.Parse(temp);

            if(string.IsNullOrEmpty(txtHoleRate.Text))
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
        }
    }
}
