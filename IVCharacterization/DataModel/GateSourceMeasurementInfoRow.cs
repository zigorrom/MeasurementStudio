using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    public struct GateSourceMeasurementInfoRow
    {
        [DataPropertyAttribute(true, true, -1, "FileName", "", "")]
        public string FileName;
        [DataPropertyAttribute(true, true, -1, "DrainSourceVoltage", "V", "")]
        public double DrainSourceVoltage;

        public GateSourceMeasurementInfoRow(string filename, double drainSourceVoltage)
        {
            FileName = filename;
            DrainSourceVoltage = drainSourceVoltage;
        
        }

    }
}
