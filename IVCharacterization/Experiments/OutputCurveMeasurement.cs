using ExperimentAbstraction;
using ExperimentDataModel;
using IVCharacterization.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.Experiments
{
    public class OutputCurveMeasurement : AbstractExperiment<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>
    {

        public OutputCurveMeasurement():base("Output curve measurement")
        {
            
        }

        public override void Start()
        {
            base.Start();
            
        }

        public override void Abort()
        {
            base.Abort();
        }

        public override void OwnInstruments()
        {
           // throw new NotImplementedException();
        }

        public override void InitializeExperiment()
        {
            //throw new NotImplementedException();
        }

        public override void InitializeInstruments()
        {
            //throw new NotImplementedException();
        }

        public override void ReleaseInstruments()
        {
            //throw new NotImplementedException();
        }

        protected override void DoMeasurement(object sender, DoWorkEventArgs e)
        {
            //throw new NotImplementedException();
        }

        public override object ViewModel
        {
            get { throw new NotImplementedException(); }
        }

        public override System.Windows.Controls.UserControl Control
        {
            get { throw new NotImplementedException(); }
        }
    }
}
