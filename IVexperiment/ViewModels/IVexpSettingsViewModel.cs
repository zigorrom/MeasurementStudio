using ChannelSwitchHelper;
using InstrumentHandlerNamespace;
using Instruments;
using Microsoft.TeamFoundation.MVVM;
//using Keithley24xxNamespace;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace IVexperiment.ViewModels
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
            _scenarioView = new MeasurementScenarioView();
            _scenarioViewModel = new MeasurementScenarioModel();
            _scenarioView.DataContext = _scenarioViewModel;

            //_scenarioWindow = new Window();
            //_scenarioWindow.Content = _scenarioView;

           
        }

        private Window _scenarioWindow;
        private MeasurementScenarioView _scenarioView;


        private MeasurementScenarioModel _scenarioViewModel;

        public MeasurementScenarioModel ScenarioViewModel
        {
            get { return _scenarioViewModel; }
            set { _scenarioViewModel = value; }
        }

        private ICommand _openScenarioCommand;
        public ICommand OpenScenarioCommand
        {
            get
            {
                return _openScenarioCommand ?? (_openScenarioCommand = new RelayCommand((b) =>
                {
                    _scenarioWindow = new Window();
                    _scenarioWindow.Content = _scenarioView;
                    _scenarioWindow.ShowDialog();
                }));//new RoutedUICommand("keyInput", "keyPressed", typeof(IMainViewModel)));
            }
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

        private double _pulseWidth;

        public double PulseWidth
        {
            get { return _pulseWidth; }
            set { SetField(ref _pulseWidth, value, "PulseLength"); }
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

        public MeasurementSpeed MeasurementSpeed
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
