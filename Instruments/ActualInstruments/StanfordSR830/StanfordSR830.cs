using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments
{
    [InstrumentAttribute("Stanford","SR830")]
    public class StanfordSR830:AbstractMessageBasedInstrument, ILockInAmplifier
    {
        public StanfordSR830(string Name,string Alias, string ResourceName):base(Name,Alias,ResourceName)
        {

        }

        public bool ReadSignal(out double Signal)
        {
            Signal = 0;
            var result = Query("OUTR?1");
            if (String.IsNullOrEmpty(result))
                return false;
            if (TryConvert(result, out Signal))
                return true;
            return false;
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }
    }
}
