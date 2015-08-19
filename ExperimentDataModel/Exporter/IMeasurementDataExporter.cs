using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public interface IMeasurementDataExporter<InfoT,DataT>:IDisposable
        where InfoT:struct,IMeasurementInfo
        where DataT:struct
    {
        string WorkingDirectory { get;  }
        void NewExperiment(string WorkingFolder);

        void Write(MeasurementData<InfoT, DataT> measurement);
        
    }
}
