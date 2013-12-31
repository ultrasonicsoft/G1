using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    public class QuoteGridEntity : INotifyPropertyChanged
    {
        private int _lineID;
        private int _quantity;
        private string _description;
        private string _dimension;
        private string _totalSqFt;
        private string _unitPrice;
        private string _total;
        private string _actualDimension;
        private string _actualTotalSQFT;
        private bool _isLogo;
        private string _actualDescription;
        private string _shape;

        public string ActualDescription
        {
            get { return _actualDescription; }
            set { _actualDescription = value; }
        }

        public string Shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        public string ActualDimension
        {
            get { return _actualDimension; }
            set { _actualDimension = value; }
        }

        public string ActualTotalSQFT
        {
            get { return _actualTotalSQFT; }
            set { _actualTotalSQFT = value; }
        }

        public bool IsLogo
        {
            get { return _isLogo; }
            set { _isLogo = value; }
        }


        public QuoteGridEntity()
        {
        }

        public int LineID
        {
            get { return _lineID; }
            set
            {
                _lineID = value;
                OnPropertyChanged("LineID");
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public string Dimension
        {
            get { return _dimension; }
            set
            {
                _dimension = value;
                OnPropertyChanged("Dimension");
            }
        }

        public string TotalSqFt
        {
            get { return _totalSqFt; }
            set
            {
                _totalSqFt = value;
                OnPropertyChanged("TotalSqFt");
            }
        }

        public string UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }

        public string Total
        {
            get { return _total; }
            set
            {
                _total = value;
                OnPropertyChanged("Total");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }

}
