using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public class ExperimentDataWriter<DataT, InfoT>
        where DataT : IFormattable
        where InfoT : IFormattable, IInfoDataRow
    {
        private StreamWriter m_InfoFileWriter;
        private DataWriter<DataT> m_DataFileWriter;

        private string m_WorkingDirectory;
        public string WorkingDirectory
        {
            get { return m_WorkingDirectory; }
            set { m_WorkingDirectory = value; }
        }

        public string InfoFileExtention { get; set; }
        public string DataFileExtention { get; set; }

        private const string DefaultFileExtention = "dat";
        private const string DefaultInfoFilePostfix = "_Main";
        private const string FileNameFormat = "{0}\\{1}.{2}";

        public ExperimentDataWriter(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
            //FileCounter = 0;
            
        }

        //private int FileCounter;
        //private 

        public void NewExperiment(string ExperimentName)
        {
            //FileCounter = 0;
            var infoFileExtention = String.IsNullOrEmpty(InfoFileExtention) ? DefaultFileExtention : InfoFileExtention;
            var InfoFileName = String.Format(FileNameFormat, WorkingDirectory, ExperimentName, infoFileExtention);

            if (File.Exists(InfoFileName))
                throw new IOException("Experiment filename exist.");

            m_InfoFileWriter = new StreamWriter(InfoFileName);
            //m_InfoFileWriter = new StreamWriter(String.Format("{0}\\{1}.{2}",WorkingDirectory, ExperimentName, InfoFileExtention));
            //m_DataFileWriter = new StreamWriter(String.Format("{0}\\{1}_{2}.{3}", WorkingDirectory, ExperimentName, FileCounter,InfoFileExtention));
        }



        public DataWriter<DataT> NewMeasurementStream(InfoT measurementInfo)
        {
            var fileExtention = String.IsNullOrEmpty(DataFileExtention) ? DefaultFileExtention : DataFileExtention;

            throw new NotImplementedException();
        }

        public void CloseWriter()
        {

        }


    }
}
