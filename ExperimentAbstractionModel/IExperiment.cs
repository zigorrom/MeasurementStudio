using InstrumentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentAbstractionModel
{
    public interface IExperiment:IInstrumentOwner
    {
        void InitializeExperiment();
        void InitializeInstruments();
        void Start();
        int ReportProgress();
        void Abort();
        
    }
}
