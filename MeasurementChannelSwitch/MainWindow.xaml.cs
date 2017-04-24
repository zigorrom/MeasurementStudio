using MeasurementChannelSwitch.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MeasurementChannelSwitch
{
    using ChannelSwitchLibrary;

    internal enum Switch
    {
        On,
        Off
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private Layout _layout;
        private int _currentChannel;
        
        
        private Button _currentButton;
        private const int MAX_CHANNELS = 32;
        private object syncRoot = new object();
        private SolidColorBrush _defaultBrush;
        private SolidColorBrush _OnBrush;
        private ChannelSwitch _switch;


        public MainWindow()
        {
            InitializeComponent();
            _defaultBrush = (SolidColorBrush) Resources["DefaultBrush"];
            _OnBrush = Brushes.Green;
            _switch = new ChannelSwitch();
            _switch.Connecting += _switch_Connecting;
            _switch.ConnectionEstablished += _switch_ConnectionEstablished;
            _switch.ConnectionLost += _switch_ConnectionLost;
            _switch.ChannelStateRequest += _switch_ChannelStateRequest;
            _switch.ChannelStateConfirmation += _switch_ChannelStateConfirmation;
            _switch.Error += _switch_Error;
            _switch.Exiting += _switch_Exiting;
        }

        
        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                throw new Exception("Refferenced object is not button");
            var N = ParseChannelNumber(button);
            await SwitchChannel(N, button);
        }

        private int ParseChannelNumber(Button sender)
        {
            var c = sender.Content.ToString() ;
            var n =int.Parse(c);
            if(n>MAX_CHANNELS||n<1)
                throw new Exception("Wrong channel number");
            return n;
        }

        private void SwitchOnBackground(Button sender)
        {
            sender.Background = _OnBrush;
        }

        private void SwitchOffBackground(Button sender)
        {
            sender.Background = _defaultBrush;
        }

        private async Task SwitchChannel(int ChannelNumber, Button Sender)
        {
            lock(syncRoot)
            {
                if(!_switch.Initialized)
                {
                    SetMessage("Initialize first!");
                    return;
                }
                if(Object.ReferenceEquals(Sender, _currentButton))
                {
                    SwitchOffBackground(Sender);
                    SendRequest((short)_currentChannel,false);
                    _currentChannel = 0;
                    _currentButton = null;
                }
                else
                {
                    if(_currentButton!=null)
                    {
                        SwitchOffBackground(_currentButton);
                        SendRequest((short)_currentChannel, false);
                    }

                    _currentButton = Sender;
                    _currentChannel = ChannelNumber;
                    SwitchOnBackground(_currentButton);
                    SendRequest((short)ChannelNumber, true);
                    
                }
            }
        }

        private bool SendRequest(short channelNumber, bool state)
        {
            return _switch.Switch(channelNumber, state);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (!_switch.Initialized)
                _switch.Initialize();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            if (_switch.Initialized)
                _switch.Exit();
        }


        void _switch_Exiting(object sender, EventArgs e)
        {
            SetMessage("Exit");
        }

        void _switch_Error(object sender, string e)
        {
            SetMessage(e);
        }

        void _switch_ChannelStateConfirmation(object sender, ChannelStatus e)
        {
            SetMessage(String.Format("State {0} for channel {1} is set", e.State, e.ChannelNumber));
        }

        void _switch_ChannelStateRequest(object sender, ChannelStatus e)
        {
            SetMessage(String.Format("Setting state {0} for channel {1}", e.State, e.ChannelNumber));
        }

        void _switch_ConnectionLost(object sender, EventArgs e)
        {
            SetMessage("Connection lost...");
        }

        void _switch_ConnectionEstablished(object sender, EventArgs e)
        {
            SetMessage(viewModel.Message = "Connection established...");
        }

        void _switch_Connecting(object sender, EventArgs e)
        {
            SetMessage(viewModel.Message = "Connecting...");
        }

        void SetMessage(string Message)
        {
            Dispatcher.BeginInvoke(new Action(() => viewModel.Message = Message));
        }

        private void SendMotorCommand(short channel, short speed)
        {
            _switch.MoveMotor(channel, speed);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            lock (syncRoot)
            {
                if (!_switch.Initialized)
                {
                    SetMessage("Initialize first!");
                    return;
                }
                SendMotorCommand(1, (short)e.NewValue);
            }
           
        }

        private void Slider_ValueChanged_1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lock (syncRoot)
            {
                if (!_switch.Initialized)
                {
                    SetMessage("Initialize first!");
                    return;
                }
                SendMotorCommand(2, (short)e.NewValue);
            }
        }
        
    }
}
