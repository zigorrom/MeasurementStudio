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
            set { SetField(ref _delayBetweenMeasurements, value, "DelayBetweenMeasurements"); }
        }

        private double _currentCompliance;

        public double CurrentCompliance
        {
            get { return _currentCompliance; }
            set { SetField(ref _currentCompliance, value, "CurrentCompliance"); }
        }

        private bool _pulseMode;

        public bool PulseMode
        {
            get { return _pulseMode; }
            set { SetField(ref _pulseMode, value, "PulseMode"); }
        }

        private double _pulseLength;

        public double PulseLength
        {
            get { return _pulseLength; }
            set { SetField(ref _pulseLength, value, "PulseLength"); }
        }

        private double _pulseDelay;

        public double PulseDelay
        {
            get { return _pulseDelay; }
            set { SetField(ref _pulseDelay, value, "PulseDelay"); }
        }

        private int _deviceAveraging;

        public int DeviceAveraging
        {
            get { return _deviceAveraging; }
            set { SetField(ref _deviceAveraging, value, "DeviceAveraging"); }
        }

        private bool _simulationMode;

        public bool SimulationMode
        {
            get { return _simulationMode; }
            set { SetField(ref _simulationMode, value, "SimulationMode"); }
        }


    }
}
