using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotter
{
    struct COMInitStructure
    {
        public string PortName;
        public int PortBaudRate;
        public Parity PortParity;
        public int PortDataBits;
        public StopBits PortStopBits;
        public Handshake PortHandshake;
        public int PortReadTimeout;
        public int PortWriteTimeout;
        public COMInitStructure(string portName, int portBaudRate = 9600, Parity portParity = Parity.None,int portDataBits = 8,StopBits portStopBits = StopBits.One, Handshake portHandShake= Handshake.None,int portReadTimeout = 500,int portWriteTimeout = 500)
        {
            PortName = portName;
            PortBaudRate = portBaudRate;
            PortParity = portParity;
            PortDataBits = portDataBits;
            PortStopBits = portStopBits;
            PortHandshake = portHandShake;
            PortReadTimeout = portReadTimeout;
            PortWriteTimeout = portWriteTimeout;
        }
    }
    class COMDevice
    {
        private SerialPort m_serialPort;
        public COMDevice(string PortName)
        {
            m_serialPort = new SerialPort(PortName);
            Initialize(new COMInitStructure(PortName));
        }
        public COMDevice(COMInitStructure init)
        {
            m_serialPort = new SerialPort();
            Initialize(init);
            //m_serialPort.PortName = init
        }

        private void Initialize(COMInitStructure init)
        {
            m_serialPort.PortName = init.PortName;
            m_serialPort.BaudRate = init.PortBaudRate;
            m_serialPort.Parity = init.PortParity;
            m_serialPort.DataBits = init.PortDataBits;
            m_serialPort.StopBits = init.PortStopBits;
            m_serialPort.Handshake = init.PortHandshake;
            m_serialPort.ReadTimeout = init.PortReadTimeout;
            m_serialPort.WriteTimeout = init.PortWriteTimeout;
        }

        public static string[] GetPortNames()
        {
            return SerialPort.GetPortNames();
        }


    }
}
