
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExperimentAbstraction
{
    public abstract class AbstractExperiment<InfoT,DataT>:ObservableExperiment<DataT>, IExperiment
    {
        private string m_Name;
        //protected Dictionary<string, IInstrument> m_Instruments;

        public AbstractExperiment(string ExperimentName)
        {
            m_Name = ExperimentName;
          //  m_Instruments = new Dictionary<string, IInstrument>();
        }
        
        public abstract void InitializeExperiment();

        public abstract void InitializeInstruments();

        public abstract void ReleaseInstruments();

        public abstract void Start();

        public abstract int ReportProgress();
        
        public abstract void Abort();
        

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


        public abstract void OwnInstruments();


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
