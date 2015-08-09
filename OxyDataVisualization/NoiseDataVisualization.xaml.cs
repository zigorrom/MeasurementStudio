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

namespace OxyDataVisualization
{
    /// <summary>
    /// Interaction logic for NoiseDataVisualization.xaml
    /// </summary>
    public partial class NoiseDataVisualization : UserControl
    {
        public NoiseDataVisualization()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cb = (ComboBox)sender;
            var s = (GraphScaleType)(cb.SelectedIndex + 1);
            oxyDataVisualization.ViewModel.Scale = s;
        }
    }
}
