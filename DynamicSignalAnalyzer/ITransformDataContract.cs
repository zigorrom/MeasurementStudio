﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSignalAnalyzer
{

    public interface ITransformDataContract
    {
        event TransformEventArgs NewdDataHandled;
        event EventHandler AveragesNumberReached;
    }
}