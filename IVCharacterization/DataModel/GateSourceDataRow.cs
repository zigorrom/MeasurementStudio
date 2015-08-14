using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    public struct GateSourceDataRow:IFormattable
    {
        
        public GateSourceDataRow(double drainSourceVoltage, double drainCurrent, double gateCurrent)
        {
            m_GateSourceVoltage = drainSourceVoltage;
            m_DrainCurrent = drainCurrent;
            m_GateCurrent = gateCurrent; 
        }


        private double m_GateSourceVoltage;

        [DataPropertyAttribute("V\\_DS", "GateSourceVoltage", "V")]//true, true, -1, "V\\_DS", "GateSourceVoltage", "V")]
        public double GateSourceVoltage
        {
            get { return m_GateSourceVoltage; }
            set { m_GateSourceVoltage = value; }
        }


        private double m_DrainCurrent;
        [DataPropertyAttribute("I\\_D", "DrainCurrent", "A")]//true, true, -1, "I\\_D", "DrainCurrent", "A")]
        public double DrainCurrent
        {
            get { return m_DrainCurrent; }
            set { m_DrainCurrent = value; }
        }

        private double m_GateCurrent;
        [DataPropertyAttribute("I\\_G", "GateCurrent", "A")]//true, true, -1, "I\\_G", "GateCurrent", "A")]
        public double GateCurrent
        {
            get { return m_GateCurrent; }
            set { m_GateCurrent = value; }
        }

        private const string RowFormat = "{0}\t{1}\t{2}";
        public override string ToString()
        {
            return ToString(RowFormat, CultureInfo.CurrentCulture);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return String.Format(format, GateSourceVoltage, DrainCurrent, GateCurrent);
        }
    }
}
