using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
//using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AgilentU2442A;
using System.Collections;

namespace AgilentU2442A
{
    public class AnalogInputChannel:AbstractChannel, IAnalogInputChannel
    {
        private const string MemberAccessExceptionMessage = "Value was not set on the device. Please check connectivity";

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


        public AnalogInputChannel(string NativeChannelName, AgilentU2542A ParentDevice):base(NativeChannelName,ParentDevice)
        {
            //base constructor automatically runs InitializeChannel() method
        }

        public AnalogInputChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice):base(ChannelIdentifier, ParentDevice)
        {
            //base constructor automatically runs InitializeChannel() method
        }

        protected override void InitializeChannel()
        {
            InitializeAcquisitionModeParameters();
            InitializePollingModeParameters();
            m_DataAquireThread = new Thread(new ParameterizedThreadStart(DataAquireThreadCycle));
            m_DataTransformThread = new Thread(new ParameterizedThreadStart(DataTransformThreadCycle));
            m_AquiredDataQueue = new Queue<string>();
            m_ProcessedDataQueue = new Queue<double[]>();
            m_state = new AquisitionState();
        }

        private void InitializeAcquisitionModeParameters()
        {
            //read data from device
            ChannelEnable = CommandSet.ROUTeENABleQueryParse(QueryCommand(CommandSet.ROUTeENABleQuery(ChannelName)));
            PointsPerShot = CommandSet.ACQuirePOINtsQueryParse(QueryCommand(CommandSet.ACQuirePOINtsQuery()));
            SampleRate = CommandSet.ACQuireSRATeQueryParse(QueryCommand(CommandSet.ACQuireSRATeQuery()));
            AquisitionVoltagePolarity= CommandSet.ROUTeCHANnelPOLarityQueryParse(QueryCommand(CommandSet.ROUTeCHANnelPOLarityQuery(ChannelName)));
            AquisitionVoltageRange = CommandSet.ROUTeCHANnelRANGeQueryParse(QueryCommand(CommandSet.ROUTeCHANnelRANGeQuery(ChannelName)));
        }
        private void InitializePollingModeParameters()
        {
            //read data from device
            AveragingNumber = CommandSet.VOLTageAVERageQueryParse(QueryCommand(CommandSet.VOLTageAVERageQuery()));
            VoltagePolarity = CommandSet.VOLTagePOLarityQueryParse(QueryCommand(CommandSet.VOLTagePOLarityQuery(ChannelName)));
            VoltageRange = CommandSet.VOLTageRANGeQueryParse(QueryCommand(CommandSet.VOLTageRANGeQuery(ChannelName)));
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


        public void StartAcquisition()
        {
            ChannelEnable = ChannelEnableEnum.Enabled;
            //m_state.AquisitionInProcess = true;
            m_DataAquireThread.Start(m_state);
            //m_DataTransformThread.Start(m_state);
        }

        public void StopAcquisition()
        {
            m_state.AquisitionStopEvent.Set();
            m_DataAquireThread.Join();
            //m_DataTransformThread.Join();
            ChannelEnable = ChannelEnableEnum.Disabled;
            //m_state.AquisitionInProcess = false;
            
            
        }

        Thread m_DataAquireThread;
        Thread m_DataTransformThread;
        Queue<string> m_AquiredDataQueue;
        Queue<double[]> m_ProcessedDataQueue;
        AquisitionState m_state;
        
        public Queue<double[]> DataQueue
        {
            get { return m_ProcessedDataQueue; }
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
                var data = QueryCommand(CommandSet.WAVeformDATAQuery());
                m_AquiredDataQueue.Enqueue(data);
            }
        }

        private void DataAquireThreadCycle(object obj)
        {
            var State = obj as AquisitionState;
            SendCommand(CommandSet.RUN());

            WaveformStatus dataStatus = WaveformStatus.EMPTY;
            while(!State.AquisitionStopEvent.WaitOne(0,false))
            {
                dataStatus = CommandSet.WAVeformSTATusQueryParse(QueryCommand(CommandSet.WAVeformSTATusQuery()));
                switch (dataStatus)
                {
                    case WaveformStatus.EMPTY:
                    case WaveformStatus.FRAG:
                        continue;
                    case WaveformStatus.DATA:
                        PopDataFromDeviceToQueue();
                        State.NewDataSetAquiredEvent.Set();
                        break;
                    case WaveformStatus.OVER:
                        PopDataFromDeviceToQueue();
                        SendCommand(CommandSet.RUN());
                        State.NewDataSetAquiredEvent.Set();
                        break;
                }
            }
            //WaveformStatus resp;
            //while(CommandSet.WAVeformCOMPleteQueryParse(QueryCommand(CommandSet.WAVeformCOMPleteQuery()))!= WaveformComplete.YES)
            //{
            //   // resp = CommandSet.WAVeformSTATusQueryParse(QueryCommand(CommandSet.WAVeformSTATusQuery()));
            //    //switch (resp)
            //    //{
            //    //    case WaveformStatus.EMPTY:
            //    //        continue;
            //    //    case WaveformStatus.FRAG:
            //    //        continue;
            //    //    case WaveformStatus.DATA:
            //    //        {
            //    //            m_AquiredDataQueue.Enqueue(QueryCommand(CommandSet.WAVeformDATAQuery()));
            //    //        }
            //    //        continue;
            //    //    case WaveformStatus.OVER:
            //    //        //State.Overload.Set();
            //    //        break;
            //    //    default:
            //    //        break;
            //    //}
            //}
            SendCommand(CommandSet.STOP());
        }

        private void DataTransformThreadCycle(object obj)
        {

        }


       

        public static AnalogInputChannels operator +(AnalogInputChannel AI1, AnalogInputChannel AI2)
        {
            throw new NotImplementedException();
        }





        
    }
}
