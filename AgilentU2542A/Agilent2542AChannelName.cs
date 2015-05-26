using Instruments.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgilentU2542A
{
    class Agilent2542AChannelName:AbstractChannelName
    {
        public Agilent2542AChannelName(AgilentU2542AChannelEnum Identifier)
            : base(Identifier)
        { }

        public override Enum ChannelIdentifierFromString(string NativeName)
        {
            throw new NotImplementedException();
        }

        public override string NameFromChannelIdentifier(Enum ChannelIdentifier)
        {
            throw new NotImplementedException();
        }
    }
}
