using ExperimentAbstraction;
using ExperimentDataModel;
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
        //private IVMainView _control;
        private List<MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>> _meaList;
        

        public OutputCurveMeasurement(IVMainViewModel viewModel):base("Output curve measurement")
        {
            _vm = viewModel;
            
            _meaList = new List<MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>>();
            
        }

        void ControlButtons_StopButtonPressed(object sender, System.Windows.RoutedEventArgs e)
        {
            Abort();
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
            //for (int i = 0; i < _meaList.Count; i++)
            //{
            //    //_meaList[i].DisplayFunc = new Func<DrainSourceDataRow, DataPoint>((x) => new DataPoint(x.DrainSourceVoltage, Math.Log(x.DrainCurrent)));
            //    //_vm.InvalidatePlot();
            //}
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


        protected override async void DoMeasurement(object sender, DoWorkEventArgs e)
        {
            _meaList.Clear();
            for (int j = 0; j < 4; j++)
            {
                var _mea = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow(String.Format("asdda_{0}", j), 123, "", 1));//, new Func<DrainSourceDataRow, Point>((x) => new Point(x.DrainSourceVoltage, x.DrainCurrent)));
                var _mea2 = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(new DrainSourceMeasurmentInfoRow(String.Format("asdda_{0}", j), 123, "", 1));//, new Func<DrainSourceDataRow, Point>((x) => new Point(x.DrainSourceVoltage, x.DrainCurrent)));

                _mea.SetXYMapping(x => new Point(x.DrainSourceVoltage, x.DrainCurrent));
                _mea2.SetXYMapping(x => new Point(x.DrainSourceVoltage, x.DrainCurrent));
                _vm.AddSeries(_mea);
                _vm.AddSeries(_mea2);
                int exp = 1000;
                for (int i = 1; i < 10000; i++)
                {
                    if (i % exp == 0)
                    {
                        _vm.ExecuteInUIThread(() =>
                        {
                            _mea.ResumeUpdate();
                            _mea2.ResumeUpdate();
                            _mea.SuspendUpdate();
                            _mea2.SuspendUpdate();
                        });
                    }
                    _vm.ExecuteInUIThread(() =>
                    {
                        _mea.Add(new DrainSourceDataRow(i, j * Math.Log(i), 0));
                        _mea2.Add(new DrainSourceDataRow(i, (j + 0.2) * Math.Log(i), 0));
                    });
                }


            }
            

        }

        public override object ViewModel
        {
            get { return null; }//_vm; }
        }

        public override System.Windows.Controls.UserControl Control
        {
            get { return null; }
        }

        public override void CleanExperiment()
        {
            throw new NotImplementedException();
        }
    }
}
