﻿using System;
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
    public partial class SaleOrderMasterContent : UserControl
    {
        private ListCollectionView m_SaleListForSearch;

        public SaleOrderMasterContent()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (m_SaleListForSearch == null || txtSearch.Text == "Search")
                    return;
                if (txtSearch.Text != "")
                {
                    if (m_SaleListForSearch.CanFilter)
                    {
                        m_SaleListForSearch.Filter =
                                new Predicate<object>(ContainsIt);

                        FilterIt();
                    }
                    else
                    {
                        m_SaleListForSearch.Filter = null;
                    }
                }
                else
                {
                    m_SaleListForSearch.Filter = null;
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
                SaleOrderEntity currentRow = value as SaleOrderEntity;

                if (dgSaleOrderDetails.Columns.Count > 1)
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

        private bool IsSearchCriteriaMatched(SaleOrderEntity currentRow)
        {
            return currentRow.SaleOrderNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                        currentRow.QuoteNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.FullName.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||

                                    currentRow.RecordedDate.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Total.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.PaymentType.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.WorksheetNumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.CustomerPONumber.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower());
        }

        public void FilterIt()
        {
            try
            {
                dgSaleOrderDetails.ItemsSource = null;
                ObservableCollection<SaleOrderEntity> fileList = new ObservableCollection<SaleOrderEntity>();

                foreach (SaleOrderEntity row in m_SaleListForSearch)
                {
                    fileList.Add(row);
                }
                dgSaleOrderDetails.ItemsSource = fileList;
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
                FillSaleOrderDetails();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillSaleOrderDetails()
        {
            try
            {

                var result = BusinessLogic.GetSaleOrderMasterData();
                dgSaleOrderDetails.ItemsSource = result;
                m_SaleListForSearch = new ListCollectionView(result);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnOpenSaleOrder_Click(object sender, RoutedEventArgs e)
        {
            OpenSalesOrder();
        }

        private void dgSaleOrderDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenSalesOrder();
        }

        private void OpenSalesOrder()
        {
            try
            {
                Dashboard parent = Window.GetWindow(this) as Dashboard;

                SaleOrderEntity entity = dgSaleOrderDetails.SelectedItem as SaleOrderEntity;

                if (entity == null)
                {
                    return;
                }

                DashboardMenu sideMenu = parent.ucDashboardMenu.CurrentPage as DashboardMenu;
                if (sideMenu != null)
                {
                    sideMenu.IsIndirectCall = true;
                    sideMenu.btnSaleOrder.IsChecked = true;
                    sideMenu.IsIndirectCall = false;
                }

                SalesOrderContent newQuote = new SalesOrderContent(true, entity.QuoteNumber);
                parent.ucMainContent.ShowPage(newQuote);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void btnDeleteSalesOrder_Click(object sender, RoutedEventArgs e)
        {
            DeleteSalesOrder();
        }

        private void DeleteSalesOrder()
        {
            try
            {
                SaleOrderEntity entity = dgSaleOrderDetails.SelectedItem as SaleOrderEntity;
                if (entity == null)
                {
                    Helper.ShowErrorMessageBox("Select Sales Order for deletion");
                    return;
                }

                bool isSalesOrderPresent = BusinessLogic.IsSalesOrderPresent(entity.QuoteNumber);
                if (isSalesOrderPresent)
                {
                    BusinessLogic.DeleteSalesOrder(entity.QuoteNumber);
                    Helper.ShowInformationMessageBox("Sales Order is delete successfully!");
                    FillSaleOrderDetails();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }


    }
}
