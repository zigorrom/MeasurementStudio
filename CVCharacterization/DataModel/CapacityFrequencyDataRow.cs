using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.DataModel
{
    [Serializable]
    public struct CapacityFrequencyDataRow
    {
        private double _capacity;

        [DataPropertyAttribute("Capacity","F", "C\\-(s)")]
        public double Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        private double _frequency;
        [DataPropertyAttribute("Frequency", "Hz","f")]
        public double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        public CapacityFrequencyDataRow(double Capacity, double Frequency)
        {
            _capacity = Capacity;
            _frequency = Frequency;
        }
    }
}
