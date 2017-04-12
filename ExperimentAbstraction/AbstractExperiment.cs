
using ExperimentDataModel;
using Instruments;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExperimentViewer
{

   

    public abstract class AbstractExperiment<InfoT, DataT> : ObservableExperiment<DataT>, IExperiment, IDisposable, IScenarioAction
        where InfoT : struct,IMeasurementInfo
        where DataT : struct
    {
        private string m_Name;

        private ConcurrentQueue<KeyValuePair<bool, MeasurementData<InfoT, DataT>>> _dataQueue = new ConcurrentQueue<KeyValuePair<bool, MeasurementData<InfoT, DataT>>>();// new ConcurrentQueue<MeasurementData<InfoT, DataT>>();
        private StreamMeasurementDataExporter<InfoT, DataT> _dataWriter;
       
        private Thread _writerThread;
        private WaitHandle _experimentStopped = new AutoResetEvent(false);
        private BackgroundWorker _worker;

       
        protected bool SimulateExperiment
        {
            get;
            set;
        }

        protected void InitializeWriterThread()
        {
            _writerThread = new Thread(new ParameterizedThreadStart((o) =>
            {
                var waitHandle = (WaitHandle)o;
                //MeasurementData<InfoT, DataT> data;
                KeyValuePair<bool, MeasurementData<InfoT, DataT>> data;
                while (!waitHandle.WaitOne(0, false))
                    while (_dataQueue.TryDequeue(out data))
                    {
                        if (data.Key == true)
                            _dataWriter.NewMeasurement(data.Value.Info);
                        _dataWriter.WriteMeasurement(data.Value);
                        //_dataWriter.Write(data);
                    }
            }));
        }
        
        protected void InitializeWriter(string WorkingDirectory, string ExperimentName)
        {
            _dataWriter = new StreamMeasurementDataExporter<InfoT, DataT>(WorkingDirectory);
            _dataWriter.NewExperiment(ExperimentName);
        }

        protected IMeasurementDataExporter<InfoT,DataT> MeasurementWriter
        {
            get { return this._dataWriter; }
            //private set { this._dataWriter = value; }
        }


        protected void NewMeasurement(InfoT measurementInfo)
        {
            _dataWriter.NewMeasurement(measurementInfo);
        }


        //protected StreamMeasurementDataExporter<InfoT, DataT> GetStreamExporter(string WorkingDirectory)
        //{
        //    return new StreamMeasurementDataExporter<InfoT, DataT>(WorkingDirectory);
        //}

        protected void EnqueueData(MeasurementData<InfoT, DataT> data, bool NewMeasurement)
        {
            _dataQueue.Enqueue(new KeyValuePair<bool,MeasurementData<InfoT, DataT>>(NewMeasurement,data));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_worker != null)
                {
                    _worker.Dispose();
                    _worker = null;
                }
            }

        }

        ~AbstractExperiment()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }


        protected string WorkingDirectory;
        protected string ExperimentName;
        protected string MeasurementName;
        protected int MeasurementCount;

        protected virtual void AssertParams()
        {
            if (String.IsNullOrEmpty(WorkingDirectory))
                throw new ArgumentNullException("Working directory is not set");

            if (String.IsNullOrEmpty(ExperimentName))
                throw new ArgumentNullException("Experiment name is not set");

            if (String.IsNullOrEmpty(MeasurementName))
                throw new ArgumentNullException("MeasurementName is not set");

            if (MeasurementCount < 0)
                throw new ArgumentNullException("Measurement count is not set");
        }
      
        
        public AbstractExperiment(string ExperimentName)
        {
            m_Name = ExperimentName;
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += SelectMeasurement;
            _worker.ProgressChanged += ProgressChanged;
            _worker.RunWorkerCompleted += RunWorkerCompleted;
            SimulateExperiment = false;
            InitializeScenarioAction();
        }


        private void SelectMeasurement(object sender, DoWorkEventArgs e)
        {
            InitializeExperiment();
            if (SimulateExperiment)
            {
                AssertParams();
                SimulateMeasurement(sender, e);
            }
            else
            {
                InitializeInstruments();
                OwnInstruments();
                AssertParams();
                DoMeasurement(sender, e);
            }
        }

        protected abstract void DoMeasurement(object sender, DoWorkEventArgs e);
        protected abstract void SimulateMeasurement(object sender, DoWorkEventArgs e);
        
        
        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FinalizeExperiment();
           
            if (e.Cancelled)
            {
                HandleMessage("Measurement was aborted");
                return;
            }

            if (e.Error != null)
            {
                HandleError(e.Error);
                return;
            }

            if (!SimulateExperiment)
                ReleaseInstruments();
            
            HandleMessage("Measurement completed");

            OnExperimentFinished(sender, e);
        }

        void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnExperimentProgressChanged(sender, e);
        }
        

        public virtual void InitializeExperiment()
        {
            InitializeWriterThread();
            _writerThread.Start(_experimentStopped);
        }
        public abstract void InitializeInstruments();
        public abstract void OwnInstruments();

        protected abstract void HandleError(Exception e);
        protected abstract void HandleMessage(string Message);
        
        public abstract void ReleaseInstruments();
        public virtual void FinalizeExperiment()
        {
            _dataWriter.Close();
            ((AutoResetEvent)_experimentStopped).Set();
            _writerThread.Join();
            _dataWriter.Close();
        }
        //public abstract void ClearExperiment();


        public void Start()
        {
            _worker.RunWorkerAsync();
            OnExperimentStarted(this, new EventArgs());
        }

        public void Pause()
        {
            OnExperimentPaused(this, EventArgs.Empty);
        }

        public void Abort()
        {
            _worker.CancelAsync();
            OnExperimentStopped(this, new EventArgs());
        }

        public bool IsRunning
        {
            get { return _worker.IsBusy; }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            private set
            {
                m_Name = value;
            }
        }

        public bool Equals(IInstrumentOwner other)
        {
            if (other.Name == m_Name)
                if (Object.ReferenceEquals(this, other))
                    return true;
            return false;
        }
        public override int GetHashCode()
        {
            return m_Name.GetHashCode();
        }

        //public void SetDisplayFunction(string Function)
        //{
        //    //Set function through expression evaluator;
        //    //https://csharpeval.codeplex.com/
        //    throw new NotImplementedException();
        //}

        #region Events
        public event EventHandler ExperimentStarted;
        protected virtual void OnExperimentStarted(object sender, EventArgs e)
        {
            var handler = ExperimentStarted;
            if (handler != null)
                handler(sender, e);
        }

        public event EventHandler ExperimentStopped;
        protected virtual void OnExperimentStopped(object sender, EventArgs e)
        {
            var handler = ExperimentStopped;
            if (handler != null)
                handler(sender, e);
        }

        public event EventHandler ExperimentPaused;
        protected virtual void OnExperimentPaused(object sender, EventArgs e)
        {
            var handler = ExperimentPaused;
            if (handler != null)
                handler(sender, e);
        }

        public event ProgressChangedEventHandler ExperimentProgressChanged;
        protected virtual void OnExperimentProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var handler = ExperimentProgressChanged;
            if (handler != null)
                handler(sender, e);
        }

        public event EventHandler ExperimentFinished;
        protected virtual void OnExperimentFinished(object sender, EventArgs e)
        {
            var handler = ExperimentFinished;
            if (handler != null)
                handler(sender, e);
        }
        #endregion

        #region Implementation of IScenarioAction interface

       

        void IScenarioAction.Abort()
        {
            this.Abort();
        }

        void IScenarioAction.Pause()
        {
            this.Pause();
        }

        void IScenarioAction.Execute()
        {
            this.Start();
        }

        bool IScenarioAction.IsExecuting
        {
            get { return IsRunning; }
        }

        public ScenarioAcrionStatus Status
        {
            get { throw new NotImplementedException(); }
        }

        void InitializeScenarioAction()
        {
            ScenarioActionStarted += ExperimentStarted;
            ScenarioActionAborted += ExperimentStopped;
            ScenarioActionPaused += ExperimentPaused;
            ScenarioActionFinished += ExperimentFinished;
            ScenarioActionProgressChanged += ExperimentProgressChanged;
        }

        public event EventHandler ScenarioActionAborted;

        public event EventHandler ScenarioActionFinished;

        public event EventHandler ScenarioActionPaused;

        public event ProgressChangedEventHandler ScenarioActionProgressChanged;

        public event EventHandler ScenarioActionStarted;


        #endregion


        
    }
}
