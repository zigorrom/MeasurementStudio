﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Ranges.Units
{
    public interface IUnits:INotifyPropertyChanged
    {
        UnitPrefixesEnum Prefix { get; set; }
        string Units { get;  }
        //string[] UnitSource { get; }
        //Dictionary<String, UnitPrefixesEnum> UnitSource { get; }
        double GetNumericValue(double magnitude);
    }
}