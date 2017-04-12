using DataVisualization.D3DataVisualization;
using ExperimentViewer;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using System;
using System.ComponentModel;
using System.Windows;
using IVexperiment.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Helper.StartStopControl;
using Helper.NewExperimentWindow;


namespace IVexperiment
{
  

    public abstract class IVMainViewModel : AbstractExperimentViewModel
    {
        public IVMainViewModel()
        {
            SetRangeViewModels(out _firstRangeViewModel, out _secondRangeViewModel);
            SetVisualization(out m_Visualization);
            //load settings from file
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

        public void AddSeries(IPointDataSource Points, string Description)
        {
            ExecuteInUIThread(() =>
            {
                if (Visualization != null)
                {
                    Visualization.AddLineGraph(Points, Description);
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
        //protected override string GetExperimentName()
        //{
        //    var d = new NewExperimentControl(ExperimentName);
        //    if (d.ShowDialog().Value)
        //        return d.ExperimentName;
        //    return String.Empty;
        //}
       
    }
}
