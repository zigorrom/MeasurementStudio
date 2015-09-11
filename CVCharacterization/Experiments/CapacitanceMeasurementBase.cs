using CVCharacterization.ViewModels;
using ExperimentAbstraction;
using ExperimentDataModel;
using Helper.Ranges.RangeHandlers;
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.Experiments
{
    public abstract class  CapacitanceMeasurementBase<InfoT,DataT>:AbstractExperiment<InfoT,DataT>
        where InfoT : struct,IMeasurementInfo
        where DataT : struct
    {
        protected CVMainViewModel _vm;

        protected string _workingDirectory;
        protected string _experimentName;
        protected string _measurementName;
        protected int _measurementCount;
        protected IInstrumentResourceItem _drainIntrumentResource;
        protected IInstrumentResourceItem _gateInstrumentResource;


        protected AbstractDoubleRangeHandler _firstRangeHandler;
        protected AbstractDoubleRangeHandler _secondRangeHandler;

        public CapacitanceMeasurementBase(CVMainViewModel viewModel, string Name)
            : base(Name)
        {
            _vm = viewModel;
        }

        public override void InitializeExperiment()
        {
            base.InitializeExperiment();

            _workingDirectory = _vm.WorkingDirectory;
            _experimentName = _vm.ExperimentName;
            _measurementName = _vm.MeasurementName;
            _measurementCount = _vm.MeasurementCount;

            _firstRangeHandler = _vm.FirstRangeViewModel.RangeHandler;
            _secondRangeHandler = _vm.SecondRangeViewModel.RangeHandler;

            //_drainIntrumentResource = _vm.IVSettingsViewModel.DrainInstrumentResource;
            //_gateInstrumentResource = _vm.IVSettingsViewModel.GateInstrumentResource;

            //SimulateExperiment = //_vm.IVSettingsViewModel.SimulationMode;
            AssertParams();
            InitializeWriter(_workingDirectory, _experimentName);
        }



        //protected override void DoMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //protected override void SimulateMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}
        private void AssertParams()
        {
            if (String.IsNullOrEmpty(_workingDirectory))
                throw new ArgumentNullException("Working directory is not set");

            if (String.IsNullOrEmpty(_experimentName))
                throw new ArgumentNullException("Experiment name is not set");

            if (String.IsNullOrEmpty(_measurementName))
                throw new ArgumentNullException("MeasurementName is not set");

            if (_measurementCount < 0)
                throw new ArgumentNullException("Measurement count is not set");

            //if (_dsRangeHandler == null)
            //    throw new ArgumentNullException("Drain Source range is not set");

            //if (_gsRangeHandler == null)
            //    throw new ArgumentNullException("Gate Source range is not set");
            if (!SimulateExperiment)
            {
                //if (_drainIntrumentResource == null)
                //    throw new ArgumentNullException("Drain instrument resource was not set");

                //if (_gate_Keithley == null)
                //    throw new ArgumentNullException("Gate instrument resource was not set");
            }
        }



        public override void InitializeInstruments()
        {
            throw new NotImplementedException();
        }

        public override void OwnInstruments()
        {
            throw new NotImplementedException();
        }

        protected override void HandleError(Exception e)
        {
            _vm.ErrorHandler(e);
        }

        protected override void HandleMessage(string Message)
        {
            _vm.MessageHandler(Message);
        }

        public override void ReleaseInstruments()
        {
            throw new NotImplementedException();
        }
    }
}
