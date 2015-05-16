using Helper.Ranges.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.DoubleRange
{
    public class Voltage:DoubleUnitValueDependencyObject
    {
        public Voltage():base("V")
        { }
        public Voltage(double Magnitude):base("V",Magnitude)
        { }
        public Voltage(double Magnitude, UnitPrefixesEnum Prefix) : base("V", Magnitude, Prefix) 
        { }
    }
    

}
