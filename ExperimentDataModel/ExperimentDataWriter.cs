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
        private const string FileNameFormat = "{0}{1}.{2}";

        public ExperimentDataWriter(string workingDirectory)
        {
            WorkingDirectory = workingDirectory;
            //FileCounter = 0;
            
        }
        ~ExperimentDataWriter()
        {
            //m_InfoFileWriter.Close();
            //m_DataFileWriter.Close();

        }
        //private int FileCounter;
        private string InfoFileHeader()
        {

            const string HeaderFormat = "{0}\t{4}\r\n{1}\t{5}\r\n{2}\t{6}";
            string Header = "";
            var infoType = typeof(InfoT);
            var properties = infoType.GetProperties();

            //for (int i = 0; i < 3; i++)
            //{
                for (int i = 0; i < properties.Length; i++)
                {
                    var attr = (DataPropertyAttribute)properties[i].GetCustomAttributes(typeof(DataPropertyAttribute), true)[0];
                    
                    Header += String.Format(Header, attr.PropertyName, attr.PropertyUnits, attr.PropertyComments);
                }

            //}
            
            
            return Header;
        }

        public void InitExperimentDataWriter(string ExperimentName)
        {
            //FileCounter = 0;
            var infoFileExtention = String.IsNullOrEmpty(InfoFileExtention) ? DefaultFileExtention : InfoFileExtention;
            var InfoFileName = String.Format(FileNameFormat, WorkingDirectory, ExperimentName, infoFileExtention);

            if (File.Exists(InfoFileName))
            {
                
                //throw new IOException("Experiment filename exist.");
            }


            m_InfoFileWriter = new StreamWriter(InfoFileName);
            var head = InfoFileHeader();
            m_InfoFileWriter.WriteLine(head);
            m_InfoFileWriter.Close();
            //m_InfoFileWriter = new StreamWriter(String.Format("{0}\\{1}.{2}",WorkingDirectory, ExperimentName, InfoFileExtention));
            //m_DataFileWriter = new StreamWriter(String.Format("{0}\\{1}_{2}.{3}", WorkingDirectory, ExperimentName, FileCounter,InfoFileExtention));
        }



        public DataWriter<DataT> InitMeasurementStream(InfoT measurementInfo)
        {
            var fileExtention = String.IsNullOrEmpty(DataFileExtention) ? DefaultFileExtention : DataFileExtention;

            throw new NotImplementedException();
        }

        public void CloseWriter()
        {

        }


    }
}
