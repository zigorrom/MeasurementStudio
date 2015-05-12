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

namespace Helper.Ranges
{
    /// <summary>
    /// Interaction logic for SimpleDoubleRangeControl.xaml
    /// </summary>
    public partial class SimpleVoltageRangeControl : UserControl
    {
        public SimpleVoltageRangeControl()
        {
            InitializeComponent();
        }

        private void SetViewModel(DoubleRangeBase range)
        {
            this.DataContext = range;
        }
    }
}
