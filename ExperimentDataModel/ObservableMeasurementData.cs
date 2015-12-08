using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ExperimentDataModel
{
    public class ObservableMeasurementData<InfoT,DataT>: ICollection<DataT>, INotifyCollectionChanged, IMeasuredDataObservable<DataT>, IPointDataSource
        where InfoT: struct, IMeasurementInfo
        where DataT: struct
    {
        public ObservableMeasurementData(InfoT info)
        {
            Info = info;
            observers = new List<IMeasuredDataObserver<DataT>>();
            _dataCollection = new List<DataT>();
            //var a = _dataCollection.GetRange( .GetEnumerator();
            _currentIndex = 0;
            
        }

        private List<DataT> _dataCollection;
        
        private InfoT _info;
        public InfoT Info
        {
            get { return _info; }
            private set { _info = value; }
        }

        #region Collection implementation

        public void Add(DataT item)
        {
            _dataCollection.Add(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add));
        }

        public void Clear()
        {
            _dataCollection.Clear();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(DataT item)
        {
            return _dataCollection.Contains(item);
        }

        public void CopyTo(DataT[] array, int arrayIndex)
        {
            _dataCollection.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _dataCollection.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(DataT item)
        {
            var result = _dataCollection.Remove(item);
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove));
            return result;
        }

        public IEnumerator<DataT> GetEnumerator()
        {
            return _dataCollection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            var handler = CollectionChanged;
            if(null!=handler)
            {
                handler(this, e);
            }

            RaiseDataChanged();
        }

        private void RaiseDataChanged()
        {
            if (Count % RefreshEveryNpoint >0)
                return;

            var handler = DataChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
        
        //public void SuspendUpdate()
        //{
        //    UpdatesEnabled = false;
        //}

        //public void ResumeUpdate()
        //{
        //    UpdatesEnabled = true;
        //    if (_collectionChanged)
        //    {
        //        _collectionChanged = false;
        //        RaiseDataChanged();
        //    }
        //}

        //private void RaiseDataChanged()
        //{
        //    if (Count % RefreshEveryNpoint >0)
        //        return;
            
        //    var handler = DataChanged;
        //    if (handler != null)
        //    {
        //        handler(this, EventArgs.Empty);
        //    }
        //}

        //protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        //{
        //    if (UpdatesEnabled)
        //    {
        //        base.OnCollectionChanged(e);
        //        RaiseDataChanged();
        //    }
        //    else
        //        _collectionChanged = true;
        //}

        //private DataT[] CurrentDisplayWindow
        //{
        //    get
        //    {
        //        var startIndex = 0;
        //        var length = Count;
        //        if (Count > DisplayPointsWindow)
        //        {
        //            startIndex = Count - DisplayPointsWindow;
        //            length = DisplayPointsWindow;
        //        }
        //        var displayArray = new DataT[length];
        //        CopyTo(displayArray, startIndex);
        //        return displayArray;
        //    }
        //}

        #region IPointEnumerator implementation

        public event EventHandler DataChanged;
        private Func<DataT, Point> xyMapping;
        private Func<DataT, double> xMapping;
        private Func<DataT, double> yMapping;

        private object SyncRoot = new object();


        private int _refreshEveryNpoints;
        public int RefreshEveryNpoint
        {
            get { return _refreshEveryNpoints; }
            set { _refreshEveryNpoints = value; }
        }


        private int _displayWindowPointsCount;
        public int DisplayWindowPointsCount
        {
            get { return _displayWindowPointsCount; }
            set { _displayWindowPointsCount = value; }
        }


        private bool _collectionChanged = false;

        private bool _updatesEnabled = true;
        public bool UpdatesEnabled
        {
            get { return _updatesEnabled; }
            private set { _updatesEnabled = value; }
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

        private int _currentIndex;


        public IPointEnumerator GetEnumerator(DependencyObject context)
        {
            _dataCollection.GetRange(0,0)
            throw new NotImplementedException();
            //   return new PointEnumerator(this);
        }

        private class PointEnumerator : IPointEnumerator
        {
            private readonly IList<DataT> dataSource;
            private readonly DataT[] data;

            private int _currentIndex;

            public PointEnumerator(IList<DataT> dataSource)
            {
                this.dataSource = dataSource;
                this.data = dataSource.CurrentDisplayWindow;
                _currentIndex = -1;
            }

            public void ApplyMappings(DependencyObject target)
            {
                dataSource.ApplyMappings(target, data[_currentIndex]);
            }

            public void GetCurrent(ref Point p)
            {
                dataSource.FillPoint(data[_currentIndex], ref p);
            }

            public bool MoveNext()
            {
                if (++_currentIndex >= data.Length)
                {
                    return false;
                }
                return true;
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }


        #endregion

        #region Observable implementation

        public IDisposable Subscribe(IMeasuredDataObserver<DataT> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
        }

        private List<IMeasuredDataObserver<DataT>> observers;

        private class Unsubscriber:IDisposable
        {
            private List<IMeasuredDataObserver<DataT>> _observers;
            private IMeasuredDataObserver<DataT> _observer;

            public Unsubscriber(List<IMeasuredDataObserver<DataT>> observers, IMeasuredDataObserver<DataT> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        #endregion

      


       


   




    }
}
