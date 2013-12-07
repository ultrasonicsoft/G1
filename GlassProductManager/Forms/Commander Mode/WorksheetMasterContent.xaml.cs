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
using Ultrasonicsoft.Products;

namespace GlassProductManager
{
    /// <summary>
    /// Interaction logic for SettingsContent.xaml
    /// </summary>
    public partial class WorksheetMasterContent : UserControl
    {
        private ListCollectionView m_WorksheetListForSearch;

        public WorksheetMasterContent()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (m_WorksheetListForSearch == null || txtSearch.Text == "Search")
                    return;
                if (txtSearch.Text != "")
                {
                    if (m_WorksheetListForSearch.CanFilter)
                    {
                        m_WorksheetListForSearch.Filter =
                                new Predicate<object>(ContainsIt);

                        FilterIt();
                    }
                    else
                    {
                        m_WorksheetListForSearch.Filter = null;
                    }
                }
                else
                {
                    m_WorksheetListForSearch.Filter = null;
                    FilterIt();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void txtSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = string.Empty;
            }
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearch.Text == string.Empty)
            {
                txtSearch.Text = "Search";
            }
        }

        public bool ContainsIt(object value)
        {
            try
            {
                WorksheetEntity currentRow = value as WorksheetEntity;

                if (dgWorksheetDetails.Columns.Count > 1)
                {
                    if (currentRow != null)
                    {
                        if (IsSearchCriteriaMatched(currentRow)) return true;
                    }
                }
                else
                {
                    if (IsSearchCriteriaMatched(currentRow)) return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            return false;
        }

        private bool IsSearchCriteriaMatched(WorksheetEntity currentRow)
        {
            return currentRow.WorksheetNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                        currentRow.CreatedOn.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.FullName.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||

                                    currentRow.DeliveryDate.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.TotalQuantity.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.QuoteNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Progress.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower());
        }

        public void FilterIt()
        {
            try
            {
                dgWorksheetDetails.ItemsSource = null;
                ObservableCollection<WorksheetEntity> fileList = new ObservableCollection<WorksheetEntity>();

                foreach (WorksheetEntity row in m_WorksheetListForSearch)
                {
                    fileList.Add(row);
                }
                dgWorksheetDetails.ItemsSource = fileList;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                FillWorksheetDetails();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillWorksheetDetails()
        {
            var result = BusinessLogic.GetWorksheetMasterData();
            dgWorksheetDetails.ItemsSource = result;
            m_WorksheetListForSearch = new ListCollectionView(result);
        }

        private void dgWorksheetDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenWorksheet();
        }

        private void OpenWorksheet()
        {
            Dashboard parent = Window.GetWindow(this) as Dashboard;

            WorksheetEntity entity = dgWorksheetDetails.SelectedItem as WorksheetEntity;

            if (entity == null)
            {
                return;
            }
            if (parent != null)
            {
                DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                DashboardHelper.ChangeDashboardSelection(parent, sideMenu.btnWorksheet);
                WorksheetContent newQuote = new WorksheetContent(true, entity.QuoteNumber);
                parent.ucMainContent.ShowPage(newQuote);
            }
        }

        private void btnOpenWorksheet_Click(object sender, RoutedEventArgs e)
        {
            OpenWorksheet();
        }

        private void btnDeleteWorksheet_Click(object sender, RoutedEventArgs e)
        {
            DeleteWorksheet();
        }

        private void DeleteWorksheet()
        {
            WorksheetEntity entity = dgWorksheetDetails.SelectedItem as WorksheetEntity;
            if (entity == null)
            {
                Helper.ShowErrorMessageBox("Select Worksheet for deletion");
                return;
            }

            bool isWorksheetPresent = BusinessLogic.IsWorksheetPresent(entity.QuoteNumber);
            if (isWorksheetPresent)
            {
                BusinessLogic.DeleteWorksheet(entity.QuoteNumber);
                Helper.ShowInformationMessageBox("Worksheet is delete successfully!");
                FillWorksheetDetails();
            }
        }

    }
}
