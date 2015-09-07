using Instruments;
using System;
using System.Collections.Generic;
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
            foreach (var i in _instrumentHandler.Instruments)
            {

            }
        }
    
    }

    //public sealed  class InstrumentHandler : INotifyPropertyChanged
    //{
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    public void OnPropertyChanged(string PropertyName)
    //    {
    //        if (null != PropertyChanged)
    //            PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
    //    }

    //    //private ObservableCollection<IInstrumentOwner> m_Owners

    //    private void InitializeViewModel()
    //    {
    //        //throw new NotImplementedException();
    //    }


    //    private IInstrumentOwner m_CurrentOwner;
    //    public IInstrumentOwner CurrentOwner
    //    {
    //        get { return m_CurrentOwner; }
    //        set
    //        {
    //            if (m_CurrentOwner == value) return;
    //            m_CurrentOwner = value;
    //            OnPropertyChanged("CurrentOwner");
    //            OnPropertyChanged("InstrumentRuleCollection");
    //        }
    //    }









    //}
}
