using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public interface IMeasuredDataObserver<in DataT>:IObserver<DataT>
    {
        ObserverOptions Options { get; }
        void OnCompleted();
        void OnError(Exception error);
        void OnNext(DataT value);
        void OnNextDataSet(DataT[] samples);
    }
}
