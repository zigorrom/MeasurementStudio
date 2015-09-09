using Instruments;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{

    public class InstrumentHandlerViewModel:INotifyPropertyChanged
    {
        #region PropertyEvents

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion


        InstrumentHandler _instrumentHandler;

        public InstrumentHandlerViewModel()
        {
            _instrumentHandler = InstrumentHandler.Instance;
        }

        public ObservableCollection<IInstrumentResourceItem> Resources { get { return _instrumentHandler.Resources; } }

    }

    








    
}
