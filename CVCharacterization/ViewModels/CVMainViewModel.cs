using CVCharacterization.Experiments;
using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using Helper.StartStopControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.ViewModels
{
    public abstract class AbstractCVMainViewModel : AbstractExperimentViewModel
    {
        public AbstractCVMainViewModel()
        {
            VoltageRange = new RangeViewModel(new Voltage(), new Voltage(), new Voltage());
            FrequencyRange = new RangeViewModel(new Frequency(), new Frequency(), new Frequency());
            Visualization = new D3VisualizationViewModel();
            
        }

        protected abstract void SetRangeViewModels(out RangeViewModel vm1, out RangeViewModel vm2);
        protected abstract void SetVisualization(out D3VisualizationViewModel visualVM);

        private RangeViewModel _voltageRange;
        public RangeViewModel VoltageRange
        {
            get { return _voltageRange; }
            set { _voltageRange = value; }
        }

        private RangeViewModel _frequencyRange;
        public RangeViewModel FrequencyRange
        {
            get { return _frequencyRange; }
            set { _frequencyRange = value; }
        }

        private D3VisualizationViewModel _visualization;
        public D3VisualizationViewModel Visualization
        {
            get { return _visualization; }
            set { _visualization = value; }
        }

        private ControlButtonsViewModel _experimentControlButtons;
        public ControlButtonsViewModel ExperimentControlButtons
        {
            get { return _experimentControlButtons; }
            set { _experimentControlButtons = value; }
        }



        protected override string GetExperimentName()
        {
            var d = new CVCharacterization.Views.NewExperiment(ExperimentName);
            if (d.ShowDialog().Value)
                return d.ExperimentName;
            return String.Empty;
        }

       

        

        

        protected override void ClearVisualization()
        {
            throw new NotImplementedException();
        }
    }
}
