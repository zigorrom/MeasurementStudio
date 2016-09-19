using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrentTimetrace.DataModel
{
    [Serializable]
    public struct TimetraceDataRow
    {
        [DataProperty("Time", "sec", "t")]
        public double Time { get; private set; }
        [DataProperty("Drain current", "A", "I\\-(D)")]
        public double DrainCurrent { get; private set; }
        [DataProperty("Gate current", "A", "I\\-(G)")]
        public double GateCurrent { get; private set; }
    }
}
