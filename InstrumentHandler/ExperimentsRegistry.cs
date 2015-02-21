using ExperimentAbstractionModel;
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
        }

        public List<IExperiment> ExperimentsList
        {
            get { return m_ExperimentList; }
        }
    }
}
