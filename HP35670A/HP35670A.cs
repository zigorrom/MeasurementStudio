using Instruments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP35670ANamespace
{
    public class HP35670A:AbstractMessageBasedInstrument
    {

        public HP35670A(string Name, string Alias, string ResourceName):base(Name, Alias, ResourceName)
        {

        }

        

        public override void DetectInstrument(object data)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
