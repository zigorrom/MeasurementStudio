using NewExperiment.DataModel;
using NewExperiment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewExperiment.Experiments
{
    public sealed class Experiment:MeasurementBase<MeasurementInfoRow,MeasurementDataRow>
    {
        public Experiment(ExperimentMainViewModel viewModel):base(viewModel, "Measurement name")
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
