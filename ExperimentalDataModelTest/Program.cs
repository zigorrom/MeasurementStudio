using ExperimentDataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Forms;
//using System.Linq;

namespace ExperimentalDataModelTest
{
    public struct DrainSourceDataRow : IFormattable
    {

        private double m_DrainSourceVoltage;
        private double m_DrainCurrent;
        private double m_GateCurrent;

        [DataPropertyAttribute("DrainSourceVoltage", "V", "V\\-(DS)")]//true, true, -1, "V\\_DS", "DrainSourceVoltage", "V")]
        public double DrainSourceVoltage
        {
            get { return m_DrainSourceVoltage; }
            set { m_DrainSourceVoltage = value; }
        }

        [DataPropertyAttribute("DrainCurrent", "A", "I\\-(D)")]//true, true, -1, "I\\_D", "DrainCurrent", "A")]
        public double DrainCurrent
        {
            get { return m_DrainCurrent; }
            set { m_DrainCurrent = value; }
        }

        [DataPropertyAttribute("GateCurrent", "A", "I\\-(G)", PropertyOrderPriorityEnum.Highest)]//true, true, -1, "I\\_G", "GateCurrent", "A")]
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

    public struct DrainSourceMeasurmentInfoRow : IMeasurementInfo//IFormattable//IInfoDataRow, IFormattable
    {
        private int m_ExperimentNumber;
        private string m_FileName;
        private double m_GateVoltage;
        private string m_Comment;
        private DateTime m_ExperimentTime;

        public DrainSourceMeasurmentInfoRow(string filename, double gateVoltage, string comment, int experimentNumber,DateTime experimentTime)
        {
            m_FileName = filename;
            m_GateVoltage = gateVoltage;
            m_Comment = comment;
            m_ExperimentNumber = experimentNumber;
            m_ExperimentTime = experimentTime;
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

        [DataPropertyAttribute("FileName", "", "", PropertyOrderPriorityEnum.High)]//true, true, -1, "FileName", "", "")]
        public string Filename
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        [DataPropertyAttribute("GateVoltage", "V", "", PropertyOrderPriorityEnum.High)]//true, true, -1, "GateVoltage", "V", "")]
        public double GateVoltage
        {
            get { return m_GateVoltage; }
            set { m_GateVoltage = value; }
        }
        [DataPropertyAttribute("Comment", "", "", PropertyOrderPriorityEnum.Normal)]//true, true, -1, "GateVoltage", "V", "")]
        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        [DataPropertyAttribute("#", "", "", PropertyOrderPriorityEnum.Highest)]
        public int ExperimentNumber
        {
            get { return m_ExperimentNumber; }
            set { m_ExperimentNumber = value; }
        }

        [DataPropertyAttribute("Experiment time", "","experiment start time", PropertyOrderPriorityEnum.Lowest)]
        public DateTime ExperimentTime
        {
            get { return m_ExperimentTime; }
            set { m_ExperimentTime = value; }
        }



    }

  

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            FolderBrowserDialog sbd = new FolderBrowserDialog();
            sbd.ShowDialog();
            using (var s = new StreamMeasurementDataExporter<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(sbd.SelectedPath))
            {
                for (int j = 0; j < 5; j++)
                {
                    var info = new DrainSourceMeasurmentInfoRow(String.Concat("file_", j), j * 0.123, String.Concat("Comment_", j), j, DateTime.Now);
                    s.NewExperiment(String.Format("exp_{0}", j));
                    s.NewMeasurement(info);
                    for (int i = 0; i < 4; i++)
                    {
                        var a = new MeasurementData<DrainSourceMeasurmentInfoRow, DrainSourceDataRow>(info);
                        var rnd = new Random();
                        for (int k = 0; k < 1000; k++)
                        {
                            a.Add(new DrainSourceDataRow(rnd.NextDouble(), rnd.NextDouble() * 10, rnd.NextDouble() * 100));
                        }
                        s.WriteMeasurement(a);
                    }
                }
                
            }
            MessageBox.Show("Done");
            

           
        }
    }
}
