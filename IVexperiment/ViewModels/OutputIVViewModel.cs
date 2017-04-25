using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using IVexperiment.Experiments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVexperiment.ViewModels
{
   
    //public sealed class IVMeasurementsViewModel:IVMainViewModel
    //{
    //    public IVMeasurementsViewModel():base()
    //    {

    //    }

    //    protected override void InitExperiment(out IExperiment experiment)
    //    {


    //        throw new NotImplementedException();
    //    }

    //}
    [Serializable()]
    public sealed class OutputIVViewModel:IVMainViewModel<OutputCurveMeasurement>
    {
        public OutputIVViewModel():base()
        {
          
        }
        protected override void InitExperiment(out OutputCurveMeasurement experiment)
        {
            experiment = new OutputCurveMeasurement(this);
            //Experiment.ExecutionStarted += Experiment_ExecutionStarted;
            //Experiment.ExecutionFinished += Experiment_ExecutionFinished;
        }

        void Experiment_ExecutionFinished(object sender, EventArgs e)
        {
            MessageHandler("Finished");
        }

        void Experiment_ExecutionStarted(object sender, EventArgs e)
        {
            MessageHandler("Started");
        }
        //protected override void InitExperiment(out IExperiment experiment)
        //{
        //    experiment = null;
        //    //if (IVSettingsViewModel.UseSampleSelector)
        //    //{
        //    //    experiment = new MeasurementScenario();
        //    //}
        //    //else
        //    //{
        //    //experiment = new OutputCurveMeasurement(this);
        //    //}
        //}

        protected override void SetVisualization(out DataVisualization.D3DataVisualization.D3VisualizationViewModel visualVM)
        {
            visualVM = new D3VisualizationViewModel
            {
                HorizontalAxisTitle = "Drain - Source Voltage, V_{DS}(V)",
                VerticalAxisTitle = "Drain Current, I<sub>D</sub>(A)",
                Title = "Output I-V Characterization",
                StrokeThickness = 10
            };
        }
        protected override void SetRangeViewModels(out RangeViewModel DrainSourceRangeViewModel, out RangeViewModel GateSourceRangeViewModel)
        {
            DrainSourceRangeViewModel = new RangeViewModel("Drain-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
            GateSourceRangeViewModel = new RangeViewModel("Gate-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
        }

       
    }
}
