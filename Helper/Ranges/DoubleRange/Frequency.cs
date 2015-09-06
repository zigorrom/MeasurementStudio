using Helper.Ranges.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.DoubleRange
{
    public class Frequency : DoubleUnitValueDependencyObject
    {
        public Frequency():base("Hz")
        { }
        public Frequency(double Magnitude):base("Hz",Magnitude)
        { }
        public Frequency(double Magnitude, UnitPrefixesEnum Prefix)
            : base("Hz", Magnitude, Prefix) 
        { }
    }
}
