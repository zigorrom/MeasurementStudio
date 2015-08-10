using OxyPlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    
    //[Serializable()]
    public class ExperimentData<InfoT, DataT>:IExperimentDataCollection<DataT,DataPoint>//ObservableCollection<DataPoint>//, ISerializable
        where InfoT: struct
        where DataT: struct
    {
        private const string CountString = "Count";
        private const string IndexerName = "Item[]";

        private InfoT _info;
        private List<DataT> _dataList;
        public Func<DataT, DataPoint> DisplayFunc { get; set; }
        private readonly SimpleMonitor _monitor;

        
        public ExperimentData(InfoT experimentInfo, Func<DataT,DataPoint> DefaultPredicate)
        {
            _monitor = new SimpleMonitor();
            _info = experimentInfo;
            _dataList = new List<DataT>();
            DisplayFunc = DefaultPredicate;
            
        }

        public void Add(DataT item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(DataT item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(DataT[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(DataT item)
        {
            throw new NotImplementedException();
        }


        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(DataT value)
        {
            throw new NotImplementedException();
        }


        public IEnumerator<DataPoint> GetEnumerator()
        {
            return _dataList.Select(DisplayFunc).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();
            return _monitor;
        }

        protected void CheckReentrancy()
        {
            if ((_monitor.Busy && (CollectionChanged != null)) && (CollectionChanged.GetInvocationList().Length > 1))
                throw new InvalidOperationException("Collection reentrancy not Allowed");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChaged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }


        public event NotifyCollectionChangedEventHandler CollectionChanged;
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if(handler!=null)
                using(BlockReentrancy())
                {
                    handler(this, e);
                }
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionReset()
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }


        private class SimpleMonitor : IDisposable
        {
            private int _busyCounter;

            public bool Busy
            {
                get { return _busyCounter > 0; }
            }

            public void Enter()
            {
                _busyCounter++;
            }
            public void Dispose()
            {
                _busyCounter--;
            }
        }




        
    }


}
