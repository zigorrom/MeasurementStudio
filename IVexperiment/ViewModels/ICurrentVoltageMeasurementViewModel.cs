using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges.SimpleRangeControl;
using Helper.ViewModelInterface;
using IVexperiment.ViewModels;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVexperiment
{
    public interface ICurrentVoltageMeasurementViewModel : IUIThreadExecutableViewModel, IEnableControllableViewModel//, IExperimentViewModel
    {
        void AddSeries(IPointDataSource Points, string Description);
        VoltageRangeViewModel DrainSourceRangeViewModel { get; set; }
        IVexpSettingsViewModel IVSettingsViewModel { get; set; }
        VoltageRangeViewModel GateSourceRangeViewModel { get; set; }
        D3VisualizationViewModel Visualization { get; }
        string ExperimentName { get; set; }
        int MeasurementCount { get; set; }
        string MeasurementName { get; set; }
        string WorkingDirectory { get; set; }
        void ErrorHandler(Exception e);
        void MessageHandler(string e);
    }
}
