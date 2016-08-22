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
            _rawDataQueue = new ConcurrentQueue<double[]>();
            _aquisitionThread = new Thread(new ParameterizedThreadStart(AquisitionMethod));
            _routingThread = new Thread(new ParameterizedThreadStart(RouteData));
            
        }

        private AgilentU2542A _parentDevice;
       
        ConcurrentQueue<double[]> _rawDataQueue;

        //private Func<double, DataT> _mappingFunction;
        //public void SetMapping(Func<double,DataT> mapFunction)
        //{
        //    _mappingFunction = mapFunction;
        //}
        Thread _aquisitionThread;
        Thread _routingThread;

        private AnalogInputChannel[] _channels;
        private void PrepareChannels()
        {
            _channels = _parentDevice.GetAnalogInputChannels().Where(x => x.ChannelEnable).OrderBy(x=>x.ChannelName).ToArray();
        }
        

        
        //private SortedList<ChannelEnum, IAnalogInputChannel> _enabledAIChannels;
        private AquisitionState _aquisitionState;

        public void StartAcquisition()
        {
            PrepareChannels();
            if (_channels == null || _channels.Length == 0)
                throw new ArgumentException();
            _aquisitionState = new AquisitionState();
            _aquisitionThread.Start(_aquisitionState);
            _routingThread.Start(_aquisitionState);
            
        }

        public void StopAcquisition()
        {
            _aquisitionState.AquisitionStopEvent.Set();
            _aquisitionThread.Join();
            _routingThread.Join();
            
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

        private void AquisitionMethod(object StateObj)
        {
            var state = StateObj as AquisitionState;

            _parentDevice.Driver.AnalogIn.Acquisition.BufferSize = 50000;
            _parentDevice.Driver.AnalogIn.MultiScan.SampleRate = 500000;
            _parentDevice.Driver.AnalogIn.MultiScan.NumberOfScans = -1;
            
            _parentDevice.Driver.AnalogIn.Acquisition.Start();
            
            //while (!_cancellationToken.IsCancellationRequested)
            while (!state.AquisitionStopEvent.WaitOne(0, false))
            {
                ///
                /// Aquire data from parent device
                ///
                double[] a = new double[1];
                try
                {
                    while (_parentDevice.Driver.AnalogIn.Acquisition.BufferStatus != Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty)
                    {
                        if (_parentDevice.Driver.AnalogIn.Acquisition.BufferStatus == Agilent.AgilentU254x.Interop.AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady)
                        {

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
                }catch
                {
                    int code =0;
                    string message = String.Empty;
                    _parentDevice.Driver.Utility.ErrorQuery(ref code, ref message);
                    throw;
                }
            }
            state.ProcessingStopEvent.Set();
            _parentDevice.Driver.AnalogIn.Acquisition.Stop();
            
        }
        private void RouteData(object StateObj)
        {
            var state = StateObj as AquisitionState;

            var nChannels = _channels.Length;
            double[] temp;
            while (!state.ProcessingStopEvent.WaitOne(0, false))
            {
                while (_rawDataQueue.Count > 0)
                {
                    if (_rawDataQueue.TryDequeue(out temp))
                    {

                        for (int i = 0; i < nChannels; i++)
                        {
                            double[] channelIdata = new double[temp.Length / nChannels];
                            for (int j = i, k = 0; j < temp.Length; j += nChannels, k++)
                            {
                                channelIdata[k] = temp[j];// _mappingFunction(temp[j]);
                            }
                            _channels[i].EnqueueData(channelIdata);
                        }

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
