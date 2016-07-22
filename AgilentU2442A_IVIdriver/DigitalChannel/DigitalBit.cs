using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A_IVIdriver
{
    public class DigitalBit
    {
        private int m_BitNumber;
        private AgilentU2542A m_ParentDevice;
        private DigitalChannel m_digitalChannel;

        public DigitalBit(DigitalChannel digitalChannel, int bitNumber)
        {
            m_digitalChannel = digitalChannel;
            m_ParentDevice = digitalChannel.ParentDevice;
            //m_digitalChannel = digitalChannel;
            m_BitNumber = bitNumber;
        }

        private bool m_value;

        public bool Value
        {
            get { 
                if(m_digitalChannel.DigitalDirection== DigitalDirectionEnum.Output)
                    throw new Exception("Channel DigitalDirection is set to Output");
                var val = m_ParentDevice.CommandSet.MEASureDIGitalBITQueryParse(m_ParentDevice.Query(m_ParentDevice.CommandSet.MEASureDIGitalBITQuery(m_BitNumber, m_digitalChannel.ChannelName)));
                if (m_value != val)
                    m_value = val;
                return m_value;
            }
            private set {
                if (m_value == value)
                    return;
                if(m_digitalChannel.DigitalDirection== DigitalDirectionEnum.Input)
                    throw new Exception("Channel DigitalDirection is set to Input");
                if(!m_ParentDevice.SendCommand(m_ParentDevice.CommandSet.SOURceDIGitalDATABIT(value, m_BitNumber,m_digitalChannel.ChannelName)))
                    throw new MemberAccessException("Value was not set on the device. Please check connectivity");
                //m_digitalChannel.DigitalWriteBit(value, m_BitNumber);
                
            }
        }

        //public bool DigitalReadBit(int bit)
        //{
        //    if (DigitalDirection == DigitalDirectionEnum.Output)
        //        throw new Exception("DigitalDirection is set to output");
        //    var val = CommandSet.MEASureDIGitalBITQueryParse(QueryCommand(CommandSet.MEASureDIGitalBITQuery(bit, ChannelName)));
        //    return val;
        //}
        //public void DigitalWriteBit(bool value, int bit)
        //{
        //   if(DigitalDirection == DigitalDirectionEnum.Input)
        //       throw new Exception("DigitalDirection is set to input");
        //   if (!SendCommand(CommandSet.SOURceDIGitalDATABIT(value, bit, ChannelName)))
        //       throw new MemberAccessException(MemberAccessExceptionMessage);
        //}
        public void Set()
        {
            Value = true;
        }

        public void Reset()
        {
            Value = false;
        }
    
    }

}
