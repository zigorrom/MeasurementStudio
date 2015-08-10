using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ExperimentDataModel
{
    interface IExperimentDataCollection<DataT,DisplatT>: INotifyPropertyChanged, INotifyCollectionChanged, IEnumerable<DisplatT>, IObserver<DataT>
    {
        void Add(DataT item);
        void Clear();
        bool Contains(DataT item);
        void CopyTo(DataT[] array, int arrayIndex);
        int Count { get; }
        bool IsReadOnly { get; }
        bool Remove(DataT item);
        
    }
}
