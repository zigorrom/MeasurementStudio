﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    internal interface IMeasuredDataObservable<DataT>
    {
        IDisposable Subscribe(IMeasuredDataObserver<DataT> observer);
    }
}
