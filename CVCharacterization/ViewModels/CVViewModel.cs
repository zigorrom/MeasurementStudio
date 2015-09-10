using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.ViewModels
{
    public class CVViewModel:AbstractCVMainViewModel
    {
        public CVViewModel()
        {
            Visualization.HorizontalAxisTitle = "Voltage, V_{s}(V)";
            Visualization.VerticalAxisTitle = "Capacity, C_{s}(F)";
            Visualization.Title = "C-V Characterization";
            Visualization.StrokeThickness = 10;
        }

        protected override void InitExperiment(out ExperimentAbstraction.IExperiment experiment)
        {
            throw new NotImplementedException();
        }

        protected override void SetRangeViewModels(out Helper.Ranges.SimpleRangeControl.RangeViewModel vm1, out Helper.Ranges.SimpleRangeControl.RangeViewModel vm2)
        {
            throw new NotImplementedException();
        }

        protected override void SetVisualization(out DataVisualization.D3DataVisualization.D3VisualizationViewModel visualVM)
        {
            throw new NotImplementedException();
        }
    }
}
