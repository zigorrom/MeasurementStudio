using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Instruments.Abstract
{
    public interface IChannelName:IEquatable<IChannelName>
    {
        string NativeName { get; }// set; }
        Enum ChannelIdentifier { get; }// set; }
        Enum ChannelIdentifierFromString(string NativeName);
        string NameFromChannelIdentifier(Enum ChannelIdentifier);
    }
}
