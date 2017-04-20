using ExperimentDataModel;
using Helper.Ranges.RangeHandlers;
using IVexperiment.DataModel;
using IVexperiment.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using ExperimentAbstraction;

namespace IVexperiment.Experiments
{

    public class OutputCurveMeasurement : IVCurveMeasurementBase<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>
    {

        public OutputCurveMeasurement(ICurrentVoltageMeasurementViewModel viewModel)
            : base(viewModel, "Output characteristic measurement")
        {
            
        }

        protected override void PerformExperiment(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {
            
            //bool StopExperiment = false;
            //int refreshEvery = 10;
            //var count = 0;
            //var counter = 0;
            //var maxCount = _firstRangeHandler.TotalPoints * _secondRangeHandler.TotalPoints;
            //var progressCalc = new Func<int, int>((c) => (int)Math.Floor(100.0 * c / maxCount));
            ////_drainKeithley.SwitchOn();
            ////_gate_Keithley.SwitchOn();
            

            ////_drainKeithley.SwitchON();
            ////_gateKeithley.SwitchON();

            //var gEnumerator = _secondRangeHandler.GetEnumerator();
            //while (gEnumerator.MoveNext() && !StopExperiment)
            //{
            //    var mea = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow(String.Format("{0}_{1}", MeasurementName, MeasurementCount++), gEnumerator.Current, "", MeasurementCount));

            //    mea.SuspendUpdate();
            //    mea.SetXYMapping(x => new Point(x.DrainSourceVoltage, x.DrainCurrent));
            //    _vm.AddSeries(mea, GetGraphLineDescription("Vg", gEnumerator.Current, "V"));///String.Format("{0} = ")   String.Concat("Vg = ",gEnumerator.Current," V"));

            //    //_gateKeithley.SetSourceVoltage(gEnumerator.Current);

            //    var dsEnumerator = _firstRangeHandler.GetEnumerator();
            //    while (dsEnumerator.MoveNext() && !StopExperiment)
            //    {
            //        StopExperiment = bgw.CancellationPending;
            //        if (StopExperiment)
            //        {
            //            e.Cancel = true; break;
            //        }

            //        if (count++ % refreshEvery == 0)
            //        {
            //            _vm.ExecuteInUIThread(() =>
            //            {
            //                mea.ResumeUpdate();
            //                mea.SuspendUpdate();
            //            });
            //        }

            //        _drainKeithley.SetSourceVoltage(dsEnumerator.Current);

            //        //var drainVolt = _drainKeithley.MeasureVoltage();
            //        //var drainCurr = _drainKeithley.MeasureCurrent();

            //        //double drainVolt, drainCurr, drainRes;
            //        //_drainKeithley.MeasureAll(out drainVolt, out drainCurr, out drainRes);
            //        //double gateVolt, gateCurr, gateRes;
            //        //_gateKeithley.MeasureAll(out gateVolt, out gateCurr, out gateRes);
            //        //var gateVolt = _gateKeithley.MeasureVoltage();
            //        //var gateCurr = _gateKeithley.MeasureCurrent();


            //        //mea.Add(new DrainSourceDataRow(drainVolt, drainCurr, gateCurr));
            //        _vm.ExecuteInUIThread(() => bgw.ReportProgress(progressCalc(counter++)));
            //        //System.Threading.Thread.Sleep(10);
            //    }

            //    _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
            //    EnqueueData(mea,true);
            //    //_writer.Write(mea);
            //    _vm.MeasurementCount++;

            //}

            ////_drainKeithley.SwitchOFF();
            //_gateKeithley.SwitchOFF();
            ////_drainKeithley.SwitchOff();
            //_gateKeithley.SwitchOff();
        }

        protected override void PerformSimulatedExperiment(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {
           
            int exp = 10;//_dsRangeHandler.Range.PointsCount / 100 ;
            //exp = exp > 0 ? exp : 1;
            var count = 0;

            var maxCount = _gateSourceRangeHandler.TotalPoints * _drainSourceRangeHandler.TotalPoints;
            var step = 100.0/maxCount;
            var counter = 0;

            var progressCalculator = new Func<int, int>((c) => (int)Math.Floor(step*c));

            var rand = new Random();
            //MeasurementDataExporter
            using (var ExperimentWriter = new MeasurementDataExporter<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(WorkingDirectory))
            {
                ExperimentWriter.OpenExperiment(ExperimentName);
                
                /////
                ///// MOSFET simulation
                /////
                foreach (var GateVoltage in _gateSourceRangeHandler)
                {
                    pauseToken.WaitWhilePausedAsync().Wait();
                    cancellationToken.ThrowIfCancellationRequested();

                    ExperimentWriter.OpenMeasurement(String.Format("{0}_{1}",MeasurementName,MeasurementCount));

                    var mea = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow(String.Format("{0}_{1}", MeasurementName, MeasurementCount), GateVoltage, "", MeasurementCount));
                    mea.SuspendUpdate();
                    mea.SetXYMapping(x => new Point(x.DrainSourceVoltage, x.DrainCurrent));
                    _vm.AddSeries(mea, GetGraphLineDescription("Vg", GateVoltage, "V"));
                    foreach (var DrainSourceVoltage in _drainSourceRangeHandler)
                    {
                        pauseToken.WaitWhilePausedAsync().Wait();
                        cancellationToken.ThrowIfCancellationRequested();
                        if (count++ % exp == 0)
                        {
                            _vm.ExecuteInUIThread(() =>
                            {
                                mea.ResumeUpdate();
                                mea.SuspendUpdate();
                            });
                        }
                        var r = rand.NextDouble();
                        mea.Add(new DrainSourceDataRow(DrainSourceVoltage, DrainCurrent(GateVoltage, DrainSourceVoltage), 0));
                        //mea.Add(new DrainSourceDataRow(dsEnumerator.Current, (r + gEnumerator.Current) * Math.Pow(dsEnumerator.Current, 2), 0));// * Math.Log(dsEnumerator.Current), 0)); //
                        progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = progressCalculator(counter++), ExperimentProgressMessage = "Experiment is running" });

                        //_vm.ExecuteInUIThread(() => bgw.ReportProgress(progressCalculator(counter++)));
                        System.Threading.Thread.Sleep(10);
                    }
                    _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
                    ExperimentWriter.WriteMeasurement(mea);
                    //EnqueueData(mea, true);
                    //_writer.Write(mea);
                    MeasurementCount++;
                }



            }
           
            
        }

        //protected override void InitializeWriter()
        //{
            
        //}
    }


    #region OldVersion
    /*public class OutputCurveMeasurement : AbstractExperiment<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>
    {
        private IVMainViewModel _vm;
        


       
        public OutputCurveMeasurement(IVMainViewModel viewModel):base("Output curve measurement")
        {
            _vm = viewModel;
        }

        public override void Start()
        {
            base.Start();
        }

        public override void Abort()
        {
            base.Abort();
        }

        public override void OwnInstruments()
        {
           // throw new NotImplementedException();
        }

        public override void InitializeExperiment()
        {
            base.InitializeExperiment();
            

            _workingDirectory = _vm.WorkingDirectory;
            _experimentName = _vm.ExperimentName;
            _measurementName = _vm.MeasurementName;
            _measurementCount = _vm.MeasurementCount;

            _dsRangeHandler = _vm.DSRangeViewModel.RangeHandler;
            _gsRangeHandler = _vm.GSRangeViewModel.RangeHandler;

            AssertParams();
            //_writer = GetStreamExporter(_workingDirectory);
            InitializeWriter(_workingDirectory, _experimentName);
        }

       

       

        private void AssertParams()
        {
            if(String.IsNullOrEmpty(_workingDirectory))
                throw new ArgumentNullException("Working directory is not set");

            if(String.IsNullOrEmpty(_experimentName))
                throw new ArgumentNullException("Experiment name is not set");

            if(String.IsNullOrEmpty(_measurementName))
                throw new ArgumentNullException("MeasurementName is not set");

            if(_measurementCount<0)
                throw new ArgumentNullException("Measurement count is not set");

            if(_dsRangeHandler == null)
                throw new ArgumentNullException("Drain Source range is not set");

            if (_gsRangeHandler == null)
                throw new ArgumentNullException("Gate Source range is not set");

        }

        public override void InitializeInstruments()
        {
            
            //base.InitializeExperiment();
            //throw new NotImplementedException();
        }

        public override void ReleaseInstruments()
        {
            
            //throw new NotImplementedException();
        }

        private string _workingDirectory;
        private string _experimentName;
        private string _measurementName;
        private int _measurementCount;

        private AbstractDoubleRangeHandler _dsRangeHandler;
        private AbstractDoubleRangeHandler _gsRangeHandler;

        //private StreamMeasurementDataExporter<DrainSourceMeasurmentInfoRow, DrainSourceDataRow> _writer;

        //private MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow> _currentData;
        

        protected override void DoMeasurement(object sender, DoWorkEventArgs e)
        {
            var bgw = (BackgroundWorker)sender;

            try
            {

                bool StopExperiment = false;

               

                //_writer.NewExperiment(_experimentName);

                int exp = 10;//_dsRangeHandler.Range.PointsCount / 100 ;
                //exp = exp > 0 ? exp : 1;
                var count = 0;

                var maxCount = _dsRangeHandler.TotalPoints * _gsRangeHandler.TotalPoints;
                var counter = 0;

                var progressCalculator = new Func<int, int>((c) => (int)Math.Floor(100.0 * c / maxCount));

                var rand = new Random();
                var gEnumerator = _gsRangeHandler.GetEnumerator();
                
                while (gEnumerator.MoveNext() && !StopExperiment)
                {
                    var mea = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow(String.Format("{0}_{1}", _measurementName, _measurementCount++), gEnumerator.Current, "", _measurementCount));
                    
                    mea.SuspendUpdate();
                    mea.SetXYMapping(x => new Point(x.DrainSourceVoltage, x.DrainCurrent));
                    _vm.AddSeries(mea);
                    var dsEnumerator = _dsRangeHandler.GetEnumerator();
                    while (dsEnumerator.MoveNext() && !StopExperiment)
                    {
                        StopExperiment = bgw.CancellationPending;
                        if (StopExperiment) break;

                        if (count++ % exp == 0)
                        {
                            _vm.ExecuteInUIThread(() =>
                           {
                               mea.ResumeUpdate();
                               mea.SuspendUpdate();
                           });
                        }
                        var r = rand.NextDouble();

                        mea.Add(new DrainSourceDataRow(dsEnumerator.Current, (r + gEnumerator.Current) * Math.Pow(dsEnumerator.Current, 2), 0));// * Math.Log(dsEnumerator.Current), 0)); //
                        _vm.ExecuteInUIThread(() => bgw.ReportProgress(progressCalculator(counter++)));
                        System.Threading.Thread.Sleep(10);
                    }

                    _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
                    EnqueueData(mea);
                    //_writer.Write(mea);
                    _vm.MeasurementCount++;
                    
                }
            }
            catch (Exception exception)
            {
                _vm.ErrorHandler(exception);
            }
            

        }

       

        
        public override void ClearExperiment()
        {
            
           // throw new NotImplementedException();
        }

        public override void FinalizeExperiment()
        {
            base.FinalizeExperiment();
            //throw new NotImplementedException();
        }
    }*/
    #endregion
}
