using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    class GateSourceMeasurementInfoRow
    {
        [DataProperty(true, true, -1, "FileName", "", "")]
        string FileName;
        [DataProperty(true, true, -1, "DrainSourceVoltage", "V", "")]
        double DrainSourceVoltage;
    }
}
