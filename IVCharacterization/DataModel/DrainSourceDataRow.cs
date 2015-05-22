using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    internal struct DrainSourceDataRow
    {
        [DataPropertyAttribute("V\\_DS", "DrainSourceVoltage", "V")]//true, true, -1, "V\\_DS", "DrainSourceVoltage", "V")]
        public double DrainSourceVoltage;
        [DataPropertyAttribute("I\\_D", "DrainCurrent", "A")]//true, true, -1, "I\\_D", "DrainCurrent", "A")]
        public double DrainCurrent;
        [DataPropertyAttribute("I\\_G", "GateCurrent", "A")]//true, true, -1, "I\\_G", "GateCurrent", "A")]
        public double GateCurrent;

        public DrainSourceDataRow(double drainSourceVoltage,double drainCurrent, double gateCurrent)
        {
            DrainSourceVoltage = drainSourceVoltage;
            DrainCurrent = drainCurrent;
            GateCurrent = gateCurrent;
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2}", DrainSourceVoltage, DrainCurrent,GateCurrent);
        }
    }
}
