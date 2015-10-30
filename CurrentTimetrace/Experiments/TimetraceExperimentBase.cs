using CurrentTimetrace.ViewModels;
using ExperimentAbstraction;
using ExperimentDataModel;
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentTimetrace.Experiments
{
    public class TimetraceExperimentBase<InfoT,DataT>:AbstractExperiment<InfoT, DataT>
        where InfoT : struct,IMeasurementInfo
        where DataT : struct
    {

        protected TimetraceMainViewModel _vm;

        protected IInstrumentResourceItem _drainInstrument;
        protected IInstrumentResourceItem _gateInstrument;

        public TimetraceExperimentBase():base("Current timetrace")
        {

        }

        protected override void DoMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void SimulateMeasurement(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void InitializeInstruments()
        {
            throw new NotImplementedException();
        }

        public override void OwnInstruments()
        {
            throw new NotImplementedException();
        }

        protected override void HandleError(Exception e)
        {
            throw new NotImplementedException();
        }

        protected override void HandleMessage(string Message)
        {
            throw new NotImplementedException();
        }

        public override void ReleaseInstruments()
        {
            throw new NotImplementedException();
        }
    }
}
