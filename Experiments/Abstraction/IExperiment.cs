
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiments
{
    public interface IExperiment:IInstrumentOwner
    {
        void InitializeExperiment();
        void InitializeInstruments();
        void OwnInstruments();
        void ReleaseInstruments();
        void Start();
        int ReportProgress();
        void Abort();
        
    }
}
