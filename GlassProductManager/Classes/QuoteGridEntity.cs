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
        public QuoteGridEntity()
        {
        }

        private int _lineID;
        public int LineID
        {
            get { return _lineID; }
            set
            {
                _lineID = value;
                OnPropertyChanged("LineID");
            }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _dimension;
        public string Dimension
        {
            get { return _dimension; }
            set
            {
                _dimension = value;
                OnPropertyChanged("Dimension");
            }
        }

        private string _totalSqFt;
        public string TotalSqFt
        {
            get { return _totalSqFt; }
            set
            {
                _totalSqFt = value;
                OnPropertyChanged("TotalSqFt");
            }
        }

        private string _unitPrice;
        public string UnitPrice
        {
            get { return _unitPrice; }
            set
            {
                _unitPrice = value;
                OnPropertyChanged("UnitPrice");
            }
        }

        private double _total;
        public double Total
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
