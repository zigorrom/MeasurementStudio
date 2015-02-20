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
                        m_experiments = new ExperimentsRegistry();
                }
                return m_experiments;
            }
        }
    }
}
