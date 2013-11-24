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
    public partial class QuoteMasterContent : UserControl
    {
        private ListCollectionView m_QuoteListForSearch;

        public QuoteMasterContent()
        {
            InitializeComponent();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (m_QuoteListForSearch == null || txtSearch.Text == "Search")
                    return;
                if (txtSearch.Text != "")
                {
                    if (m_QuoteListForSearch.CanFilter)
                    {
                        m_QuoteListForSearch.Filter =
                                new Predicate<object>(ContainsIt);

                        FilterIt();
                    }
                    else
                    {
                        m_QuoteListForSearch.Filter = null;
                    }
                }
                else
                {
                    m_QuoteListForSearch.Filter = null;
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
                QuoteMasterEntity currentRow = value as QuoteMasterEntity;

                if (dgQuoteDetails.Columns.Count > 1)
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

        private bool IsSearchCriteriaMatched(QuoteMasterEntity currentRow)
        {
            return currentRow.QuoteStatus.ToString()
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

                                    currentRow.CreatedOn.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.Total.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.EstimatedShipDate.ToString()
                                              .ToLower()
                                              .Contains(txtSearch.Text
                                                                .ToLower()) ||
                                    currentRow.PaymentType.ToString()
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
                dgQuoteDetails.ItemsSource = null;
                ObservableCollection<QuoteMasterEntity> fileList = new ObservableCollection<QuoteMasterEntity>();

                foreach (QuoteMasterEntity row in m_QuoteListForSearch)
                {
                    fileList.Add(row);
                }
                dgQuoteDetails.ItemsSource = fileList;
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
                FillQuoteDetails();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }

        private void FillQuoteDetails()
        {
            var result = BusinessLogic.GetQuoteMasterData();
            dgQuoteDetails.ItemsSource = result;
            m_QuoteListForSearch = new ListCollectionView(result);
        }

    }
}
