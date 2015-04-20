using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    public class AnalogInputChannels:List<AnalogInputChannel>,IAnalogInputChannel
    {
        public AnalogInputChannels(AnalogInputChannel AI1, AnalogInputChannel AI2)
        {
            //subscribe on events
        }

        public double AnalogRead()
        {
            var channelList = this.Select(x => x.NativeChannelName).ToArray();

            throw new NotImplementedException();
        }

        public void StartAcquisition()
        {
            throw new NotImplementedException();
        }

        public void StopAcquisition()
        {
            throw new NotImplementedException();
        }
    }
}
