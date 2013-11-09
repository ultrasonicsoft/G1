using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    public class InsulationDetails : INotifyPropertyChanged
    {
        public InsulationDetails()
        {
            Glass = new ObservableCollection<ValueIDPair>();
            Thickness = new ObservableCollection<ValueIDPair>();
            Tempered = new ObservableCollection<ValueIDPair>();
            SqFt = 0;
            Total = 0;
        }

        public ObservableCollection<ValueIDPair> Glass { get; set; }

        public ObservableCollection<ValueIDPair> Thickness { get; set; }

        public ObservableCollection<ValueIDPair> Tempered { get; set; }

        private double _sqft;
        public double SqFt
        {
            get { return _sqft; }
            set
            {
                _sqft = value;
                OnPropertyChanged("Area");
            }
        }

        private double _Total;
        public double Total
        {
            get { return _Total; }
            set
            {
                _Total = value;
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

    public class ValueIDPair : INotifyPropertyChanged
    {
        public ValueIDPair()
        {
            ID = 0;
            Value = String.Empty;
        }

        public ValueIDPair(int id, string value)
        {
            ID = id;
            Value = value;
        }

        private int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                _ID = value;
                OnPropertyChanged("ID");
            }
        }

        private string _Value;
        public string Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                OnPropertyChanged("Value");
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
