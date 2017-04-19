﻿using System;
namespace ExperimentAbstraction
{
    public interface IExperimentViewModel
    {
        void AddSeries(Microsoft.Research.DynamicDataDisplay.DataSources.IPointDataSource Points, string Description);
        
        
        string WorkingDirectory { get; set; }
        string ExperimentName { get; set; }
        string MeasurementName { get; set; }
        int MeasurementCount { get; set; }
        
        
        void ErrorHandler(Exception e);
        void MessageHandler(string e);
    }
}
