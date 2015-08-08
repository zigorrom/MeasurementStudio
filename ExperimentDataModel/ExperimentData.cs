using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    [Serializable()]
    public class ExperimentData<InfoT, DataT>
        where InfoT: struct
        where DataT: struct
    {
        private InfoT _info;
        private List<DataT> _dataList;

    }
}
