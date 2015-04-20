using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2442A
{
    [InstrumentAttribute("Agilent","U2542A")]
    public class AgilentU2542A:AbstractMessageBasedInstrument//,IDAQ
    {
        private AgilentU2542ACommandClass m_commandSet;
        public AgilentU2542ACommandClass CommandSet
        {
            get { return m_commandSet; }
        }

        public AgilentU2542A(string Name,string Alias,string ResourceName):base(Name,Alias,ResourceName)
        {
            m_commandSet = new AgilentU2542ACommandClass();
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
