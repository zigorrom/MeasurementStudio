using InstrumentAbstraction.InstrumentInterfaces;
using Instruments;
using Ke2400DotNetWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley24xx
{
    
    public class Keithley24xx:ke2400, IInstrument,ISourceMeasurementUnit
    {
        public Keithley24xx(string Name, string Alias, string ResourceName):base(ResourceName, true, true)
        {
            
        }


        private string m_name;

        private string m_alias;
        private string m_resourceName;
        private IInstrumentOwner m_owner;
        private InstrumentState m_state;

        public string Name
        {
            get { return m_name; }
        }

        public string Alias
        {
            get { return m_alias; }
        }

        public string ResourceName
        {
            get { return m_resourceName; }
        }

        public IInstrumentOwner InstrumentOwner
        {
            get
            {
                return m_owner;
            }
            set
            {
                m_owner = value;
            }
        }

        public InstrumentState State
        {
            get
            {
                return m_state;
            }
            set
            {
                m_state = value;
            }
        }

        public bool IsAlive(bool SendIDN)
        {
            return true;
        }

        public void Reset()
        {
            reset();
        }

        public void DetectInstrument(object data)
        {
            
            throw new NotImplementedException();
        }

        public bool Equals(IInstrument other)
        {
            if (other.Alias == Alias)
                if (other.Name == Name)
                    if (other.ResourceName == ResourceName)
                        if (other.State == State)
                            if (Object.ReferenceEquals(this, other))
                                //if (other.InstrumentOwner.Name == InstrumentOwner.Name)
                                return true;
            return false;
        }

        public void SwitchOn()
        {
            base.Initiate();
        }

        public void SwitchOff()
        {
            throw new NotImplementedException();
        }

        public bool SetVoltageLimit(double Value)
        {
            throw new NotImplementedException();
        }

        public bool SetCurrentLimit(double Value)
        {
            throw new NotImplementedException();
        }

        public bool SetSourceVoltage(double Value)
        {
            throw new NotImplementedException();
        }

        public bool SetSourceCurrent(double Value)
        {
            throw new NotImplementedException();
        }

        public double MeasureVoltage(int NumberOfAverages, double TimeDelay)
        {
            
            throw new NotImplementedException();
        }

        public double MeasureCurrent(int NumberOfAverages, double TimeDelay)
        {
            throw new NotImplementedException();
        }

        public double MeasureResistance(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            throw new NotImplementedException();
        }

        public double MeasurePower(double valueThroughTheStrusture, int NumberOfAverages, double TimeDelay, SourceMode sourceMode)
        {
            throw new NotImplementedException();
        }
    }
}
