using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalDataModelTest
{
    internal struct DrainSourceDataRow : IFormattable
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

        public DrainSourceDataRow(double drainSourceVoltage, double drainCurrent, double gateCurrent)
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
            return String.Format(formatProvider, RowFormat, DrainSourceVoltage, DrainCurrent, GateCurrent);
        }
    }

    internal struct DrainSourceMeasurmentInfoRow : IInfoDataRow,IFormattable
    {
        private int m_ExperimentNumber;
        private string m_FileName;
        private double m_GateVoltage;
        private string m_Comment;

        public DrainSourceMeasurmentInfoRow(string filename, double gateVoltage, string comment, int experimentNumber)
        {
            m_FileName = filename;
            m_GateVoltage = gateVoltage;
            m_Comment = comment;
            m_ExperimentNumber = experimentNumber;
        }

        private const string RowFormat = "{0}\t{1}\t{2}\t{3}";
        public override string ToString()
        {
            return ToString(RowFormat, CultureInfo.CurrentCulture);
        }
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return String.Format(formatProvider, RowFormat, m_ExperimentNumber, m_GateVoltage, m_FileName, m_Comment);
        }
        
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

        [DataPropertyAttribute("#", "", "")]
        public int ExperimentNumber
        {
            get { return m_ExperimentNumber; }
            set { m_ExperimentNumber = value; }
        }





       
    }

    class Program
    {
        static void Main(string[] args)
        {
            //IVDataModel model = new IVDataModel();
            //RawDataFormatter fmt = new RawDataFormatter();
            //using(Stream s = new FileStream(Directory.GetCurrentDirectory()+"\\text.txt",FileMode.Create,FileAccess.Write,FileShare.None))
            //{
            //    fmt.Serialize(s, model);
            //}
            var a = new ExperimentDataWriter<DrainSourceDataRow, DrainSourceMeasurmentInfoRow>(@"C:\Users\i.zadorozhnyi\Desktop\");
           
        }
    }
}
