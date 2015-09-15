using InstrumentHandlerNamespace;
using Instruments;
using Keithley24xxNamespace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        
        public IVexpSettingsViewModel()
        {
            _instrumentHandler = InstrumentHandler.Instance;
            
        }

        private InstrumentHandler _instrumentHandler;

        public ObservableCollection<IInstrumentResourceItem> Resources
        {
            get
            {
                if (_instrumentHandler != null)
                {
                    return _instrumentHandler.Resources;
                }
                return null;
            }
        }

        private IInstrumentResourceItem _drainInstrumentResource;

        public IInstrumentResourceItem DrainInstrumentResource
        {
            get { return _drainInstrumentResource; }
            set { SetField(ref _drainInstrumentResource, value, "DrainInstrumentResource"); }
        }

        private IInstrumentResourceItem _gateInstrumentResource;

        public IInstrumentResourceItem GateInstrumentResource
        {
            get { return _gateInstrumentResource; }
            set { SetField(ref _gateInstrumentResource, value, "GateInstrumentResource"); }
        }

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

        private MeasurementSpeed _measurementSpeed;

        public MeasurementSpeed measurementSpeed
        {
            get { return _measurementSpeed; }
            set { SetField(ref _measurementSpeed, value, "measurementSpeed"); }
        }

        private bool _waitForValueSet;
        public bool WaitForValueSet
        {
            get { return _waitForValueSet; }
            set { SetField(ref _waitForValueSet, value, "WaitForValueSet"); }
        }


    }
}
