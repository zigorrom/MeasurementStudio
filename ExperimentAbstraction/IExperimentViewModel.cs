using System;
namespace ExperimentAbstraction
{
    public interface IExperimentViewModel : IEnableControllableViewModel
    {
        //void AddSeries(Microsoft.Research.DynamicDataDisplay.DataSources.IPointDataSource Points, string Description);
        
        
        string WorkingDirectory { get; set; }
        string ExperimentName { get; set; }
        string MeasurementName { get; set; }
        int MeasurementCount { get; set; }
        INewExperiment IExperiment { get; }
        
        
        void ErrorHandler(Exception e);
        void MessageHandler(string e);
    }
}
