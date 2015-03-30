using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.Properties
{
    public class CountingModeProperty:PropertyBase<CountingModeEnum>
    {
        public CountingModeProperty(string PropertyName):base(PropertyName)
        {

        }

        public static implicit operator CountingModeEnum(CountingModeProperty cmp)
        {
            return cmp.PropertyValue;
        }
    }
}
