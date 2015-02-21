using ExperimentAbstractionModel;
using InstrumentAbstractionModel;
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
    public sealed class InstrumentOwnerEntry:List<IInstrument>
    {
        private IInstrumentOwner m_Experiment;
        //private List<IInstrument> m_Instruments;
        public InstrumentOwnerEntry(IInstrumentOwner instrumentOwner):base()
        {
            
        }

        

    }
}
