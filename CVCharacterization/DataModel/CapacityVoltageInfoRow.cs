using ExperimentDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVCharacterization.DataModel
{
    public struct CapacityVoltageInfoRow:IMeasurementInfo
    {
        public CapacityVoltageInfoRow(string filename, double frequency, int experimentNumber)
        {
            _filename = filename;
            _frequency = frequency;
            _experimentNumber = experimentNumber;
        }


        private string _filename;
        [DataPropertyAttribute("Filename","","")]
        public string Filename
        {
            get { return _filename; }
            set { _filename = value; }
        }

        private double _frequency;
        [DataPropertyAttribute("Frequency","Hz","")]
        public double Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        private int _experimentNumber;
        [DataPropertyAttribute("#","","")]
        public int ExperimentNumber
        {
            get { return _experimentNumber; }
            set { _experimentNumber = value; }
        }



    }
}
