using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{

    [Serializable]
    public struct GateSourceMeasurementInfoRow:IMeasurementInfo
    {
        
        private string m_FileName;
        private double m_DrainSourceVoltage;
        private string m_Comment;
        private int m_ExperimentNumber;


        public GateSourceMeasurementInfoRow(string filename, double drainSourceVoltage, string Comment, int experimentNumber)
        {
            m_FileName = filename;
            m_DrainSourceVoltage = drainSourceVoltage;
            m_Comment = Comment;
            m_ExperimentNumber = experimentNumber;
        }
        [DataPropertyAttribute("FileName", "", "")]
        public string Filename
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }
        [DataPropertyAttribute("DrainSourceVoltage", "V", "")]
        public double DrainSourceVoltage
        {
            get { return m_DrainSourceVoltage;}
            set { m_DrainSourceVoltage = value; }
        }
        [DataPropertyAttribute("Comment", "", "")]
        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        [DataPropertyAttribute("#", "", "")]
        public int ExperimentNumber
        {
            get { return m_ExperimentNumber; }
            set { m_ExperimentNumber = value; }
        }
    }
}
