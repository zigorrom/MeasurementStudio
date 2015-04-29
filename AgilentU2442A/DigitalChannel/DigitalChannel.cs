using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class DigitalChannel:AbstractChannel
    {
        public DigitalChannel(string NativeChannelName, AgilentU2542A ParentDevice)
            :base(NativeChannelName,ParentDevice)
        {

        }

        protected override void InitializeChannel()
        {
            throw new NotImplementedException();
        }
    }
}
