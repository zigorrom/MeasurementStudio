﻿using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using Helper.Ranges.DoubleRange;
using Helper.Ranges.SimpleRangeControl;
using IVCharacterization.Experiments;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.ViewModels
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

    public sealed class OutputIVViewModel:IVMainViewModel
    {
        public OutputIVViewModel():base()
        {
          
        }
        protected override void InitExperiment(out IExperiment experiment)
        {
            //if (IVSettingsViewModel.UseSampleSelector)
            //{
            //    experiment = new MeasurementScenario();
            //}
            //else
            //{
            experiment = new OutputCurveMeasurement(this);
            //}
        }

        protected override void SetVisualization(out DataVisualization.D3DataVisualization.D3VisualizationViewModel visualVM)
        {
            visualVM = new D3VisualizationViewModel
            {
                HorizontalAxisTitle = "Drain - Source Voltage, V_{DS}(V)",
                VerticalAxisTitle = "Drain Current, I_{D}(A)",
                Title = "Output I-V Characterization",
                StrokeThickness = 10
            };
        }

        protected override void SetRangeViewModels(out RangeViewModel vm1, out RangeViewModel vm2)
        {
            vm1 = new RangeViewModel("Drain-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
            vm2 = new RangeViewModel("Gate-Source Voltage Range", new Voltage(), new Voltage(), new Voltage());
        }

       
    }
}
