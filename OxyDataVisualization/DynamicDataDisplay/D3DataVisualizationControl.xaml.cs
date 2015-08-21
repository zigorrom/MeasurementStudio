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

namespace DataVisualization.DynamicDataDisplay
{
    /// <summary>
    /// Interaction logic for D3DataVisualizationControl.xaml
    /// </summary>
    public partial class D3DataVisualizationControl : UserControl
    {
        public D3DataVisualizationControl()
        {
            InitializeComponent();
            model = new D3MainViewModel(plotter);
            model.ChartTitle = "asdasdasdasfsagasdgasgasg";
            model.HorizontalAxisTitle = "horizontal";
            model.VerticalAxisTitle = "vertical";
            DataContext = model;
        }

        public D3MainViewModel model { get; private set; }
    }
}
