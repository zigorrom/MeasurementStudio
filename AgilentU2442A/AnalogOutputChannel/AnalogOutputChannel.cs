using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogOutputChannel:AbstractChannel
    {
        //public AnalogOutputChannel(string NativeChannelName, AgilentU2542A ParentDevice)
        //    :base(NativeChannelName,ParentDevice)
        //{

        //}
        public AnalogOutputChannel(ChannelEnum ChannelIdentifier, AgilentU2542A ParentDevice):base(ChannelIdentifier, ParentDevice)
        {
            
        }
        protected override void InitializeChannel()
        {
            throw new NotImplementedException();
        }

        private double m_voltage;

        public double Voltage
        {
            get {
                var val =  CommandSet.SOURceVOLTageQuery QueryCommand(CommandSet.SOURceVOLTageQuery(ChannelName))
                
                return m_voltage; 
            }
            set {
                if (m_voltage == value)
                    return;
                if(!SendCommand(CommandSet.SOURceVOLTage(value,ChannelName)))
                    throw new MemberAccessException(MemberAccessExceptionMessage);
                m_voltage = value; 
                
            }
        }
        
        
        
    }
}
