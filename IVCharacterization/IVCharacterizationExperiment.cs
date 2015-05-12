using ExperimentAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization
{
    public class IVCharacterizationExperiment:AbstractExperiment
    {
        private const string ExperimentName = "IV characterization";
        public IVCharacterizationExperiment():base(ExperimentName)
        {

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

        public override void OwnInstruments()
        {
            throw new NotImplementedException();
        }
    }
}
