using Helper.ViewModelInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExperimentAbstraction.ViewModels
{
    using ExperimentAbstraction;
    
    using Helper.NewExperimentWindow;
    using Microsoft.TeamFoundation.MVVM;
    using System.Windows;
    using System.Windows.Input;
    using System.Xml.Serialization;
    
    [Serializable()]
    public abstract class NewAbstractExperimentViewModel<ExperimentType> : INotifyPropertyChanged, IUIThreadExecutableViewModel, IEnableControllableViewModel, IExperimentViewModel, IExecutableViewModel
        where ExperimentType: INewExperiment
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

        public NewAbstractExperimentViewModel()
        {
            InitExperiment(out _experiment);
        }

        private ExperimentType _experiment;

         [XmlIgnoreAttribute]
        public ExperimentType Experiment
        {
            get { return _experiment; }
            private set { SetField<ExperimentType>(ref _experiment, value, "Experiment"); }
        }

        public INewExperiment IExperiment
        {
            get { return _experiment; }
        }


        public IExecutable Executable
        {
            get { return _experiment; }
        }

        private bool m_globalIsEnabled;
        public bool GlobalIsEnabled
        {
            get { return m_globalIsEnabled; }
            set
            {
                SetField(ref m_globalIsEnabled, value, "GlobalIsEnabled");
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


        protected abstract void InitExperiment(out ExperimentType experiment);


        protected abstract void ClearVisualization();

        #region Commands
        private ICommand _createNewExperiment;

        public ICommand CreateNewExperiment
        {
            get
            {
                return _createNewExperiment ?? (_createNewExperiment = new RelayCommand(() =>
                {
                    ExperimentName = GetExperimentName();
                    ClearVisualization();
                }));
            }
        }
        private string GetExperimentName()
        {
            var d = new NewExperimentControl(ExperimentName);
            if (d.ShowDialog().Value)
                return d.ExperimentName;
            return String.Empty;
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

        private ICommand _openWorkingDirectory;
        public ICommand OpenWorkingDirectory
        {
            get
            {
                return _openWorkingDirectory ?? (_openWorkingDirectory = new RelayCommand((o) =>
                {
                    try
                    {
                        System.Diagnostics.Process.Start(WorkingDirectory);
                    }
                    catch (Exception ex)
                    {
                        ErrorHandler(ex);
                    }

                }, (o) =>
                {
                    return !String.IsNullOrEmpty(WorkingDirectory);
                }));
            }
        }

        #endregion






        public void ExecuteInUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public async Task ExecuteInUIThreadAsync(Action action)
        {
            await Application.Current.Dispatcher.BeginInvoke(action, null);
        }

        public virtual void ErrorHandler(Exception e)
        {
            System.Diagnostics.Debug.WriteLine("Error occured:");
            System.Diagnostics.Debug.Write(e.ToString());
            System.Diagnostics.Debug.WriteLine("**************");

            MessageBox.Show(e.Message, "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public virtual void MessageHandler(string e)
        {
            System.Diagnostics.Debug.WriteLine("Message arrived:");
            System.Diagnostics.Debug.Write(e);
            System.Diagnostics.Debug.WriteLine("**************");

            MessageBox.Show(e, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }




        
    }
}
