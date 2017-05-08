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

namespace ChannelSwitchTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            sw = new ChannelSwitchLibrary.ArduinoChannelSwitch("ArduinoChannelSwitch", "ACS", "COM26");

            for (short i = 1; i < 33; i++)
            {
                sw.SwitchChannel(i, true);
            }

        }
        ChannelSwitchLibrary.ArduinoChannelSwitch sw;
    }
}
