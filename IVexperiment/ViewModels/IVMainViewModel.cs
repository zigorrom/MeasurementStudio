using DataVisualization.D3DataVisualization;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using System;
using System.ComponentModel;
using System.Windows;
using IVexperiment.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Helper.StartStopControl;
using Helper.NewExperimentWindow;
using ExperimentAbstraction.ViewModels;
using ExperimentAbstraction;


namespace IVexperiment
{
    public interface ICurrentVoltageMeasurementViewModel
    {
        void AddSeries(IPointDataSource Points, string Description);
        RangeViewModel DrainSourceRangeViewModel { get; set; }
        IVexpSettingsViewModel IVSettingsViewModel { get; set; }
        RangeViewModel GateSourceRangeViewModel { get; set; }
        D3VisualizationViewModel Visualization { get; }
        string ExperimentName { get; set; }
        int MeasurementCount { get; set; }
        string MeasurementName { get; set; }
        string WorkingDirectory { get; set; }
        void ErrorHandler(Exception e);
        void MessageHandler(string e);
    }
    

    public abstract class IVMainViewModel<ExperimentType> : NewAbstractExperimentViewModel<ExperimentType>, ICurrentVoltageMeasurementViewModel
        where ExperimentType:INewExperiment
    {
        public IVMainViewModel()
        {
            SetRangeViewModels(out _drainSourceRangeViewModel, out _gateSourceRangeViewModel);
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

        private RangeViewModel _drainSourceRangeViewModel;
        public RangeViewModel DrainSourceRangeViewModel
        {
            get { return _drainSourceRangeViewModel; }
            set { _drainSourceRangeViewModel = value; }
        }

        private RangeViewModel _gateSourceRangeViewModel;

        public RangeViewModel GateSourceRangeViewModel
        {
            get { return _gateSourceRangeViewModel; }
            set { _gateSourceRangeViewModel = value; }
        }

        public IVexpSettingsViewModel IVSettingsViewModel { get; set; }
        protected abstract void SetRangeViewModels(out RangeViewModel DrainSourceRangeViewModel, out RangeViewModel GateSourceRangeViewModel);
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
