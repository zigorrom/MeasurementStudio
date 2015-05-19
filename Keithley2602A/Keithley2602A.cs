using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    [InstrumentAttribute("Keithley", "2602A")]
    public class Keithley2602A : AbstractMessageBasedInstrument
    {
        public Keithley2602A(string Name, string Alias, string ResourceName)
            : base(Name,Alias,ResourceName)
        {

        }

        public override bool InitializeDevice()
        {
            return base.InitializeDevice();
                
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
