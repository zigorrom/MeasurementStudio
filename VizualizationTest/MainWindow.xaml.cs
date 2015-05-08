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
using System.Timers;

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
            parentData = new LinkedList<Point>();
            
            data = new ObservableDataSource<Point>(parentData);
            data.SetXYMapping(p => p);
            data.Collection.Add(new Point(1, 1));
            data.Collection.Add(new Point(2, 2));
            Plotter.AddLineGraph(data, Colors.Red);
            model.HorizontalAxisLabel = "asfdasd";
            model.VertivalAxisLabel = "sdfgsdgsg";
            timer = new Timer(25);
            timer.Elapsed += timer_Elapsed;
            
        }
        LinkedList<Point> parentData;
        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var rand = new Random();
                count += 100;
                var y = rand.NextDouble() * 100;
                data.Collection.Add(new Point(count, y));
                if (data.Collection.Count > 5000)
                    data.Collection.RemoveAt(0);
            }));
                
            
        }

        
        private ObservableDataSource<Point> data;
        private DataVisualization.VisualizationViewModel model;
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.LineThickness = e.NewValue;
        }
        double count = 0;
        Timer timer;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.Enabled)
                timer.Stop();
            else
                timer.Start();
        }
    }
}
