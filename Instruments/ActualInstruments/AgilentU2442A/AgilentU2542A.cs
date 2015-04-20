using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instruments.ActualInstruments.AgilentU2442A
{
    [InstrumentAttribute("Agilent","U2542A")]
    public class AgilentU2542A:AbstractMessageBasedInstrument//,IDAQ
    {
        public AgilentU2542A(string Name,string Alias,string ResourceName):base(Name,Alias,ResourceName)
        {
             
        }

        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}
