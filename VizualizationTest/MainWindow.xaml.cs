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
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;

namespace VizualizationTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            model = new DataVisualization.VisualizationViewModel();
            Plotter.SetDataContext(model);
            var data = new ObservableDataSource<Point>();
            data.SetXYMapping(p => p);
            data.Collection.Add(new Point(1, 1));
            data.Collection.Add(new Point(2, 2));
            Plotter.AddLineGraph(data, Colors.Red);

        }
        private DataVisualization.VisualizationViewModel model;
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.LineThickness = e.NewValue;
        }
    }
}
