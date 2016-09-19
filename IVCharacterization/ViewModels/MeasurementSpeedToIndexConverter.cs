//using Keithley24xxNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IVCharacterization.ViewModels
{
    public enum MeasurementSpeed
    {
        Slow,
        Middle,
        Fast
    }

    [ValueConversion(typeof(int), typeof(MeasurementSpeed))]
    internal class MeasurementSpeedToIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is MeasurementSpeed))
                return Binding.DoNothing;
            var val = (MeasurementSpeed)value;
            switch (val)
            {
                case MeasurementSpeed.Fast:
                    return 2;
                case MeasurementSpeed.Middle:
                    return 1;
                case MeasurementSpeed.Slow:
                default:
                    return 0;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is int))
                return Binding.DoNothing;
            var v = (int)value;
            switch (v)
            {

                case 1: return MeasurementSpeed.Middle;
                case 2: return MeasurementSpeed.Fast;
                case 0:
                default:
                    return MeasurementSpeed.Slow;
            }
        }
    }
}
