﻿using CurrentTimetrace.Experiments;
using DataVisualization.D3DataVisualization;
using ExperimentAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentTimetrace.ViewModels
{
    public class TimetraceMainViewModel:AbstractExperimentViewModel
    {

        private D3VisualizationViewModel _visualization;
        public D3VisualizationViewModel Visualization
        {
            get { return _visualization; }
            private set { SetField(ref _visualization, value, "Visualization"); }
        }



        protected override void InitExperiment(out IExperiment experiment)
        {
            experiment = new TimetraceExperiment(this);
            //throw new NotImplementedException();
        }

        protected override void ClearVisualization()
        {
           // throw new NotImplementedException();
        }
    }
}
