using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    internal struct DrainSourceMeasurmentInfoRow:IInfoDataRow//,IFormattable
    {
        
        private string m_FileName;
        private double m_GateVoltage;
        private string m_Comment;

        public DrainSourceMeasurmentInfoRow(string filename, double gateVoltage,string comment)
        {
            m_FileName = filename;
            m_GateVoltage = gateVoltage;
            m_Comment = comment;
        }

        private const string RowFormat = "{0}\t{1}";
        public override string ToString()
        {
            return String.Format(RowFormat, m_FileName, m_GateVoltage);
        }

        //public string ToString(string format, IFormatProvider formatProvider)
        //{
        //    throw new NotImplementedException();
        //}

        [DataPropertyAttribute("FileName", "", "")]//true, true, -1, "FileName", "", "")]
        public string Filename
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        [DataPropertyAttribute("GateVoltage", "V", "")]//true, true, -1, "GateVoltage", "V", "")]
        public double GateVoltage
        {
            get { return m_GateVoltage; }
            set { m_GateVoltage = value; }
        }
        [DataPropertyAttribute("Comment", "", "")]//true, true, -1, "GateVoltage", "V", "")]
        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }
      
    }
}
