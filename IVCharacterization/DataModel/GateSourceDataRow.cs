using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    internal struct GateSourceDataRow
    {
        [DataPropertyAttribute(true, true, -1, "V\\_DS", "GateSourceVoltage", "V")]
        public double GateSourceVoltage;
        [DataPropertyAttribute(true, true, -1, "I\\_D", "DrainCurrent", "A")]
        public double DrainCurrent;
        [DataPropertyAttribute(true, true, -1, "I\\_G", "GateCurrent", "A")]
        public double GateCurrent;
        
        public GateSourceDataRow(double drainSourceVoltage, double drainCurrent, double gateCurrent)
        {
            GateSourceVoltage = drainSourceVoltage;
            DrainCurrent = drainCurrent;
            GateCurrent = gateCurrent; 
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2}");
        }
    }
}
