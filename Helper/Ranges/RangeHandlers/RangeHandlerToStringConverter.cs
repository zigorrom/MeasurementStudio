using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Helper.Ranges.RangeHandlers
{
    [ValueConversion(typeof(string),typeof(AbstractDoubleRangeHandler))]
    public class RangeHandlerToStringConverter:IValueConverter
    {
        private const string NormalRangeHandler = "Normal";
        private const string BackAndForthRangeHandler = "BackAndForth";
        private const string ZeroCrossingRangeHandler = "ZeroStart";
        private const string ZeroCrossingBackAndForthRangeHandler = "ZeroStartBackAndForth";


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
