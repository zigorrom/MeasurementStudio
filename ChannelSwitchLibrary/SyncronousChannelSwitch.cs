using CommandMessenger;
using CommandMessenger.Transport;
using CommandMessenger.Transport.Serial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelSwitchLibrary
{
    public class SyncronousChannelSwitch
    {
        public SyncronousChannelSwitch()
        {

        }

        private ITransport _transport;
        private CmdMessenger _cmdMessenger;

        public void Setup(string portName = "")
        {
            System.Diagnostics.Debug.WriteLine("***********************************START*************************");
            if (String.IsNullOrEmpty(portName))
                _transport = new SerialTransport { CurrentSerialSettings = { DtrEnable = false} };
            else
                _transport = new SerialTransport { CurrentSerialSettings = { PortName = portName, BaudRate = 115200, DtrEnable = false } };

            _cmdMessenger = new CmdMessenger(_transport, BoardType.Bit16);
            //_cmdMessenger.NewLineReceived += _cmdMessenger_NewLineReceived;
            //_cmdMessenger.NewLineSent += _cmdMessenger_NewLineSent;
            _cmdMessenger.Connect();

        }

    }
}
