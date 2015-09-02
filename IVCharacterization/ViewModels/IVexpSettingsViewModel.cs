using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.ViewModels
{
    public class IVexpSettingsViewModel:INotifyPropertyChanged
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
        private bool _useSampleSelector;
        public bool UseSampleSelector
        {
            get { return _useSampleSelector; }
            set
            {
                SetField(ref _useSampleSelector, value, "UseSampleSelector");
            }

        }

    }
}
