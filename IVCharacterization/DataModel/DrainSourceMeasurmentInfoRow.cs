using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    internal struct DrainSourceMeasurmentInfoRow
    {
        [DataPropertyAttribute(true, true, -1, "FileName", "", "")]
        public string FileName;
        [DataPropertyAttribute(true, true, -1, "GateVoltage", "V", "")]
        public double GateVoltage;

        public DrainSourceMeasurmentInfoRow(string filename, double gateVoltage)
        {
            FileName = filename;
            GateVoltage = gateVoltage;
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1}", FileName, GateVoltage);
        }
    }
}
