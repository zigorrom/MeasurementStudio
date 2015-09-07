using Instruments;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordSR830
{
    [Export(typeof(IInstrument))]
    [ExportMetadata("InstrumentMetadata", typeof(IMultimeter))]
    [InstrumentAttribute("Stanford", "SR830")]
    public class StanfordSR830 : AbstractMessageBasedInstrument//, ILockInAmplifier
    {
        public StanfordSR830(string Name, string Alias, string ResourceName)
            : base(Name, Alias, ResourceName)
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

        public override void DetectInstrument(object data)
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
