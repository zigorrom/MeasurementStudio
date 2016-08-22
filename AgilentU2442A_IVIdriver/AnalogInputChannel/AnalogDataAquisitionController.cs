using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgilentU2442A_IVIdriver;

namespace AgilentU2442A_IVIdriver
{
    internal class AnalogDataAquisitionController//IDataRouter
    {
        public AnalogDataAquisitionController(AgilentU2542A ParentDevice)
        {
            _parentDevice = ParentDevice;
            //_mappingFunction = MapFunction;
            _cancellationTokenSource= new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _dataAcquisitionTask = new Task(AquisitionMethod, _cancellationToken);
            _dataRoutingTask = new Task(RouteData, _cancellationToken);
            _rawDataQueue = new ConcurrentQueue<double[]>();
        }

        private AgilentU2542A _parentDevice;
        private CancellationTokenSource _cancellationTokenSource;
        private CancellationToken _cancellationToken;
        Task _dataAcquisitionTask;
        Task _dataRoutingTask;
        ConcurrentQueue<double[]> _rawDataQueue;
        //private Func<double, DataT> _mappingFunction;
        //public void SetMapping(Func<double,DataT> mapFunction)
        //{
        //    _mappingFunction = mapFunction;
        //}


        private AnalogInputChannel[] _channels;
        private void PrepareChannels()
        {
            _channels = _parentDevice.GetAnalogInputChannels().Where(x => x.ChannelEnable).OrderBy(x=>x.ChannelName).ToArray();
        }
        

        
        //private SortedList<ChannelEnum, IAnalogInputChannel> _enabledAIChannels;
        
        public void StartAcquisition()
        {
            PrepareChannels();
            if (_channels == null || _channels.Length == 0)
                throw new ArgumentException();
            _dataAcquisitionTask.Start();
            _dataRoutingTask.Start();
        }

        public void StopAcquisition()
        {
            _cancellationTokenSource.Cancel();
            _dataAcquisitionTask.Wait();
        }

        private int SampleRate
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private int SamplesPerShot
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private void AquisitionMethod()
        {
            _parentDevice.Driver.AnalogIn.Acquisition.Start();
            _parentDevice.Driver.AnalogIn.Acquisition.BufferSize = 10000;
            while (!_cancellationToken.IsCancellationRequested)
            {
                ///
                /// Aquire data from parent device
                ///
                if (_parentDevice.Driver.AnalogIn.Acquisition.BufferStatus == Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady)
                {
                    double[] a = new double[1];
                    _parentDevice.Driver.AnalogIn.Acquisition.FetchScale(ref a);

                    //var rnd = new Random();
                    //var a = new double[1000];
                    //for (int i = 0; i < 1000; i++)
                    //{
                    //    a[i] = rnd.NextDouble() * 1000;
                    //}
                    _rawDataQueue.Enqueue(a);
                }
            }
            _parentDevice.Driver.AnalogIn.Acquisition.Stop();
        }
        private void RouteData()
        {
            var nChannels = _channels.Length;
            double[] temp;
            while(!(_cancellationToken.IsCancellationRequested&&(_rawDataQueue.Count==0)))
            {
                
                if (_rawDataQueue.TryDequeue(out temp))
                {
                    
                    for (int i = 0; i < nChannels; i++)
                    {
                        double[] channelIdata = new double[temp.Length / nChannels];
                        for (int j = i, k =0; j < temp.Length; j+=nChannels,k++)
                        {
                            channelIdata[k] = temp[j];// _mappingFunction(temp[j]);
                        }
                        _channels[i].EnqueueData(channelIdata);
                    }
                    
                }
            }
        }


        //public void EnableAIchannelsForAquisition(ChannelEnum channels)
        //{
        //    if ((channels & ChannelEnum.AI_CH101) != ChannelEnum.None) { }
        //    if ((channels & ChannelEnum.AI_CH102)!=ChannelEnum.None) { }
        //    if ((channels & ChannelEnum.AI_CH103) != ChannelEnum.None) { }
        //    if ((channels & ChannelEnum.AI_CH104) != ChannelEnum.None) { }
        //}


        
        
    }
}
