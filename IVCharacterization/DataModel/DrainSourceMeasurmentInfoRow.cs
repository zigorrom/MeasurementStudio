using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    internal struct DrainSourceMeasurmentInfoRow:IInfoDataRow
    {
        
        private string m_FileName;
        
        private double m_GateVoltage;

        public DrainSourceMeasurmentInfoRow(string filename, double gateVoltage)
        {
            m_FileName = filename;
            m_GateVoltage = gateVoltage;
        }

        public override string ToString()
        {
            return String.Format("{0}\t{1}", m_FileName, m_GateVoltage);
        }

        [DataPropertyAttribute("FileName", "", "")]//true, true, -1, "FileName", "", "")]
        public string Filename
        {
            get
            {
                return m_FileName;
            }
            set
            {
                m_FileName = value;
            }
        }

        [DataPropertyAttribute("GateVoltage", "V", "")]//true, true, -1, "GateVoltage", "V", "")]
        public double GateVoltage
        {
            get { return m_GateVoltage; }
        }
    }
}
