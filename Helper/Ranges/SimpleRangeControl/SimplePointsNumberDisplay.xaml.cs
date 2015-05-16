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

namespace Helper.Ranges.SimpleRangeControl
{
    /// <summary>
    /// Interaction logic for SimplePointsNumberDisplay.xaml
    /// </summary>
    public partial class SimplePointsNumberDisplay : UserControl
    {
        public SimplePointsNumberDisplay()
        {
            InitializeComponent();
        }

        public string ValueLabel
        {
            get { return (string)Label.Content; }
            set { Label.Content = value; }
        }
    }
}
