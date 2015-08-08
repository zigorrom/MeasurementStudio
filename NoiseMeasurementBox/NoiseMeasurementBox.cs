using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoiseMeasurementBox
{
    using Agilent.AgilentU254x.Interop;

    public class NoiseMeasurementBox:IDisposable
    {
        private AgilentU254xClass _agilent;
        private DataRouter _router;

        
        public bool Initialized {
            get
            {
                return _agilent.Initialized;
            }
        }


        public NoiseMeasurementBox()
        {
            _agilent = new AgilentU254xClass();
        }

        public void Initialize(string ResourceName, bool IdQuery, bool Reset  )
        {
            _agilent.Initialize(ResourceName, IdQuery, Reset);
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool SetDrainSourceVoltage(double Voltage)
        {
            return false;
        }

        public bool SetGateVoltage(double Voltage)
        {
            return false;
        }


    }
}
