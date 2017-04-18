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

namespace Helper.StartStopControl
{
    /// <summary>
    /// Interaction logic for ControlButtonsPanel.xaml
    /// </summary>
    public partial class ControlButtonsPanel : UserControl
    {
        public ControlButtonsPanel()
        {
            InitializeComponent();
        }
        private const string pauseText = "Pause";
        private const string resumeText = "Resume";

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if(PauseButton.Content.ToString() == pauseText)
            {
                PauseButton.Content = resumeText;
            }
            else
            {
                PauseButton.Content = pauseText;
            }
        }

       
    }
}
