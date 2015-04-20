using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    public interface IDAQ
    {
        void Initialize();

        bool AnalogRead(string ChannelName, out double Value);
        bool AnalogWrite(string ChannelName, double Value);

        

    }
}
