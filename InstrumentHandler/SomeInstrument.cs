using ExperimentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentHandlerNamespace
{
   
    public class IVExperiment : AbstractExperiment
    {
        private const string ExperimentName = "IV_MEASUREMENT";
        private readonly AvailableInstrumentsEmuneration NecessaryInstruments = AvailableInstrumentsEmuneration.SMU_Keithley2400 | AvailableInstrumentsEmuneration.SMU_Keithley2430;
        public IVExperiment()
            : base(ExperimentName)
        {

            //InitializeInstruments();
            //InitializeExperiment();
        }

        public override void InitializeExperiment()
        {
            throw new NotImplementedException();
        }

        public override void InitializeInstruments()
        {
            throw new NotImplementedException();
        }

        public override void ReleaseInstruments()
        {
            throw new NotImplementedException();
        }
        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override int ReportProgress()
        {
            throw new NotImplementedException();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
