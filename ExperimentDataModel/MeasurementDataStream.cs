using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    [Serializable]
    class MeasurementDataStream<InfoT, DataT> : IPointDataSource
    {
        public event EventHandler DataChanged;

        public IPointEnumerator GetEnumerator(System.Windows.DependencyObject context)
        {
            throw new NotImplementedException();
        }

        public InfoT MeasurementInfo
        {
            private set;
            get;
        }
        
        private int blockSize;
        public int BlockSize
        {
            get { return blockSize; }
            set { blockSize = value; }
        }

        Queue<MeasurementDataBlock> _dataQueue;


    }

    class MeasurementDataBlock
    {

    }
}
