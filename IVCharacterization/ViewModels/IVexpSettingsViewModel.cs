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
            get
            {
                return _useSampleSelector;
            }
            set
            {
                SetField(ref _useSampleSelector, value, "UseSampleSelector");
            }

        }

        private double _delayBetweenMeasurements;

        public double DelayBetweenMeasurements
        {
            get { return _delayBetweenMeasurements; }
            set { _delayBetweenMeasurements = value; }
        }

        private double _currentCompliance;

        public double CurrentCompliance
        {
            get { return _currentCompliance; }
            set { _currentCompliance = value; }
        }

        private bool _pulseMode;

        public bool PulseMode
        {
            get { return _pulseMode; }
            set { _pulseMode = value; }
        }

        private double _pulseLength;

        public double PulseLength
        {
            get { return _pulseLength; }
            set { _pulseLength = value; }
        }

        private double _pulseDelay;

        public double PulseDelay
        {
            get { return _pulseDelay; }
            set { _pulseDelay = value; }
        }

        private int _deviceAveraging;

        public int DeviceAveraging
        {
            get { return _deviceAveraging; }
            set { _deviceAveraging = value; }
        }



    }
}
