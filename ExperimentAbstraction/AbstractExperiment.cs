
using ExperimentDataModel;
using Instruments;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExperimentAbstraction
{
    public abstract class AbstractExperiment<InfoT, DataT> : ObservableExperiment<DataT>, IExperiment, IDisposable
        where InfoT : struct,IMeasurementInfo
        where DataT : struct
    {
        private string m_Name;
        
        protected ConcurrentQueue<MeasurementData<InfoT, DataT>> _dataQueue;

        protected StreamMeasurementDataExporter<InfoT, DataT> _dataWriter;

        private BackgroundWorker _worker;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~AbstractExperiment()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
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
            // free native resources if there are any.
            if (_dataQueue != null)
            {
                //_dataQueue.TryDequeue
                _dataQueue = null;
            }
        }
        public AbstractExperiment(string ExperimentName)
        {
            m_Name = ExperimentName;
            _dataQueue = new ConcurrentQueue<MeasurementData<InfoT, DataT>>();
            _worker = new BackgroundWorker();
            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.DoWork += DoMeasurement;
            _worker.ProgressChanged += _worker_ProgressChanged;
            _worker.RunWorkerCompleted += _worker_RunWorkerCompleted;

            //InitializeInstruments();
            //InitializeExperiment();

        }

        protected StreamMeasurementDataExporter<InfoT, DataT> GetStreamExporter(string WorkingDirectory)
        {
            return new StreamMeasurementDataExporter<InfoT, DataT>(WorkingDirectory);
        }

        void _worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnExperimentFinished(sender, e);
        }

        void _worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnExperimentProgressChanged(sender, e);
        }


        public abstract void OwnInstruments();

        public abstract void InitializeExperiment();
        

        public abstract void InitializeInstruments();

        public abstract void ReleaseInstruments();

        public abstract void FinalizeExperiment();
        
        public virtual void Start()
        {
            InitializeInstruments();
            InitializeExperiment();
            
            OnExperimentStarted(this, new EventArgs());
            _worker.RunWorkerAsync();
        }

        protected abstract void DoMeasurement(object sender, DoWorkEventArgs e);

        public virtual void Pause()
        {
            OnExperimentPaused(this, EventArgs.Empty);
            throw new NotImplementedException();
        }

        public virtual void Abort()
        {
            _worker.CancelAsync();
            OnExperimentStopped(this, new EventArgs());
        }

        public virtual void New(string ExperimentName)
        {
            ClearExperiment();
            Name = ExperimentName;
        }

        public abstract void ClearExperiment();

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

        

        

        public void SetDisplayFunction(string Function)
        {
            //Set function through expression evaluator;
            //https://csharpeval.codeplex.com/
            throw new NotImplementedException();
        }

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















       
    }
}
