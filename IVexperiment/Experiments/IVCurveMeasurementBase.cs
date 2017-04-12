using ChannelSwitchHelper;
using DeviceIO;
using ExperimentViewer;
using ExperimentDataModel;
using Helper.Ranges.RangeHandlers;
using Instruments;
using IVexperiment.ViewModels;
using Keithley24xx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVexperiment.Experiments
{
    public abstract class IVCurveMeasurementBase<InfoT, DataT> : AbstractExperiment<InfoT, DataT>
        where InfoT : struct,IMeasurementInfo
        where DataT : struct
    {
        protected IVMainViewModel _vm;

        //protected string _workingDirectory;
        //protected string _experimentName;
        //protected string _measurementName;
        //protected int _measurementCount;
        protected IInstrumentResourceItem _drainIntrumentResource;
        protected IInstrumentResourceItem _gateInstrumentResource;


        protected AbstractDoubleRangeHandler _firstRangeHandler;
        protected AbstractDoubleRangeHandler _secondRangeHandler;

       

        //protected Keithley24xx _gate_Keithley;

        protected ISourceMeterUnit _drainKeithley;
        protected ISourceMeterUnit _gateKeithley;


        protected IVexpSettingsViewModel _settings;
        //protected MeasurementScenarioModel _measurementScenario;

        protected string GetGraphLineDescription(string Name, double Value, string  Units)
        {
            return String.Format("{0} = {1:f4} {2}", Name, Value, Units);
        }

        public IVCurveMeasurementBase(IVMainViewModel viewModel, string Name)
            : base(Name)
        {
            _vm = viewModel;
        }

        public override void InitializeExperiment()
        {
            //try { 
            base.InitializeExperiment();
            
            WorkingDirectory = _vm.WorkingDirectory;
            ExperimentName = _vm.ExperimentName;
            MeasurementName = _vm.MeasurementName;
            MeasurementCount = _vm.MeasurementCount;

            _firstRangeHandler = _vm.FirstRangeViewModel.RangeHandler;
            _secondRangeHandler = _vm.SecondRangeViewModel.RangeHandler;

            _settings = _vm.IVSettingsViewModel;
            //_measurementScenario = _settings.ScenarioViewModel;

            _drainIntrumentResource = _settings.DrainInstrumentResource;
            _gateInstrumentResource = _settings.GateInstrumentResource;

            SimulateExperiment = _settings.SimulationMode;
            //AssertParams();
            InitializeWriter(WorkingDirectory, ExperimentName);

            //}
            //catch (Exception ex)
            //{
            //    _vm.ErrorHandler(ex);
            //}
        }

        public override void InitializeInstruments()
        {

            //var k1 = new Keithley2430(_drainIntrumentResource.Resource);
            //_drainKeithley = k1.SMU_Channel;

            //var k2 = new Keithley2430(_gateInstrumentResource.Resource);
            //_gateKeithley = k2.SMU_Channel;
            var drainIO = new VisaDevice(_drainIntrumentResource.Resource);
            var draink = new Keithley24xx<Keithley2430>(drainIO);

            _drainKeithley = draink.Channel;
            _drainKeithley.Initialize(drainIO);

            var gateIO = new VisaDevice(_gateInstrumentResource.Resource);
            var gatek = new Keithley24xx<Keithley2400>(gateIO);

            _gateKeithley = gatek.Channel;
            _gateKeithley.Initialize(gateIO);

            //_drainKeithley = new Keithley2430Channel(new VisaDevice(_drainIntrumentResource.Resource));
            //_gateKeithley = new Keithley2430Channel(new VisaDevice(_gateInstrumentResource.Resource));

            _drainKeithley.SMU_SourceMode = SourceMode.Voltage;
            _gateKeithley.SMU_SourceMode = SourceMode.Voltage;

            _drainKeithley.SetCompliance(SourceMode.Voltage, _settings.CurrentCompliance);
            _gateKeithley.SetCompliance(SourceMode.Voltage, _settings.CurrentCompliance);
            
            var npls = 0.0;
            switch (_settings.MeasurementSpeed)
            {
                case MeasurementSpeed.Slow:
                    npls = 0.01;
                    break;
                case MeasurementSpeed.Middle:
                    npls = 1;
                    break;
                case MeasurementSpeed.Fast:
                    npls = 10;
                    break;
                default:
                    throw new ArgumentException("NPLC not set");
            }

            _drainKeithley.SetNPLC(npls);
            _gateKeithley.SetNPLC(npls);


            _drainKeithley.SetAveraging(_settings.DeviceAveraging);
            _gateKeithley.SetAveraging(_settings.DeviceAveraging);

            if(_settings.PulseMode)
            {
                _drainKeithley.SMU_ShapeMode = ShapeMode.Pulse;
                _drainKeithley.PulseWidth = _settings.PulseWidth;
                _drainKeithley.PulseDelay = _settings.PulseDelay;
            }
            else
            {
                _drainKeithley.SMU_ShapeMode = ShapeMode.DC;
            }

            _drainKeithley.SetCompliance(SourceMode.Voltage, _settings.CurrentCompliance);
            _gateKeithley.SetCompliance(SourceMode.Voltage, _settings.CurrentCompliance);


            //_drainKeithley.SetNPLC()

            //_drainKeithley = new Keithley24xx(_drainIntrumentResource.Name, _drainIntrumentResource.Alias, _drainIntrumentResource.Resource);
            //if (!_drainKeithley.IsAlive(true))
            //    throw new ArgumentException("Drain Keithley doesnt respond");

            //_gate_Keithley = new Keithley24xx(_gateInstrumentResource.Name, _gateInstrumentResource.Alias, _gateInstrumentResource.Resource);
            //if (!_drainKeithley.IsAlive(true))
            //    throw new ArgumentException("Gate Keithley doesnt respond");


            //if(!_drainKeithley.SetCurrentLimit(_settings.CurrentCompliance))
            //{
            //    HandleMessage("Current limit was not set for drain");
            //}

            //if(!_gate_Keithley.SetCurrentLimit(_settings.CurrentCompliance))
            //{
            //    HandleMessage("Current Limit was not set for gate");
            //}



        }

        protected override void DoMeasurement(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void SimulateMeasurement(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void OwnInstruments()
        {
            
            //_drainKeithley.InstrumentOwner = this;
            //_gate_Keithley.InstrumentOwner = this;
        }

        protected override void AssertParams()
        {
            base.AssertParams();

            if (_firstRangeHandler == null)
                throw new ArgumentNullException("Range is not set");

            if (_secondRangeHandler == null)
                throw new ArgumentNullException("Range is not set");
            if (!SimulateExperiment)
            {
                if (_drainIntrumentResource == null)
                    throw new ArgumentNullException("Drain instrument resource was not set");

                if (_gateKeithley == null)
                    throw new ArgumentNullException("Gate instrument resource was not set");
            }
        }

        public override void ReleaseInstruments()
        {
            //_drainKeithley.InstrumentOwner = null;
            //_gateKeithley.InstrumentOwner = null;
        }

        protected override void HandleError(Exception e)
        {
            _vm.ErrorHandler(e);
        }

        protected override void HandleMessage(string Message)
        {
            _vm.MessageHandler(Message);
            //throw new NotImplementedException();
        }


        ///
        /// MOSFET simulation
        ///

        private const double mu = 0.02 ;
        private const double eps = 3.9;
        private const double eps0 = 8.8541e-12;
        private const double d = 8e-9;
        private const double W = 10e-6;
        private const double L = 20e-6;
        private const double Vth = 0.45;

        protected double DrainCurrent(double GateVoltage, double DrainSourceVoltage)
        {
            if (GateVoltage > Vth)
            {

                if (DrainSourceVoltage < (GateVoltage - Vth))
                {
                    return mu * (eps * eps0 / d) * (W / L) * ((GateVoltage - Vth) * DrainSourceVoltage - DrainSourceVoltage * DrainSourceVoltage / 2);
                }
                else
                {
                    return mu * (eps * eps0 / 2 / d) * (W / L) * (GateVoltage - Vth) * (GateVoltage - Vth) * (1+0.01*(DrainSourceVoltage-GateVoltage+Vth));
                }


            }
            else
            {
                return 0;
            }



        }

    }
}
