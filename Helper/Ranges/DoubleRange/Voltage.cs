using Helper.Ranges.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.DoubleRange
{
    public class Voltage : DoubleNumericValue
    {
        public Voltage():base(new Volt())
        { }
        public override DoubleNumericValue Add(DoubleNumericValue other)
        {
            if (!Units.Equals(other.Units))
                throw new ArgumentException("Value of other units");
            if (Units.Prefix != other.Units.Prefix)
                other.CastToPrefix(Units.Prefix);
            
        }

        public override DoubleNumericValue Subtruct(DoubleNumericValue other)
        {
            throw new NotImplementedException();
        }

        public override DoubleNumericValue Multiply(DoubleNumericValue other)
        {
            throw new NotImplementedException();
        }

        public override DoubleNumericValue Divide(DoubleNumericValue other)
        {
            throw new NotImplementedException();
        }
    }

}
