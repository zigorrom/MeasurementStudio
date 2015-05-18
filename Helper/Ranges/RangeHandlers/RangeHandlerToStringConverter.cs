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
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var handler = value as AbstractDoubleRangeHandler;
            if (handler == null)
                return Binding.DoNothing;
            return handler.HandlerName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as string;
            if (val == null)
                return Binding.DoNothing;
            switch (val)
            {
                case AbstractDoubleRangeHandler.BackAndForthRangeHandler: return new BackAndForthRangeHandler();
                case AbstractDoubleRangeHandler.NormalRangeHandler: return new NormalDoubleRangeHandler();
                case AbstractDoubleRangeHandler.ZeroCrossingBackAndForthRangeHandler: return new ZeroCrossingBackAndForthRangeHandler();
                case AbstractDoubleRangeHandler.ZeroCrossingRangeHandler: return new ZeroCrossRangeHandler();
                default:
                    return Binding.DoNothing;
            }
        }
    }
}
