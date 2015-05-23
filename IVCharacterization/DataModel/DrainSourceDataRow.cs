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
        
        private double m_DrainSourceVoltage;
        private double m_DrainCurrent;        
        private double m_GateCurrent;

        [DataPropertyAttribute("V\\_DS", "DrainSourceVoltage", "V")]//true, true, -1, "V\\_DS", "DrainSourceVoltage", "V")]
        public double DrainSourceVoltage
        {
            get { return m_DrainSourceVoltage; }
            set { m_DrainSourceVoltage = value; }
        }

        [DataPropertyAttribute("I\\_D", "DrainCurrent", "A")]//true, true, -1, "I\\_D", "DrainCurrent", "A")]
        public double DrainCurrent
        {
            get { return m_DrainCurrent; }
            set { m_DrainCurrent = value; }
        }

        [DataPropertyAttribute("I\\_G", "GateCurrent", "A")]//true, true, -1, "I\\_G", "GateCurrent", "A")]
        public double GateCurrent
        {
            get { return m_GateCurrent; ; }
            set { m_GateCurrent = value; }
        }

        public DrainSourceDataRow(double drainSourceVoltage,double drainCurrent, double gateCurrent)
        {
            m_DrainSourceVoltage = drainSourceVoltage;
            m_DrainCurrent = drainCurrent;
            m_GateCurrent = gateCurrent;
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
