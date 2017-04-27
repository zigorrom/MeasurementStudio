using System;
using System.ComponentModel;
using System.Windows.Input;
namespace ExperimentAbstraction
{
    public interface IExperimentDataContext : INotifyPropertyChanged
    {
        string ExperimentName { get; set; }
        int MeasurementCount { get; set; }
        string MeasurementName { get; set; }
        string WorkingDirectory { get; set; }
        ICommand CreateNewExperiment { get;}
        ICommand SelectWorkingDirectory { get; }
        ICommand OpenWorkingDirectory { get;}

    }

    public class ExperimentDataContextChangedEventArgs:EventArgs
    {
        public ExperimentDataContextChangedEventArgs()
        {

        }

        public IExperimentDataContext OldExperimentDataContext { get; set; }
        public IExperimentDataContext NewExperimentDataContext { get; set; }

    }

    public interface IExperimentDataContextAcceptor
    {
        bool UseExperimentDataContext { get; set; }
        IExperimentDataContext ExperimentDataContext { get; set; }
        event EventHandler<ExperimentDataContextChangedEventArgs> ExperimentDataContextChanged;
    }

}
