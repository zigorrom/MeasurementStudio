using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public interface IMeasurementDataExporter<InfoT,DataT>
        where InfoT:struct
        where DataT:struct
    {
        string WorkingDirectory { get;  }
        void NewExperiment(string WorkingFolder);

        void Write(MeasurementData<InfoT, DataT> measurement);
        void WriteDelayed(MeasurementData<InfoT, DataT> measurement);
    }
}
