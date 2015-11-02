using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentTimetrace.DataModel
{
    [Serializable]
    public struct TimetraceInfoRow : IMeasurementInfo
    {
        [DataProperty("Event #", "", "")]
        public int EventNumber { get; private set; }
        [DataProperty("Description", "", "")]
        public string EventDescription { get; private set; }
        [DataProperty("Time", "sec", "t")]
        public double Time { get; private set; }

        [DataProperty("Filename", "", "")]
        public string Filename
        {
            get; set;
        }
    }
}
