using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    internal interface IMeasuredDataObserver<in DataT>:IObserver<DataT>
    {
        public ObserverOptions Options { get; }   
        public void OnNextDataSet(DataT[] samples);
    }
}
