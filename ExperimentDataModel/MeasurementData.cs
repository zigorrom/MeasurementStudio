﻿
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
using System.Windows.Threading;

namespace ExperimentDataModel
{
    /// <summary>
    /// https://searchcode.com/codesearch/view/11042258/
    /// </summary>


    [Serializable]
    public class MeasurementData<InfoT, DataT> : IMeasurementDataCollection<DataT>
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

        public event EventHandler DataChanged;

        private bool _collectionChanged = false;

        private bool _updatesEnabled = true;

//        private InfoT _measurementInfo;
        public InfoT Info
        {
            get;
            private set;
        }


        private Func<DataT, Point> xyMapping;
        private Func<DataT, double> xMapping;
        private Func<DataT, double> yMapping;
        //private readonly List<Mapping<DataT>> mappings = new List<Mapping<DataT>>();
        

        private readonly ObservableCollection<DataT> _measurementCollection = new ObservableCollection<DataT>();

        public MeasurementData(InfoT info)
        {
            Info = info;
            _measurementCollection.CollectionChanged += OnCollectionChanged;
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
            if (_collectionChanged)
            {
                _collectionChanged = false;
                RaiseDataChanged();
            }
        }

        private void RaiseDataChanged()
        {
            var handler = DataChanged;
            if (handler != null)
            {
                
                //Dispatcher.CurrentDispatcher.Invoke(()=>handler(this, EventArgs.Empty));
                handler(this, EventArgs.Empty);
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_updatesEnabled)
                RaiseDataChanged();
            else
                _collectionChanged = true;
        }

        public ObservableCollection<DataT> Collection
        {
            get { return _measurementCollection; }
        }

        public void AppendMany(IEnumerable<DataT> data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            _updatesEnabled = false;
            foreach (var p in data)
            {
                
                _measurementCollection.Add(p);
            }
            _updatesEnabled = true;
            RaiseDataChanged();
        }

        public void SetXMapping(Func<DataT, double> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException();
            this.xMapping = mapping;
        }

        public void SetYMapping(Func<DataT, double> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException("mapping");

            this.yMapping = mapping;
        }

        public void SetXYMapping(Func<DataT, Point> mapping)
        {
            if (mapping == null)
                throw new ArgumentNullException("mapping");

            this.xyMapping = mapping;
        }

        private void ApplyMappings(DependencyObject target, DataT elem)
        {

            throw new NotImplementedException();
        //    //if (target != null)
        //    //{
        //    //    foreach (var mapping in mappings)
        //    //    //{
        //    //        target.SetValue(mapping.Property, mapping.F(elem));
        //    //    }
        //    //}
        }


        private void FillPoint(DataT elem, ref Point point)
        {
            if(xyMapping != null)
            {
                point = xyMapping(elem);
            }
            else
            {
                if (xMapping != null)
                {
                    point.X = xMapping(elem);
                }
                if (yMapping != null)
                {
                    point.Y = yMapping(elem);
                }
            }

        }

        public IPointEnumerator GetEnumerator(DependencyObject context)
        {
            return new PointEnumerator(this);
        }


        private class PointEnumerator:IPointEnumerator
        {
            private readonly MeasurementData<InfoT, DataT> dataSource;
            private readonly IEnumerator<DataT> enumerator;

            public PointEnumerator(MeasurementData<InfoT, DataT> dataSource)
            {
                this.dataSource = dataSource;
                enumerator = dataSource.Collection.GetEnumerator() ;
            }

            public void ApplyMappings(DependencyObject target)
            {
                dataSource.ApplyMappings(target, enumerator.Current);
            }

            public void GetCurrent(ref Point p)
            {
                dataSource.FillPoint(enumerator.Current, ref p);
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public void Dispose()
            {
                enumerator.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        public IEnumerator<DataT> GetEnumerator()
        {
            return _measurementCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


}
