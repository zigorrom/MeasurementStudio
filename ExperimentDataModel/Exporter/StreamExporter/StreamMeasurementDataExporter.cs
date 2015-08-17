using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public class StreamMeasurementDataExporter<InfoT,DataT>:AbstractMeasurementDataExporter<InfoT, DataT>
        where InfoT:struct
        where DataT:struct
    {


        public override void Write(MeasurementData<InfoT, DataT> measurement)
        {
            throw new NotImplementedException();
        }

        public override void WriteDelayed(MeasurementData<InfoT, DataT> measurement)
        {
            throw new NotImplementedException();
        }
    }
}
