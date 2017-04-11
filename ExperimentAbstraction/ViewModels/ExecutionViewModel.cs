using ExperimentAbstraction.HelperExecutables.TimeDelay;
using Helper.StartStopControl;
using Helper.ViewModelInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ExperimentAbstraction
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
                Thread.Sleep(100);
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
    public class TestTimeDelayExecutable : INewExperiment
    {
        public TestTimeDelayExecutable()
        {
            StopwatchObj = new Stopwatch();
        }

        public void Execute(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {
            OnExecutionStarted(this, EventArgs.Empty);
            StopwatchObj.Start();

            var CurrentTimeSpan = TimeSpan.Zero;
            var LastTimespan = TimeSpan.Zero;
            do
            {
                pauseToken.WaitWhilePausedAsync().Wait();
                cancellationToken.ThrowIfCancellationRequested();
                CurrentTimeSpan = StopwatchObj.Elapsed;
                if ((CurrentTimeSpan - LastTimespan).TotalMilliseconds > 1)
                {
                    progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = CurrentTimeSpan.Milliseconds, ExperimentProgressMessage = "Waiting..." });
                    LastTimespan = CurrentTimeSpan;
                }
                //CurrentTimeSpan = LastTimespan;
            } while (CurrentTimeSpan.TotalMilliseconds < TimeDelay);
            progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Done, ExperimentProgress = CurrentTimeSpan.Milliseconds, ExperimentProgressMessage = "Ready!" });
            StopwatchObj.Reset();
            OnExecutionFinished(this, EventArgs.Empty);

        }

        public int TimeDelay { get; set; }

        private System.Diagnostics.Stopwatch StopwatchObj { get; set; }

        public bool IsRunning
        {
            get { throw new NotImplementedException(); }
        }

        public ExecutionStatus Status
        {
            get { throw new NotImplementedException(); }
        }

        #region events
        public event EventHandler<ExecutionStatus> StatusChanged;
        private void OnStatusChanged(object sender, ExecutionStatus status)
        {
            var handler = StatusChanged;
            if (handler != null)
            {
                handler(sender, status);
            }
        }

        public event EventHandler ExecutionStarted;

        protected virtual void OnExecutionStarted(object sender, EventArgs e)
        {
            var handler = ExecutionStarted;
            if (handler != null)
                handler(sender, e);
        }

        public event EventHandler ExecutionAborted;
        protected virtual void OnExecutionAborted(object sender, EventArgs e)
        {
            var handler = ExecutionAborted;
            if (handler != null)
                handler(sender, e);
        }


        public event ProgressChangedEventHandler ProgressChanged;
        protected virtual void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var handler = ProgressChanged;
            if (handler != null)
                handler(sender, e);
        }

        public event EventHandler ExecutionFinished;
        protected virtual void OnExecutionFinished(object sender, EventArgs e)
        {
            var handler = ExecutionFinished;
            if (handler != null)
                handler(sender, e);
        }
        #endregion

        #region Not supported methods
        public void InitializeExperiment()
        {
            throw new NotSupportedException();
        }

        public void InitializeInstruments()
        {
            throw new NotSupportedException();
        }

        public void OwnInstruments()
        {
            throw new NotSupportedException();
        }

        public void ReleaseInstruments()
        {
            throw new NotSupportedException();
        }

        public void FinalizeExperiment()
        {
            throw new NotSupportedException();
        }

        #endregion

        public bool SimulateExperiment
        {
            get;
            set;
        }

        public object ViewModel
        {
            get { return new object(); }
        }

        public string Name
        {
            get { return "TimeDelay"; }
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

        public ExecutionViewModel()
        {

            ExperimentExecutionManager = new ExecutionManager();
            ExperimentExecutionManager.Add(new testAction("test1"));
            var td = new TimeDelayExecutableViewModel();
            
            ExperimentExecutionManager.Add(new TestTimeDelayExecutable() { TimeDelay = 10000 });
            ExperimentExecutionManager.Add(new testAction("test2"));
            

            ExperimentControlButtons = new ControlButtonsViewModel();
            ExperimentControlButtons.PauseCommandRaised += ExperimentControlButtons_PauseCommandRaised;
            ExperimentControlButtons.StartCommandRaised += ExperimentControlButtons_StartCommandRaised;
            ExperimentControlButtons.StopCommandRaised += ExperimentControlButtons_StopCommandRaised;
            GlobalIsEnabled = true;
            ExperimentIsRunning = false;
            ExperimentIsPaused = false;

            InitEventHandlers();
        }

        private void InitEventHandlers()
        {
            if (ExperimentExecutionManager == null)
                ExperimentExecutionManager = new ExecutionManager();
            ExperimentExecutionManager.ExecutionLoopStarted += ExperimentExecutionManager_ExecutionLoopStarted;
            ExperimentExecutionManager.ExecutionLoopFinished += ExperimentExecutionManager_ExecutionLoopFinished;
            ExperimentExecutionManager.ExecutionProgressChanged += ExperimentExecutionManager_ExecutionProgressChanged;
            ExperimentExecutionManager.NewExecutableStarted += ExperimentExecutionManager_NewExecutableStarted;
           
        }

        void ExperimentExecutionManager_NewExecutableStarted(object sender, IExecutable e)
        {
            //MessageHandler("New executable started");
            if (e is INewExperiment)
            {
                CurrentExperimentViewModel = ((INewExperiment)e).ViewModel;
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
        }

        void ExperimentExecutionManager_ExecutionLoopStarted(object sender, EventArgs e)
        {
            
        }

        public ExecutionManager ExperimentExecutionManager { get; private set; }
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
