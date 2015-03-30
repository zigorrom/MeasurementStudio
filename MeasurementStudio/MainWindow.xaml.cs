using Helper.Ranges;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace MeasurementStudio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            range = new DoublePropertyValueRange();
            Range.DataContext = range;
        }
        DoublePropertyValueRange range;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in range)
            {
                Debug.WriteLine(item);
            }
        }
    }
}
