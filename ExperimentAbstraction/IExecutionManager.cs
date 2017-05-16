using System;
namespace ExperimentAbstraction
{
    public interface IExecutionManager
    {
        event EventHandler ExecutionLoopFinished;
        event EventHandler ExecutionLoopStarted;
        event EventHandler<ExecutionReport> ExecutionProgressChanged;
        event EventHandler<IExecutable> NewExecutableStarted;
       
        bool IsRunning { get; }
        void Start();
        void Pause();
        void Resume();
        void Abort();
        void Wait();
    }
}
