
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

        //private readonly LinkedList<DataT> _measurementCollection = new LinkedList<DataT>();

        private readonly ObservableCollection<DataT> _measurementCollection = new ObservableCollection<DataT>();
        private object SyncRoot = new object();

        public MeasurementData(InfoT info)
        {
            Info = info;
            _measurementCollection.CollectionChanged += OnCollectionChanged;
            if (typeof(DataT) == typeof(Point))
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

            ///
            /// Implement rising event on every few samples
            ///



            var handler = DataChanged;
            if (handler != null)
            {
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
                //_measurementCollection.AddLast(p);
                _measurementCollection.Add(p);
            }
            _updatesEnabled = true;
            //OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
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

        }


        private void FillPoint(DataT elem, ref Point point)
        {
            if (xyMapping != null)
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


        private class PointEnumerator : IPointEnumerator
        {
            private readonly MeasurementData<InfoT, DataT> dataSource;
            private readonly IEnumerator<DataT> enumerator;

            public PointEnumerator(MeasurementData<InfoT, DataT> dataSource)
            {
                this.dataSource = dataSource;
                var collection = new List<DataT>(dataSource.Collection);
                enumerator = collection.GetEnumerator();
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

        #region ICollection

        public IEnumerator<DataT> GetEnumerator()
        {
            return _measurementCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(DataT item)
        {
            lock (SyncRoot)
            {
                _measurementCollection.Add(item);
                //_measurementCollection.AddLast(item);
                //OnCollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public void Clear()
        {
            _measurementCollection.Clear();
        }

        public bool Contains(DataT item)
        {
            return _measurementCollection.Contains(item);
        }

        public void CopyTo(DataT[] array, int arrayIndex)
        {
            _measurementCollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _measurementCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(DataT item)
        {
            lock (SyncRoot)
            {
                return _measurementCollection.Remove(item);
            }
        }
        #endregion
    }


}
