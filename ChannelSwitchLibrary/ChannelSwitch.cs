using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelSwitchLibrary
{
    using CommandMessenger;
    using CommandMessenger.Queue;
    using CommandMessenger.Transport;
    using CommandMessenger.Transport.Serial;
    using System.Diagnostics;

    
    enum Command
    {
        Watchdog,
        Acknowledge,
        SwitchChannel,
        Error,
        MotorCommand,
        
    }

   

    public class ChannelStatus
    {
        public int ChannelNumber{get;private set;}
        public bool State{get;private set;}
        public ChannelStatus(int channelNumber, bool channelState)
        {
            ChannelNumber = channelNumber;
            State = channelState;
        }
    }

    public class ChannelSwitch
    {
        public event EventHandler ConnectionEstablished;
        private void OnConnectionEstablished()
        {
            var handler = ConnectionEstablished;
            if (handler != null)
                handler(this, new EventArgs());
        }
        
        public event EventHandler ConnectionLost;
        private void OnConnectionLost()
        {
            var handler = ConnectionLost;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public event EventHandler<ChannelStatus> ChannelStateRequest;
        private void OnChannelStateRequest(ChannelStatus status)
        {
            var handler = ChannelStateRequest;
            if (handler != null)
                handler(this, status);
        }

        public event EventHandler<ChannelStatus> ChannelStateConfirmation;
        private void OnChannelStateConfirmation(ChannelStatus status)
        {
            var handler = ChannelStateConfirmation;
            if (handler != null)
                handler(this, status);
        }
        
        public event EventHandler Connecting;
        private void OnConnecting()
        {
            var handler = Connecting;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public event EventHandler Exiting;
        private void OnExiting()
        {
            var handler = Exiting;
            if (handler != null)
                handler(this, new EventArgs());
        }

        public event EventHandler<string> Error;
        private void OnErrorEvent(string Message)
        {
            var handler = Error;
            if (handler != null)
                handler(this, Message);
        }

        const string UniqueDeviceID = "517cea54-8f17-4761-b735-094897c20ffd";
        const int WatchdogTimeout = 5000;
        const int WatchdogRetryTimeout = 100;

        //public bool RunLoop { get; set; }
        private ITransport _transport;
        private CmdMessenger _cmdMessenger;
        private ConnectionManager _connectionManager;
        public bool Initialized { get; private set; }

        public ChannelSwitch()
        {
            Initialized = false;
        }

        ~ChannelSwitch()
        {
            //Exit();
        }

        public void Initialize(string PortName = "")
        {
            Debug.WriteLine("***********************************START*************************");

            if (String.IsNullOrEmpty(PortName))
                _transport = new SerialTransport { CurrentSerialSettings = { DtrEnable = false } };
            else
                _transport = new SerialTransport { CurrentSerialSettings = { PortName = PortName } };

            _cmdMessenger = new CmdMessenger(_transport, BoardType.Bit16) { PrintLfCr = false };
            _cmdMessenger.NewLineReceived += _cmdMessenger_NewLineReceived;
            _cmdMessenger.NewLineSent += _cmdMessenger_NewLineSent;

            AttachCallbacks();
            _connectionManager = new SerialConnectionManager((_transport as SerialTransport), _cmdMessenger, (int)Command.Watchdog, UniqueDeviceID);

            _connectionManager.WatchdogEnabled = true;
            _connectionManager.WatchdogTimeout = WatchdogTimeout;
            _connectionManager.WatchdogRetryTimeout = WatchdogRetryTimeout;

            _connectionManager.ConnectionFound += _connectionManager_ConnectionFound;
            _connectionManager.ConnectionTimeout += _connectionManager_ConnectionTimeout;
            OnConnecting();
            _connectionManager.StartConnectionManager();
        }

        void _connectionManager_ConnectionTimeout(object sender, EventArgs e)
        {
            Initialized = false;
            OnConnectionLost();
        }

        void _connectionManager_ConnectionFound(object sender, EventArgs e)
        {
            Initialized = true;
            Debug.WriteLine("Connection found");
            OnConnectionEstablished();
        }

        void _cmdMessenger_NewLineSent(object sender, CommandEventArgs e)
        {
            Debug.WriteLine(String.Format("Sent > {0}",e.Command.CommandString()));
        }

        void _cmdMessenger_NewLineReceived(object sender, CommandEventArgs e)
        {
            Debug.WriteLine(String.Format("Received > {0}", e.Command.CommandString()));
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
            Debug.WriteLine(String.Format("Watchdog > {0}", receivedCommand.CommandString()));
        }

        private void OnError(ReceivedCommand receivedCommand)
        {
            Debug.WriteLine(String.Format("Error > {0}", receivedCommand.CommandString()));
            OnErrorEvent(String.Format("Error > {0}", receivedCommand.CommandString()));
        }

        private void OnAcknowledge(ReceivedCommand receivedCommand)
        {
            Debug.WriteLine(receivedCommand.CommandString());
            //var a = receivedCommand.ReadInt16Arg();
            //var b = receivedCommand.ReadBoolArg();
            //Debug.WriteLine(String.Format("channel number: {0}, status {1}", a,b));
        }

        private void OnUnknownCommand(ReceivedCommand receivedCommand)
        {
            Debug.WriteLine("Unknown command");
            OnErrorEvent("Error > Unknown command");
        }




        public void Exit()
        {
            OnExiting();
            if (_connectionManager != null)
            {
                _connectionManager.ConnectionFound -= _connectionManager_ConnectionFound;
                _connectionManager.ConnectionTimeout -= _connectionManager_ConnectionTimeout;
                _connectionManager.Dispose();
            }
            if (_cmdMessenger != null)
            {
                _cmdMessenger.Disconnect();
                _cmdMessenger.Dispose();
            }
            if (_transport != null)
            {
                _transport.Disconnect();
                _transport.Dispose();
                
            }

        }

        public bool Switch(short ChannelName, bool state)
        {
            if (!Initialized)
                throw new Exception("Not yet initialized");
            OnChannelStateRequest(new ChannelStatus(ChannelName, state));
            var command = new SendCommand((int)Command.SwitchChannel,(int)Command.Acknowledge,1000);
            command.AddArgument(ChannelName);
            command.AddArgument(state);
            var response = _cmdMessenger.SendCommand(command);
            if (response.Ok)
            {
                var cn = response.ReadInt16Arg();
                var cs = response.ReadBoolArg();
                Debug.WriteLine(String.Format("Command response - channelNumber={0}; channelState={1}", cn,cs));
                if (ChannelName == cn && state == cs)
                {
                    OnChannelStateConfirmation(new ChannelStatus(cn, cs));
                    return true;
                }
                
            }
            return false;
            
        }


        public bool MoveMotor(short Channel, short Speed)
        {
            if (!Initialized)
                throw new Exception("Not yet initialized");
           
            var command = new SendCommand((int)Command.MotorCommand, (int)Command.Acknowledge, 1000);
            command.AddArgument(Channel);
            command.AddArgument(Speed);
            var response = _cmdMessenger.SendCommand(command);
            if (response.Ok)
            {
                var cn = response.ReadInt16Arg();
                var cs = response.ReadInt16Arg();
                Debug.WriteLine(String.Format("Command response - channel={0}; speed={1}", cn, cs));
                if (Channel == cn && Speed == cs)
                {
                    Debug.WriteLine("Alles gut");
                    //OnChannelStateConfirmation(new ChannelStatus(cn, cs));
                    return true;
                }

            }
            return false;
        }

    }
}
