using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IVCharacterization
{
    //[ValueConversion(typeof(string), typeof(IVCharacteristicTypeEnum))]
    //class CharacteristicTypeValueConverter:IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        var val = (IVCharacteristicTypeEnum)value;
    //        switch (val)
    //        {
    //            case IVCharacteristicTypeEnum.Output:
    //                return IVCharacteristicNames.OutputCharacteristic;
    //            case IVCharacteristicTypeEnum.Transfer: return IVCharacteristicNames.TransferCharacteristic;
    //            default:
    //                return Binding.DoNothing;
    //        }
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        var val = value as string;
    //        if (val == null)
    //            return Binding.DoNothing;
    //        switch (val)
    //        {
    //            case IVCharacteristicNames.OutputCharacteristic: return IVCharacteristicTypeEnum.Output;
    //            case IVCharacteristicNames.TransferCharacteristic: return IVCharacteristicTypeEnum.Transfer;
    //            default: return Binding.DoNothing;
    //        }
    //    }
    //}
}
