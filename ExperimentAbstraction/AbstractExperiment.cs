
using ExperimentDataModel;
using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExperimentAbstraction
{
    public abstract class AbstractExperiment<InfoT,DataT>:ObservableExperiment<DataT>, IExperiment
        where InfoT : struct
        where DataT : struct
    {
        private string m_Name;
        protected Queue<MeasurementData<InfoT, DataT>> _dataQueue;

        public AbstractExperiment(string ExperimentName)
        {
            m_Name = ExperimentName;
            _dataQueue = new Queue<MeasurementData<InfoT, DataT>>();
            InitializeInstruments();
            InitializeExperiment();
        }
       

        public abstract void OwnInstruments();

        public abstract void InitializeExperiment();

        public abstract void InitializeInstruments();

        public abstract void ReleaseInstruments();

        public virtual void Start()
        {
            OnExperimentStarted(this, new EventArgs());
        }

        protected abstract void DoMeasurement(object sender, DoWorkEventArgs e);

        public virtual void ReportProgress()
        {
            OnExperimentProgressChanged(this, new EventArgs());
        }

        
        public virtual void Abort()
        {
            OnExperimentStopped(this, new EventArgs());
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

        public abstract object ViewModel { get; }

        public abstract UserControl Control
        {
            get;
        }

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

        public event EventHandler ExperimentProgressChanged;
        protected virtual void OnExperimentProgressChanged(object sender, EventArgs e)
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
    }
}
