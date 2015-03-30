using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.Properties
{
    public class IntProperty:PropertyBase<int>
    {
        public IntProperty(string PropertyName):base(PropertyName)
        {

        }

        public static implicit operator int(IntProperty ip)
        {
            return ip.PropertyValue;
        }
    }
}
