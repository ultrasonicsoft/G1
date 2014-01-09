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
    /// Interaction logic for NewQuoteContent.xaml
    /// </summary>
    public partial class NewQuoteContent : UserControl
    {
        public NewQuoteContent()
        {
            try
            {
                InitializeComponent();

                if (ucNewQuoteItems.CurrentPage == null)
                {
                    NewQuoteItemsContent items = new NewQuoteItemsContent();
                    ucNewQuoteItems.ShowPage(items);
                }

                if (ucNewQuoteGrid.CurrentPage == null)
                {
                    NewQuoteGridContent grid = new NewQuoteGridContent();
                    ucNewQuoteGrid.ShowPage(grid);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
            
        }

        public NewQuoteContent(bool _isOpenQuoteRequested, string _quoteNumber)
        {
            try
            {
                InitializeComponent();

                if (ucNewQuoteItems.CurrentPage == null)
                {
                    NewQuoteItemsContent items = new NewQuoteItemsContent();
                    ucNewQuoteItems.ShowPage(items);
                }

                if (ucNewQuoteGrid.CurrentPage == null)
                {
                    NewQuoteGridContent grid = null;
                    if (_isOpenQuoteRequested == true)
                    {
                        grid = new NewQuoteGridContent(_isOpenQuoteRequested, _quoteNumber);
                    }
                    else
                    {
                        grid = new NewQuoteGridContent();
                    }
                    ucNewQuoteGrid.ShowPage(grid);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
            }
        }
    }
}
