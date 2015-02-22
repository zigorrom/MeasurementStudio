using ExperimentAbstractionModel;
using InstrumentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    public sealed class ExperimentsRegistry
    {
        private static volatile ExperimentsRegistry m_experiments;
        private static object syncRoot = new object();

        private List<IExperiment> m_ExperimentList;

        private ExperimentsRegistry()
        {
            m_ExperimentList = new List<IExperiment>();
        }
        public static ExperimentsRegistry Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (m_experiments == null)
                    {
                        m_experiments = new ExperimentsRegistry();
                        m_experiments.InitializeCustomExperiments();
                    }

                }
                return m_experiments;
            }
        }

        private void InitializeCustomExperiments()
        {
            ///
            /// Here add code to create all custom experiments to ExperimentsList
            ///
            m_ExperimentList.Add(new IVExperiment());
            m_ExperimentList.Add(new IVExperiment());
        }

        public List<IExperiment> ExperimentsList
        {
            get { return m_ExperimentList; }
        }

        public IEnumerable<IInstrumentOwner> OwnerEnumeration
        {
            get {
                foreach (var exp in m_ExperimentList)
                {
                    yield return (IInstrumentOwner)exp;
                }
            }
        }

        public List<IInstrumentOwner> OwnersList
        {
            get
            {
                var list = new List<IInstrumentOwner>();
                foreach (var item in ExperimentsList)
                {
                    list.Add(item as IInstrumentOwner);
                }
                return list;
            }
        }
    }
}
