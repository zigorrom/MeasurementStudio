using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes.Numeric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataVisualization.D3DataVisualization
{
    public enum AxisType
    {
        Horizontal,
        Vertical
    }

    [ValueConversion(typeof(GraphScaleType), typeof(NumericAxis))]
    public class GraphTypeToAxisConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
     
            return false;//((Visibility)value) == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return true;///((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
