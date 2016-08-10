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

        Queue<MeasurementDataBlock<DataT>> _dataQueue;



    }

    struct MeasurementDataBlock<DataT>:IPointDataSource
    {
        public MeasurementDataBlock(int blockNumber, DataT[] data)
        {
            BlockNumber = blockNumber;
            Data = data;
        }

        private int _blockNumber;

        public int BlockNumber
        {
            get { return _blockNumber; }
            private set { _blockNumber = value; }
        }

        private DataT[] _data;
        public DataT[] Data
        {
            get { return _data; }
            private set { _data = value; }
        }


        public event EventHandler DataChanged;

        public IPointEnumerator GetEnumerator(System.Windows.DependencyObject context)
        {
            throw new NotImplementedException();
        }
    }
}
