﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public interface IInfoDataRow
    {
        string Filename { get; set; }
        string Comment { get; set; }
    }
}