using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Devices;

using NationalInstruments;
using NationalInstruments.NI4882;

using Ivi.Visa.Interop;
using System.Threading;

namespace Agilent_U2542A
{
    public class AgilentUSB_Device : IExperimentalDevice, IDisposable
    {
        #region AgilentUSB_Device settings

        private ResourceManager _rMgr;
        private FormattedIO488 _src;

        private string _Id;
        public string Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                this.Dispose();
                this.InitDevice();
            }
        }

        private bool _IsAlive;
        public bool IsAlive
        {
            get { return _IsAlive; }
        }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
        }

        private int _TimeDelay = 25;
        public int TimeDelay
        {
            get { return _TimeDelay; }
            set { _TimeDelay = value; }
        }

        #endregion

        #region Singleton pattern implementation

        private static AgilentUSB_Device _Instance;
        public static AgilentUSB_Device Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new AgilentUSB_Device();

                return _Instance;
            }
        }

        #endregion

        #region Constructor / destructor

        public AgilentUSB_Device()
        {
            _Id = "USB0::0x0957::0x1718::TW52524501::INSTR";
            _rMgr = new ResourceManager();
            _src = new FormattedIO488();
            _IsAlive = false;
            _IsBusy = false;
        }

        ~AgilentUSB_Device()
        {
            this.Dispose();
        }

        #endregion

        #region Internal implementation variables and functions

        private void _SetBusy()
        {
            _IsBusy = true;
        }

        private void _SetNotBusy()
        {
            _IsBusy = false;
        }

        #endregion

        #region IExperimentalDevice implementation

        public virtual bool InitDevice()
        {
            this._SetBusy();

            try
            {
                _src.IO = (IMessage)_rMgr.Open(this._Id);
                _IsAlive = true;

                _SetNotBusy();

                return true;
            }
            catch
            {
                this._SetNotBusy();
                return false;
            }
        }

        public virtual bool SendCommandRequest(string RequestString)
        {
            if (IsBusy)
                throw new Exception("Device is busy");

            this._SetBusy();

            try
            {
                CheckValue.assertTrue(_IsAlive, "No Device Opened");
            }
            catch
            {
                _SetNotBusy();
                return false;
            }

            try
            {
                _src.IO.LockRsrc();

                var writeBytes = Encoding.ASCII.GetBytes(RequestString + '\n');
                _src.IO.Write(writeBytes, writeBytes.Length);

                _src.IO.UnlockRsrc();
            }
            catch
            {
                this._SetNotBusy();
                return false;
            }

            this._SetNotBusy();

            return true;
        }

        public virtual string ReceiveDeviceAnswer()
        {
            if (IsBusy)
                throw new Exception("Device is busy");

            _SetBusy();

            try { CheckValue.assertTrue(_IsAlive, "No Device Opened"); }
            catch
            {
                this._SetNotBusy();
                return null;
            }

            try
            {
                _src.IO.LockRsrc();

                string result = _src.ReadString();

                _src.IO.UnlockRsrc();

                this._SetNotBusy();

                return result;
            }
            catch
            {
                this._SetNotBusy();
                return null;
            }
        }

        public string RequestQuery(string Query)
        {
            this.SendCommandRequest(Query);

            return this.ReceiveDeviceAnswer().TrimEnd('\n');
        }

        #endregion

        #region IDisposable implementation

        public void Dispose()
        {
            if (_src != null)
                _src.IO.Close();
        }

        #endregion
    }
}
