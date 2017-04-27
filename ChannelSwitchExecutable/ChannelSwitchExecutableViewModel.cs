using ExperimentAbstraction;
using Helper.ViewModelInterface;
using InstrumentHandlerNamespace;
using Instruments;
using Microsoft.TeamFoundation.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChannelSwitchExecutable
{
    internal enum ChannelExchangeStatusEnum
    {
        Pending,
        Done
    }
    
    public class ChannelSwitchEventArgs:EventArgs
    {
        public ChannelSwitchEventArgs(Button button, int channel)
        {
            PressedButton = button;
            SelectedChannel = channel;
        }
        public Button PressedButton { get; private set; }
        public int SelectedChannel { get; private set; }
    }

    public class ChannelSwitchExecutableViewModel : INotifyPropertyChanged, IExecutableViewModel, IExperimentViewModel, IUIThreadExecutableViewModel
    {
        #region PropertyEvents

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetField<ST>(ref ST field, ST value, string propertyName)
        {
            if (EqualityComparer<ST>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        private void OnPropertyChanged(string PropertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion

        
        public ChannelSwitchExecutableViewModel()
        {
            _instrumentHandler = InstrumentHandler.Instance;
            SelectedChannel = 1;
            PreviousChannel = 1;
            ChannelSwitchExperiment = new ChannelSwitchExecutable(this);
            _executionManager = new SingleTaskExecutionManager();
            _executionManager.CurrentExecutable = ChannelSwitchExperiment;
            ChannelExchangeStatus = ChannelExchangeStatusEnum.Done;

            //_defaultBrush = (SolidColorBrush)Resources["DefaultBrush"];
            //_OnBrush = Brushes.Green;
        }

        SingleTaskExecutionManager _executionManager;

        public event EventHandler<ChannelSwitchEventArgs> ChannelSwitched;
        public void OnChannelSwitched(object sender, ChannelSwitchEventArgs channel)
        {
            var handler = ChannelSwitched;
            if(handler != null)
            {
                handler(sender, channel);
            }
        }

        internal ChannelExchangeStatusEnum ChannelExchangeStatus
        {
            get;
            set;
        }

        private ChannelSwitchExecutable ChannelSwitchExperiment;
        //private SolidColorBrush _defaultBrush;
        //private SolidColorBrush _OnBrush;

        
        public IExecutable Executable
        {
            get { return ChannelSwitchExperiment; }
            
        }


        private bool CheckChannelWithhinRange(int channel)
        {
            if (channel > 0 && channel < 33)
                return true;
            return false;
        }

        private int _selectedChannel;

        public int SelectedChannel
        {
            get { return _selectedChannel; }
            set { SetField(ref _selectedChannel, value, "SelectedChannel"); }
        }

        private int _previousChannel;

        public int PreviousChannel
        {
            get { return _previousChannel; }
            set { SetField(ref _previousChannel, value, "PreviousChannel"); }
        }

        //private Button PreviousButton
        //{
        //    get;
        //    set;
        //}
        public void SwitchToChannel(int channel)
        {
            PreviousChannel = SelectedChannel;
            SelectedChannel = channel;
            ChannelExchangeStatus = ChannelExchangeStatusEnum.Pending;
            ExecuteInUIThread(()=>OnChannelSwitched(this, new ChannelSwitchEventArgs(null, SelectedChannel)));
        }

        private void SwitchToChannel(Button pressedButton)
        {
            var button = (Button)pressedButton;
            var channelNumber = int.Parse(button.Content.ToString());

            PreviousChannel = SelectedChannel;
            SelectedChannel = channelNumber;
            ChannelExchangeStatus = ChannelExchangeStatusEnum.Pending;
            ExecuteInUIThread(()=>OnChannelSwitched(this, new ChannelSwitchEventArgs(button, channelNumber)));

        }

        // when this command is called - means button was pressed and need to execute the channel switch experiment.

        private ICommand _buttonPressed;
        public ICommand ButtonPressed
        {
            get
            {
                return _buttonPressed ?? (_buttonPressed = new RelayCommand((b) =>
                {
                    var button = (Button)b;
                    SwitchToChannel(button);
                    _executionManager.Start();
                }));
            }
        }




        private InstrumentHandler _instrumentHandler;

        public ObservableCollection<IInstrumentResourceItem> Resources
        {
            get
            {
                if (_instrumentHandler != null)
                {
                    return _instrumentHandler.Resources;
                }
                return null;
            }
        }

        private IInstrumentResourceItem _instrumentResource;

        public IInstrumentResourceItem InstrumentResource
        {
            get { return _instrumentResource; }
            set { SetField(ref _instrumentResource, value, "InstrumentResource"); }
        }

        public virtual void ErrorHandler(Exception e)
        {
            System.Diagnostics.Debug.WriteLine("Error occured:");
            System.Diagnostics.Debug.Write(e.ToString());
            System.Diagnostics.Debug.WriteLine("**************");

            MessageBox.Show(e.Message, "Error occured", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public virtual void MessageHandler(string e)
        {
            System.Diagnostics.Debug.WriteLine("Message arrived:");
            System.Diagnostics.Debug.Write(e);
            System.Diagnostics.Debug.WriteLine("**************");

            MessageBox.Show(e, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        public string WorkingDirectory
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ExperimentName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string MeasurementName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MeasurementCount
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public INewExperiment IExperiment
        {
            get { return ChannelSwitchExperiment; }
        }

        public bool GlobalIsEnabled
        {
            get;
            set;
        }

        public void ExecuteInUIThread(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        public async Task ExecuteInUIThreadAsync(Action action)
        {
            await Application.Current.Dispatcher.BeginInvoke(action, null);
        }


        public ICommand CreateNewExperiment
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand SelectWorkingDirectory
        {
            get { throw new NotImplementedException(); }
        }

        public ICommand OpenWorkingDirectory
        {
            get { throw new NotImplementedException(); }
        }
    }
}
