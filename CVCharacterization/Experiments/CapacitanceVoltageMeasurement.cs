using CVCharacterization.DataModel;
using CVCharacterization.ViewModels;
using ExperimentAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.Experiments
{
    public sealed class CapacitanceVoltageMeasurement
    {
        public CapacitanceVoltageMeasurement(AbstractCVMainViewModel viewModel)
        {

        }
    }

    //sealed public class CapacitanceVoltageMeasurement:AbstractExperiment<CapacityVoltageInfoRow,CapacityVoltageDataRow>
    //{
    //    public CapacitanceVoltageMeasurement():base("CapacitanceVoltageMeasurement")
    //    {

    //    }

    //    public override void OwnInstruments()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void InitializeExperiment()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void InitializeInstruments()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void ReleaseInstruments()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void FinalizeExperiment()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override void DoMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void ClearExperiment()
    //    {
    //        throw new NotImplementedException();
    //    }

        
    //}
}
