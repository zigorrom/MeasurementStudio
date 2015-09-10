using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using System;
using System.ComponentModel;
using System.Windows;
using IVCharacterization.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Helper.StartStopControl;


namespace IVCharacterization
{
  

    public abstract class IVMainViewModel : AbstractExperimentViewModel
    {
        public IVMainViewModel()
        {
            SetRangeViewModels(out _firstRangeViewModel, out _secondRangeViewModel);
            SetVisualization(out m_Visualization);
            IVSettingsViewModel = new IVexpSettingsViewModel();
        }


        private D3VisualizationViewModel m_Visualization;
        public D3VisualizationViewModel Visualization
        {
            get { return m_Visualization; }
            private set
            {
                SetField(ref m_Visualization, value, "Visualization");
            }
        }

        public void AddSeries(IPointDataSource Points)
        {
            ExecuteInUIThread(() =>
            {
                if (Visualization != null)
                {
                    Visualization.AddLineGraph(Points);
                }
            });
            
        }

        private RangeViewModel _firstRangeViewModel;
        public RangeViewModel FirstRangeViewModel
        {
            get { return _firstRangeViewModel; }
            set { _firstRangeViewModel = value; }
        }

        private RangeViewModel _secondRangeViewModel;

        public RangeViewModel SecondRangeViewModel
        {
            get { return _secondRangeViewModel; }
            set { _secondRangeViewModel = value; }
        }

        public IVexpSettingsViewModel IVSettingsViewModel { get; set; }
        protected abstract void SetRangeViewModels(out RangeViewModel vm1, out RangeViewModel vm2);
        protected abstract void SetVisualization(out D3VisualizationViewModel visualVM);
        
        
        protected override void ClearVisualization()
        {
            ExecuteInUIThread(() => Visualization.Clear());
        }
        protected override string GetExperimentName()
        {
            var d = new IVCharacterization.Views.NewExperiment(ExperimentName);
            if (d.ShowDialog().Value)
                return d.ExperimentName;
            return String.Empty;
        }
       
    }
}
