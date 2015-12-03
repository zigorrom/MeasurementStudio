using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
