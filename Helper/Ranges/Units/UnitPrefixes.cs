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

    public enum UnitPrefixesEnum
    {
        YOTTA = 24,
        ZETTA = 21,
        EXA = 18,
        PETA = 15,
        TERA = 12,
        GIGA = 9,
        MEGA = 6,
        KILO = 3,
        HECTO = 2,
        DECA = 1,
        DEFAULT = 0,
        DECI = -1,
        CENTI = -2,
        MILLI = -3,
        MICRO = -6,
        NANO = -9,
        PICO = -12,
        FEMTO = -15,
        ATTO = -18,
        ZEPTO = -21,
        YOCTO = -24
    }
}
