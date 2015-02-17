using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO.Ports;
using System.Threading;

namespace Devices
{
    public class COM_Device : IExperimentalDevice, IDisposable
    {
        #region COM Port settings

        private SerialPort _COM_Port;
        public SerialPort COM_Port
        {
            get { return _COM_Port; }
        }

        private int _TimeDelay = 25;
        public int TimeDelay
        {
            get { return _TimeDelay; }
            set { _TimeDelay = value; }
        }

        private string _comPort;
        private int _baud;
        private Parity _parity;
        private int _dataBits;
        private StopBits _stopBits;
        private string _returnToken;

        #endregion

        #region COM Port initialization

        private void SetSerialPort(string comPort, int baud, Parity parity, int dataBits, StopBits stopBits, string returnToken)
        {
            this._comPort = comPort;
            this._baud = baud;
            this._parity = parity;
            this._dataBits = dataBits;
            this._stopBits = stopBits;
            this._returnToken = returnToken;

            this._COM_Port = new SerialPort(comPort, baud, parity, dataBits, stopBits);
            
            //COM Device general settings

            this._COM_Port.NewLine = returnToken;
            this._COM_Port.RtsEnable = true;
            this._COM_Port.DtrEnable = true;

            //Setting max possible timeouts for IO operations

            this._COM_Port.ReadTimeout = SerialPort.InfiniteTimeout;
            this._COM_Port.WriteTimeout = SerialPort.InfiniteTimeout;
        }

        public COM_Device(string comPort = "COM1", int baud = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One, string returnToken = ">")
        {
            this.SetSerialPort(comPort, baud, parity, dataBits, stopBits, returnToken);
            this.InitDevice();
        }

        ~COM_Device()
        {
            this.Dispose();
        }

        #endregion

        #region IExperimentalDevice implementation

        public virtual bool InitDevice()
        {
            try
            {
                if (!_COM_Port.IsOpen == true)
                    _COM_Port.Open();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool SendCommandRequest(string RequestString)
        {
            try
            {
                var strBytes = Encoding.ASCII.GetBytes(RequestString + '\n');
                _COM_Port.Write(strBytes, 0, strBytes.Length);
                //Thread.Sleep(_TimeDelay);
                return true;
            }
            catch
            { return false; }
        }

        public virtual string ReceiveDeviceAnswer()
        {
            var COM_DeviceResponce = string.Empty;

            try { COM_DeviceResponce = _COM_Port.ReadLine(); }
            catch
            {
                COM_DeviceResponce = string.Empty;
            }

            return COM_DeviceResponce;
        }

        public virtual string RequestQuery(string Query)
        {
            SendCommandRequest(Query);
            return ReceiveDeviceAnswer();
        }

        #endregion

        #region Correctly disposing the instance

        public virtual void Dispose()
        {
            if (_COM_Port != null)
            {
                if (_COM_Port.IsOpen == true)
                {
                    _COM_Port.Close();
                    _COM_Port.Dispose();
                }

                _COM_Port.Dispose();
                _COM_Port = null;
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
