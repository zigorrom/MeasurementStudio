using CVCharacterization.DataModel;
using CVCharacterization.ViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.Experiments
{
    public sealed class CapacitanceVoltageMeasurement:CapacitanceMeasurementBase<CapacityVoltageInfoRow,CapacityVoltageDataRow>
    {
        public CapacitanceVoltageMeasurement(CVViewModelBase viewModel)
            : base(viewModel, "C-V measurement")
        {

        }

        protected override void DoMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void SimulateMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new NotImplementedException();
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
