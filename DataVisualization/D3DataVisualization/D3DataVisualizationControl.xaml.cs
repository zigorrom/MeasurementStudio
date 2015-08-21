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
using DataVisualization.D3DataVisualization;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace DataVisualization.D3DataVisualization
{
    /// <summary>
    /// Interaction logic for D3DataVisualizationControl.xaml
    /// </summary>
    public partial class D3DataVisualizationControl : UserControl
    {
        public D3DataVisualizationControl()
        {
            InitializeComponent();
            plotter.AddLineGraph(new ObservableDataSource<Point>(enumerator(100)));
            plotter.AddLineGraph(new ObservableDataSource<Point>(enumerator(300)));
        }

        private IEnumerable<Point> enumerator(int mult)
        {
            for (int i = 0; i < 1000; i+=10)
            {
                yield return new Point(i, mult* Math.Sin(i));
            }
        }
    }
}
