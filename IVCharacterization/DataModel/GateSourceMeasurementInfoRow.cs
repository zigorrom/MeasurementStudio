using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    public struct GateSourceMeasurementInfoRow:IInfoDataRow
    {
        [DataPropertyAttribute("FileName", "", "")]//true, true, -1, "FileName", "", "")]
        public string FileName;
        [DataPropertyAttribute("DrainSourceVoltage", "V", "")]//true, true, -1, "DrainSourceVoltage", "V", "")]
        public double DrainSourceVoltage;

        public GateSourceMeasurementInfoRow(string filename, double drainSourceVoltage)
        {
            FileName = filename;
            DrainSourceVoltage = drainSourceVoltage;
        
        }


        public string Filename
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
