using CVCharacterization.Experiments;
using Helper.Ranges.DoubleRange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.ViewModels
{
    public class CVViewModel:CVViewModelBase
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
            vm1 = new Helper.Ranges.SimpleRangeControl.RangeViewModel("Voltage Range", new Voltage(), new Voltage(), new Voltage());
            vm2 = new Helper.Ranges.SimpleRangeControl.RangeViewModel("Frequency Range", new Frequency(), new Frequency(), new Frequency());
        }

        protected override void SetVisualization(out DataVisualization.D3DataVisualization.D3VisualizationViewModel visualVM)
        {
            visualVM = new DataVisualization.D3DataVisualization.D3VisualizationViewModel
            {
                HorizontalAxisTitle = "Sample Voltage, V_{s}(V)",
                VerticalAxisTitle = "Capacity, C_s (F)",
                Title = "C-V Characterization"
            };
        }
    }
}
