using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentAbstraction
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
        }

       

        

        private List<IScenarioAction> _scenarioExecutionList;
        private BackgroundWorker _scenarioExecutor;
        //private Queue<IScenarioAction> _executionQueue;
        private IScenarioAction _currentScenarioAction;

        public bool SimulateMeasurementScenario
        {
            get;
            set;
        }

        private void ScenarioExecution(object sender, DoWorkEventArgs e)
        {
            
        }

        private void ActionExecutionCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
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

        public void Start()
        {
            _scenarioExecutor.RunWorkerAsync();
            
        }

        private void EnqueueActionList()
        {

        }

        public bool IsRunning
        {
            get { throw new NotImplementedException(); }
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Abort()
        {
            throw new NotImplementedException();
        }

        

        public event EventHandler ExperimentStarted;

        public event EventHandler ExperimentStopped;

        public event EventHandler ExperimentPaused;

        public event System.ComponentModel.ProgressChangedEventHandler ExperimentProgressChanged;

        public event EventHandler ExperimentFinished;

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public bool Equals(Instruments.IInstrumentOwner other)
        {
            throw new NotImplementedException();
        }

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
