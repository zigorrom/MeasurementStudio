using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogInputChannel:AbstractChannel, IAnalogInputChannel
    {
        public AnalogInputChannel(string NativeChannelName, string AliasChannelName, AgilentU2542A ParentDevice):base(NativeChannelName,AliasChannelName,ParentDevice)
        {

        }
        public AnalogInputChannel(AnalogInputChannel AI1, AnalogInputChannel AI2):base("","",AI1.ParentDevice)
        {
            throw new NotImplementedException();
        }

        

        public double AnalogRead()
        {
            return 0;   
        }

        public void StartAcquisition()
        {

        }

        public void StopAcquisition()
        {

        }

        public static AnalogInputChannels operator +(AnalogInputChannel AI1, AnalogInputChannel AI2)
        {
            return new AnalogInputChannels(AI1, AI2);
        }
    }
}
