using ExperimentAbstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExecutables.TimeDelay
{
    public class TimeDelayExecutable:INewExperiment
    {
        public TimeDelayExecutable()
        {
            StopwatchObj = new Stopwatch();
        }

        public void Execute(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {
            OnExecutionStarted(this, EventArgs.Empty);
            StopwatchObj.Start();
            
            var CurrentTimeSpan = TimeSpan.Zero;
            var TempTimespan = TimeSpan.Zero;
            do
            {
                pauseToken.WaitWhilePausedAsync().Wait();
                cancellationToken.ThrowIfCancellationRequested();
                TempTimespan= StopwatchObj.Elapsed;
                if ((TempTimespan - CurrentTimeSpan).TotalMilliseconds > 1)
                    progress.Report(new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.Running, ExperimentProgress = CurrentTimeSpan.Milliseconds, ExperimentProgressMessage = "Waiting..." });
                CurrentTimeSpan = TempTimespan;
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
            get { throw new NotImplementedException(); }
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
