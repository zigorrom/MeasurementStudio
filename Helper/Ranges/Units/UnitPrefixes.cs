using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.Units
{
    public struct UnitPrefixesNames
    {
        public const string YOTTA = "Y";
        public const string ZETTA = "Z";
        public const string EXA = "E";
        public const string PETA = "P";
        public const string TERA = "T";
        public const string GIGA = "G";
        public const string MEGA = "M";
        public const string KILO = "k";
        public const string HECTO = "h";
        public const string DECA = "da";
        public const string DEFAULT = "";
        public const string DECI = "d";
        public const string CENTI = "c";
        public const string MILLI = "m";
        public const string MICRO = "u";
        public const string NANO = "n";
        public const string PICO = "p";
        public const string FEMTO = "f";
        public const string ATTO = "a";
        public const string ZEPTO = "z";
        public const string YOCTO = "y";
    }

    public struct UnitPrefixesValues
    {
        public const double YOTTA = 1000000000000000000000000.0;
        public const double ZETTA = 1000000000000000000000.0;
        public const double EXA =   1000000000000000000.0;
        public const double PETA =  1000000000000000.0;
        public const double TERA =  1000000000000.0;
        public const double GIGA =  1000000000.0;
        public const double MEGA =  1000000.0;
        public const double KILO =  1000.0;
        public const double HECTO = 100.0;
        public const double DECA =  10.0;
        public const double DEFAULT = 1.0;
        public const double DECI =  0.1;
        public const double CENTI = 0.01;
        public const double MILLI = 0.001;
        public const double MICRO = 0.000001;
        public const double NANO =  0.000000001;
        public const double PICO =  0.000000000001;
        public const double FEMTO = 0.000000000000001;
        public const double ATTO =  0.000000000000000001;
        public const double ZEPTO = 0.000000000000000000001;
        public const double YOCTO = 0.000000000000000000000001;

        public static double ConvertFromPrefixToDouble(UnitPrefixesEnum prefix)
        {
            switch (prefix)
            {
                case UnitPrefixesEnum.YOTTA: return UnitPrefixesValues.YOTTA;
                case UnitPrefixesEnum.ZETTA: return UnitPrefixesValues.ZETTA;
                case UnitPrefixesEnum.EXA: return UnitPrefixesValues.EXA;
                case UnitPrefixesEnum.PETA: return UnitPrefixesValues.PETA;
                case UnitPrefixesEnum.TERA: return UnitPrefixesValues.TERA;
                case UnitPrefixesEnum.GIGA: return UnitPrefixesValues.GIGA;
                case UnitPrefixesEnum.MEGA: return UnitPrefixesValues.MEGA;
                case UnitPrefixesEnum.KILO: return UnitPrefixesValues.KILO;
                case UnitPrefixesEnum.HECTO: return UnitPrefixesValues.HECTO;
                case UnitPrefixesEnum.DECA: return UnitPrefixesValues.DECA;
                case UnitPrefixesEnum.DEFAULT: return UnitPrefixesValues.DEFAULT;
                case UnitPrefixesEnum.DECI: return UnitPrefixesValues.DECI;
                case UnitPrefixesEnum.CENTI: return UnitPrefixesValues.CENTI;
                case UnitPrefixesEnum.MILLI: return UnitPrefixesValues.MILLI;
                case UnitPrefixesEnum.MICRO: return UnitPrefixesValues.MICRO;
                case UnitPrefixesEnum.NANO: return UnitPrefixesValues.NANO;
                case UnitPrefixesEnum.PICO: return UnitPrefixesValues.PICO;
                case UnitPrefixesEnum.FEMTO: return UnitPrefixesValues.FEMTO;
                case UnitPrefixesEnum.ATTO: return UnitPrefixesValues.ATTO;
                case UnitPrefixesEnum.ZEPTO: return UnitPrefixesValues.ZEPTO;
                case UnitPrefixesEnum.YOCTO: return UnitPrefixesValues.YOCTO;
                default: return UnitPrefixesValues.DEFAULT;
            }
        }
    }



    public enum UnitPrefixesEnum
    {
        YOTTA = 10,
        ZETTA = 9,
        EXA = 8,
        PETA = 7,
        TERA = 6,
        GIGA = 5,
        MEGA = 4,
        KILO = 3,
        HECTO = 2,
        DECA = 1,
        DEFAULT = 0,
        DECI = -1,
        CENTI = -2,
        MILLI = -3,
        MICRO = -4,
        NANO = -5,
        PICO = -6,
        FEMTO = -7,
        ATTO = -8,
        ZEPTO = -9,
        YOCTO = -10
    }
}
