using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentDataModel
{
    public class ObserverOptions
    {
        private int _samplesNumber;

        public int SamplesNumber
        {
            get { return _samplesNumber; }
            private set { _samplesNumber = value; }
        }

    }
}
