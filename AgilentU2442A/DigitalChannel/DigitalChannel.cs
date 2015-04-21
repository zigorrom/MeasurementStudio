using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class DigitalChannel:AbstractChannel
    {
        public DigitalChannel(string NativeChannelName, string AliasChannelName, AgilentU2542A ParentDevice)
            :base(NativeChannelName,AliasChannelName,ParentDevice)
        {

        }
    }
}
