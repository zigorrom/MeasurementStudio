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

        private readonly ObservableCollection<DataT> _measurementCollection = new ObservableCollection<DataT>();
        private object SyncRoot = new object();

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


        private int _updatePointsCounter = 0;


        private void RaiseDataChanged()
        {
            if (_updatePointsCounter < RefreshEveryNpoint)
                return;
            
            _updatePointsCounter = 0;
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

        public IDisposable Subscribe(IObserver<DataT[]> observer)
        {
            
            throw new NotImplementedException();
        }

        

        public IPointEnumerator GetEnumerator(DependencyObject context)
        {
            
            throw new NotImplementedException();
        }








        
    }
}
