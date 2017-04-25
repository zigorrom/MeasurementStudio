using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using IVexperiment.Experiments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVexperiment.ViewModels
{
    [Serializable()]
    public sealed class TransfrerIVViewModel:IVMainViewModel<TransferCurveMeasurement>
    {
        public TransfrerIVViewModel()
        {
            //Visualization.HorizontalAxisTitle = "Gate - Source Voltage, V_{DS}(V)";
            //Visualization.VerticalAxisTitle = "Drain Current, I_{D}(A)";
            //Visualization.Title = "Transfer I-V Characterization";
            //Visualization.StrokeThickness = 10;
            //FirstRangeViewModel = new RangeViewModel("Gate-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
            //SecondRangeViewModel = new RangeViewModel("Drain-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
        }

        protected override void InitExperiment(out TransferCurveMeasurement experiment)
        {
            experiment = new TransferCurveMeasurement(this);
        }
       
        protected override void SetRangeViewModels(out RangeViewModel DrainSourceRangeViewModel, out RangeViewModel GateSourceRangeViewModel)
        {
            DrainSourceRangeViewModel = new RangeViewModel("Drain-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
            GateSourceRangeViewModel = new RangeViewModel("Gate-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
        }

        protected override void SetVisualization(out DataVisualization.D3DataVisualization.D3VisualizationViewModel visualVM)
        {
            visualVM = new D3VisualizationViewModel
            {
                HorizontalAxisTitle = "Gate - Source Voltage, V_{DS}(V)",
                VerticalAxisTitle = "Drain Current, I_{D}(A)",
                Title = "Transfer I-V Characterization",
                StrokeThickness = 10
            };
        }
    }
}
