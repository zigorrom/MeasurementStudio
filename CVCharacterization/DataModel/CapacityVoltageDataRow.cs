using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.DataModel
{
    [Serializable]
    public struct CapacityVoltageDataRow
    {
        public CapacityVoltageDataRow(double capacity, double voltage)
        {
            _capacity = capacity;
            _voltage = voltage;
        }

        private double _capacity;
                [DataPropertyAttribute("Capacity","F","C\\-(s)")]
        public double Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        private double _voltage;
        [DataPropertyAttribute("Voltage","V","V\\-(s)")]
        public double Voltage
        {
            get { return _voltage; }
            set { _voltage = value; }
        }


    }
}
