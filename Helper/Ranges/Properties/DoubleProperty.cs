using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.Properties
{
    public class DoubleProperty:PropertyBase<double>
    {
        public DoubleProperty(string PropertyName):base(PropertyName)
        {
            
        }
        public static implicit operator double(DoubleProperty dp)
        {
            return dp.PropertyValue;
        }
        //  User-defined conversion from double to Digit 
        //public static implicit operator DoubleProperty(double d)
        //{
        //    return new Digit(d);
        //}

    }
}
