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
    
    internal class AnalogDataAquisitionController : IDataAquisition, IDisposable//IDataRouter
    {
        internal class AquisitionStateObject
        {
            public AquisitionStateObject()
            {
                m_AquisitionInProgress = false;
                m_AquisitionStopEvent = new ManualResetEvent(false);
                m_ProcessingStopEvent = new ManualResetEvent(false);
                m_AquisitionStoppedEvent = new ManualResetEvent(false);
                m_AllEventsArray = new WaitHandle[2] { m_AquisitionStopEvent, m_ProcessingStopEvent };
            }

            public EventWaitHandle AquisitionStopEvent
            {
                get { return m_AquisitionStopEvent; }
            }

            public EventWaitHandle AquisitionStoppedEvent
            {
                get { return m_AquisitionStoppedEvent; }
            }

            public EventWaitHandle ProcessingStopEvent
            {
                get { return m_ProcessingStopEvent; }
            }

            public WaitHandle[] EventArray
            {
                get { return m_AllEventsArray; }
            }

            public bool AquisitionInProgress
            {
                get { return m_AquisitionInProgress; }
                private set
                {
                    m_AquisitionInProgress = value;
                }
            }

            public void SignalAcquisitionStop()
            {
                m_AquisitionStopEvent.Set();
                AquisitionInProgress = false;
            }

            public void SignalAcquisitionStopped()
            {
                AquisitionInProgress = false;
                m_AquisitionStoppedEvent.Set();
            }

            public void SignalAqcuisitionStart()
            {
                throw new NotImplementedException();
            }

            public void SignalAcquisitionAbort()
            {
                throw new NotImplementedException();
            }

            private bool m_AquisitionInProgress;
            private EventWaitHandle m_AquisitionStoppedEvent;
            private EventWaitHandle m_AquisitionStopEvent;
            private EventWaitHandle m_ProcessingStopEvent;
            private WaitHandle[] m_AllEventsArray;
            
        }

        internal enum States
        {
            Idle,
            Start,
            InProgress,
            Stop,
            Abort
        }

        private AgilentU2542A _parentDevice;
        private Agilent.AgilentU254x.Interop.AgilentU254x _driver;
        private ConcurrentQueue<double[]> _rawDataQueue;
        private Thread _aquisitionThread;
        private Thread _routingThread;
        private AquisitionStateObject _threadControlObject;

        private bool _disposed = false;

        private object SyncObj = new object();

        private AnalogInputChannel[] _channels;

        private States _state;
        private States State
        {
            get { 
                //lock(SyncObj)
                //{
                    return _state;
                //}
            }
            set
            {
                //lock (SyncObj)
                //{
                    _state = value;
                //}
            }
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


        public AnalogDataAquisitionController(AgilentU2542A ParentDevice)
        {
            _parentDevice = ParentDevice;
            _driver = _parentDevice.Driver;

            InitThreads();
        }

        private void InitThreads()
        {
            _threadControlObject = new AquisitionStateObject();
            _rawDataQueue = new ConcurrentQueue<double[]>();
            _aquisitionThread = new Thread(new ParameterizedThreadStart(AquisitionLoop));
            _routingThread = new Thread(new ParameterizedThreadStart(DataRouteLoop));
            State = States.Idle;
            RunThreads();
        }

        private void RunThreads()
        {
            _aquisitionThread.Start();
            _routingThread.Start();
        }

        private void JoinThreads()
        {
            _aquisitionThread.Join();
            _routingThread.Join();
        }



        private void DataRouteLoop(object obj)
        {
            //throw new NotImplementedException();
        }




        private void AquisitionLoop(object obj)
        {
            while (true)
            {
                if (this._disposed)
                {
                    return;
                }

                lock (SyncObj)
                {
                    switch (State)
                    {
                        case States.Start:
                            {
                                OnAcquisitionStartRequest(_threadControlObject);
                                State = States.InProgress;
                            }
                            break;
                        case States.InProgress:
                            {
                                AcquisitionMethod(_threadControlObject);
                            }
                            break;
                        case States.Stop:
                            {
                                OnAcquisitionStopRequest(_threadControlObject);

                                State = States.Idle;
                            }
                            break;
                        case States.Abort:
                            {
                                OnAcquisitionStopRequest(_threadControlObject);
                                State = States.Idle;
                                return;
                            }
                        case States.Idle:
                        default:
                            break;
                    }
                }


            }

        }

        private void OnAcquisitionStartRequest(AquisitionStateObject state)
        {
            //Console.WriteLine("start");
        }

        private void AcquisitionMethod(AquisitionStateObject state)
        {
            
            while (state.AquisitionInProgress)
            {
                Console.WriteLine("acquisition");
            }
            State = States.Idle;
            //state.SignalAcquisitionStopped();
        }

        private void AcquisitionStopped()
        {
            //_threadControlObject.SignalAcquisitionStopped();
        }

        private void OnAcquisitionStopRequest(AquisitionStateObject state)
        {
            //Console.WriteLine("stop");
            //state.SignalAcquisitionStop();
            
        }

        public void Dispose()
        {
            bool waitForThreads = false;
            if(!this._disposed)
            {
                GC.SuppressFinalize(this);
                this._disposed = true;
                waitForThreads = true;
            }
            if(waitForThreads)
            {
                JoinThreads();
            }
        }
       

        private void PrepareChannels()
        {
            _channels = _parentDevice.GetAnalogInputChannels().Where(x => x.ChannelEnable).OrderBy(x=>x.ChannelName).ToArray();
        }


        
        public void StartAcquisition()
        {
            PrepareChannels();
            if (_channels == null || _channels.Length == 0)
                throw new ArgumentException();
            State = States.Start;
        }

       



        public void StopAcquisition()
        {
            State = States.Stop;
            _threadControlObject.AquisitionStoppedEvent.WaitOne();
            //WAIT HERE FOR THE THREAD TO FINISH EXECUTING CURRENT METHOD
        }

      
        // IMPLEMENT AMOUNT OF DATA TO AQUIRE


        
        

       

       

      




        //private void AquisitionMethod(object StateObj)
        //{
        //    //var state = StateObj as AquisitionControlObject;
        //    ////bool IsRunning = true;
            
        //    //double[] buffer = new double[1];
        //    ////int msToWait = 0;
        //    ////int ErrorCount = 0;
        //    ////const int MAX_ERROR = 3;

        //    ////while (IsRunning)
        //    ////{
        //    ////    switch (state.AquisitionState)
        //    ////    {
        //    ////        case States.Start:
        //    ////            {
        //    ////                buffer = new double[SamplesPerShot];
        //    ////                msToWait = 100 * SamplesPerShot / SampleRate;
        //    ////                _parentDevice.Driver.AnalogIn.MultiScan.NumberOfScans = -1;
        //    ////                _parentDevice.Driver.AnalogIn.Acquisition.Start();
        //    ////                state.AquisitionState = States.InProgress;
        //    ////            }
        //    ////            break;
        //    ////        case States.InProgress:
        //    ////            {
                            
        //    ////                if(ErrorCount < MAX_ERROR)
        //    ////                {
        //    ////                    try
        //    ////                    {
        //    ////                        switch (_parentDevice.Driver.AnalogIn.Acquisition.BufferStatus)
        //    ////                        {
        //    ////                            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady:
        //    ////                                {
        //    ////                                    _parentDevice.Driver.AnalogIn.Acquisition.FetchScale(ref buffer);
        //    ////                                    _rawDataQueue.Enqueue(buffer);
        //    ////                                    Console.WriteLine("DataReady");
        //    ////                                }
        //    ////                                break;

        //    ////                            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusFragment:
        //    ////                                Console.WriteLine("DataFragment");
        //    ////                                break;
        //    ////                            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusOverRun:
        //    ////                                Console.WriteLine("Overrun");
        //    ////                                break;
        //    ////                            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty:
        //    ////                                {
        //    ////                                    Console.WriteLine("Empty");
        //    ////                                    state.AquisitionState = States.Stop;
        //    ////                                }
        //    ////                                break;
        //    ////                        }
        //    ////                        Thread.Sleep(msToWait);
        //    ////                    }
        //    ////                    catch (Exception e)
        //    ////                    {
        //    ////                        ErrorCount++;

        //    ////                    }
        //    ////                }
        //    ////                else
        //    ////                {
        //    ////                    state.AquisitionState = States.Stop;
        //    ////                }
        //    ////            }
        //    ////            break;
        //    ////        case States.Pause:
        //    ////            {

        //    ////            }
        //    ////            break;
        //    ////        case States.Stop:
        //    ////            {
        //    ////                try
        //    ////                {
        //    ////                    _parentDevice.Driver.AnalogIn.Acquisition.Stop();
        //    ////                }
        //    ////                catch (Exception)
        //    ////                {

                                
        //    ////                }
        //    ////            }
        //    ////            break;
        //    ////        case States.Abort:
        //    ////            {

        //    ////            }
        //    ////            break;
        //    ////        case States.Idle:
        //    ////        default:
        //    ////            {

        //    ////            }
        //    ////            break;
        //    ////    }
        //    ////}


        //    //#region Olddata
        //    //bool dataReady = false;
        //    //bool cont = true;
        //    //bool stopped = false;
        //    //int errorCount = 0;
        //    //const int ERROR_MAX_OCCUR = 2;
        //    ////while (!_cancellationToken.IsCancellationRequested)
        //    //while (cont)
        //    //{
        //    //    try
        //    //    {
        //    //        if (true)//state.AquisitionStopEvent.WaitOne(0, false))
        //    //        {
        //    //            _parentDevice.Driver.AnalogIn.Acquisition.Stop();
        //    //            stopped = true;
        //    //        }
        //    //        ///
        //    //        /// Aquire data from parent device
        //    //        ///

        //    //        //double[] a = new double[sps];

        //    //        switch (_parentDevice.Driver.AnalogIn.Acquisition.BufferStatus)
        //    //        {
        //    //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusDataReady:
        //    //                {
        //    //                    _parentDevice.Driver.AnalogIn.Acquisition.FetchScale(ref buffer);
        //    //                    _rawDataQueue.Enqueue(buffer);
        //    //                    Console.WriteLine("DataReady");
        //    //                }
        //    //                break;

        //    //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusFragment:
        //    //                Console.WriteLine("DataFragment");
        //    //                break;
        //    //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusOverRun:
        //    //                Console.WriteLine("Overrun");
        //    //                break;
        //    //            case AgilentU254xBufferStatusEnum.AgilentU254xBufferStatusEmpty:
        //    //                {
        //    //                    Console.WriteLine("Empty");
        //    //                    if (stopped)
        //    //                        cont = false;
        //    //                }
        //    //                break;
        //    //        }
        //    //        // Thread.Sleep(msToWait);


        //    //    }
        //    //    catch
        //    //    {
        //    //        errorCount++;
        //    //        Console.WriteLine("{0} error occurance", errorCount);
        //    //        if (errorCount > ERROR_MAX_OCCUR)
        //    //        {
        //    //            try
        //    //            {
        //    //                _parentDevice.Driver.AnalogIn.Acquisition.Stop();

        //    //            }
        //    //            catch
        //    //            {
        //    //                Console.WriteLine("Error occured more than {0} times.", ERROR_MAX_OCCUR);

        //    //            }
        //    //            stopped = true;
        //    //            cont = false;
        //    //        }
        //    //    }
        //    //}
        //    ////state.ProcessingStopEvent.Set();
        //    //#endregion

        //}
        //private void RouteData(object StateObj)
        //{
        //    //var state = StateObj as AquisitionControlObject;

        //    //var nChannels = _channels.Length;
        //    //double[] temp;
        //    //while (true)//(!state.ProcessingStopEvent.WaitOne(0, false))
        //    //{
        //    //    while (_rawDataQueue.Count > 0)
        //    //    {
        //    //        if (_rawDataQueue.TryDequeue(out temp))
        //    //        {

        //    //            for (int i = 0; i < nChannels; i++)
        //    //            {
        //    //                double[] channelIdata = new double[temp.Length / nChannels];
        //    //                for (int j = i, k = 0; j < temp.Length; j += nChannels, k++)
        //    //                {
        //    //                    channelIdata[k] = temp[j];// _mappingFunction(temp[j]);
        //    //                }
        //    //                _channels[i].EnqueueData(channelIdata);
        //    //            }

        //    //        }
        //    //    }
        //    //}
           
        //}







        
    }
}
