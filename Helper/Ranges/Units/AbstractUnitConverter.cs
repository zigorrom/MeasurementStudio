using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Helper.Ranges.Units
{
    [ValueConversion(typeof(string),typeof(UnitPrefixesEnum))]
    public class AbstractUnitConverter : IValueConverter
    {
        private string m_UnitName;
        public AbstractUnitConverter(string UnitName)
        {
            m_UnitName = UnitName;
        }


       

        private string GetUnitName(string prefix)
        {
            return prefix + m_UnitName;
        }

        private string ExtractPrefix(string UnitName)
        {
            var val = UnitName;
            if (val.EndsWith(m_UnitName))
                val = val.Substring(0, val.LastIndexOf(m_UnitName));
            return val;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = value as string;
            if (val == null)
                return Binding.DoNothing;
            val = ExtractPrefix(val);
            switch (val)
            {
                case UnitPrefixesNames.YOTTA: return UnitPrefixesEnum.YOTTA;
                case UnitPrefixesNames.ZETTA: return UnitPrefixesEnum.ZETTA;
                case UnitPrefixesNames.EXA: return UnitPrefixesEnum.EXA;
                case UnitPrefixesNames.PETA: return UnitPrefixesEnum.PETA;
                case UnitPrefixesNames.TERA: return UnitPrefixesEnum.TERA;
                case UnitPrefixesNames.GIGA: return UnitPrefixesEnum.GIGA;
                case UnitPrefixesNames.MEGA: return UnitPrefixesEnum.MEGA;
                case UnitPrefixesNames.KILO: return UnitPrefixesEnum.KILO;
                case UnitPrefixesNames.HECTO: return UnitPrefixesEnum.HECTO;
                case UnitPrefixesNames.DECA: return UnitPrefixesEnum.DECA;

                case UnitPrefixesNames.DECI: return UnitPrefixesEnum.DECI;
                case UnitPrefixesNames.CENTI: return UnitPrefixesEnum.CENTI;
                case UnitPrefixesNames.MILLI: return UnitPrefixesEnum.MILLI;
                case UnitPrefixesNames.MICRO: return UnitPrefixesEnum.MICRO;
                case UnitPrefixesNames.NANO: return UnitPrefixesEnum.NANO;
                case UnitPrefixesNames.PICO: return UnitPrefixesEnum.PICO;
                case UnitPrefixesNames.FEMTO: return UnitPrefixesEnum.FEMTO;
                case UnitPrefixesNames.ATTO: return UnitPrefixesEnum.ATTO;
                case UnitPrefixesNames.ZEPTO: return UnitPrefixesEnum.ZEPTO;
                case UnitPrefixesNames.YOCTO: return UnitPrefixesEnum.YOCTO;
                default:
                    return UnitPrefixesEnum.DEFAULT;
            }
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (UnitPrefixesEnum)value;
            if (val == null)
                return Binding.DoNothing;
            switch (val)
            {
                case UnitPrefixesEnum.YOTTA: return GetUnitName(UnitPrefixesNames.YOTTA);
                case UnitPrefixesEnum.ZETTA: return GetUnitName(UnitPrefixesNames.ZETTA);
                case UnitPrefixesEnum.EXA: return GetUnitName(UnitPrefixesNames.EXA);
                case UnitPrefixesEnum.PETA: return GetUnitName(UnitPrefixesNames.PETA);
                case UnitPrefixesEnum.TERA: return GetUnitName(UnitPrefixesNames.TERA);
                case UnitPrefixesEnum.GIGA: return GetUnitName(UnitPrefixesNames.GIGA);
                case UnitPrefixesEnum.MEGA: return GetUnitName(UnitPrefixesNames.MEGA);
                case UnitPrefixesEnum.KILO: return GetUnitName(UnitPrefixesNames.KILO);
                case UnitPrefixesEnum.HECTO: return GetUnitName(UnitPrefixesNames.HECTO);
                case UnitPrefixesEnum.DECA: return GetUnitName(UnitPrefixesNames.DECA);

                case UnitPrefixesEnum.DECI: return GetUnitName(UnitPrefixesNames.DECI);
                case UnitPrefixesEnum.CENTI: return GetUnitName(UnitPrefixesNames.CENTI);
                case UnitPrefixesEnum.MILLI: return GetUnitName(UnitPrefixesNames.MILLI);
                case UnitPrefixesEnum.MICRO: return GetUnitName(UnitPrefixesNames.MICRO);
                case UnitPrefixesEnum.NANO: return GetUnitName(UnitPrefixesNames.NANO);
                case UnitPrefixesEnum.PICO: return GetUnitName(UnitPrefixesNames.PICO);
                case UnitPrefixesEnum.FEMTO: return GetUnitName(UnitPrefixesNames.FEMTO);
                case UnitPrefixesEnum.ATTO: return GetUnitName(UnitPrefixesNames.ATTO);
                case UnitPrefixesEnum.ZEPTO: return GetUnitName(UnitPrefixesNames.ZEPTO);
                case UnitPrefixesEnum.YOCTO: return GetUnitName(UnitPrefixesNames.YOCTO);
                default: return m_UnitName;
            }
        }
    }
}
