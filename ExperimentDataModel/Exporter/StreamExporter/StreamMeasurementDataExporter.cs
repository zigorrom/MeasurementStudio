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
        /// <summary>
        /// test method
        /// </summary>
        public void Write(InfoT a)
        {
            var s = _exportInfoFunction(a);
        }
        public override void Write(MeasurementData<InfoT, DataT> measurement)
        {
            
        }

        public override void WriteDelayed(MeasurementData<InfoT, DataT> measurement)
        {
            throw new NotImplementedException();
        }
    }
}
