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

namespace ChannelSwitchExecutable
{
    /// <summary>
    /// Interaction logic for ChannelSwitchView.xaml
    /// </summary>
    public partial class ChannelSwitchView : UserControl
    {
        public ChannelSwitchView()
        {
            InitializeComponent();
            _defaultBrush = (SolidColorBrush)Resources["DefaultBrush"];
            _OnBrush = Brushes.Green;
            DataContextChanged += ChannelSwitchView_DataContextChanged;
        }
        private Button _currentButton;
        private SolidColorBrush _defaultBrush;
        private SolidColorBrush _OnBrush;
        private const int MAX_CHANNELS = 32;
        private object syncRoot = new object();

        void ChannelSwitchView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var newDataContext = e.NewValue as ChannelSwitchExecutableViewModel;
            if(newDataContext!= null)
            {
                newDataContext.ChannelSwitched += ChannelSwitched;
            }
        }

        private void ChannelSwitched(object sender, ChannelSwitchEventArgs e)
        {
            var button = e.PressedButton as Button;
            if (button == null)
                throw new Exception("Refferenced object is not button");
            var N = e.SelectedChannel;

            lock (syncRoot)
            {
                if (Object.ReferenceEquals(button, _currentButton))
                {
                    SwitchOffBackground(button);
                    _currentButton = null;
                }
                else
                {
                    if (_currentButton != null)
                    {
                        SwitchOffBackground(_currentButton);
                    }
                    _currentButton = button;
                    SwitchOnBackground(_currentButton);
                }
            }
            System.Threading.Thread.Sleep(1000);
        }

        private void SwitchOnBackground(Button sender)
        {
            sender.Background = _OnBrush;
        }
        private void SwitchOffBackground(Button sender)
        {
            sender.Background = _defaultBrush;
        }
      
  

    }
}
