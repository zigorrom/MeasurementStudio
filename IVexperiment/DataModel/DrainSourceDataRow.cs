using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVexperiment.DataModel
{
    [Serializable]
    public struct DrainSourceDataRow
    {
        
        private double m_DrainSourceVoltage;
        private double m_DrainCurrent;        
        private double m_GateCurrent;

        [DataPropertyAttribute("V\\-(DS)", "DrainSourceVoltage", "V")]
        public double DrainSourceVoltage
        {
            get { return m_DrainSourceVoltage; }
            set { m_DrainSourceVoltage = value; }
        }

        [DataPropertyAttribute("I\\-(D)", "DrainCurrent", "A")]
        public double DrainCurrent
        {
            get { return m_DrainCurrent; }
            set { m_DrainCurrent = value; }
        }
        [DataPropertyAttribute("I\\-(G)", "GateCurrent", "A")]
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
