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
    [Serializable]
    public class MeasurementData<InfoT, DataT> : IMeasurementDataCollection<DataT, DataPoint>
        where InfoT : struct
        where DataT : struct
    {
        #region Property and collection changed events
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

            if (handler != null)

                handler(this, e);
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
#endregion




        private const string CountString = "Count";
        private const string IndexerName = "Item[]";

        private InfoT _info;
        private List<DataT> _dataList;
        public List<DataT> Items { get { return _dataList; } }

        public Func<DataT, DataPoint> DisplayFunc { get; set; }

        private object SyncRoot = new object();


        public MeasurementData(InfoT experimentInfo, Func<DataT, DataPoint> DefaultPredicate)
        {
            _info = experimentInfo;
            _dataList = new List<DataT>();
            DisplayFunc = DefaultPredicate;
        }
        ~MeasurementData()
        {

        }
        public void Add(DataT item)
        {
            lock (SyncRoot)
            {
                _dataList.Add(item);
                OnPropertyChaged(CountString);
                OnPropertyChaged(IndexerName);
                OnCollectionChanged(NotifyCollectionChangedAction.Add, item, _dataList.Count);
            }
        }

        public void Clear()
        {
            lock (SyncRoot)
            {
                _dataList.Clear();
                OnPropertyChaged(CountString);
                OnPropertyChaged(IndexerName);
                OnCollectionReset();
            }
        }

        public bool Contains(DataT item)
        {
            return _dataList.Contains(item);
        }

        public void CopyTo(DataT[] array, int arrayIndex)
        {
            _dataList.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return _dataList.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DataT item)
        {
            lock (SyncRoot)
            {
                var result = _dataList.Remove(item);
                OnPropertyChaged(CountString);
                OnPropertyChaged(IndexerName);
                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
                return result;
            }
        }


       
        public int IndexOf(DataT item)
        {
            return _dataList.IndexOf(item);
        }

        public void Insert(int index, DataT item)
        {
            _dataList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _dataList.RemoveAt(index);
        }

        public DataT this[int index]
        {
            get
            {
                return _dataList[index];
            }
            set
            {
                _dataList[index] = value;
            }
        }

        public IEnumerator<DataT> GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<DataPoint> DisplayEnumerator
        {
            get
            {
                return _dataList.Select(DisplayFunc).GetEnumerator();
            }
            
        }
    }


}
