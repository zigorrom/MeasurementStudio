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
    using Helper.AbstractPropertyChangedClass;
    using Helper.NewExperimentWindow;
    using Microsoft.TeamFoundation.MVVM;
    using System.Windows;
    using System.Windows.Input;
    using System.Xml.Serialization;

    [Serializable()]
    public abstract class NewAbstractExperimentViewModel<ExperimentType> : AbstractNotifyPropertyChangedClass, INotifyPropertyChanged, IUIThreadExecutableViewModel, IEnableControllableViewModel, IExperimentViewModel, IExecutableViewModel, IExperimentDataContextAcceptor
        where ExperimentType : INewExperiment
    {
        #region PropertyEvents Inherited from AbstractNotifyPropertyChangedClass
        // Inherited from AbstractNotifyPropertyChangedClass
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        //{
        //    if (EqualityComparer<ST>.Default.Equals(field, value))
        //        return false;
        //    field = value;
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}
        //private void OnPropertyChanged(string PropertyName)
        //{   
        //    var handler = PropertyChanged;
        //    if (handler != null)
        //        handler(this, new PropertyChangedEventArgs(PropertyName));
        //}
        #endregion

        public NewAbstractExperimentViewModel()
        {
            InitExperiment(out _experiment);
        }

        // Maybe use the ExperimentDataContext event in order to define actions which sould be used 

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

        //private bool SetExperimentDataContextProperty(object value, object target, string PropertyName)
        //{
        //    if (UseExperimentDataContext)
        //    {
        //        try
        //        {
        //            if (target != null)
        //            {
        //                var prop = target.GetType().GetProperty(PropertyName);
        //                prop.SetValue(target, value);
        //                return true;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            return false;
        //        }
        //    }
        //    return false;
        //}

        private bool GetExperimentDataContextProperty<T>(object target, out T value, string PropertyName)
        {
            value = default(T);
            if (UseExperimentDataContext)
                if (target != null)
                {
                    try
                    {
                        var prop = target.GetType().GetProperty(PropertyName);
                        value = (T)prop.GetValue(target);
                        return true;
                    }
                    catch (Exception e)
                    {
                        return false;
                    }
                }
            return false;

        }

        private string _experimentName;
        private const string ExperimentNamePropertyName = "ExperimentName";
        public string ExperimentName
        {
            get
            {
                string temp;
                if (GetExperimentDataContextProperty<string>(ExperimentDataContext, out temp, ExperimentNamePropertyName))
                    return temp;
                return _experimentName;
            }
            set
            {
                //if (!SetExperimentDataContextProperty(value, ExperimentDataContext, ExperimentNamePropertyName))
                SetField(ref _experimentName, value, ExperimentNamePropertyName);
            }
        }

        private string _measurementName;
        private const string MeasurementNamePropertyName = "MeasurementName";
        public string MeasurementName
        {
            get
            {
                string temp;
                if (GetExperimentDataContextProperty<string>(ExperimentDataContext, out temp, MeasurementNamePropertyName))
                    return temp;
                return _measurementName;
            }
            set
            {
                //if (!SetExperimentDataContextProperty(value, ExperimentDataContext, MeasurementNamePropertyName))
                SetField(ref _measurementName, value, MeasurementNamePropertyName);
            }
        }

        private string _workingDirectory;
        private const string WorkingDirectoryPropertyName = "WorkingDirectory";
        public string WorkingDirectory
        {
            get
            {
                string temp;
                if (GetExperimentDataContextProperty<string>(ExperimentDataContext, out temp, WorkingDirectoryPropertyName))
                    return temp;
                return _workingDirectory;
            }
            set
            {
                //if (!SetExperimentDataContextProperty(value, ExperimentDataContext, WorkingDirectoryPropertyName))
                SetField(ref _workingDirectory, value, WorkingDirectoryPropertyName);
            }
        }

        private int _measurementCount;
        private const string MeasurementCountPropertyName = "MeasurementCount";
        public int MeasurementCount
        {
            get
            {
                int temp;
                if (GetExperimentDataContextProperty(ExperimentDataContext, out temp, MeasurementCountPropertyName))
                    return temp;
                return _measurementCount;
            }
            set
            {
                //if (!SetExperimentDataContextProperty(value, ExperimentDataContext, MeasurementCountPropertyName))
                SetField(ref _measurementCount, value, MeasurementCountPropertyName);
            }
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





        private bool _useExperimentDataContext;
        public bool UseExperimentDataContext
        {
            get { return _useExperimentDataContext; }
            set { SetField(ref _useExperimentDataContext, value, "UseExperimentDataContext"); }
        }

        private IExperimentDataContext _experimentDataContext;
        public IExperimentDataContext ExperimentDataContext
        {
            get { return _experimentDataContext; }
            set
            {
                var previousDataContext = ExperimentDataContext;
                if (SetField(ref _experimentDataContext, value, "ExperimentDataContext"))
                {
                    OnExperimentDataContextChanged(this, new ExperimentDataContextChangedEventArgs() { OldExperimentDataContext = previousDataContext, NewExperimentDataContext = ExperimentDataContext });
                    ExperimentDataContext.PropertyChanged += OnPropertyChanged;
                    //refresh all bindings
                    //OnPropertyChanged(String.Empty);
                }
            }
        }

       

        public event EventHandler<ExperimentDataContextChangedEventArgs> ExperimentDataContextChanged;
        private void OnExperimentDataContextChanged(object sender, ExperimentDataContextChangedEventArgs e)
        {
            var handler = ExperimentDataContextChanged;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

    }
}
