using System;
namespace ExperimentAbstraction
{
    public enum ScenarioAcrionStatus
    {
        Queued,
        Executing,
        Done,
        Aborted
    }

    public interface IScenarioAction
    {
        void Abort();
        void Execute();
        //void Execute(bool blocking)
        bool IsExecuting { get; }
        void Pause();
        ScenarioAcrionStatus Status { get; }

        // add support of parameters for action

        event EventHandler ScenarioActionAborted;
        event EventHandler ScenarioActionFinished;
        event EventHandler ScenarioActionPaused;
        event System.ComponentModel.ProgressChangedEventHandler ScenarioActionProgressChanged;
        event EventHandler ScenarioActionStarted;
    }
}
