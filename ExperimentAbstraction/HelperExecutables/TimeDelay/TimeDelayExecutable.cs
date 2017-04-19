using ExperimentAbstraction;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentAbstraction.HelperExecutables.TimeDelay
{
    public class TimeDelayExecutable:INewExperiment
    {
        public TimeDelayExecutable(TimeDelayExecutableViewModel viewModel)
        {
            StopwatchObj = new Stopwatch();
            ViewModel = viewModel;
            TimeDelay = (int)viewModel.Delay.TotalMilliseconds;
        }
        private const int ProgressRefreshTime = 5;
        
        public void Execute(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {

            OnExecutionStarted(this, EventArgs.Empty);
            StopwatchObj.Start();
            var progressStep = TimeDelay / 100;
            var CurrentTimeSpan = TimeSpan.Zero;
            var LastTimespan = TimeSpan.Zero;
            do
            {
                if (pauseToken.IsPaused)
                    StopwatchObj.Stop();
                pauseToken.WaitWhilePausedAsync().Wait();
                if (!pauseToken.IsPaused)
                    StopwatchObj.Start();
                cancellationToken.ThrowIfCancellationRequested();
                CurrentTimeSpan = StopwatchObj.Elapsed;
                OnTimeElapsed(this, CurrentTimeSpan);
                if ((CurrentTimeSpan - LastTimespan).TotalMilliseconds > ProgressRefreshTime)
                {
                    progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = (int)Math.Floor(CurrentTimeSpan.TotalMilliseconds/progressStep), ExperimentProgressMessage = "Waiting..." });
                    LastTimespan = CurrentTimeSpan;
                }
                //CurrentTimeSpan = LastTimespan;
            } while (CurrentTimeSpan.TotalMilliseconds < TimeDelay);
            progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Done, ExperimentProgress = CurrentTimeSpan.Milliseconds, ExperimentProgressMessage = "Ready!" });
            StopwatchObj.Reset();
            OnExecutionFinished(this, EventArgs.Empty);

        }

        public int TimeDelay { get; set; }

        private Stopwatch StopwatchObj { get; set; }

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

        public event EventHandler<TimeSpan> TimeElapsed;
        protected virtual void OnTimeElapsed(object sender, TimeSpan t)
        {
            var handler = TimeElapsed;
            if(handler!= null)
            {
                handler(sender, t);
            }
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
            get;
            private set;
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
}
