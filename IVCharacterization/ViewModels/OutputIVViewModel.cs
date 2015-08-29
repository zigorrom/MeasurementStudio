using IVCharacterization.Experiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.ViewModels
{
   
    public sealed class OutputIVViewModel:IVMainViewModel
    {
        public OutputIVViewModel():base()
        {
            Visualization.HorizontalAxisTitle = "Drain - Source Voltage, V_{DS}(V)";
            Visualization.VerticalAxisTitle = "Drain Current, I_{D}(A)";
            Visualization.Title = "Output I-V Characterization";
            Visualization.StrokeThickness = 10;

        }

        protected override void InitExperiment()
        {
            Experiment = new OutputCurveMeasurement();
        }

        protected override void ExperimentStartedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void ExperimentProgressChangedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void ExperimentFinishedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void ExperimentStoppedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void ExperimentPausedHandler(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
