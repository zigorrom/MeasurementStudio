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
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes.Numeric;

namespace DataVisualization.D3DataVisualization
{
    /// <summary>
    /// Interaction logic for D3DataVisualizationControl.xaml
    /// </summary>
    public partial class D3DataVisualizationControl : UserControl, ID3View
    {
        public D3DataVisualizationControl()
        {
            InitializeComponent();

            //var dc = DataContext as D3VisualizationViewModel;
            //if (dc == null)
            //    throw new NullReferenceException();
            //dc.View = this;
            //(DataContext as D3VisualizationViewModel).View = this as ID3View;
            DataContextChanged += D3DataVisualizationControl_DataContextChanged;
        }

        void D3DataVisualizationControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
                return;
            var dc = DataContext as D3VisualizationViewModel;
            if (dc == null)
                return;
            dc.View = this;

            //SetScale(dc.ScaleType);
        }
         


        private IEnumerable<Point> enumerator(int mult)
        {
            for (int i = 0; i < 1000; i+=10)
            {
                yield return new Point(i, mult* Math.Sin(i));
            }
        }

        public void SetScale(GraphScaleType scaleType)
        {
            
            HorizontalAxis xAxis;
            VerticalAxis yAxis;
            switch (scaleType)
            {

                case GraphScaleType.LinLog:
                    {
                        xAxis = new HorizontalAxis();
                        yAxis = new VerticalAxis
                        {
                            TicksProvider = new LogarithmNumericTicksProvider(10),
                            LabelProvider = new UnroundingLabelProvider()
                        };
                    }
                    break;
                case GraphScaleType.LogLog:
                    {
                        xAxis = new HorizontalAxis
                        {
                            TicksProvider = new LogarithmNumericTicksProvider(10),
                            LabelProvider = new UnroundingLabelProvider()
                        };
                        yAxis = new VerticalAxis
                        {
                            TicksProvider = new LogarithmNumericTicksProvider(10),
                            LabelProvider = new UnroundingLabelProvider()
                        };
                    }
                    break;
                case GraphScaleType.LinLin:
                default:
                    {
                        xAxis = new HorizontalAxis();
                        yAxis = new VerticalAxis();
                    }
                    break;
            }
            plotter.MainHorizontalAxis = xAxis;
            plotter.MainVerticalAxis = yAxis;
        }
        
        public void AddSeries(IEnumerable<Point> data)
        {
            var d = new ObservableDataSource<Point>(data);
            d.SetXYMapping(p => p);

            

            plotter.AddLineGraph(d);
            
        }

        
    }
}
