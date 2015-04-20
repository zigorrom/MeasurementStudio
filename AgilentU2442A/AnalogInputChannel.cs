using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogInputChannel:AbstractChannel
    {
        public AnalogInputChannel(string NativeChannelName, string AliasChannelName, AgilentU2542A ParentDevice):base(NativeChannelName,AliasChannelName,ParentDevice)
        {

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
    }
}
