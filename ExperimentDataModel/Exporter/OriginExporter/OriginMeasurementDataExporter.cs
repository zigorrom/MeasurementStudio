using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public class OriginMeasurementDataExporter<InfoT, DataT>:IMeasurementDataExporter<InfoT,DataT>
        where InfoT : struct, IMeasurementInfo
        where DataT : struct
    {
        public string WorkingDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public void NewExperiment(string WorkingFolder)
        {
            throw new NotImplementedException();
        }

        public void Write(MeasurementData<InfoT, DataT> measurement)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
