using NoiseMeasurementLegacy.DataModel;
using NoiseMeasurementLegacy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseMeasurementLegacy.Experiments
{
    public sealed class NoiseMeasurement : NoiseMeasurementBase<MeasurementInfoRow, MeasurementDataRow>
    {
        public NoiseMeasurement(ExperimentMainViewModel viewModel)
            : base(viewModel, "Measurement name")
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
}
