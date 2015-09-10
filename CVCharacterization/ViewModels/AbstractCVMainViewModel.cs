using CVCharacterization.Experiments;
using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
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
    public abstract class AbstractCVMainViewModel : AbstractExperimentViewModel
    {
        public AbstractCVMainViewModel()
        {
            SetRangeViewModels(out _firstRangeViewModel, out _secondRangeViewModel);
            SetVisualization(out _visualization);
            
            //ExperimentControlButtons = new ControlButtonsViewModel();
        }

        protected abstract void SetRangeViewModels(out RangeViewModel vm1, out RangeViewModel vm2);
        protected abstract void SetVisualization(out D3VisualizationViewModel visualVM);

        private RangeViewModel _firstRangeViewModel;
        public RangeViewModel VoltageRange
        {
            get { return _firstRangeViewModel; }
            set { _firstRangeViewModel = value; }
        }

        private RangeViewModel _secondRangeViewModel;
        public RangeViewModel FrequencyRange
        {
            get { return _secondRangeViewModel; }
            set { _secondRangeViewModel = value; }
        }

        private D3VisualizationViewModel _visualization;
        public D3VisualizationViewModel Visualization
        {
            get { return _visualization; }
            set { _visualization = value; }
        }

        public void AddSeries(IPointDataSource Points)
        {
            ExecuteInUIThread(() =>
                {
                    if(Visualization!=null)
                    {
                        Visualization.AddLineGraph(Points);
                    }
                });
        }
        
        protected override string GetExperimentName()
        {
            var d = new CVCharacterization.Views.NewExperiment(ExperimentName);
            if (d.ShowDialog().Value)
                return d.ExperimentName;
            return String.Empty;
        }

        protected override void ClearVisualization()
        {
            ExecuteInUIThread(() => Visualization.Clear());
        }
    }
}
