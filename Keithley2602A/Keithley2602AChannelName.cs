using Instruments.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley2602A
{
    public class Keithley2602AChannelName:AbstractChannelName
    {
        public Keithley2602AChannelName(string Name, Keithley2602AChannelsEnum ChannelIdentifier)
            : base(Name, ChannelIdentifier)
        { }

        public Keithley2602AChannelName(string Name)
            : base(Name)
        { }

        public Keithley2602AChannelName(Keithley2602AChannelsEnum ChannelIdentifier)
            : base(ChannelIdentifier)
        { }


        public override Enum ChannelIdentifierFromString(string NativeName)
        {
            switch (NativeName)
            {
                case "ChannelA":
                case "A": return Keithley2602AChannelsEnum.ChannelA;
                case "ChannelB":
                case "B": return Keithley2602AChannelsEnum.ChannelB;
                default:
                    throw new ArgumentException();
                    //break;
            }
        }

        public override string NameFromChannelIdentifier(Enum ChannelIdentifier)
        {
            var ident = (Keithley2602AChannelsEnum)ChannelIdentifier;
            switch (ident)
            {
                case Keithley2602AChannelsEnum.ChannelA:
                    return "ChannelA";
                case Keithley2602AChannelsEnum.ChannelB:
                    return "ChannelB";
                default:
                    throw new ArgumentException();
            }
        }
    }
}
