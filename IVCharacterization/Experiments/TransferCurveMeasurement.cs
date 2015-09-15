﻿using ExperimentAbstraction;
using ExperimentDataModel;
using IVCharacterization.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IVCharacterization.Experiments
{

    public class TransferCurveMeasurement:IVCurveMeasurementBase<GateSourceMeasurementInfoRow,GateSourceDataRow>
    {

        public TransferCurveMeasurement(IVMainViewModel viewModel)
            : base(viewModel,"Transfer characteristic measurement")
        {

        }

        protected override void DoMeasurement(object sender, DoWorkEventArgs e)
        {
            var bgw = (BackgroundWorker)sender;
            bool StopExperiment = false;
            int refreshEvery = 10;
            var count = 0;
            var counter = 0;
            var maxCount = _dsRangeHandler.TotalPoints * _gsRangeHandler.TotalPoints;
            var progressCalc = new Func<int, int>((c) => (int)Math.Floor(100.0 * c / maxCount));

            var dsEnumerator = _dsRangeHandler.GetEnumerator();
            while (dsEnumerator.MoveNext() && !StopExperiment)
            {
                var mea = new MeasurementData<GateSourceMeasurementInfoRow, GateSourceDataRow>(new GateSourceMeasurementInfoRow(String.Format("{0}_{1}", MeasurementName, MeasurementCount++), dsEnumerator.Current, "", MeasurementCount));

                mea.SuspendUpdate();
                mea.SetXYMapping(x => new Point(x.GateSourceVoltage, x.DrainCurrent));
                _vm.AddSeries(mea);

                _drainKeithley.SetSourceVoltage(dsEnumerator.Current);

                var gsEnumerator = _gsRangeHandler.GetEnumerator();
                while (dsEnumerator.MoveNext() && !StopExperiment)
                {
                    StopExperiment = bgw.CancellationPending;
                    if (StopExperiment)
                    {
                        e.Cancel = true; break;
                    }

                    if (count++ % refreshEvery == 0)
                    {
                        _vm.ExecuteInUIThread(() =>
                        {
                            mea.ResumeUpdate();
                            mea.SuspendUpdate();
                        });
                    }
                    
                    _gate_Keithley.SetSourceVoltage(gsEnumerator.Current);

                    double drainVolt, drainCurr, drainRes;
                    _drainKeithley.MeasureAll(out drainVolt, out drainCurr, out drainRes);
                    double gateVolt, gateCurr, gateRes;
                    _gate_Keithley.MeasureAll(out gateVolt, out gateCurr, out gateRes);

                    mea.Add(new GateSourceDataRow(gateVolt, drainCurr, gateCurr));// * Math.Log(dsEnumerator.Current), 0)); //);
                    _vm.ExecuteInUIThread(() => bgw.ReportProgress(progressCalc(counter++)));
                    System.Threading.Thread.Sleep(10);
                }

                _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
                EnqueueData(mea);
                //_writer.Write(mea);
                _vm.MeasurementCount++;

            }

        }

        protected override void SimulateMeasurement(object sender, DoWorkEventArgs e)
        {
            var bgw = (BackgroundWorker)sender;
            bool StopExperiment = false;

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
                var mea = new MeasurementData<GateSourceMeasurementInfoRow, GateSourceDataRow>(new GateSourceMeasurementInfoRow(String.Format("{0}_{1}", MeasurementName, MeasurementCount++), gEnumerator.Current, "", MeasurementCount));

                mea.SuspendUpdate();
                mea.SetXYMapping(x => new Point(x.GateSourceVoltage, x.DrainCurrent));
                _vm.AddSeries(mea);
                var dsEnumerator = _dsRangeHandler.GetEnumerator();
                while (dsEnumerator.MoveNext() && !StopExperiment)
                {
                    StopExperiment = bgw.CancellationPending;
                    if (StopExperiment)
                    {
                        e.Cancel = true; break;
                    }

                    if (count++ % exp == 0)
                    {
                        _vm.ExecuteInUIThread(() =>
                        {
                            mea.ResumeUpdate();
                            mea.SuspendUpdate();
                        });
                    }
                    var r = rand.NextDouble();

                    mea.Add(new GateSourceDataRow(dsEnumerator.Current, (r + gEnumerator.Current) * Math.Pow(dsEnumerator.Current, 2), 0));// * Math.Log(dsEnumerator.Current), 0)); //
                    _vm.ExecuteInUIThread(() => bgw.ReportProgress(progressCalculator(counter++)));
                    System.Threading.Thread.Sleep(10);
                }

                _vm.ExecuteInUIThread(() => mea.ResumeUpdate());
                EnqueueData(mea);
                //_writer.Write(mea);
                _vm.MeasurementCount++;

            }
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
