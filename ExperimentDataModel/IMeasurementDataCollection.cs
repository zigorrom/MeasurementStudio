using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ExperimentDataModel
{
    interface IMeasurementDataCollection<DataT,DisplayT>:INotifyPropertyChanged, INotifyCollectionChanged, IList<DataT>//, IEnumerable<DisplatT>,// IObserver<DataT>//, IList<DataT>
    {
        IEnumerator<DisplayT> DisplayEnumerator { get; }

        //void Add(DataT item);
        //void Clear();
        //bool Contains(DataT item);
        //void CopyTo(DataT[] array, int arrayIndex);
        //int Count { get; }
        //bool IsReadOnly { get; }
        //bool Remove(DataT item);
        
    }
}
