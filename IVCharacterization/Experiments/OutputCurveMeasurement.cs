using ExperimentAbstraction;
using ExperimentDataModel;
using Helper.Ranges.RangeHandlers;
using IVCharacterization.DataModel;
using IVCharacterization.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace IVCharacterization.Experiments
{
    public class OutputCurveMeasurement : AbstractExperiment<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>
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
    }
}
