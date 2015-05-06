using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgilentU2442A
{
    public class DigitalBit
    {
        private int m_BitNumber;
        private DigitalChannel m_digitalChannel;

        public DigitalBit(DigitalChannel digitalChannel, int bitNumber)
        {
            m_digitalChannel = digitalChannel;
            m_BitNumber = bitNumber;
        }

        private bool m_value;

        public bool Value
        {
            get { return m_value; }
            private set {
                if (m_value == value)
                    return;
                m_digitalChannel.DigitalWriteBit(value, m_BitNumber);
            }
        }


        public void Set()
        {
           
        }

        public void Reset()
        {

        }
    
    }

}
