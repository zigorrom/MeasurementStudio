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
        
        private string m_FileName;
        private double m_DrainSourceVoltage;
        private string m_Comment;


        public GateSourceMeasurementInfoRow(string filename, double drainSourceVoltage, string Comment)
        {
            m_FileName = filename;
            m_DrainSourceVoltage = drainSourceVoltage;
            m_Comment = Comment;
        }

        [DataPropertyAttribute("FileName", "", "")]//true, true, -1, "FileName", "", "")]
        public string Filename
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        [DataPropertyAttribute("DrainSourceVoltage", "V", "")]//true, true, -1, "DrainSourceVoltage", "V", "")]
        public double DrainSourceVoltage
        {
            get { return m_DrainSourceVoltage;}
            set { m_DrainSourceVoltage = value; }
        }

        [DataPropertyAttribute("Comment", "", "")]//true, true, -1, "DrainSourceVoltage", "V", "")]
        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }
    }
}
