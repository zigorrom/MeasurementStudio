﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MeasurementStudio
{
    public interface IMainViewModel
    {
        IMeasurementView View { get; set; }
        void DataContextIsSet();

        ICommand KeyPressed { get; }
    }
}
