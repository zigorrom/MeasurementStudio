using System;
namespace ExperimentAbstraction
{
    public interface IScenarioAction
    {
        void Abort();
        void Execute();
        bool IsExecuting { get; }
        void Pause();

        event EventHandler ScenarioActionAborted;
        event EventHandler ScenarioActionFinished;
        event EventHandler ScenarioActionPaused;
        event System.ComponentModel.ProgressChangedEventHandler ScenarioActionProgressChanged;
        event EventHandler ScenarioActionStarted;
    }
}
