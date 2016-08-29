using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgilentU2442A_IVIdriver;
using Agilent.AgilentU254x.Interop;

namespace AgilentU2442A_IVIdriver
{
    
    internal class AnalogDataAquisitionController : IDataAquisition//IDataRouter
    {
        
        internal enum States
        {
            Idle,
            Start,
            InProgress,
            Pause,
            Stop,
            Abort
        }



        internal class AquisitionControlObject
        {
            public AquisitionControlObject()
            {
                AquisitionState = States.Idle;
                ProcessingState = States.Idle;
                //m_NewDataSetAquiredEvent = new AutoResetEvent(false);
                //m_AquisitionStopEvent = new ManualResetEvent(false);
                //m_ProcessingStopEvent = new ManualResetEvent(false);
                //m_AllEventsArray = new WaitHandle[2] { m_AquisitionStopEvent, m_ProcessingStopEvent };
                //m_ProcessingEventArray = new WaitHandle[1] { m_ProcessingStopEvent };
            }

            //private AquisitionState _aquisitionState;
            public States AquisitionState { get;  set; }

            public States ProcessingState { get; set; }

            public void StartAll()
            {
                AquisitionState = States.Start;
                ProcessingState = States.Start;
            }

            public void StopAll()
            {
                AquisitionState = States.Stop;
                ProcessingState = States.Stop;
            }
            

            

            //public EventWaitHandle AquisitionStopEvent
            //{
            //    get { return m_AquisitionStopEvent; }
            //}


            //public EventWaitHandle ProcessingStopEvent
            //{
            //    get { return m_ProcessingStopEvent; }
            //}

            //public WaitHandle[] EventArray
            //{
            //    get { return m_AllEventsArray; }
            //}

            //public WaitHandle[] ProcessingEventArray
            //{
            //    get { return m_ProcessingEventArray; }
            //}

            //private EventWaitHandle m_AquisitionStopEvent;
            //private EventWaitHandle m_ProcessingStopEvent;
            //private WaitHandle[] m_AllEventsArray;
            //private WaitHandle[] m_ProcessingEventArray;
        }
       


        public AnalogDataAquisitionController(AgilentU2542A ParentDevice)
        {
           
            _parentDevice = ParentDevice;
            _driver = _parentDevice.Driver;
            //_mappingFunction = MapFunction;

            InitThreads();
        }

        private void InitThreads()
        {
            _rawDataQueue = new ConcurrentQueue<double[]>();
            _aquisitionState = new AquisitionControlObject();
            _aquisitionThread = new Thread(new ParameterizedThreadStart(AquisitionMethod));
            _routingThread = new Thread(new ParameterizedThreadStart(RouteData));
            _aquisitionThread.Start(_aquisitionState);
            _routingThread.Start(_aquisitionState);
        }

        private AgilentU2542A _parentDevice;
        private Agilent.AgilentU254x.Interop.AgilentU254x _driver;
        ConcurrentQueue<double[]> _rawDataQueue;
        Thread _aquisitionThread;
        Thread _routingThread;
        
        private AnalogInputChannel[] _channels;
        
        private void PrepareChannels()
        {
            _channels = _parentDevice.GetAnalogInputChannels().Where(x => x.ChannelEnable).OrderBy(x=>x.ChannelName).ToArray();
        }
        
        private AquisitionControlObject _aquisitionState;

        private Task _aquisitionTask;
        private Task _routingTask;

        public void StartAcquisition()
        {
            PrepareChannels();
            if (_channels == null || _channels.Length == 0)
                throw new ArgumentException();
            _aquisitionState.StartAll();
            //_aquisitionThread.Start(_aquisitionState);
            //_routingThread.Start(_aquisitionState);
        }

        public void StopAcquisition()
        {
            //_aquisitionState.AquisitionStopEvent.Set();
            _aquisitionState.StopAll();
            //_aquisitionThread.Join();
            //_routingThread.Join();
            
        }

        public int SampleRate
        {
            get
            {
                return _driver.AnalogIn.MultiScan.SampleRate;
            }
            set
            {
                _driver.AnalogIn.MultiScan.SampleRate = value;
            }
        }

        public int SamplesPerShot
        {
            get
            {
                return _driver.AnalogIn.Acquisition.BufferSize;
            }
            set
            {
                _driver.AnalogIn.Acquisition.BufferSize = value;
            }
        }



        private void AquisitionMethod(object StateObj)
        {
            var state = StateObj as AquisitionControlObject;
            bool IsRunning = true;

            double[] buffer = new double[1];
            int msToWait = 0;
            int ErrorCount = 0;
            const int MAX_ERROR = 3;

            while (IsRunning)
            {
                switch (state.AquisitionState)
                {
                    case States.Start:
                        {
                            buffer = new double[SamplesPerShot];
                            msToWait = 100 * SamplesPerShot / SampleRate;
                            _parentDevice.Driver.AnalogIn.MultiScan.NumberOfScans = -1;
                            _parentDevice.Driver.AnalogIn.Acquisition.Start();
                            state.AquisitionState = States.InProgress;
                        }
                        break;
                    case States.InProgress:
                        {
                            
                            if(ErrorCount < MAX_ERROR)
                            {
                                try
                                {
                                    switch (_parentDevice.Driver.AnalogIn.Acquisition.BufferStatus)
                                    {
                                        case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady:
                                            {
                                                _parentDevice.Driver.AnalogIn.Acquisition.FetchScale(ref buffer);
                                                _rawDataQueue.Enqueue(buffer);
                                                Console.WriteLine("DataReady");
                                            }
                                            break;

                                        case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusFragment:
                                            Console.WriteLine("DataFragment");
                                            break;
                                        case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusOverRun:
                                            Console.WriteLine("Overrun");
                                            break;
                                        case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty:
                                            {
                                                Console.WriteLine("Empty");
                                                state.AquisitionState = States.Stop;
                                            }
                                            break;
                                    }
                                    Thread.Sleep(msToWait);
                                }
                                catch (Exception e)
                                {
                                    ErrorCount++;

                                }
                            }
                            else
                            {
                                state.AquisitionState = States.Stop;
                            }
                        }
                        break;
                    case States.Pause:
                        {

                        }
                        break;
                    case States.Stop:
                        {
                            try
                            {
                                _parentDevice.Driver.AnalogIn.Acquisition.Stop();
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                        break;
                    case States.Abort:
                        {

                        }
                        break;
                    case States.Idle:
                    default:
                        {

                        }
                        break;
                }
            }

            

            //bool dataReady = false;
            //bool cont = true;
            //bool stopped = false;
            //int errorCount = 0;
            //const int ERROR_MAX_OCCUR = 2;
            ////while (!_cancellationToken.IsCancellationRequested)
            //while (cont)
            //{
            //    try
            //    {
            //        if (true)//state.AquisitionStopEvent.WaitOne(0, false))
            //        {
            //            _parentDevice.Driver.AnalogIn.Acquisition.Stop();
            //            stopped = true;
            //        }
            //        ///
            //        /// Aquire data from parent device
            //        ///
                    
            //        //double[] a = new double[sps];
                   
            //        switch (_parentDevice.Driver.AnalogIn.Acquisition.BufferStatus)
            //        {
            //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady:
            //                {
            //                    _parentDevice.Driver.AnalogIn.Acquisition.FetchScale(ref buffer);
            //                    _rawDataQueue.Enqueue(buffer);
            //                    Console.WriteLine("DataReady");
            //                }
            //                break;

            //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusFragment:
            //                Console.WriteLine("DataFragment");
            //                break;
            //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusOverRun:
            //                Console.WriteLine("Overrun");
            //                break;
            //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty:
            //                {
            //                    Console.WriteLine("Empty");
            //                    if (stopped)
            //                        cont = false;
            //                }
            //                break;
            //        }
            //       // Thread.Sleep(msToWait);
                   

            //    }
            //    catch
            //    {
            //        errorCount++;
            //        Console.WriteLine("{0} error occurance",errorCount);
            //        if (errorCount > ERROR_MAX_OCCUR)
            //        {
            //            try
            //            {
            //                _parentDevice.Driver.AnalogIn.Acquisition.Stop();
                            
            //            }
            //            catch
            //            {
            //                Console.WriteLine("Error occured more than {0} times.", ERROR_MAX_OCCUR);
                            
            //            }
            //            stopped = true;
            //            cont = false;
            //        }
            //    }
            //}
           // state.ProcessingStopEvent.Set();


        }
        private void RouteData(object StateObj)
        {
            var state = StateObj as AquisitionControlObject;

            var nChannels = _channels.Length;
            double[] temp;
            while (true)//(!state.ProcessingStopEvent.WaitOne(0, false))
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
