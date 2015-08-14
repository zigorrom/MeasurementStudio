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
        private MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow> _mea;

        public OutputCurveMeasurement():base("Output curve measurement")
        {
            _vm = new OutputIVViewModel();
            _control = new IVMainView();
            _control.DataContext = _vm;
            _mea = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow("asdda", 123, "", 1), new Func<DrainSourceDataRow, OxyPlot.DataPoint>((x) => new DataPoint(x.DrainSourceVoltage, x.DrainCurrent)));
            _control.ControlButtons.StartButtonPressed += ControlButtons_StartButtonPressed;
        }

        void ControlButtons_StartButtonPressed(object sender, System.Windows.RoutedEventArgs e)
        {
            Start();
        }

        public override void Start()
        {
            base.Start();
            _vm.Visualization.AddSeries(_mea);
           
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
            //throw new NotImplementedException();
            for (int i = 0; i < 1000; i++)
            {
                _mea.Add(new DrainSourceDataRow(i, i * 100, 0));
                _vm.Visualization.CurrentPlotModel.InvalidatePlot(true);    
                System.Threading.Thread.Sleep(100);
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
