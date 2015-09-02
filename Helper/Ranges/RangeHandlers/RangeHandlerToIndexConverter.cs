using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Helper.Ranges.RangeHandlers
{
    [ValueConversion(typeof(int), typeof(AbstractDoubleRangeHandler))]
    public class RangeHandlerToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var handler = value as AbstractDoubleRangeHandler;
            if (handler == null)
                return Binding.DoNothing;
            switch (handler.HandlerName)
            {
                case AbstractDoubleRangeHandler.BackAndForthRangeHandler: return 1;
                case AbstractDoubleRangeHandler.NormalRangeHandler: return 0;
                case AbstractDoubleRangeHandler.ZeroCrossingBackAndForthRangeHandler: return 3;
                case AbstractDoubleRangeHandler.ZeroCrossingRangeHandler: return 2;
                default:
                    return Binding.DoNothing;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (int)value;

            switch (val)
            {
                case 1: return new BackAndForthRangeHandler();
                case 0: return new NormalDoubleRangeHandler();
                case 3: return new ZeroCrossingBackAndForthRangeHandler();
                case 2: return new ZeroCrossRangeHandler();
                default:
                    return Binding.DoNothing;
            }
        }
    }
}
