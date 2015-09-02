using ExperimentAbstraction;
using IVCharacterization.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.Experiments
{
    public class TransferCurveMeasurement : AbstractExperiment<GateSourceMeasurementInfoRow,GateSourceDataRow>
    {
        public TransferCurveMeasurement():base("Transfer curve measurement")
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

       
        public override void Abort()
        {
            throw new NotImplementedException();
        }

        public override void OwnInstruments()
        {
            throw new NotImplementedException();
        }

        public override object ViewModel
        {
            get { throw new NotImplementedException(); }
        }

        


        protected override void DoMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void CleanExperiment()
        {
            throw new NotImplementedException();
        }

        public override void FinalizeExperiment()
        {
            throw new NotImplementedException();
        }
    }
}
