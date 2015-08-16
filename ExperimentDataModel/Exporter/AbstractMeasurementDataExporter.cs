using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public abstract class AbstractMeasurementDataExporter<InfoT,DataT>:IMeasurementDataExporter<InfoT,DataT>
        where InfoT:struct
        where DataT:struct
    {
        public AbstractMeasurementDataExporter()
        {

        }


        private string _workingDirectory;
        public string WorkingDirectory
        {
            get { return _workingDirectory; }
        }

        public void NewExperiment(string WorkingFolder)
        {
            _workingDirectory = WorkingFolder;
        }

        public abstract void Write(MeasurementData<InfoT, DataT> measurement);


        public abstract void WriteDelayed(MeasurementData<InfoT, DataT> measurement);
        
    }
}
