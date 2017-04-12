using CVCharacterization.Experiments;
using DataVisualization.D3DataVisualization;
using ExperimentViewer;
using Helper.NewExperimentWindow;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using Helper.StartStopControl;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.ViewModels
{
    public abstract class CVViewModelBase : AbstractExperimentViewModel
    {
        public CVViewModelBase()
        {
            SetRangeViewModels(out _firstRangeViewModel, out _secondRangeViewModel);
            SetVisualization(out _visualization);
            SettingsViewModel = new CVSettingsViewModel();
            //ExperimentControlButtons = new ControlButtonsViewModel();
        }

        protected abstract void SetRangeViewModels(out RangeViewModel vm1, out RangeViewModel vm2);
        protected abstract void SetVisualization(out D3VisualizationViewModel visualVM);
        public CVSettingsViewModel SettingsViewModel { get; set; }


        private RangeViewModel _firstRangeViewModel;
        public RangeViewModel FirstRangeViewModel
        {
            get { return _firstRangeViewModel; }
            set { SetField(ref _firstRangeViewModel, value,"FirstRangeViewModel"); }
        }

        private RangeViewModel _secondRangeViewModel;
        public RangeViewModel SecondRangeViewModel
        {
            get { return _secondRangeViewModel; }
            set { SetField(ref _secondRangeViewModel, value, "SecondRangeViewModel"); }
        }

        private D3VisualizationViewModel _visualization;
        public D3VisualizationViewModel Visualization
        {
            get { return _visualization; }
            set { SetField(ref _visualization, value, "Visualization"); }
        }

        public void AddSeries(IPointDataSource Points, string Description)
        {
            ExecuteInUIThread(() =>
                {
                    if(Visualization!=null)
                    {
                        Visualization.AddLineGraph(Points,Description);
                    }
                });
        }
        
        //protected override string GetExperimentName()
        //{
        //    var d = new NewExperimentControl(ExperimentName);
        //    if (d.ShowDialog().Value)
        //        return d.ExperimentName;
        //    return String.Empty;
        //}

        protected override void ClearVisualization()
        {
            ExecuteInUIThread(() => Visualization.Clear());
        }
    }
}
