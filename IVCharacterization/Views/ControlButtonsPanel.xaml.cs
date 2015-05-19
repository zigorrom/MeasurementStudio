using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IVCharacterization
{
	/// <summary>
	/// Interaction logic for ControlButtonsPanel.xaml
	/// </summary>
	public partial class ControlButtonsPanel : UserControl
	{
		public ControlButtonsPanel()
		{
			this.InitializeComponent();
            StartButton.Click += OnStartButtonPressed;
            PauseButton.Click += OnPauseButtonPressed;
            StopButton.Click += OnStopButtonPressed;
		}
        public event RoutedEventHandler StopButtonPressed;
        void OnStopButtonPressed(object sender, RoutedEventArgs e)
        {
            var handler = StopButtonPressed;
            if (handler != null)
                handler(sender, e);
        }
        public event RoutedEventHandler PauseButtonPressed;
        private void OnPauseButtonPressed(object sender, RoutedEventArgs e)
        {
            var handler = PauseButtonPressed;
            if (handler != null)
                handler(sender, e);
        }

        public event RoutedEventHandler StartButtonPressed;
        public void OnStartButtonPressed(object sender, RoutedEventArgs e)
        {
            var handler = StartButtonPressed;
            if (handler != null)
                handler(sender, e);
        }
        
	}
}