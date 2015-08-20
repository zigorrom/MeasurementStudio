using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExperimentDataModel
{
    public class SimpleMeasurementData<InfoT,DataT>:ObservableCollection<DataT>
        where InfoT:struct, IMeasurementInfo
        where DataT:struct
    {
        public Func<DataT, Point> MappingFunction { get; private set; }
        public InfoT Info { get; private set; }

        public SimpleMeasurementData(InfoT experimentInfo, Func<DataT,Point> DefaultMappingFunction)
        {
            Info = experimentInfo;
            MappingFunction = DefaultMappingFunction;
        }

    }

    
    
}
