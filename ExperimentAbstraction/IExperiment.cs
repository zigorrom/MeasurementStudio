using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExperimentAbstraction
{

    public enum ExecutionStatus
    {
        None,
        Queued,
        Running,
        Done,
        Failed,
        Aborted
        
    }

    public class ExecutionReport
    {
        public int ExperimentProgress { get; set; }
        public ExecutionStatus ExperimentExecutionStatus { get; set; }
        public string ExperimentProgressMessage { get; set; }
        public static ExecutionReport Empty
        {
            get
            {
                return new ExecutionReport { ExperimentExecutionStatus = ExecutionStatus.None, ExperimentProgress = 0, ExperimentProgressMessage = String.Empty };
            }
        }

    }

    public interface IExecutable
    {
        void Execute();
        void Execute(object ExperimentStartObject, DoWorkEventArgs e);
        //void Execute(IProgress<ExecutionReport> progress, CancellationToken cancellationToken, PauseToken pauseToken);
        ExecutionReport Execute(IProgress<ExecutionReport> progress, CancellationToken cancellationToken, PauseToken pauseToken);
        void Abort();
        void Pause();
        void Resume();

        bool IsRunning { get; }
        ExecutionStatus Status { get; }

        event EventHandler<ExecutionStatus> StatusChanged;
        event EventHandler ExecutionStarted;
        event EventHandler ExecutionAborted;
        event ProgressChangedEventHandler ProgressChanged;
        event EventHandler ExecutionFinished;
    }

    public interface INewExperiment:IInstrumentOwner, IExecutable
    {
        void InitializeExperiment();
        void InitializeInstruments();
        void OwnInstruments();
        void ReleaseInstruments();
        void FinalizeExperiment();

        bool SimulateExperiment { get; set; }

        object ViewModel { get; }
    }


    public interface IExperiment:IInstrumentOwner
    {

        void InitializeExperiment();
        void InitializeInstruments();
        void OwnInstruments();
        void ReleaseInstruments();
        void FinalizeExperiment();
        void Start();
        bool IsRunning { get; }
        void Pause();
        void Abort();
        //void AssertParams();
        //void New(string ExperimentName);
        //void ClearExperiment();
        
        //UserControl Control { get; }
       // void SetDisplayFunction(string Function);


        event EventHandler ExperimentStarted;
        event EventHandler ExperimentStopped;
        event EventHandler ExperimentPaused;
        event ProgressChangedEventHandler ExperimentProgressChanged;
        event EventHandler ExperimentFinished;
    }
}
