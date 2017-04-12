using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IVexperiment.ViewModels
{
    [ValueConversion(typeof(int), typeof(int))]
    internal class SelectedIndexToDeviceAveragingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is int))
                return Binding.DoNothing;
            var val = (int)value;
            switch (val)
            {
                case 1: return 0;
                case 5: return 1;
                case 10: return 2;
                case 20: return 3;
                case 30: return 4;
                case 40: return 5;
                case 50: return 6;
                case 60: return 7;
                case 70: return 8;
                case 80: return 9;
                case 90: return 10;
                case 100: return 11;
                default: return 11;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is int))
                return Binding.DoNothing;
            var v = (int)value;
            switch (v)
            {
                case 0: return 1;
                case 1: return 5;
                case 2: return 10;
                case 3: return 20;
                case 4: return 30;
                case 5: return 40;
                case 6: return 50;
                case 7: return 60;
                case 8: return 70;
                case 9: return 80;
                case 10: return 90;
                case 11: return 100;
                default: return 100;

                
            }
        }
    }
}
