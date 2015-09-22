using ExperimentDataModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseMeasurementLegacy.DataModel
{
    public struct MeasurementInfoRow : IMeasurementInfo
    {
        private string _filename;

        [DataPropertyAttribute("FileName", "", "")]
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

    }
}
