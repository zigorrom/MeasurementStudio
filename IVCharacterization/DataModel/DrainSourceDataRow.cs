using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    internal struct DrainSourceDataRow:IFormattable
    {
        [DataPropertyAttribute(true, true, -1, "V\\_DS", "DrainSourceVoltage", "V")]
        public double DrainSourceVoltage;
        [DataPropertyAttribute(true, true, -1, "I\\_D", "DrainCurrent", "A")]
        public double DrainCurrent;
        [DataPropertyAttribute(true, true, -1, "I\\_G", "GateCurrent", "A")]
        public double GateCurrent;

        public DrainSourceDataRow(double drainSourceVoltage,double drainCurrent, double gateCurrent)
        {
            DrainSourceVoltage = drainSourceVoltage;
            DrainCurrent = drainCurrent;
            GateCurrent = gateCurrent;
        }

        const string RowFormat = "{0}\t{1}\t{2}";

        public override string ToString()
        {
            return ToString(RowFormat, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return String.Format(formatProvider,RowFormat, DrainSourceVoltage, DrainCurrent, GateCurrent);
        }
    }
}
