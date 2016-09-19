using CurrentTimetrace.DataModel;
using CurrentTimetrace.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentTimetrace.Experiments
{
    public class TimetraceExperiment:TimetraceExperimentBase<TimetraceInfoRow,TimetraceDataRow>
    {
        public TimetraceExperiment(TimetraceMainViewModel viewModel):base(viewModel, "Timetrace measurement")
        {

        }

    }
}
