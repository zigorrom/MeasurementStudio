
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

namespace ExperimentAbstraction
{
    public abstract class AbstractExperiment<InfoT, DataT> : ObservableExperiment<DataT>, IExperiment, IDisposable
        where InfoT : struct,IMeasurementInfo
        where DataT : struct
    {
        private string m_Name;
        
        private ConcurrentQueue<MeasurementData<InfoT, DataT>> _dataQueue= new ConcurrentQueue<MeasurementData<InfoT, DataT>>();
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
                MeasurementData<InfoT, DataT> data;
                while (!waitHandle.WaitOne(0, false))
                    while (_dataQueue.TryDequeue(out data))
                    {
                        _dataWriter.Write(data);
                    }
            }));
        }
        
        protected void InitializeWriter(string WorkingDirectory, string ExperimentName)
        {
            _dataWriter = new StreamMeasurementDataExporter<InfoT, DataT>(WorkingDirectory);
            _dataWriter.NewExperiment(ExperimentName);
        }

        protected StreamMeasurementDataExporter<InfoT, DataT> GetStreamExporter(string WorkingDirectory)
        {
            return new StreamMeasurementDataExporter<InfoT, DataT>(WorkingDirectory);
        }

        protected void EnqueueData(MeasurementData<InfoT, DataT> data)
        {
            _dataQueue.Enqueue(data);
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
        }


        private void SelectMeasurement(object sender, DoWorkEventArgs e)
        {
            if (SimulateExperiment)
            {
                InitializeExperiment();
                SimulateMeasurement(sender, e);
            }
            else
            {
                InitializeExperiment();
                InitializeInstruments();
                OwnInstruments();
                DoMeasurement(sender, e);
            }
        }

        protected abstract void DoMeasurement(object sender, DoWorkEventArgs e);
        protected abstract void SimulateMeasurement(object sender, DoWorkEventArgs e);
        
        
        void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ReleaseInstruments();
            FinalizeExperiment();
            //ClearExperiment();
            OnExperimentFinished(sender, e);
            if (e.Cancelled)
                HandleMessage("Measurement was aborted");
            if (e.Error != null)
                HandleError(e.Error);

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
            ((AutoResetEvent)_experimentStopped).Set();
            _writerThread.Join();
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
       
    }
}
