﻿using ExperimentAbstraction;
using ExperimentDataModel;
using Helper.Ranges.RangeHandlers;
using Instruments;
using Keithley24xxNamespace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.Experiments
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


        protected AbstractDoubleRangeHandler _dsRangeHandler;
        protected AbstractDoubleRangeHandler _gsRangeHandler;

        protected Keithley24xx _drainKeithley;

        protected Keithley24xx _gate_Keithley;


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

            _dsRangeHandler = _vm.FirstRangeViewModel.RangeHandler;
            _gsRangeHandler = _vm.SecondRangeViewModel.RangeHandler;

            _drainIntrumentResource = _vm.IVSettingsViewModel.DrainInstrumentResource;
            _gateInstrumentResource = _vm.IVSettingsViewModel.GateInstrumentResource;

            SimulateExperiment = _vm.IVSettingsViewModel.SimulationMode;
            AssertParams();
            InitializeWriter(WorkingDirectory, ExperimentName);

            //}
            //catch (Exception ex)
            //{
            //    _vm.ErrorHandler(ex);
            //}
        }

        public override void InitializeInstruments()
        {
            //try
            //{
            _drainKeithley = new Keithley24xx(_drainIntrumentResource.Name, _drainIntrumentResource.Alias, _drainIntrumentResource.Resource);
            if (_drainKeithley.IsAlive(true))
                throw new ArgumentException("Drain Keithley doesnt respond");

            _gate_Keithley = new Keithley24xx(_gateInstrumentResource.Name, _gateInstrumentResource.Alias, _gateInstrumentResource.Resource);
            if (_drainKeithley.IsAlive(true))
                throw new ArgumentException("Gate Keithley doesnt respond");
            //}
            //catch (Exception ex)
            //{
            //    _vm.ErrorHandler(ex);
            //}
        }

        public override void OwnInstruments()
        {
            
            _drainKeithley.InstrumentOwner = this;
            _gate_Keithley.InstrumentOwner = this;
        }

        protected override void AssertParams()
        {
            base.AssertParams();

            if (_dsRangeHandler == null)
                throw new ArgumentNullException("Drain Source range is not set");

            if (_gsRangeHandler == null)
                throw new ArgumentNullException("Gate Source range is not set");
            if (!SimulateExperiment)
            {
                if (_drainIntrumentResource == null)
                    throw new ArgumentNullException("Drain instrument resource was not set");

                if (_gate_Keithley == null)
                    throw new ArgumentNullException("Gate instrument resource was not set");
            }
        }

        public override void ReleaseInstruments()
        {
            _drainKeithley.InstrumentOwner = null;
            _gate_Keithley.InstrumentOwner = null;
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

    }
}
