using ExperimentAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelSwitchExecutable
{
    public class ChannelSwitchExecutable:INewExperiment
    {
        public ChannelSwitchExecutable(ChannelSwitchExecutableViewModel viewModel)
        {

        }

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
            get { throw new NotImplementedException(); }
        }

        public void Execute(IProgress<ExecutionReport> progress, System.Threading.CancellationToken cancellationToken, PauseToken pauseToken)
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

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool Equals(Instruments.IInstrumentOwner other)
        {
            throw new NotImplementedException();
        }
    }
}
