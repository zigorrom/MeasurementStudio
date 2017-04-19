using ExperimentAbstraction;
using ExperimentAbstraction.HelperExecutables.TimeDelay;
using Helper.StartStopControl;
using Helper.ViewModelInterface;
using IVexperiment.ViewModels;
using ScenarioBuilder.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ExperimentViewer.ViewModels
{
    class testAction : INewExperiment
    {
        public testAction(string name)
        {
            this.Name = name;
        }
        

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Execute(object ExperimentStartObject, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void Execute(IProgress<ExecutionReport> progress, CancellationToken cancellationToken, PauseToken pauseToken)
        {
            ExecutionReport report = ExecutionReport.Empty;
            for (int i = 0; i < 100; i++)
            {
                pauseToken.WaitWhilePausedAsync().Wait();
                cancellationToken.ThrowIfCancellationRequested();
                report = new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = i, ExperimentProgressMessage = String.Format("{0} running normally...", Name) };
                progress.Report(report);
                Thread.Sleep(20);
            }
            //}catch(OperationCanceledException e)
            //{
            //    progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Aborted, ExperimentProgress = 0, ExperimentProgressMessage = "aborted" });
            //}
            //return report;
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public bool IsRunning
        {
            get { throw new NotImplementedException(); }
        }

        public ExecutionStatus Status
        {
            get { throw new NotImplementedException(); }
        }

        public event EventHandler<ExecutionStatus> StatusChanged;

        public event EventHandler ExecutionStarted;

        public event EventHandler ExecutionAborted;

        public event System.ComponentModel.ProgressChangedEventHandler ProgressChanged;

        public event EventHandler ExecutionFinished;


        public void InitializeExperiment()
        {
            throw new NotImplementedException();
        }

        public void InitializeInstruments()
        {
            throw new NotImplementedException();
        }

        public void OwnInstruments()
        {
            throw new NotImplementedException();
        }

        public void ReleaseInstruments()
        {
            throw new NotImplementedException();
        }

        public void FinalizeExperiment()
        {
            throw new NotImplementedException();
        }

        public bool SimulateExperiment
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object ViewModel
        {
            get { return Name; }
        }

        public string Name
        {
            get;
            private set;
        }

        public bool Equals(Instruments.IInstrumentOwner other)
        {
            throw new NotImplementedException();
        }
    }
    
    public class ExecutionViewModel : INotifyPropertyChanged, IUIThreadExecutableViewModel
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

        #region IUIThreadExecutableViewModel
        public async Task ExecuteInUIThreadAsync(Action action)
        {
            await Application.Current.Dispatcher.BeginInvoke(action, null);
        }
        public void ExecuteInUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }
        #endregion 
#region ExperimentNames
        private const string OUTPUT_IV = "Output I-V";
        private const string TRANSFER_IV = "Transfer I-V";
        private const string SCENARIO_EXPERIMENT= "Experiment scenario";

        private enum EXPERIMENT_NAME_ENUM
        {
            OUTPUT_IV,
            TRANSFER_IV,
            SCENARIO_EXPERIMENT
        }

#endregion

        public ExecutionViewModel()
        {
            _experimentList = new List<ExperimentMenuItemViewModel>();
            _experimentList.Add(new ExperimentMenuItemViewModel(OUTPUT_IV, this));
            _experimentList.Add(new ExperimentMenuItemViewModel(TRANSFER_IV, this));
            _experimentList.Add(new ExperimentMenuItemViewModel(SCENARIO_EXPERIMENT, this));

            //ExperimentExecutionManager = new SequentialTaskExecutionManager();
            ////ExperimentExecutionManager.Add(new testAction("test1"));
            
            //var td = new TimeDelayExecutableViewModel();
            //td.Delay = TimeSpan.FromMilliseconds(2000);
            
            //ExperimentExecutionManager.Add(td.DelayExecutable);
            ////ExperimentExecutionManager.Add(new testAction("test2"));
            ////ExperimentExecutionManager.Add(td.DelayExecutable);
            ////ExperimentExecutionManager.Add(new testAction("test3"));

            //var transfet_iv = new OutputIVViewModel ();
            //transfet_iv.IVSettingsViewModel.SimulationMode = true;
            //transfet_iv.DrainSourceRangeViewModel.Start.NumericValue = 0 ;
            //transfet_iv.DrainSourceRangeViewModel.End.NumericValue = 10;
            //transfet_iv.DrainSourceRangeViewModel.PointsCount.PointsCount = 201;

            //transfet_iv.GateSourceRangeViewModel.Start.NumericValue = 0;
            //transfet_iv.GateSourceRangeViewModel.End.NumericValue = 10;
            //transfet_iv.GateSourceRangeViewModel.PointsCount.PointsCount = 21;

            //transfet_iv.SelectWorkingDirectory.Execute(new object());
            //transfet_iv.MeasurementName= "test_fajhsfjkahflfs";
            //transfet_iv.ExperimentName = "test";

            //ExperimentExecutionManager.Add(transfet_iv.IExperiment);
            //ExperimentExecutionManager.Add(td.DelayExecutable);
            //ExperimentExecutionManager.Add(transfet_iv.IExperiment);

            //var a = new ScenarioBuilder.MainWindow();
            //a.ShowDialog();

            ExperimentControlButtons = new ControlButtonsViewModel();
            ExperimentControlButtons.PauseCommandRaised += ExperimentControlButtons_PauseCommandRaised;
            ExperimentControlButtons.StartCommandRaised += ExperimentControlButtons_StartCommandRaised;
            ExperimentControlButtons.StopCommandRaised += ExperimentControlButtons_StopCommandRaised;
            GlobalIsEnabled = true;
            ExperimentIsRunning = false;
            ExperimentIsPaused = false;

            //InitEventHandlers();
        }

        private void InitEventHandlers()
        {
            if (ExperimentExecutionManager == null)
                ExperimentExecutionManager = new SequentialTaskExecutionManager();
            ExperimentExecutionManager.ExecutionLoopStarted += ExperimentExecutionManager_ExecutionLoopStarted;
            ExperimentExecutionManager.ExecutionLoopFinished += ExperimentExecutionManager_ExecutionLoopFinished;
            ExperimentExecutionManager.ExecutionProgressChanged += ExperimentExecutionManager_ExecutionProgressChanged;
            ExperimentExecutionManager.NewExecutableStarted += ExperimentExecutionManager_NewExecutableStarted;
           
        }

        private void InitExecutionManagerEventHandlers(IExecutionManager manager)
        {
            manager.ExecutionLoopStarted += ExperimentExecutionManager_ExecutionLoopStarted;
            manager.ExecutionLoopFinished += ExperimentExecutionManager_ExecutionLoopFinished;
            manager.ExecutionProgressChanged += ExperimentExecutionManager_ExecutionProgressChanged;
            manager.NewExecutableStarted += ExperimentExecutionManager_NewExecutableStarted;
        }

        public void OpenExperiment(string ExperimentName)
        {
            this.MessageHandler(ExperimentName);
            if(SCENARIO_EXPERIMENT!=ExperimentName)
            {
                InitSingleTaskExecution(ExperimentName);
            }
            else
            {
                InitSequencialTaskExecution();
            }
        }

        private void InitSingleTaskExecution(string ExperimentName)
        {
            switch (ExperimentName)
            {
                case OUTPUT_IV:
                    {
                        var experimentVM = new OutputIVViewModel();
                        experimentVM.GlobalIsEnabled = true;
                        var executionManager = new SingleTaskExecutionManager();
                        InitExecutionManagerEventHandlers(executionManager);
                        executionManager.CurrentExecutable = experimentVM.IExperiment;
                        ExecuteInUIThread(() => CurrentExperimentViewModel = experimentVM);
                        ExperimentExecutionManager = executionManager;
                    }break;
                case TRANSFER_IV:
                    {
                        var experimentVM = new TransfrerIVViewModel();
                        experimentVM.GlobalIsEnabled = true;
                        var executionManager = new SingleTaskExecutionManager();
                        InitExecutionManagerEventHandlers(executionManager);
                        executionManager.CurrentExecutable = experimentVM.IExperiment;
                        ExecuteInUIThread(() => CurrentExperimentViewModel = experimentVM);
                        ExperimentExecutionManager = executionManager;
                    }break;
                default:
                    break;
                    
            }
        }

        private void InitSequencialTaskExecution()
        {
            var scenarioBuilderVM = new ScenarioBuilderViewModel();
            var executionManager = new SequentialTaskExecutionManager();
            InitExecutionManagerEventHandlers(executionManager);
            ExecuteInUIThread(()=>CurrentExperimentViewModel = scenarioBuilderVM);
            ExperimentExecutionManager = executionManager;
        }

        //https://www.codeproject.com/Articles/37848/WPF-Data-Bound-Menus
        private List<ExperimentMenuItemViewModel> _experimentList;
        public IEnumerable<ExperimentMenuItemViewModel> ExperimentList
        {
            get { return _experimentList; }
        }

       


        void ExperimentExecutionManager_NewExecutableStarted(object sender, IExecutable e)
        {
            //MessageHandler("New executable started");
            if (e is INewExperiment)
            {
                ExecuteInUIThread(()=> CurrentExperimentViewModel = ((INewExperiment)e).ViewModel);
                
            }
        }

        void ExperimentExecutionManager_ExecutionProgressChanged(object sender, ExecutionReport e)
        {
            CurrentProgress = e.ExperimentProgress;
            CurrentStatus = e.ExperimentProgressMessage;
        }

        void ExperimentExecutionManager_ExecutionLoopFinished(object sender, EventArgs e)
        {
            MessageHandler("ExperimentCompleted");
            ExperimentControlButtons.Reset();
            CurrentProgress = 0;
            CurrentStatus = String.Empty;
            ExperimentIsRunning = false;
        }

        void ExperimentExecutionManager_ExecutionLoopStarted(object sender, EventArgs e)
        {
            ExperimentIsRunning = true;   
        }

        public IExecutionManager ExperimentExecutionManager { get; private set; }
        
        //public SingleTaskExecutionManager SingleExperimentExecutionManager { get; private set; }
        //public SequentialTaskExecutionManager SequencialExperimentExecutionManager { get; private set; }
        
        public ControlButtonsViewModel ExperimentControlButtons { get; private set; }
        
        /// <summary>
        /// Global is enabled
        /// </summary>
        private bool m_globalIsEnabled;
        public bool GlobalIsEnabled
        {
            get { return m_globalIsEnabled; }
            set
            {
                SetField(ref m_globalIsEnabled, value, "GlobalIsEnabled");
            }
        }

        /// <summary>
        /// Experimenr Is running
        /// </summary>
        private bool _ExperimentIsRunning;
        public bool ExperimentIsRunning
        {
            get { return _ExperimentIsRunning; }
            set
            {
                SetField(ref _ExperimentIsRunning, value, "ExperimentIsRunning");
            }
        }

        /// <summary>
        /// Experiment is paused
        /// </summary>
        private bool _ExperimentIsPaused;
        public bool ExperimentIsPaused
        {
            get { return _ExperimentIsPaused; }
            set { SetField(ref _ExperimentIsPaused, value, "ExperimentIsPaused"); }
        }

        /// <summary>
        /// Current experiment progress
        /// </summary>
        private int _currentProgress;
        public int CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                SetField(ref _currentProgress, value, "CurrentProgress");
            }
        }

        private string  _currentStatus;
        public string  CurrentStatus
        {
            get { return _currentStatus; }
            set { SetField(ref _currentStatus, value, "CurrentStatus"); }
        }

        private object _currentExperimentViewModel;
        public object CurrentExperimentViewModel
        {
            get { return _currentExperimentViewModel; }
            set { SetField(ref _currentExperimentViewModel, value, "CurrentExperimentViewModel"); }
        }

        

        protected bool CheckParametersBeforeStart(out string Message)
        {
            Message = String.Empty;
            //var res = true;
            //if (Experiment.IsRunning)
            //{
            //    Message = "Experiment is running";
            //    return false;
            //}
            //if (String.IsNullOrEmpty(ExperimentName))
            //{
            //    Message = "Fill in the experiment name";
            //    return false;
            //}
            //if (String.IsNullOrEmpty(MeasurementName))
            //{
            //    Message = "Fill in the measurement name";
            //    return false;
            //}
            var expExecutor = ExperimentExecutionManager as SequentialTaskExecutionManager;
            var scenarioBuilderViewModel = CurrentExperimentViewModel as ScenarioBuilderViewModel;
            if(expExecutor != null && scenarioBuilderViewModel!=null)
            {
                expExecutor.Clear();
                foreach (var item in scenarioBuilderViewModel.ScenarioExperimentsList)
                {
                    if (item.ViewModel is IExecutableViewModel)
                        expExecutor.Add(((IExecutableViewModel)item.ViewModel).Executable);
                }
            }
            else
            {
                Message = "Error with serial executor or scenario builder";
                return false;
            }
            return true;
        }

        void ExperimentControlButtons_StopCommandRaised(object sender, EventArgs e)
        {
            ExecuteInUIThread(() => GlobalIsEnabled = true);
            //Experiment.Abort();
            ExperimentExecutionManager.Abort();
            ExperimentControlButtons.Reset();
            ExperimentIsRunning = false;
            ExperimentIsPaused = false;
        }

        void ExperimentControlButtons_StartCommandRaised(object sender, EventArgs e)
        {
            string Message = String.Empty;
            if (CheckParametersBeforeStart(out Message))
            {
                //ExperimentIsRunning = true;
                ExecuteInUIThread(() => GlobalIsEnabled = false);
                ExperimentIsRunning = true;
                ExperimentIsPaused = false;
                ExperimentExecutionManager.Start();
            }
            else
            {
                ExperimentControlButtons.Reset();
                //MessageHandler(Message);
                //MessageBox.Show(Message);
            }
        }

        void ExperimentControlButtons_PauseCommandRaised(object sender, EventArgs e)
        {
            ExecuteInUIThread(() => GlobalIsEnabled = true);
            if (ExperimentIsPaused)
            {
                ExperimentIsPaused = false;
                ExperimentExecutionManager.Resume();
            }
            else
            {
                ExperimentIsPaused = true;
                ExperimentExecutionManager.Pause();
            } 
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
