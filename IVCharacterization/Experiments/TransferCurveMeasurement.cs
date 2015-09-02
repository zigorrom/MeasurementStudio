using ExperimentAbstraction;
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
    public class TransferCurveMeasurement : AbstractExperiment<GateSourceMeasurementInfoRow,GateSourceDataRow>
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
    }
}
