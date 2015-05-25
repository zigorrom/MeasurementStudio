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
    }
}
