using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IVCharacterization.DataModel
{
    internal class ExperimentDataWriter
    {
        private Stream m_InfoFileStream;
        private Stream m_DatafileStream;
        private string WorkingDirectory;

        private string InfoFileHeader;
        private string DataFileHeader;

    
        public ExperimentDataWriter()
        {
            
        }

        private void Initialize()
        {

        }

        private void InitHeader(Type dataType)
        {

        }

        public void NewMeasurement<T>(string Filename)
        {

        }





    }
}
