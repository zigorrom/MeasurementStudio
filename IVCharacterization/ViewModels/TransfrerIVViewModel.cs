using IVCharacterization.Experiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.ViewModels
{
    public sealed class TransfrerIVViewModel:IVMainViewModel
    {
        public TransfrerIVViewModel()
        {
            Visualization.HorizontalAxisTitle = "Gate - Source Voltage, V_{DS}(V)";
            Visualization.VerticalAxisTitle = "Drain Current, I_{D}(A)";
            Visualization.Title = "Transfer I-V Characterization";
            Visualization.StrokeThickness = 10;
        }



        protected override void InitExperiment()
        {
            //Experiment = new TransferCurveMeasurement(this);
        }
    }
}
