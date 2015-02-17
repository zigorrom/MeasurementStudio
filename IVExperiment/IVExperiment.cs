using ExperimentAbstractionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVExperiment
{
    public class IVExperiment:AbstractExperiment
    {
        private const string ExperimentName = "IV_MEASUREMENT";

        public IVExperiment():base(ExperimentName)
        {
            InitializeInstruments();
            InitializeExperiment();
        }

        public override void InitializeExperiment()
        {
            throw new NotImplementedException();
        }

        public override void InitializeInstruments()
        {
            
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
