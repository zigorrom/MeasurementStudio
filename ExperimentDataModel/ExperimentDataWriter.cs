using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public class ExperimentDataWriter<DataT,InfoT>
    {
        private StreamWriter m_InfoFileWriter;
        private StreamWriter m_DataFileWriter;

        private string m_WorkingDirectory;
        public string WorkingDirectory
        {
            get { return m_WorkingDirectory; }
            set { m_WorkingDirectory = value; }
        }

        public string InfoFileExtention { get; set; }
        public string DataFileExtention { get; set; }

        private const string DefaultFileExtention = "dat";
       

        public ExperimentDataWriter(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
            FileCounter = 1;
        }

        private int FileCounter;
        //private 

        public void Create(string ExperimentName)
        {
            FileCounter = 1;
            var fileExtention = String.IsNullOrEmpty(DataFileExtention)?DefaultFileExtention: DataFileExtention;
            var infoFileExtention = String.IsNullOrEmpty(InfoFileExtention)?DefaultFileExtention: InfoFileExtention;

            m_InfoFileWriter = new StreamWriter(String.Format("{0}\\{1}.{2}",WorkingDirectory, ExperimentName, InfoFileExtention));
            m_DataFileWriter = new StreamWriter(String.Format("{0}\\{1}_{2}.{3}", WorkingDirectory, ExperimentName, FileCounter,InfoFileExtention));
        }

        public void CloseWriter()
        {

        }


    }
}
