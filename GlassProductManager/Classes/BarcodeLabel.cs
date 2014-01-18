using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassProductManager
{
    public class BarcodeLabel : INotifyPropertyChanged
    {
        private int _lineID;
        private int _itemID;
        private int _id;
        private string _wsNumber;
      

        public string WSNumber
        {
            get { return _wsNumber; }
            set { _wsNumber = value;
            OnPropertyChanged("WSNumber");
            }
        }

        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
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

        public int ItemID
        {
            get { return _itemID; }
            set
            {
                _itemID = value;
                OnPropertyChanged("ItemID");
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
