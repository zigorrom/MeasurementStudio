using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments.ActualInstruments.Spotter
{
    public class SpotterPlatform:AbstractMessageBasedInstrument
    {
        public SpotterPlatform(string ResourceName):base("SpotterPlatform","SP",ResourceName)
        {

        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }

        

    }
}
