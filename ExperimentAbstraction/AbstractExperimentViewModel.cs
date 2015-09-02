using Helper.ViewModelInterface;
using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Input;

namespace ExperimentAbstraction
{
    public abstract class AbstractExperimentViewModel : INotifyPropertyChanged, IUIThreadExecutableViewModel
    {
        #region PropertyEvents

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        private IExperiment _experiment;
        public virtual IExperiment Experiment
        {
            get
            {
                return _experiment;
            }
            protected set
            {
                SetField(ref _experiment, value, "IVExperiment");
            }
        }

        private bool _ExperimentIsRunning;
        public bool ExperimentIsRunning
        {
            get { return _ExperimentIsRunning; }
            set
            {
                SetField(ref _ExperimentIsRunning, value, "ExperimentIsRunning");
            }
        }

        private string _experimentName;
        public string ExperimentName
        {
            get { return _experimentName; }
            set { SetField(ref _experimentName, value, "ExperimentName"); }
        }

        private string _measurementName;
        public string MeasurementName
        {
            get { return _measurementName; }
            set { SetField(ref _measurementName, value, "MeasurementName"); }
        }

        private string _workingDirectory;
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
            set { SetField(ref _workingDirectory, value, "WorkingDirectory"); }
        }

        private int _measurementCount;
        public int MeasurementCount
        {
            get { return _measurementCount; }
            set { SetField(ref _measurementCount, value, "MeasurementCount"); }
        }

        private const string MeasurementName_MeasurementCount_Separator = "_";

        private ICommand _createNewExperiment;
        
        public ICommand CreateNewExperiment
        {
            get { return _createNewExperiment ?? (_createNewExperiment = new RelayCommand(() => {
                ExperimentName = GetExperimentName();
                ClearVisualization();
            })); }
        }
        protected abstract string GetExperimentName();


        private int _currentProgress;
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                SetField(ref _currentProgress, value, "CurrentProgress");
            }
        }


        private System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
        private ICommand _selectWorkingDirectory;
        public ICommand SelectWorkingDirectory
        {
            get
            {
                return _selectWorkingDirectory ?? (_selectWorkingDirectory = new RelayCommand(() =>
                {
                    //var fbd = new System.Windows.Forms.FolderBrowserDialog();
                    
                    if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        WorkingDirectory = fbd.SelectedPath;
                    }

                }));
            }
        }

        public AbstractExperimentViewModel()
        {
            InitExperiment();
            InitEventHandlers();
            ExperimentIsRunning = false;
        }
        protected abstract void InitExperiment();

        private void InitEventHandlers()
        {
            if (Experiment == null)
                throw new ArgumentNullException("Experiment not defined");
            Experiment.ExperimentStarted += ExperimentStartedHandler;
            Experiment.ExperimentPaused += ExperimentPausedHandler;
            Experiment.ExperimentStopped += ExperimentStoppedHandler;
            Experiment.ExperimentFinished += ExperimentFinishedHandler;
            Experiment.ExperimentProgressChanged += ExperimentProgressChangedHandler;
        }

        protected abstract bool CheckParametersBeforeStart(out string Message);
        
        protected abstract void ExperimentProgressChangedHandler(object sender, ProgressChangedEventArgs e);
        protected abstract void ExperimentFinishedHandler(object sender, EventArgs e);
        protected abstract void ExperimentStoppedHandler(object sender, EventArgs e);
        protected abstract void ExperimentPausedHandler(object sender, EventArgs e);
        protected abstract void ExperimentStartedHandler(object sender, EventArgs e);

        protected abstract void ClearVisualization();
        public async Task ExecuteInUIThreadAsync(Action action)
        {
            await Application.Current.Dispatcher.BeginInvoke(action, null);
        }
        public void ExecuteInUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public virtual void ErrorHandler(Exception e)
        {
            System.Diagnostics.Debug.WriteLine("Error occured:");
            System.Diagnostics.Debug.Write(e.ToString());
            System.Diagnostics.Debug.WriteLine("**************");

            MessageBox.Show(e.Message, "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }


    }
}
