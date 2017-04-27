using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.AbstractPropertyChangedClass
{
    public abstract class AbstractNotifyPropertyChangedClass:INotifyPropertyChanged
    {  
        #region PropertyEvents

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged(string PropertyName)
        {   
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
