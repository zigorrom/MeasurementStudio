using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [InstrumentAttribute("Stanford","SR830")]
    public class StanfordSR830:ILockInAmplifier
    {
        public bool ReadSignal(out double Signal)
        {
            throw new NotImplementedException();
        }
    }
}
