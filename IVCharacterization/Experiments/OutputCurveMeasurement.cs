using ExperimentAbstraction;
using ExperimentDataModel;
using IVCharacterization.DataModel;
using IVCharacterization.ViewModels;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.Experiments
{
    public class OutputCurveMeasurement : AbstractExperiment<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>
    {
        private OutputIVViewModel _vm;
        private IVMainView _control;
        //private MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow> _mea;

        public OutputCurveMeasurement():base("Output curve measurement")
        {
            _vm = new OutputIVViewModel();
            _control = new IVMainView();
            _control.DataContext = _vm;
            
            
            _control.ControlButtons.StartButtonPressed += ControlButtons_StartButtonPressed;
        }

        void ControlButtons_StartButtonPressed(object sender, System.Windows.RoutedEventArgs e)
        {
            Start();
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

        protected override void DoMeasurement(object sender, DoWorkEventArgs e)
        {
            for (int j = 0; j < 4; j++)
            {
                var _mea = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow(String.Format("asdda_{0}",j), 123, "", 1), new Func<DrainSourceDataRow, OxyPlot.DataPoint>((x) => new DataPoint(x.DrainSourceVoltage, x.DrainCurrent)));
                _vm.AddSeries(_mea);
                //_vm.Visualization.AddSeries(_mea);
                //throw new NotImplementedException();
                for (int i = 0; i < 100000; i++)
                {
                    _mea.Add(new DrainSourceDataRow(i, i*j , 0));

                    if (i % 1000 == 0)
                        _vm.InvalidatePlot(true);
                    //_vm.Visualization.CurrentPlotModel.InvalidatePlot(true);    
                    //System.Threading.Thread.Sleep(100);
                }
            }
            

        }

        public override object ViewModel
        {
            get { return _vm; }
        }

        public override System.Windows.Controls.UserControl Control
        {
            get { return _control; }
        }
    }
}
