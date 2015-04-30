using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AIAquisitionParameters:AbstractNotifyPropertyChanged
    {
        private AgilentU2542A m_ParentDevice;
        private AgilentU2542ACommandClass m_commandSet;
        public AIAquisitionParameters(AgilentU2542A ParentDevice)
        {
            m_ParentDevice = ParentDevice;
            m_commandSet = m_ParentDevice.CommandSet;
        }


        private ChannelOutputEnableEnum m_OutputEnable;
        public ChannelOutputEnableEnum OutputEnable
        {
            get { return m_OutputEnable; }
            set
            {
                if (m_OutputEnable == value)
                    return;
                m_OutputEnable = value;
                //m_ParentDevice.SendCommand()
                Send command to Device

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
                m_VoltagePolarity = value;
                OnPropertyChanged("VoltagePolarity");
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
                m_PointsPerShot = value;
                OnPropertyChanged("PointsPerShot");
            }
        }





    }
}
