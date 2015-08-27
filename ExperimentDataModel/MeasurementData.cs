
using Microsoft.Research.DynamicDataDisplay.DataSources;
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
using System.Windows;

namespace ExperimentDataModel
{
    [Serializable]
    public class MeasurementData<InfoT, DataT> : IMeasurementDataCollection<DataT, Point>
        where InfoT : struct, IMeasurementInfo
        where DataT : struct
    {
//        #region Property and collection changed events
//        public event PropertyChangedEventHandler PropertyChanged;
//        protected virtual void OnPropertyChaged(string PropertyName)
//        {
//            var handler = PropertyChanged;
//            if (handler != null)
//                handler(this, new PropertyChangedEventArgs(PropertyName));
//        }


//        public event NotifyCollectionChangedEventHandler CollectionChanged;
//        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
//        {
//            var handler = CollectionChanged;

//            if (handler != null)

//                handler(this, e);
//        }

//        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item)
//        {
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item));
//        }

//        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
//        {
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
//        }

//        private void OnCollectionReset()
//        {
//            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
//        }
//#endregion


//        private const string CountString = "Count";
//        private const string IndexerName = "Item[]";

//        private InfoT _info;
//        public InfoT Info
//        {
//            get { return _info; }
//            private set { _info = value; }
//        }

//        private List<DataT> _dataList;
//        public List<DataT> Items { get { return _dataList; } }

//        public Func<DataT, Point> DisplayFunc { get; set; }

//        private object SyncRoot = new object();


//        public MeasurementData(InfoT experimentInfo, Func<DataT, Point> DefaultFunctor)
//        {
//            Info = experimentInfo;
//            _dataList = new List<DataT>();
//            DisplayFunc = DefaultFunctor;
//        }
//        ~MeasurementData()
//        {
//            IPointDataSource a = new ObservableDataSource<Point>();
            
//        }
//        public void Add(DataT item)
//        {
//            lock (SyncRoot)
//            {
//                _dataList.Add(item);
//                OnPropertyChaged(CountString);
//                OnPropertyChaged(IndexerName);
//                OnCollectionChanged(NotifyCollectionChangedAction.Add, item, _dataList.Count);
//            }
//        }

//        public void Clear()
//        {
//            lock (SyncRoot)
//            {
//                _dataList.Clear();
//                OnPropertyChaged(CountString);
//                OnPropertyChaged(IndexerName);
//                OnCollectionReset();
//            }
//        }

//        public bool Contains(DataT item)
//        {
//            return _dataList.Contains(item);
//        }

//        public void CopyTo(DataT[] array, int arrayIndex)
//        {
//            _dataList.CopyTo(array, arrayIndex);
//        }

//        public int Count
//        {
//            get
//            {
//                return _dataList.Count;
//            }
//        }

//        public bool IsReadOnly
//        {
//            get { return false; }
//        }

//        public bool Remove(DataT item)
//        {
//            lock (SyncRoot)
//            {
//                var result = _dataList.Remove(item);
//                OnPropertyChaged(CountString);
//                OnPropertyChaged(IndexerName);
//                OnCollectionChanged(NotifyCollectionChangedAction.Remove, item);
//                return result;
//            }
//        }


       
//        public int IndexOf(DataT item)
//        {
//            return _dataList.IndexOf(item);
//        }

//        public void Insert(int index, DataT item)
//        {
//            _dataList.Insert(index, item);
//        }

//        public void RemoveAt(int index)
//        {
//            _dataList.RemoveAt(index);
//        }

//        public DataT this[int index]
//        {
//            get
//            {
//                return _dataList[index];
//            }
//            set
//            {
//                _dataList[index] = value;
//            }
//        }

//        public IEnumerator<DataT> GetEnumerator()
//        {
//            return _dataList.GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }

//        public IEnumerator<Point> DisplayEnumerator
//        {
//            get
//            {
//                return _dataList.Select(DisplayFunc).GetEnumerator();
//            }
            
//        }

//        public event EventHandler DataChanged;

//        public IPointEnumerator GetEnumerator(DependencyObject context)
//        {
            
//            throw new NotImplementedException();
//        }

        private bool _collectionChanged = false;

        private bool _updatesEnabled = true;

        private InfoT _measurementInfo;

        private Func<DataT, Point> xyMapping;
        private Func<DataT, double> xMapping;
        private Func<DataT, double> yMapping;


        private readonly ObservableCollection<DataT> _measurementCollection;

        public MeasurementData(InfoT info)
        {
            _measurementInfo = info;

            if(typeof(DataT)==typeof(Point))
            {
                xyMapping = t => (Point)(object)t;
            }
        }

        public void SuspendUpdate()
        {
            _updatesEnabled = false;
        }

        public void ResumeUpdate()
        {
            _updatesEnabled = true;
            if (collectionChanged)
            {
                collectionChanged = false;
                RaiseDataChanged();
            }
        }





        private class PointEnumerator:IPointEnumerator
        {
            private readonly MeasurementData<InfoT, DataT> dataSource;
            private readonly IEnumerator<DataT> enumerator;

            public PointEnumerator(MeasurementData<InfoT, DataT> dataSource)
            {
                this.dataSource = dataSource;
                enumerator = dataSource.GetEnumerator();
            }

            public void ApplyMappings(DependencyObject target)
            {
                throw new NotImplementedException();
            }

            public void GetCurrent(ref Point p)
            {
                throw new NotImplementedException();
            }

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }


}
