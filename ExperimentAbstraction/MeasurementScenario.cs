using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExperimentViewer
{

    public class MeasurementScenario : IList<IScenarioAction>, IExperiment
    {
        public MeasurementScenario()
        {
            _scenarioExecutionList = new List<IScenarioAction>();
            _scenarioExecutor = new BackgroundWorker();
            _scenarioExecutor.WorkerSupportsCancellation = true;
            _scenarioExecutor.WorkerReportsProgress = true;
            _scenarioExecutor.DoWork += ScenarioExecution;
            _scenarioExecutor.RunWorkerCompleted += ActionExecutionCompleted;
            SimulateMeasurementScenario = true;
            _currentScenarioAction = null;
            _waitForScenarioActionComplete = new AutoResetEvent(false);
        }

       
        private List<IScenarioAction> _scenarioExecutionList;
        
        private BackgroundWorker _scenarioExecutor;
        private WaitHandle _waitForScenarioActionComplete;
        //private Queue<IScenarioAction> _executionQueue;
        private IScenarioAction _currentScenarioAction;

        public bool SimulateMeasurementScenario
        {
            get;
            set;
        }

        public void Start()
        {
            //_currentScenarioEnumerator = _scenarioExecutionList.GetEnumerator();
            _scenarioExecutor.RunWorkerAsync();
            OnScenarioExecutionStarted(this, new EventArgs());

        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Abort()
        {
            _scenarioExecutor.CancelAsync();
            _currentScenarioAction.Abort();
        }

        private void ScenarioExecution(object sender, DoWorkEventArgs e)
        {
            
            foreach (var currentScenarioAction in _scenarioExecutionList)
            {
                if(_scenarioExecutor.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                _currentScenarioAction = currentScenarioAction;
                currentScenarioAction.ScenarioActionStarted += currentScenarioActionStarted;
                currentScenarioAction.ScenarioActionPaused += currentScenarioActionPaused;
                currentScenarioAction.ScenarioActionProgressChanged += currentScenarioActionProgressChanged;
                currentScenarioAction.ScenarioActionAborted += currentScenarioActionAborted;
                currentScenarioAction.ScenarioActionFinished += currentScenarioActionFinished;
                
                currentScenarioAction.Execute();


                _waitForScenarioActionComplete.WaitOne();

                currentScenarioAction.ScenarioActionStarted -= currentScenarioActionStarted;
                currentScenarioAction.ScenarioActionPaused -= currentScenarioActionPaused;
                currentScenarioAction.ScenarioActionProgressChanged -= currentScenarioActionProgressChanged;
                currentScenarioAction.ScenarioActionAborted -= currentScenarioActionAborted;
                currentScenarioAction.ScenarioActionFinished -= currentScenarioActionFinished;
            }
            
        }

        private void ActionExecutionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {

            }
            else
            {

            }
        }


#region Scenario Action event handlers 
        private void currentScenarioActionFinished(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            ((AutoResetEvent)_waitForScenarioActionComplete).Set();
        }

        private void currentScenarioActionAborted(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void currentScenarioActionProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void currentScenarioActionPaused(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void currentScenarioActionStarted(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

#endregion



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

       
        public bool IsRunning
        {
            get { throw new NotImplementedException(); }
        }

        

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool Equals(Instruments.IInstrumentOwner other)
        {
            throw new NotImplementedException();
        }


        #region scenario events 

        public event EventHandler ExperimentStarted;
        private void OnScenarioExecutionStarted(object sender, EventArgs e)
        {
            var handler = ExperimentStarted;
            if (handler != null)
                handler(sender, e);
        }


        public event EventHandler ExperimentStopped;
        private void OnScenarioExecutionStopped(object sender, EventArgs e)
        {
            var handler = ExperimentStopped;
            if (handler != null)
                handler(sender, e);
        }


        public event EventHandler ExperimentPaused;
        private void OnScenarioExecutionPaused(object sender, EventArgs e)
        {
            var handler = ExperimentPaused;
            if (handler != null)
                handler(sender, e);
        }



        public event System.ComponentModel.ProgressChangedEventHandler ExperimentProgressChanged;
        public void OnScenarioProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var handler = ExperimentProgressChanged;
            if (handler != null)
                handler(sender, e);
        }


        public event EventHandler ExperimentFinished;
        public void OnScenarioFinished(object sender, EventArgs e)
        {
            var handler = ExperimentFinished;
            if (handler != null)
                handler(sender, e);
        }

        #endregion

       

        #region IList interface implementation

        public int IndexOf(IScenarioAction item)
        {
            return _scenarioExecutionList.IndexOf(item);
        }

        public void Insert(int index, IScenarioAction item)
        {
            _scenarioExecutionList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _scenarioExecutionList.RemoveAt(index);
        }

        public IScenarioAction this[int index]
        {
            get
            {
                return _scenarioExecutionList[index];
            }
            set
            {
                _scenarioExecutionList[index] = value;
            }
        }

        public void Add(IScenarioAction NewScenarioActionItem)
        {
            _scenarioExecutionList.Add(NewScenarioActionItem);
        }

        public void Clear()
        {
            _scenarioExecutionList.Clear();
        }

        public bool Contains(IScenarioAction item)
        {
            return _scenarioExecutionList.Contains(item);
        }

        public void CopyTo(IScenarioAction[] array, int arrayIndex)
        {
            _scenarioExecutionList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _scenarioExecutionList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IScenarioAction item)
        {
            return _scenarioExecutionList.Remove(item);
        }

        public IEnumerator<IScenarioAction> GetEnumerator()
        {
            return _scenarioExecutionList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
