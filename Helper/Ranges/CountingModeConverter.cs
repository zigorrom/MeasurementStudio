using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Helper.Ranges
{
    [ValueConversion(typeof(object),typeof(CountingModeEnum))]
    public class CountingModeConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is CountingModeEnum)
            {
                var mode = (CountingModeEnum)value;

                switch (mode)
                {
                    case CountingModeEnum.BackAndForth:
                        return "BackAndForth";
                    case CountingModeEnum.Repetitive:
                    default:
                        return "Repetitive";
                }
            }
            return null;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ComboBoxItem)
            {
                var cbi = (ComboBoxItem)value;

                switch (cbi.Content.ToString())
                {
                    case "BackAndForth":
                        return CountingModeEnum.BackAndForth;

                    case "Repetitive":
                    default:
                        return CountingModeEnum.Repetitive;
                }

            }
            return null;
        }
    }
}
