using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogInputChannel:AbstractChannel, IAnalogInputChannel
    {
        private const string MemberAccessExceptionMessage = "Value was not set on the device. Please check connectivity";

        private ChannelOutputEnableEnum m_OutputEnable;
        public ChannelOutputEnableEnum OutputEnable
        {
            get { return m_OutputEnable; }
            set
            {
                if (m_OutputEnable == value)
                    return;
                if (!SendCommand(CommandSet.ROUTeENABle(value, ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_OutputEnable = value;
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
                if (!SendCommand(CommandSet.ACQuirePOINts(value)))
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
            
            
        }

        public AnalogInputChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice):base(ChannelIdentifier, ParentDevice)
        {

        }

        protected override void InitializeChannel()
        {
            //m_AcquisitionParameters = new AIAquisitionParameters();
            //m_PollingParameters = new AIPollingModeParameters();
            //AquisitionParameters.PropertyChanged+=
            InitializeAcquisitionMode();
            InitializePollingMode();

        }

      

        public void InitializeAcquisitionMode()
        {
            //read data from device
            m_OutputEnable = CommandSet.ROUTeENABleQueryParse(QueryCommand(CommandSet.ROUTeENABleQuery(ChannelName)));
            m_PointsPerShot = CommandSet.ACQuirePOINtsQueryParse(QueryCommand(CommandSet.ACQuirePOINtsQuery()));
            m_SampleRate = CommandSet.ACQuireSRATeQueryParse(QueryCommand(CommandSet.ACQuireSRATeQuery()));
            m_AquisitionVoltagePolarity = CommandSet.ROUTeCHANnelPOLarityQueryParse(QueryCommand(CommandSet.ROUTeCHANnelPOLarityQuery(ChannelName)));
            m_AquisitionVoltageRange = CommandSet.ROUTeCHANnelRANGeQueryParse(QueryCommand(CommandSet.ROUTeCHANnelRANGeQuery(ChannelName)));
        }
        public void InitializePollingMode()
        {
            //read data from device
            m_AveragingNumber = CommandSet.VOLTageAVERageQueryParse(QueryCommand(CommandSet.VOLTageAVERageQuery()));
            m_VoltagePolarity = CommandSet.VOLTagePOLarityQueryParse(QueryCommand(CommandSet.VOLTagePOLarityQuery(ChannelName)));
            m_VoltageRange = CommandSet.VOLTageRANGeQueryParse(QueryCommand(CommandSet.VOLTageRANGeQuery(ChannelName)));
        }

        

        
        

        
        public virtual double AnalogRead()
        {
            double value = 0;
            try
            {
                SendCommand(CommandSet.MEASureVOLTageDCQuery(NativeChannelName));
                var strVal = GetResponce();
                value = Convert.ToDouble(strVal);
            }
            catch (Exception e)
            {
                throw;
            }
            return value;
        }

        public void SingleShotAquicition()
        {

        }

        public void StartAcquisition()
        {

        }

        public void StopAcquisition()
        {

        }

        public static AnalogInputChannels operator +(AnalogInputChannel AI1, AnalogInputChannel AI2)
        {
            throw new NotImplementedException();
        }





        
    }
}
