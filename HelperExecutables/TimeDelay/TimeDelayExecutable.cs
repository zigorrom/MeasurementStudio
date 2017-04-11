using ExperimentAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperExecutables.TimeDelay
{
    public class TimeDelayExecutable:INewExperiment
    {
        public void Execute(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
        {
            
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

        public bool SimulateExperiment
        {
            get;
            set;
        }

        public object ViewModel
        {
            get { throw new NotImplementedException(); }
        }
    }
}
