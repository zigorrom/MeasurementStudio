using InstrumentAbstraction.InstrumentInterfaces;
using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    [InstrumentAttribute("Keithley", "2602A")]
    public class Keithley2602A : AbstractMessageBasedInstrument//, ISourceMeasurementUnit
    {
        public Keithley2602ACommandBuilder CommandSet { get; set; }

        public Keithley2602A(string Name, string Alias, string ResourceName)
            : base(Name,Alias,ResourceName)
        {
            CommandSet = new Keithley2602ACommandBuilder();
        }

        public override bool InitializeDevice()
        {

            if (base.InitializeDevice())
                if (SendCommand(CommandSet.BeeperEnable()))
                    return true;
            return false;
                
        }



        public override void DetectInstrument()
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }

        //public override AbstractCommandBuilder CommandSet
        //{
        //    get { throw new NotImplementedException(); }
        //}
    }
}
