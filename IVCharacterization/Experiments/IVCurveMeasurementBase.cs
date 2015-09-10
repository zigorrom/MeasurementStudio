using ExperimentAbstraction;
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

        protected string _workingDirectory;
        protected string _experimentName;
        protected string _measurementName;
        protected int _measurementCount;
        protected IInstrumentResourceItem _drainIntrumentResource;
        protected IInstrumentResourceItem _gateInstrumentResource;


        protected AbstractDoubleRangeHandler _dsRangeHandler;
        protected AbstractDoubleRangeHandler _gsRangeHandler;

        protected Keithley24xx _drainKeithley;

        protected Keithley24xx _gate_Keithley;


        public IVCurveMeasurementBase(IVMainViewModel viewModel)
            : base("Output curve measurement")
        {
            _vm = viewModel;
        }

        public override void InitializeExperiment()
        {
            //try { 
            base.InitializeExperiment();

            _workingDirectory = _vm.WorkingDirectory;
            _experimentName = _vm.ExperimentName;
            _measurementName = _vm.MeasurementName;
            _measurementCount = _vm.MeasurementCount;

            _dsRangeHandler = _vm.FirstRangeViewModel.RangeHandler;
            _gsRangeHandler = _vm.SecondRangeViewModel.RangeHandler;

            _drainIntrumentResource = _vm.IVSettingsViewModel.DrainInstrumentResource;
            _gateInstrumentResource = _vm.IVSettingsViewModel.GateInstrumentResource;

            SimulateExperiment = _vm.IVSettingsViewModel.SimulationMode;
            AssertParams();
            InitializeWriter(_workingDirectory, _experimentName);

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


      
        //public override void InitializeInstruments()
        //{

        //    //base.InitializeExperiment();
        //    //throw new NotImplementedException();
        //}

        //public override void ReleaseInstruments()
        //{

        //    //throw new NotImplementedException();
        //}



        //private StreamMeasurementDataExporter<DrainSourceMeasurmentInfoRow, DrainSourceDataRow> _writer;

        //private MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow> _currentData;


        //protected override void DoMeasurement(object sender, DoWorkEventArgs e)
        //{
        //    var bgw = (BackgroundWorker)sender;

        //    try
        //    {

        //        bool StopExperiment = false;



        //        //_writer.NewExperiment(_experimentName);

        //        int exp = 10;//_dsRangeHandler.Range.PointsCount / 100 ;
        //        //exp = exp > 0 ? exp : 1;
        //        var count = 0;

        //        var maxCount = _dsRangeHandler.TotalPoints * _gsRangeHandler.TotalPoints;
        //        var counter = 0;

        //        var progressCalculator = new Func<int, int>((c) => (int)Math.Floor(100.0 * c / maxCount));

        //        var rand = new Random();
        //        var gEnumerator = _gsRangeHandler.GetEnumerator();

        //        while (gEnumerator.MoveNext() && !StopExperiment)
        //        {
        //            var mea = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow(String.Format("{0}_{1}", _measurementName, _measurementCount++), gEnumerator.Current, "", _measurementCount));

        //            mea.SuspendUpdate();
        //            mea.SetXYMapping(x => new Point(x.DrainSourceVoltage, x.DrainCurrent));
        //            _vm.AddSeries(mea);
        //            var dsEnumerator = _dsRangeHandler.GetEnumerator();
        //            while (dsEnumerator.MoveNext() && !StopExperiment)
        //            {
        //                StopExperiment = bgw.CancellationPending;
        //                if (StopExperiment) break;

        //                if (count++ % exp == 0)
        //                {
        //                    _vm.ExecuteInUIThread(() =>
        //                   {
        //                       mea.ResumeUpdate();
        //                       mea.SuspendUpdate();
        //                   });
        //                }
        //                var r = rand.NextDouble();

        //                mea.Add(new DrainSourceDataRow(dsEnumerator.Current, (r + gEnumerator.Current) * Math.Pow(dsEnumerator.Current, 2), 0));// * Math.Log(dsEnumerator.Current), 0)); //
        //                _vm.ExecuteInUIThread(() => bgw.ReportProgress(progressCalculator(counter++)));
        //                System.Threading.Thread.Sleep(10);
        //            }

        //            _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
        //            EnqueueData(mea);
        //            //_writer.Write(mea);
        //            _vm.MeasurementCount++;

        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        _vm.ErrorHandler(exception);
        //    }


        //}




        //public override void ClearExperiment()
        //{

        //   // throw new NotImplementedException();
        //}

        //public override void FinalizeExperiment()
        //{
        //    base.FinalizeExperiment();
        //    //throw new NotImplementedException();
        //}

    }
}
