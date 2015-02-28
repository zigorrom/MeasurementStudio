
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentAbstractionModel
{
    public abstract class AbstractExperiment:IExperiment
    {
        private string m_Name;
        protected Dictionary<string, IInstrument> m_Instruments;

        public AbstractExperiment(string ExperimentName)
        {
            m_Name = ExperimentName;
            m_Instruments = new Dictionary<string, IInstrument>();
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
    }
}
