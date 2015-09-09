using System;
namespace Instruments
{
    public interface IInstrumentResourceItem:IEquatable<IInstrumentResourceItem>
    {
        string Alias { get; set; }
        string IDN { get; }
        string Name { get; set; }
        string Resource { get; }
    }
}
