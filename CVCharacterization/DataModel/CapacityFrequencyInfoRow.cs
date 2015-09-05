using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.DataModel
{
    [Serializable]
    public struct CapacityFrequencyInfoRow : IMeasurementInfo
    {

        public CapacityFrequencyInfoRow(string filename, double voltage, int experimentNumber)
        {
            _filename = filename;
            _voltage = voltage;
            _experimentNumber = experimentNumber;
        }

        private string _filename;

        [DataPropertyAttribute("Filename", "", "")]
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private double _voltage;
        [DataPropertyAttribute("Voltage", "V", "")]
        public double Voltage
        {
            get { return _voltage; }
            set { _voltage = value; }
        }

        private int _experimentNumber;
        [DataPropertyAttribute("#", "", "")]
        public int ExperimentNumber
        {
            get { return _experimentNumber; }
            set { _experimentNumber = value; }
        }


    }
}
