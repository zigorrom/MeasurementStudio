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
            RunLoop = true;
        }

        public bool RunLoop { get; private set; }
        private ITransport _transport;
        private CmdMessenger _cmdMessenger;

        public void Setup(string portName = "COM8")
        {
            System.Diagnostics.Debug.WriteLine("***********************************START*************************");
            if (String.IsNullOrEmpty(portName))
                _transport = new SerialTransport { CurrentSerialSettings = { DtrEnable = false} };
            else
                _transport = new SerialTransport { CurrentSerialSettings = { PortName = portName, BaudRate = 115200, DtrEnable = false } };

            _cmdMessenger = new CmdMessenger(_transport, BoardType.Bit16);
            AttachCallbacks();
            _cmdMessenger.NewLineReceived += _cmdMessenger_NewLineReceived;
            _cmdMessenger.NewLineSent += _cmdMessenger_NewLineSent;
            _cmdMessenger.Connect();
            while (!_transport.IsConnected()) ;

        }

        public void Exit()
        {
            _cmdMessenger.Disconnect();
            _cmdMessenger.Dispose();
            _transport.Dispose();

        }


        void _cmdMessenger_NewLineSent(object sender, CommandEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Sent > {0}", e.Command.CommandString()));
        }

        void _cmdMessenger_NewLineReceived(object sender, CommandEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Received > {0}", e.Command.CommandString()));
        }

        private void AttachCallbacks()
        {
            _cmdMessenger.Attach(OnUnknownCommand);
            _cmdMessenger.Attach((int)Command.Acknowledge, OnAcknowledge);
            _cmdMessenger.Attach((int)Command.Error, OnError);
            _cmdMessenger.Attach((int)Command.Watchdog, OnWatchdog);
        }

        private void OnWatchdog(ReceivedCommand receivedCommand)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Watchdog > {0}", receivedCommand.CommandString()));
        }

        private void OnError(ReceivedCommand receivedCommand)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Error > {0}", receivedCommand.CommandString()));
            //OnErrorEvent(String.Format("Error > {0}", receivedCommand.CommandString()));
        }

        private void OnAcknowledge(ReceivedCommand receivedCommand)
        {
            System.Diagnostics.Debug.WriteLine(receivedCommand.CommandString());
            //var a = receivedCommand.ReadInt16Arg();
            //var b = receivedCommand.ReadBoolArg();
            //Debug.WriteLine(String.Format("channel number: {0}, status {1}", a,b));
        }

        private void OnUnknownCommand(ReceivedCommand receivedCommand)
        {
            System.Diagnostics.Debug.WriteLine("Unknown command");
            //OnErrorEvent("Error > Unknown command");
        }

        public bool SwitchToChannel(short ChannelName, bool state)
        {
            var command = new SendCommand((int)Command.SwitchChannel, (int)Command.Acknowledge, 1000);
            command.AddArgument(ChannelName);
            command.AddArgument(state);
            
            System.Diagnostics.Debug.WriteLine("Initialized? {0}", _transport.IsConnected());
            var response = _cmdMessenger.SendCommand(command);
            //while (!response.Ok) ;
            if (response.Ok)
            {
                var cn = response.ReadInt16Arg();
                var cs = response.ReadBoolArg();
                System.Diagnostics.Debug.WriteLine(String.Format("Command response - channelNumber={0}; channelState={1}", cn, cs));
                if (ChannelName == cn && state == cs)
                {
                    //OnChannelStateConfirmation(new ChannelStatus(cn, cs));
                    RunLoop = false;
                    return true;
                }

            }
            //RunLoop = false;
            return false;
        }

    }
}
