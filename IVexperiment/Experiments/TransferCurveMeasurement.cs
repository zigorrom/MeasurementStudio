using ExperimentDataModel;
using IVexperiment.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ExperimentAbstraction;

namespace IVexperiment.Experiments
{

    public class TransferCurveMeasurement:IVCurveMeasurementBase<GateSourceMeasurementInfoRow,GateSourceDataRow>
    {

        public TransferCurveMeasurement(ICurrentVoltageMeasurementViewModel viewModel)
            : base(viewModel,"Transfer characteristic measurement")
        {

        }

        //protected override void InitializeWriter()
        //{
           
        //}

        protected override void PerformExperiment(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {

            //((IEnableControllableViewModel)ViewModel).GlobalIsEnabled = false;
            //OnExecutionStarted(this, EventArgs.Empty);

            int exp = RefreshPoints;//_dsRangeHandler.Range.PointsCount / 100 ;
            //exp = exp > 0 ? exp : 1;
            var count = 0;

            var maxCount = _gateSourceRangeHandler.TotalPoints * _drainSourceRangeHandler.TotalPoints;
            var step = 100.0 / maxCount;
            var counter = 0;

            var progressCalculator = new Func<int, int>((c) => (int)Math.Floor(step * c));

            var rand = new Random();

            double measGateVoltage = 0, measGateCurrent = 0, measDrainVoltage = 0, measDrainCurrent = 0, resistance = 0;

            using (var ExperimentWriter = new MeasurementDataExporter<GateSourceMeasurementInfoRow, GateSourceDataRow>(WorkingDirectory))
            {
                ExperimentWriter.OpenExperiment(ExperimentName);

                _drainKeithley.SwitchOn();
                _gateKeithley.SwitchOn();

                foreach (var DrainVoltage in _drainSourceRangeHandler)
                {
                    pauseToken.WaitWhilePausedAsync().Wait();
                    cancellationToken.ThrowIfCancellationRequested();

                    ExperimentWriter.OpenMeasurement(String.Format("{0}_{1}", MeasurementName, MeasurementCount));

                    _drainKeithley.SetSourceVoltage(DrainVoltage);

                    var mea = new MeasurementData<GateSourceMeasurementInfoRow, GateSourceDataRow>(new GateSourceMeasurementInfoRow(String.Format("{0}_{1}", MeasurementName, MeasurementCount), DrainVoltage, "", MeasurementCount));
                    mea.SuspendUpdate();
                    mea.SetXYMapping(x => new Point(x.GateSourceVoltage, x.DrainCurrent));
                    _vm.AddSeries(mea, GetGraphLineDescription("Vds", DrainVoltage, "V"));

                    foreach (var GateVoltage in _gateSourceRangeHandler)
                    {

                        pauseToken.WaitWhilePausedAsync().Wait();
                        cancellationToken.ThrowIfCancellationRequested();
                        _gateKeithley.SetSourceVoltage(GateVoltage);

                        if (count++ % exp == 0)
                        {
                            _vm.ExecuteInUIThread(() =>
                            {
                                mea.ResumeUpdate();
                                mea.SuspendUpdate();
                            });
                        }
                        _gateKeithley.MeasureAll(out measGateVoltage, out measGateCurrent, out resistance);
                        _drainKeithley.MeasureAll(out measDrainVoltage, out measDrainCurrent, out resistance);

                        mea.Add(new GateSourceDataRow(measGateVoltage,measDrainCurrent, measGateCurrent));
                        progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = progressCalculator(counter++), ExperimentProgressMessage = "Experiment is running" });

                        System.Threading.Thread.Sleep(10);

                    }
                    _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
                    ExperimentWriter.WriteMeasurement(mea);
                    //EnqueueData(mea, true);
                    //_writer.Write(mea);
                    MeasurementCount++;
                }
                _drainKeithley.SwitchOff();
                _gateKeithley.SwitchOff();

            }
        }

        protected override void PerformSimulatedExperiment(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {
            //((IEnableControllableViewModel)ViewModel).GlobalIsEnabled = false;
            //OnExecutionStarted(this, EventArgs.Empty);

            int exp = 10;//_dsRangeHandler.Range.PointsCount / 100 ;
            //exp = exp > 0 ? exp : 1;
            var count = 0;

            var maxCount = _gateSourceRangeHandler.TotalPoints * _drainSourceRangeHandler.TotalPoints;
            var step = 100.0 / maxCount;
            var counter = 0;

            var progressCalculator = new Func<int, int>((c) => (int)Math.Floor(step * c));

            var rand = new Random();

            using (var ExperimentWriter = new MeasurementDataExporter<GateSourceMeasurementInfoRow, GateSourceDataRow>(WorkingDirectory))
            {
                ExperimentWriter.OpenExperiment(ExperimentName);

                foreach (var DrainVoltage in _drainSourceRangeHandler)
                {
                    pauseToken.WaitWhilePausedAsync().Wait();
                    cancellationToken.ThrowIfCancellationRequested();

                    ExperimentWriter.OpenMeasurement(String.Format("{0}_{1}", MeasurementName, MeasurementCount));

                    var mea = new MeasurementData<GateSourceMeasurementInfoRow, GateSourceDataRow>(new GateSourceMeasurementInfoRow(String.Format("{0}_{1}", MeasurementName, MeasurementCount), DrainVoltage, "", MeasurementCount));
                    mea.SuspendUpdate();
                    mea.SetXYMapping(x => new Point(x.GateSourceVoltage, x.DrainCurrent));
                    _vm.AddSeries(mea, GetGraphLineDescription("Vds", DrainVoltage, "V"));

                    foreach (var GateVoltage in _gateSourceRangeHandler)
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
                        mea.Add(new GateSourceDataRow(GateVoltage, DrainCurrent(GateVoltage, DrainVoltage), 0));
                        progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = progressCalculator(counter++), ExperimentProgressMessage = "Experiment is running" });

                        System.Threading.Thread.Sleep(10);

                    }
                    _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
                    ExperimentWriter.WriteMeasurement(mea);
                    //EnqueueData(mea, true);
                    //_writer.Write(mea);
                    MeasurementCount++;
                }
            }
            //OnExecutionFinished(this, EventArgs.Empty);
           
        }

        
    }

#region OldVersion
    /*public class TransferCurveMeasurement : AbstractExperiment<GateSourceMeasurementInfoRow,GateSourceDataRow>
    {

        private IVMainViewModel _vm;
        public TransferCurveMeasurement(IVMainViewModel viewModel):base("Transfer curve measurement")
        {
            _vm = viewModel;
        }

        public override void InitializeExperiment()
        {
            //throw new NotImplementedException();
        }

        public override void InitializeInstruments()
        {
            //throw new NotImplementedException();
        }

        public override void ReleaseInstruments()
        {
            //throw new NotImplementedException();
        }

        public override void Start()
        {
            base.Start();
        }

       
        public override void Abort()
        {
            base.Start();
        }

        public override void OwnInstruments()
        {
            //throw new NotImplementedException();
        }

      

        


        protected override void DoMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var bgw = (BackgroundWorker)sender;
            //_meaList.Clear();
            bool StopExperiment = false;
            var WorkingDirectory = _vm.WorkingDirectory;
            var ExperimentName = _vm.ExperimentName;
            var MeasurementName = _vm.MeasurementName;
            try
            {
                using (var writer = GetStreamExporter(WorkingDirectory))
                {
                    
                    writer.NewExperiment(ExperimentName);
                    for (int j = 0; j < 5 && !StopExperiment; j++)
                    {
                        var _mea = new MeasurementData<GateSourceMeasurementInfoRow, GateSourceDataRow>(new GateSourceMeasurementInfoRow(String.Format("{0}_{1}", MeasurementName, _vm.MeasurementCount++), 123, "", 1));//, new Func<DrainSourceDataRow, Point>((x) => new Point(x.DrainSourceVoltage, x.DrainCurrent)));


                        _mea.SuspendUpdate();
                        _mea.SetXYMapping(x => new Point(x.GateSourceVoltage, x.DrainCurrent));
                        _vm.AddSeries(_mea);

                        int exp = 10;
                        var rand = new Random();
                        for (int i = 1; i < 100 && !StopExperiment; i++)
                        {
                            StopExperiment = bgw.CancellationPending;
                            if (i % exp == 0)
                            {
                                _vm.ExecuteInUIThread(() =>
                                {
                                    _mea.ResumeUpdate();
                                    _mea.SuspendUpdate();
                                });
                            }
                            var r = rand.NextDouble();

                            _mea.Add(new GateSourceDataRow(i, (r + j) * Math.Log(i), 0));
                            System.Diagnostics.Debug.WriteLine(_mea.Count);
                            System.Threading.Thread.Sleep(2);
                        }
                        _vm.ExecuteInUIThread(() => _mea.ResumeUpdate());
                        writer.Write(_mea);
                        _vm.ExecuteInUIThread(() => bgw.ReportProgress(j * 20));
                    }
                }


            }
            catch (Exception exception)
            {
                _vm.ErrorHandler(exception);
            }
        }

        public override void ClearExperiment()
        {
            throw new NotImplementedException();
        }

        public override void FinalizeExperiment()
        {
            throw new NotImplementedException();
        }
    }*/
#endregion
}
