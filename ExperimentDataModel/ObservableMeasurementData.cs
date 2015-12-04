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
    public class ObservableMeasurementData<InfoT,DataT>: ObservableCollection<DataT>, IObservable<DataT[]>, IPointDataSource
        where InfoT: struct, IMeasurementInfo
        where DataT: struct
    {
        public ObservableMeasurementData(InfoT info)
        {
            Info = info;
        }

        private InfoT _info;

        public InfoT Info
        {
            get { return _info; }
            private set { _info = value; }
        }

        public event EventHandler DataChanged;
        private Func<DataT, Point> xyMapping;
        private Func<DataT, double> xMapping;
        private Func<DataT, double> yMapping;
       
        private object SyncRoot = new object();

        #region Settings

        private int _refreshEveryNpoints;

        public int RefreshEveryNpoint
        {
            get { return _refreshEveryNpoints; }
            set { _refreshEveryNpoints = value; }
        }

        
        private int _saveRequestEveryNpoints;

        public int SaveRequestEveryNpoints
        {
            get { return _saveRequestEveryNpoints; }
            set { _saveRequestEveryNpoints = value; }
        }

        
        private int _displayPointsWindow;

        public int DisplayPointsWindow
        {
            get { return _displayPointsWindow; }
            set { _displayPointsWindow = value; }
        }
        

        private bool _collectionChanged = false;

        private bool _updatesEnabled = true;

        public bool UpdatesEnabled
        {
            get { return _updatesEnabled; }
            private set { _updatesEnabled = value; }
        }


        #endregion 

        public void SuspendUpdate()
        {
            UpdatesEnabled = false;
        }

        public void ResumeUpdate()
        {
            UpdatesEnabled = true;
            if (_collectionChanged)
            {
                _collectionChanged = false;
                RaiseDataChanged();
            }
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

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (UpdatesEnabled)
            {
                base.OnCollectionChanged(e);
                RaiseDataChanged();
            }
            else
                _collectionChanged = true;
        }



        private DataT[] CurrentDisplayWindow
        {
            get
            {
                var startIndex = 0;
                var length = Count;
                if (Count > DisplayPointsWindow)
                {
                    startIndex = Count - DisplayPointsWindow;
                    length = DisplayPointsWindow;
                }
                var displayArray = new DataT[length];
                CopyTo(displayArray, startIndex);
                return displayArray;
            }
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

        private void ApplyMappings(DependencyObject target, object p)
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<DataT[]> observer)
        {
            
            throw new NotImplementedException();
        }

        

        public IPointEnumerator GetEnumerator(DependencyObject context)
        {
            return new PointEnumerator(this);
        }


        private class PointEnumerator : IPointEnumerator
        {
            private readonly ObservableMeasurementData<InfoT, DataT> dataSource;
            private readonly IEnumerator enumerator;

            public PointEnumerator(ObservableMeasurementData<InfoT, DataT> dataSource)
            {
                this.dataSource = dataSource;
                enumerator = dataSource.CurrentDisplayWindow.GetEnumerator();
            }

            public void ApplyMappings(DependencyObject target)
            {
                dataSource.ApplyMappings(target, enumerator.Current);
            }

            public void GetCurrent(ref Point p)
            {
                dataSource.FillPoint((DataT)enumerator.Current, ref p);
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }


    }
}
