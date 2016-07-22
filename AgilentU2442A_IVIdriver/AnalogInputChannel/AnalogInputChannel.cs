using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgilentU2442A_IVIdriver;
using System.Collections;
using System.Diagnostics;

namespace AgilentU2442A_IVIdriver
{
    public class AnalogInputChannel:AbstractChannel, IAnalogInputChannel
    {
        

        private ChannelEnableEnum m_ChannelEnable;
        public ChannelEnableEnum ChannelEnable
        {
            get { return m_ChannelEnable; }
            set
            {
                if (m_ChannelEnable == value)
                    return;
                if (!SendCommand(CommandSet.ROUTeENABle(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_ChannelEnable = value;
                OnPropertyChanged("OutputEnable");
            }
        }

        private VoltageRangeEnum m_VoltageRange;
        public VoltageRangeEnum VoltageRange
        {
            get { return m_VoltageRange; }
            set
            {
                if (m_VoltageRange == value)
                    return;
                if (!SendCommand(CommandSet.VOLTageRANGe(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_VoltageRange = value;
                OnPropertyChanged("VoltageRange");
            }
        }

        private PolarityEnum m_VoltagePolarity;
        public PolarityEnum VoltagePolarity
        {
            get { return m_VoltagePolarity; }
            set
            {
                if (m_VoltagePolarity == value)
                    return;
                if(!SendCommand(CommandSet.VOLTagePOLarity(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_VoltagePolarity = value;
                OnPropertyChanged("VoltagePolarity");
            }
        }

        private VoltageRangeEnum m_AquisitionVoltageRange;
        public VoltageRangeEnum AquisitionVoltageRange
        {
            get { return m_AquisitionVoltageRange; }
            set
            {
                if (m_AquisitionVoltageRange == value)
                    return;
                if (!SendCommand(CommandSet.ROUTeCHANnelRANGe(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_AquisitionVoltageRange = value;
                OnPropertyChanged("AquisitionVoltageRange");
            }
        }

        private PolarityEnum m_AquisitionVoltagePolarity;
        public PolarityEnum AquisitionVoltagePolarity
        {
            get { return m_AquisitionVoltagePolarity; }
            set
            {
                if (m_AquisitionVoltagePolarity == value)
                    return;
                if(!SendCommand(CommandSet.ROUTeCHANnelPOLarity(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_AquisitionVoltagePolarity = value;
                OnPropertyChanged("AquisitionVoltagePolarity");
            }
        }

        private int m_SampleRate;
        public int SampleRate
        {
            get { return m_SampleRate; }
            set
            {
                if (m_SampleRate == value)
                    return;
                if(!SendCommand(CommandSet.ACQuireSRATe(value)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_SampleRate = value;
                OnPropertyChanged("SampleRate");
            }
        }

        private int m_PointsPerShot;
        public int PointsPerShot
        {
            get { return m_PointsPerShot; }
            set
            {
                if (m_PointsPerShot == value)
                    return;
                if (!(SendCommand(CommandSet.ACQuirePOINts(value))&&SendCommand(CommandSet.WAVeformPOINts(value))))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_PointsPerShot = value;
                ParentDevice.SetBufferSize(m_PointsPerShot + 1000); // including 10 starting characters and \n in the end;
                OnPropertyChanged("PointsPerShot");
            }
        }


        private int m_AveragingNumber;

        public int AveragingNumber
        {
            get { return m_AveragingNumber; }
            set
            {
                if (m_AveragingNumber == value)
                    return;
                if(!SendCommand(CommandSet.VOLTageAVERage(value)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_AveragingNumber = value;
                OnPropertyChanged("AveragingNumber");
            }

        }

        //[Obsolete("Use [public AnalogInputChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice):base(ChannelIdentifier, ParentDevice)] constructor instead")]
        //public AnalogInputChannel(string NativeChannelName, AgilentU2542A ParentDevice)
        //    : base(NativeChannelName, ParentDevice)
        //{
        //    //base constructor automatically runs InitializeChannel() method
        //}

        public AnalogInputChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice):base(ChannelIdentifier, ParentDevice)
        {
            //base constructor automatically runs InitializeChannel() method
        }

        protected override void InitializeChannel()
        {
            InitializeAcquisitionModeParameters();
            InitializePollingModeParameters();
            m_DataAquireThread = new Thread(new ParameterizedThreadStart(DataAquireThreadCycle));
            //m_DataAquireThread.Priority = ThreadPriority.AboveNormal;
            m_DataTransformThread = new Thread(new ParameterizedThreadStart(DataTransformThreadCycle));
            //m_DataTransformThread.Priority = ThreadPriority.AboveNormal;
            m_AquiredDataQueue = new Queue<string>();
            m_ProcessedDataQueue = new Queue<double[]>();
            m_AquisitionStateObj = new AquisitionState();
            ParentDevice.SetTimeout(3000);
        }

        private void InitializeAcquisitionModeParameters()
        {
            //read data from device
            m_ChannelEnable = CommandSet.ROUTeENABleQueryParse(QueryCommand(CommandSet.ROUTeENABleQuery(ChannelName)));
            OnPropertyChanged("OutputEnable");
            m_PointsPerShot = CommandSet.ACQuirePOINtsQueryParse(QueryCommand(CommandSet.ACQuirePOINtsQuery()));
            OnPropertyChanged("PointsPerShot");
            m_SampleRate = CommandSet.ACQuireSRATeQueryParse(QueryCommand(CommandSet.ACQuireSRATeQuery()));
            OnPropertyChanged("SampleRate");
            m_AquisitionVoltagePolarity= CommandSet.ROUTeCHANnelPOLarityQueryParse(QueryCommand(CommandSet.ROUTeCHANnelPOLarityQuery(ChannelName)));
            OnPropertyChanged("AquisitionVoltagePolarity");
            m_AquisitionVoltageRange = CommandSet.ROUTeCHANnelRANGeQueryParse(QueryCommand(CommandSet.ROUTeCHANnelRANGeQuery(ChannelName)));
            OnPropertyChanged("AquisitionVoltageRange");
        }
        private void InitializePollingModeParameters()
        {
            //read data from device
            m_AveragingNumber = CommandSet.VOLTageAVERageQueryParse(QueryCommand(CommandSet.VOLTageAVERageQuery()));
            OnPropertyChanged("AveragingNumber");
            m_VoltagePolarity = CommandSet.VOLTagePOLarityQueryParse(QueryCommand(CommandSet.VOLTagePOLarityQuery(ChannelName)));
            OnPropertyChanged("VoltagePolarity");
            m_VoltageRange = CommandSet.VOLTageRANGeQueryParse(QueryCommand(CommandSet.VOLTageRANGeQuery(ChannelName)));
            OnPropertyChanged("VoltageRange");
        }

        public double AnalogRead()
        {
            double value = 0;
            if (!SendCommand(CommandSet.MEASureVOLTageDCQuery(NativeChannelName)))
                throw new MemberAccessException(MemberAccessExceptionMessage);
            var strVal = GetResponce();
            value = CommandSet.StringToDouble(strVal);
            return value;
        }

        public double AnalogRead(int NumberOfAverages)
        {
            AveragingNumber = NumberOfAverages;
            return AnalogRead();
        }

        public string SingleShotAquicition()
        {
            ChannelEnable = ChannelEnableEnum.Enabled;
            SendCommand(CommandSet.DIGitize());
            while (CommandSet.WAVeformCOMPleteQueryParse(QueryCommand(CommandSet.WAVeformCOMPleteQuery())) != WaveformComplete.YES) ;
            return QueryCommand(CommandSet.WAVeformDATAQuery());
        }

        public void SingleShotAquicition(out double[] data)
        {
            var StrArr = SingleShotAquicition();
            var ConversionFunction = InitConversionFunction();
            ParseStringToDoubleArray(ref StrArr, out data, ConversionFunction);
        }
        

        public void StartAcquisition()
        {
            m_AquisitionStateObj = new AquisitionState();
            ChannelEnable = ChannelEnableEnum.Enabled;
            //m_state.AquisitionInProcess = true;
            m_DataAquireThread.Start(m_AquisitionStateObj);
            m_DataTransformThread.Start(m_AquisitionStateObj);
        }

        public void StopAcquisition()
        {
            //SendCommand(CommandSet.STOP());
            m_AquisitionStateObj.AquisitionStopEvent.Set();
            m_DataAquireThread.Join();
            m_DataTransformThread.Join();
            ChannelEnable = ChannelEnableEnum.Disabled;
            //m_state.AquisitionInProcess = false;

        }

        Thread m_DataAquireThread;
        Thread m_DataTransformThread;
        Queue<string> m_AquiredDataQueue;
        Queue<double[]> m_ProcessedDataQueue;
        AquisitionState m_AquisitionStateObj;
        
        public Queue<double[]> DataQueue
        {
            get { return m_ProcessedDataQueue; }
        }

        public void DequeueData(out double[] data)
        {
            data = null;
            lock(((ICollection)m_ProcessedDataQueue).SyncRoot)
            {
                if (m_ProcessedDataQueue.Count > 0)
                    data = m_ProcessedDataQueue.Dequeue();
            }
        }


        public event EventHandler DataSetReady;
        private void OnDataSetReady()
        {
            var handler = DataSetReady;
            if(handler != null)
                handler(this, new EventArgs());
        }

        private void PopDataFromDeviceToQueue()
        {
            lock (((ICollection)m_AquiredDataQueue).SyncRoot)
            {
                SendCommand(CommandSet.WAVeformDATAQuery());
                //var data = QueryCommand(CommandSet.WAVeformDATAQuery());
                var data = GetResponce();
                m_AquiredDataQueue.Enqueue(data);
            }
        }

        private void CheckDeviceBuffer(object StateObj)
        {
            var State = StateObj as AquisitionState;
            m_DataBufferStatus = CommandSet.WAVeformSTATusQueryParse(QueryCommand(CommandSet.WAVeformSTATusQuery()));
            Debug.WriteLine(m_DataBufferStatus);
            switch (m_DataBufferStatus)
            {
                case WaveformStatus.EMPTY:
                case WaveformStatus.FRAG:
                    return;
                case WaveformStatus.DATA:
                    {
                        PopDataFromDeviceToQueue();
                        State.NewDataSetAquiredEvent.Set();
                    }
                    break;
                case WaveformStatus.OVER:
                    {
                        PopDataFromDeviceToQueue();
                        State.NewDataSetAquiredEvent.Set();
                        SendCommand(CommandSet.RUN());

                    }
                    break;
            }
        }

        private WaveformStatus m_DataBufferStatus;

        private void DataAquireThreadCycle(object StateObj)
        {
            var State = StateObj as AquisitionState;
            m_DataBufferStatus = WaveformStatus.EMPTY;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            SendCommand(CommandSet.RUN());
            while (!State.AquisitionStopEvent.WaitOne(0, false))
            {
                CheckDeviceBuffer(StateObj);
                Debug.WriteLine(sw.ElapsedMilliseconds);
            }
            Debug.WriteLine("End aquisition cycle");
            Debug.WriteLine(sw.ElapsedMilliseconds);
            SendCommand(CommandSet.STOP());
            Debug.WriteLine(sw.ElapsedMilliseconds);
            CheckDeviceBuffer(StateObj);
            State.ProcessingStopEvent.Set();
        }

        private const int HeaderLength = 10;
        private int ParseLengthAndRemoveHeader(ref string StrArr)
        {
            if (StrArr.Length < HeaderLength)
                return 0;
            var header = StrArr.Substring(0, HeaderLength);
            StrArr = StrArr.Substring(HeaderLength);
            if (!header.StartsWith("#8"))
                return 0;
            var len = 0;
            if (!int.TryParse(header.Substring(2), out len))
                return 0;
            if (len % 2 != 0)
                return 0;
            return len;
        }

        private void ParseStringToDoubleArray(ref string StrArr, out double[] data, Func<int,double> PolarityRangeDependentTransformFunction)
        {
            var len = ParseLengthAndRemoveHeader(ref StrArr)/2;
            Debug.WriteLine("parse length expected {0}, real{1}", len, StrArr.Length);
            data = new double[len];
            if (len == 0)
                return;
            //var bytes = Encoding.Default.GetBytes(StrArr);
            int IntValue = 0;
            int LSByte = 0;
            int MSByte = 0;
            int temp = 0;
            //char is 2 byte value
            for (int i = 0; i < len; i++)
            {
                temp = (int)StrArr[i];
                LSByte =  0xff&temp>> 8;
                MSByte = temp<< 8;
                IntValue = ( MSByte| LSByte);
                data[i] = PolarityRangeDependentTransformFunction(IntValue);
            }
        }

        private double InitRangeValue()
        {
            switch (AquisitionVoltageRange)
            {
                case VoltageRangeEnum.V10: return 10;
                case VoltageRangeEnum.V5: return 5;
                case VoltageRangeEnum.V2_5: return 2.5;
                case VoltageRangeEnum.V1_25: return 1.25;
                default: return 10;
            }
        }

        private const int Divider = 65536;
        private const int HalfDivider = 32768;

        private Func<int,double> InitConversionFunction()
        {
            double range = InitRangeValue();
            switch (AquisitionVoltagePolarity)
            {
                case PolarityEnum.Unipolar:
                    return new Func<int,double>((x)=>(x*1.0/Divider+0.5)*range);
                case PolarityEnum.Bipolar:
                default:
                    return new Func<int, double>((x) => (x*range / HalfDivider));
            }
        }

        private void DataTransformThreadCycle(object StateObj)
        {
            var State = StateObj as AquisitionState;
            var dataStr = String.Empty;
            double[] data;
            var ConversionFunction = InitConversionFunction();
            
            while((WaitHandle.WaitAny(State.ProcessingEventArray)!=1))
            {
                Debug.WriteLine("in the processing cycle");
                lock(((ICollection)m_AquiredDataQueue).SyncRoot)
                {
                    if (m_AquiredDataQueue.Count > 0)
                        dataStr = m_AquiredDataQueue.Dequeue();
                }
                //data = new double[1];
                ParseStringToDoubleArray(ref dataStr, out data, ConversionFunction);
                m_ProcessedDataQueue.Enqueue(data);
                OnDataSetReady();
            }
            Debug.WriteLine("processing cycle finished");
        }


       

        public static AnalogInputChannels operator +(AnalogInputChannel AI1, AnalogInputChannel AI2)
        {
            throw new NotImplementedException();
        }





        
    }
}
