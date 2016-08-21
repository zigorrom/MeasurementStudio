using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgilentU2442A_IVIdriver
{
    internal class AnalogDataAquisitionController
    {
        public AnalogDataAquisitionController(Agilent.AgilentU254x.Interop.AgilentU254x ParentDevice)
        {
            _parentDevice = ParentDevice;
            _cancellationTokenSource= new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _dataAcquisitionTask = new Task(AquisitionMethod, _cancellationToken);
            _dataRoutingTask = new Task(RouteData, _cancellationToken);
            _rawDataQueue = new ConcurrentQueue<double[]>();
        }

        private Agilent.AgilentU254x.Interop.AgilentU254x _parentDevice;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        Task _dataAcquisitionTask;
        Task _dataRoutingTask;
        ConcurrentQueue<double[]> _rawDataQueue;
        

        
        public void StartAcquisition()
        {
            _dataAcquisitionTask.Start();
            _dataRoutingTask.Start();
        }

        public void StopAcquisition()
        {
            _cancellationTokenSource.Cancel();
            _dataAcquisitionTask.Wait();
        }

        public void AquisitionMethod()
        {
            while(!_cancellationToken.IsCancellationRequested)
            {
                ///
                /// Aquire data from parent device
                ///
                var rnd = new Random();
                var a = new double[1000];
                for (int i = 0; i < 1000; i++)
                {
                    a[i] = rnd.NextDouble() * 1000;
                }
                _rawDataQueue.Enqueue(a);

            }
            
        }
        public void RouteData()
        {
            while(!_cancellationToken.IsCancellationRequested&&(_rawDataQueue.Count>0))
            {
                double[] temp;
                if (_rawDataQueue.TryDequeue(out temp))
                {

                }
            }
        }


      
    }
}
