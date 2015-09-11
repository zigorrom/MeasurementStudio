using CVCharacterization.Experiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.ViewModels
{
    public class CVViewModel:CVMainViewModel
    {
        public CVViewModel():base()
        {
            
        }

        protected override void InitExperiment(out ExperimentAbstraction.IExperiment experiment)
        {
            experiment = new CapacitanceVoltageMeasurement(this);
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
